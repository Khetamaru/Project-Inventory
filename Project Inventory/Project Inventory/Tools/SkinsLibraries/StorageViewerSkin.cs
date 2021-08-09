using System;
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
        public static void LoadSkinPosition(UIElement uiElement, SkinLocation labelSkin)
        {
            switch (labelSkin)
            {
                case SkinLocation.CenterCenter:
                    Center(uiElement);
                    break;

                case SkinLocation.CenterLeft:
                    CenterLeft(uiElement);
                    break;

                case SkinLocation.CenterRight:
                    CenterRight(uiElement);
                    break;

                case SkinLocation.TopCenter:
                    TopCenter(uiElement);
                    break;

                case SkinLocation.BottomCenter:
                    BottomCenter(uiElement);
                    break;

                case SkinLocation.TopLeft:
                    TopLeft(uiElement);
                    break;

                case SkinLocation.TopRight:
                    TopRight(uiElement);
                    break;

                case SkinLocation.BottomLeft:
                    BottomLeft(uiElement);
                    break;

                case SkinLocation.BottomRight:
                    BottomRight(uiElement);
                    break;
            }
        }

        private static void BottomRight(UIElement uiElement)
        {
            if (uiElement as TextBox != null)
            {
                (uiElement as TextBox).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as TextBox).VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (uiElement as ComboBox != null)
            {
                (uiElement as ComboBox).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as ComboBox).VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (uiElement as DatePicker != null)
            {
                (uiElement as DatePicker).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as DatePicker).VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (uiElement as Label != null)
            {
                (uiElement as Label).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as Label).VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (uiElement as Button != null)
            {
                (uiElement as Button).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as Button).VerticalAlignment = VerticalAlignment.Bottom;
            }
        }

        private static void BottomLeft(UIElement uiElement)
        {
            if (uiElement as TextBox != null)
            {
                (uiElement as TextBox).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as TextBox).VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (uiElement as ComboBox != null)
            {
                (uiElement as ComboBox).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as ComboBox).VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (uiElement as DatePicker != null)
            {
                (uiElement as DatePicker).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as DatePicker).VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (uiElement as Label != null)
            {
                (uiElement as Label).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as Label).VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (uiElement as Button != null)
            {
                (uiElement as Button).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as Button).VerticalAlignment = VerticalAlignment.Bottom;
            }
        }

        private static void TopRight(UIElement uiElement)
        {
            if (uiElement as TextBox != null)
            {
                (uiElement as TextBox).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as TextBox).VerticalAlignment = VerticalAlignment.Top;
            }
            else if (uiElement as ComboBox != null)
            {
                (uiElement as ComboBox).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as ComboBox).VerticalAlignment = VerticalAlignment.Top;
            }
            else if (uiElement as DatePicker != null)
            {
                (uiElement as DatePicker).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as DatePicker).VerticalAlignment = VerticalAlignment.Top;
            }
            else if (uiElement as Label != null)
            {
                (uiElement as Label).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as Label).VerticalAlignment = VerticalAlignment.Top;
            }
            else if (uiElement as Button != null)
            {
                (uiElement as Button).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as Button).VerticalAlignment = VerticalAlignment.Top;
            }
        }

        private static void TopLeft(UIElement uiElement)
        {
            if (uiElement as TextBox != null)
            {
                (uiElement as TextBox).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as TextBox).VerticalAlignment = VerticalAlignment.Top;
            }
            else if (uiElement as ComboBox != null)
            {
                (uiElement as ComboBox).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as ComboBox).VerticalAlignment = VerticalAlignment.Top;
            }
            else if (uiElement as DatePicker != null)
            {
                (uiElement as DatePicker).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as DatePicker).VerticalAlignment = VerticalAlignment.Top;
            }
            else if (uiElement as Label != null)
            {
                (uiElement as Label).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as Label).VerticalAlignment = VerticalAlignment.Top;
            }
            else if (uiElement as Button != null)
            {
                (uiElement as Button).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as Button).VerticalAlignment = VerticalAlignment.Top;
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
            else if (uiElement as Button != null)
            {
                (uiElement as Button).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as Button).VerticalAlignment = VerticalAlignment.Center;
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
            else if (uiElement as Button != null)
            {
                (uiElement as Button).HorizontalAlignment = HorizontalAlignment.Left;
                (uiElement as Button).VerticalAlignment = VerticalAlignment.Center;
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
            else if (uiElement as Button != null)
            {
                (uiElement as Button).HorizontalAlignment = HorizontalAlignment.Right;
                (uiElement as Button).VerticalAlignment = VerticalAlignment.Center;
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
            else if (uiElement as Button != null)
            {
                (uiElement as Button).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as Button).VerticalAlignment = VerticalAlignment.Top;
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
            else if (uiElement as Button != null)
            {
                (uiElement as Button).HorizontalAlignment = HorizontalAlignment.Center;
                (uiElement as Button).VerticalAlignment = VerticalAlignment.Bottom;
            }
        }
    }
}
