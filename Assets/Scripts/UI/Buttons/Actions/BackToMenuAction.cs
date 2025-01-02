using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UI.Buttons.Actions
{
    public class BackToMenuAction : IButtonAction
    {
        private readonly GameManager gameManager;

        public BackToMenuAction(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Execute()
        {
            gameManager.ReturnToMainMenu();
        }
    }
}
