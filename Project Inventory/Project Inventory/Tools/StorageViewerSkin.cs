using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory.Tools
{
    public static class StorageViewerSkin
    {
        public static void LoadLabelSkin(Label label, SkinsName labelSkin)
        {
            switch(labelSkin)
            {
                case (SkinsName.Standart):
                    StandartLabel(label);
                    break;
            }
        }

        public static void LoadLabelSkinPosition(Label label, SkinsName labelSkin)
        {
            switch (labelSkin)
            {
                case (SkinsName.Center):
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
