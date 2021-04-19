using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public static class ButtonSkins
    {

        // Button Location //

        public static Button TopLeft(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.VerticalAlignment = VerticalAlignment.Top;

            return button;
        }

        public static Button TopCenter(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Top;

            return button;
        }

        public static Button TopRight(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.VerticalAlignment = VerticalAlignment.Top;

            return button;
        }

        public static Button CenterLeft(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.VerticalAlignment = VerticalAlignment.Center;

            return button;
        }

        public static Button CenterCenter(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;

            return button;
        }

        public static Button CenterRight(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.VerticalAlignment = VerticalAlignment.Center;

            return button;
        }

        public static Button BottomLeft(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.VerticalAlignment = VerticalAlignment.Bottom;

            return button;
        }

        public static Button BottomCenter(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Bottom;

            return button;
        }

        public static Button BottomRight(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.VerticalAlignment = VerticalAlignment.Bottom;

            return button;
        }

        // Button Skin //

        public static Button StandartButtonSkin(Button button)
        {

            button.Width *= 3;
            button.Height *= 3;
            button.Padding = new Thickness(10);

            return button;
        }

        public static Button StandartLittleMargin(Button button)
        {

            button.Width *= 3;
            button.Height *= 3;
            button.Padding = new Thickness(10);
            button.Margin = new Thickness(10);

            return button;
        }
    }
}
