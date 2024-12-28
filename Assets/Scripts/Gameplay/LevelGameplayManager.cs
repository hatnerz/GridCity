using Assets.Scripts.Core;
using Assets.Scripts.Score;
using Assets.Scripts.UI.Hud;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class LevelGameplayManager : MonoBehaviour
    {
        [SerializeField] private CardManager cardManager;
        [SerializeField] private GridManager gridManager;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private LevelStateInformation levelStateInformation;

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
            UpdateCurrentScoreHudInformation(currentScore);
        }

        private void UpdateCurrentScoreHudInformation(int score)
        {
            levelStateInformation.CurrentScoreText.text = $"Current score: {score}";
        }
    }

}
