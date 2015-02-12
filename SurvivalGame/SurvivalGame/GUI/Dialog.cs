using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurvivalGame.GUI
{
    class Dialog
    {
        string displayedText;
        bool displayOkButton;

        public Dialog(string text, bool okButton = true)
        {
            this.displayedText = text;
            this.displayOkButton = okButton;
        }

        public void Draw()
        {

        }
    }
}
