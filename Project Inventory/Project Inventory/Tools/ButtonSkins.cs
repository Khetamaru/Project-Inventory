using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public static class ButtonSkins
    {

        // Button Location //

        public static void TopLeft(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.VerticalAlignment = VerticalAlignment.Top;
        }

        public static void TopCenter(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Top;
        }

        public static void TopRight(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.VerticalAlignment = VerticalAlignment.Top;
        }

        public static void CenterLeft(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.VerticalAlignment = VerticalAlignment.Center;
        }

        public static void CenterCenter(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
        }

        public static void CenterRight(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.VerticalAlignment = VerticalAlignment.Center;
        }

        public static void BottomLeft(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.VerticalAlignment = VerticalAlignment.Bottom;
        }

        public static void BottomCenter(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Bottom;
        }

        public static void BottomRight(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.VerticalAlignment = VerticalAlignment.Bottom;
        }

        // Button Skin //

        public static void StandartButtonSkin(Button button)
        {

            button.Width *= 3;
            button.Height *= 3;
            button.Padding = new Thickness(10);
        }

        public static void StandartLittleMargin(Button button)
        {

            button.Width *= 3;
            button.Height *= 3;
            button.Padding = new Thickness(10);
            button.Margin = new Thickness(10);
        }
    }
}
