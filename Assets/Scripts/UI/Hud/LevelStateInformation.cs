using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Hud
{
    public class LevelStateInformation : MonoBehaviour
    {
        [SerializeField] public TMP_Text LevelNumberText;
        [SerializeField] public TMP_Text TotalCardsInDeckText;
        [SerializeField] public TMP_Text RemainsCardsInDeckText;
        [SerializeField] public TMP_Text LevelTargetScoreText;
        [SerializeField] public TMP_Text CurrentScoreText;
    }
}
