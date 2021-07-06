using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory.Tools
{
    public static class StorageViewerSkin
    {
        public static void LoadLabelSkin(UIElement uiElement, SkinsName labelSkin)
        {
            switch(labelSkin)
            {
                case (SkinsName.Standart):
                    StandartLabel(uiElement);
                    break;
            }
        }

        public static void LoadLabelSkinPosition(UIElement uiElement, SkinsName labelSkin)
        {
            switch (labelSkin)
            {
                case (SkinsName.Center):
                    Center(uiElement);
                    break;
            }
        }

        private static void StandartLabel(UIElement uiElement)
        {
        }

        private static void Center(UIElement uiElement)
        {
            if (uiElement as TextBox != null)
            {
                (uiElement as TextBox).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as TextBox).VerticalAlignment = VerticalAlignment.Center;
            }
            else if(uiElement as ListBox != null)
            {
                (uiElement as ListBox).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as ListBox).VerticalAlignment = VerticalAlignment.Center;
            }
            else if(uiElement as DatePicker != null)
            {
                (uiElement as DatePicker).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as DatePicker).VerticalAlignment = VerticalAlignment.Center;
            }
            else if(uiElement as Label != null)
            {
                (uiElement as Label).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as Label).VerticalAlignment = VerticalAlignment.Center;
            }
        }

        //private static 
    }
}
