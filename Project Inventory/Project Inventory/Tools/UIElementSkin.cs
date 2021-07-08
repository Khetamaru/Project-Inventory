using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_Inventory.Tools
{
    public static class UIElementSkin
    {

        // Form //

        public static void TextBoxSkinForm(TextBox textBox)
        {
            WpfScreen wpfScreen = new WpfScreen();
            textBox.Width = wpfScreen.PrimaryScreenSizeWidth() / 4;
            textBox.Height = 40;
            textBox.HorizontalAlignment = HorizontalAlignment.Center;
            textBox.VerticalAlignment = VerticalAlignment.Center;
        }

        public static void TextBoxNumberSkinForm(TextBox textBox)
        {
            TextBoxSkinForm(textBox);

            TextBoxNumberValidationHandler(textBox);
        }

        public static void DatePickerSkinForm(DatePicker datePicker)
        {
            datePicker.HorizontalAlignment = HorizontalAlignment.Center;
            datePicker.VerticalAlignment = VerticalAlignment.Center;
        }

        public static void ComboBoxSkinForm(ComboBox comboBox, string[] optionTab)
        {

            foreach (string option in optionTab)
            {
                comboBox.Items.Add(option);
            }

            if (comboBox.SelectedItem == null) 
            { 
                comboBox.SelectedItem = comboBox.Items[0]; 
            }

            comboBox.HorizontalAlignment = HorizontalAlignment.Center;
            comboBox.VerticalAlignment = VerticalAlignment.Center;
        }

        public static void LabelSkinForm(Label label, string content)
        {
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.Content = content;
        }

        // Storage Viewer (Modify) //

        public static void TextBoxSkinModify(TextBox textBox)
        {
            WpfScreen wpfScreen = new WpfScreen();
            textBox.MinWidth = wpfScreen.PrimaryScreenSizeWidth() / 100 * 5;
            textBox.HorizontalAlignment = HorizontalAlignment.Center;
            textBox.VerticalAlignment = VerticalAlignment.Center;
        }

        public static void TextBoxNumberSkinModify(TextBox textBox)
        {
            TextBoxSkinModify(textBox);
            TextBoxNumberValidationHandler(textBox);
        }

        public static void DatePickerSkinModify(DatePicker datePicker)
        {
            datePicker.HorizontalAlignment = HorizontalAlignment.Center;
            datePicker.VerticalAlignment = VerticalAlignment.Center;
        }

        public static void ComboBoxSkinModify(ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null) { comboBox.SelectedItem = comboBox.Items[0]; }
            comboBox.HorizontalAlignment = HorizontalAlignment.Center;
            comboBox.VerticalAlignment = VerticalAlignment.Center;
        }

        public static void LabelSkinModify(Label label)
        {
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
        }

        // Other //

        public static void TextBoxNumberValidationHandler(TextBox textBox)
        {
            textBox.PreviewTextInput += new TextCompositionEventHandler((object sender, TextCompositionEventArgs e) =>
            {
                NumberValidationTextBox(sender, e);
            });
        }

        private static void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
