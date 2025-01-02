using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartGameAction : IButtonAction
{
    private readonly GameManager gameManager;

    public StartGameAction(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Execute()
    {
        gameManager.OpenLevelSelect();
    }
}
