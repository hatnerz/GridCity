using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuitGameAction : IButtonAction
{
    private readonly GameManager gameManager;

    public QuitGameAction(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Execute()
    {
        gameManager.QuitGame();
    }
}