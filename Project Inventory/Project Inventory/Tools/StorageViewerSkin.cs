using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory.Tools
{
    public static class StorageViewerSkin
    {
        public static void LoadLabelSkin(Label label, string labelSkin)
        {
            switch(labelSkin)
            {
                case ("standart"):
                    StandartLabel(label);
                    break;
            }
        }

        public static void LoadLabelSkinPosition(Label label, string labelSkin)
        {
            switch (labelSkin)
            {
                case ("center"):
                    Center(label);
                    break;
            }
        }

        private static void StandartLabel(Label label)
        {
        }

        private static void Center(Label label)
        {
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
        }

        //private static 
    }
}
