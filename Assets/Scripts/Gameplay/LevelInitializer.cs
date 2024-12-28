using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class LevelInitializer : MonoBehaviour
    {
        [SerializeField] private CardManager cardManager;
        [SerializeField] private GridManager gridManager;

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
        }
    }

}
