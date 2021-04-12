using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Project_Inventory
{
    public static class ButtonSkins
    {
        public static Button StandartButtonSkin(Button button)
        {

            button.Width *= 3;
            button.Height *= 3;
            button.Padding = new System.Windows.Thickness(10);

            return button;
        }
    }
}
