using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory.Tools
{
    public static class StorageViewerSkin
    {
        public static Label LoadLabelSkin(Label label, string labelSkin)
        {
            switch(labelSkin)
            {
                case ("standart"):
                    label = StandartLabel(label);
                    break;
            }

            return label;
        }

        public static Label LoadLabelSkinPosition(Label label, string labelSkin)
        {
            switch (labelSkin)
            {
                case ("center"):
                    label = Center(label);
                    break;
            }

            return label;
        }

        private static Label StandartLabel(Label label)
        {
            return label;
        }

        private static Label Center(Label label)
        {
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;

            return label;
        }
    }
}
