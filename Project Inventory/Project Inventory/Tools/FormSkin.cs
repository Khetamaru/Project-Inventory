using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_Inventory.Tools
{
    public static class FormSkin
    {
        public static TextBox TextBoxSkin(TextBox textBox)
        {
            WpfScreen wpfScreen = new WpfScreen();
            textBox.Width = wpfScreen.PrimaryScreenSizeWidth() / 4;
            textBox.Height = 40;
            textBox.HorizontalAlignment = HorizontalAlignment.Center;
            textBox.VerticalAlignment = VerticalAlignment.Center;

            return textBox;
        }

        public static TextBox TextBoxNumberSkin(TextBox textBox)
        {
            textBox = TextBoxSkin(textBox);

            textBox.PreviewTextInput += new TextCompositionEventHandler((object sender, TextCompositionEventArgs e) =>
            {
                NumberValidationTextBox(sender, e);
            });

            return textBox;
        }

        public static DatePicker DatePickerSkin(DatePicker datePicker)
        {
            datePicker.HorizontalAlignment = HorizontalAlignment.Center;
            datePicker.VerticalAlignment = VerticalAlignment.Center;

            return datePicker;
        }

        public static ListBox ListBoxSkin(ListBox listBox, string[] optionTab)
        {
            listBox.HorizontalAlignment = HorizontalAlignment.Center;
            listBox.VerticalAlignment = VerticalAlignment.Center;

            foreach(string option in optionTab)
            {
                listBox.Items.Add(option);
            }

            return listBox;
        }

        public static Label LabelSkin(Label label, string content)
        {
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.Content = content;

            return label;
        }

        private static void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
