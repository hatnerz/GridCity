using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private ActionButton startGameButton;
    [SerializeField] private ActionButton quitGameButton;

    private void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        if (startGameButton != null)
        {
            startGameButton.SetAction(new StartGameAction(GameManager.Instance));
        }
        else
        {
            Debug.LogError("Start Game Button is not assigned in the inspector!");
        }

        if (quitGameButton != null)
        {
            quitGameButton.SetAction(new QuitGameAction(GameManager.Instance));
        }
        else
        {
            Debug.LogError("Quit Game Button is not assigned in the inspector!");
        }
    }
}