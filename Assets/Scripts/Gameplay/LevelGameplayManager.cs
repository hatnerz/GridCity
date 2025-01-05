using Assets.Scripts.Core;
using Assets.Scripts.Score;
using Assets.Scripts.UI.Hud;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class LevelGameplayManager : MonoBehaviour
    {
        [SerializeField] private CardManager cardManager;
        [SerializeField] private GridManager gridManager;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private LevelStateInformation levelStateInformation;
        [SerializeField] private BuildingScoreVisualizer buildingScoreVisualizer;
        [SerializeField] private Canvas levelHudCanvas;
        [SerializeField] private GameObject winMenuPrefab;
        [SerializeField] private GameObject gameOverMenuPrefab;

        private void Start()
        {
            var currentLevel = GameplayState.CurrentLevelNumber;
            var levelData = ResourceManager.Instance.LevelDataDictionary[currentLevel];
            if (cardManager == null || gridManager == null)
            {
                throw new MissingReferenceException("CardManager or GridManager is not set in LevelInitializer");
            }

            cardManager.InitializeLevelDeck(levelData);
            gridManager.InitializeLevelGrid(levelData);

            buildingScoreVisualizer.InitializeBuildingPlaces(
                gridManager.BuildingPlacesObjects.Cast<GameObject>()
                    .Select(e => e.GetComponent<BuildingPlace>()).ToList());

            SetInitialLevelHudInformation(levelData);

            cardManager.OnCardPlayed += (_) => UpdateDeckHudInformation();
            gridManager.OnBuildingPlaced += (_) => HandleScore();
        }

        private void SetInitialLevelHudInformation(LevelData levelData)
        {
            levelStateInformation.LevelNumberText.text = $"Level {levelData.LevelNumber}";

            levelStateInformation.LevelTargetScoreText.text = $"Target score: {levelData.TargetScore}";
            levelStateInformation.CurrentScoreText.text = $"Current score: {scoreManager.CalculateTotalScore()}";

            levelStateInformation.TotalCardsInDeckText.text = $"Total cards: {cardManager.InitialDeckSize}";
            levelStateInformation.RemainsCardsInDeckText.text = $"Remains cards: {cardManager.RemainsCardsInDeck}";
        }

        private void UpdateDeckHudInformation()
        {
            levelStateInformation.RemainsCardsInDeckText.text = $"Remains cards: {cardManager.RemainsCardsInDeck}";
        }

        private void HandleScore()
        {
            var currentScore = scoreManager.CalculateTotalScore();
            buildingScoreVisualizer.VisualizeBuildingPlaceNewScore();
            UpdateCurrentScoreHudInformation(currentScore);

            if(cardManager.RemainsCardsInHand == 0)
            {
                EndLevel();
            }
        }

        private void UpdateCurrentScoreHudInformation(int score)
        {
            levelStateInformation.CurrentScoreText.text = $"Current score: {score}";
        }

        private void EndLevel()
        {
            var totalPlayerScore = scoreManager.CalculateTotalScore();
            var currentLevelData = ResourceManager.Instance.LevelDataDictionary[GameplayState.CurrentLevelNumber];
            var requiredScore = currentLevelData.TargetScore;

            Debug.Log(totalPlayerScore);
            Debug.Log(requiredScore);

            if (totalPlayerScore >= requiredScore)
            {
                var winMenu = Instantiate(winMenuPrefab, levelHudCanvas.transform);
                winMenu.GetComponent<WinMenuManager>().SetScoreInformation(totalPlayerScore, requiredScore);
            }
            else
            {
                var loseMenu = Instantiate(gameOverMenuPrefab, levelHudCanvas.transform);
                loseMenu.GetComponent<GameOverMenuManager>().SetScoreInformation(totalPlayerScore, requiredScore);
            }
        }
    }

}
