using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory.Tools
{
    public static class StorageViewerSkin
    {
        /// <summary>
        /// Apply UIElement Position in Grid
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="labelSkin"></param>
        public static void LoadSkinPosition(UIElement uiElement, SkinsName labelSkin)
        {
            switch (labelSkin)
            {
                case SkinsName.Center:
                    Center(uiElement);
                    break;

                case SkinsName.CenterLeft:
                    CenterLeft(uiElement);
                    break;

                case SkinsName.CenterRight:
                    CenterRight(uiElement);
                    break;

                case SkinsName.TopCenter:
                    TopCenter(uiElement);
                    break;

                case SkinsName.BottomCenter:
                    BottomCenter(uiElement);
                    break;
            }
        }

        private static void Center(UIElement uiElement)
        {
            if (uiElement as TextBox != null)
            {
                (uiElement as TextBox).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as TextBox).VerticalAlignment = VerticalAlignment.Center;
            }
            else if(uiElement as ComboBox != null)
            {
                (uiElement as ComboBox).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as ComboBox).VerticalAlignment = VerticalAlignment.Center;
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

        private static void CenterLeft(UIElement uiElement)
        {
            if (uiElement as TextBox != null)
            {
                (uiElement as TextBox).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as TextBox).VerticalAlignment = VerticalAlignment.Center;
            }
            else if (uiElement as ComboBox != null)
            {
                (uiElement as ComboBox).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as ComboBox).VerticalAlignment = VerticalAlignment.Center;
            }
            else if (uiElement as DatePicker != null)
            {
                (uiElement as DatePicker).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as DatePicker).VerticalAlignment = VerticalAlignment.Center;
            }
            else if (uiElement as Label != null)
            {
                (uiElement as Label).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as Label).VerticalAlignment = VerticalAlignment.Center;
            }
        }

        private static void CenterRight(UIElement uiElement)
        {
            if (uiElement as TextBox != null)
            {
                (uiElement as TextBox).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as TextBox).VerticalAlignment = VerticalAlignment.Center;
            }
            else if (uiElement as ComboBox != null)
            {
                (uiElement as ComboBox).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as ComboBox).VerticalAlignment = VerticalAlignment.Center;
            }
            else if (uiElement as DatePicker != null)
            {
                (uiElement as DatePicker).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as DatePicker).VerticalAlignment = VerticalAlignment.Center;
            }
            else if (uiElement as Label != null)
            {
                (uiElement as Label).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as Label).VerticalAlignment = VerticalAlignment.Center;
            }
        }

        private static void TopCenter(UIElement uiElement)
        {
            if (uiElement as TextBox != null)
            {
                (uiElement as TextBox).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as TextBox).VerticalAlignment = VerticalAlignment.Top;
            }
            else if (uiElement as ComboBox != null)
            {
                (uiElement as ComboBox).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as ComboBox).VerticalAlignment = VerticalAlignment.Top;
            }
            else if (uiElement as DatePicker != null)
            {
                (uiElement as DatePicker).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as DatePicker).VerticalAlignment = VerticalAlignment.Top;
            }
            else if (uiElement as Label != null)
            {
                (uiElement as Label).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as Label).VerticalAlignment = VerticalAlignment.Top;
            }
        }

        private static void BottomCenter(UIElement uiElement)
        {
            if (uiElement as TextBox != null)
            {
                (uiElement as TextBox).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as TextBox).VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (uiElement as ComboBox != null)
            {
                (uiElement as ComboBox).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as ComboBox).VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (uiElement as DatePicker != null)
            {
                (uiElement as DatePicker).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as DatePicker).VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (uiElement as Label != null)
            {
                (uiElement as Label).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as Label).VerticalAlignment = VerticalAlignment.Bottom;
            }
        }
    }
}
