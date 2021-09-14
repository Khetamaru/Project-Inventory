using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Project_Inventory.BDD;

namespace Project_Inventory.Tools
{
    public static class UIElementSkin
    {

        // Form //

        /// <summary>
        /// Text Box Skin used at Form Page
        /// </summary>
        /// <param name="textBox"></param>
        public static void TextBoxSkinForm(TextBox textBox, WpfScreen wpfScreen)
        {
            textBox.Width = wpfScreen.PrimaryScreenSizeWidth() / 4;
            textBox.Height = 40;
            textBox.HorizontalAlignment = HorizontalAlignment.Center;
            textBox.VerticalAlignment = VerticalAlignment.Center;
        }

        /// <summary>
        /// Text Box Number Skin used at Form Page
        /// </summary>
        /// <param name="textBox"></param>
        public static void TextBoxNumberSkinForm(TextBox textBox, WpfScreen wpfScreen)
        {
            TextBoxSkinForm(textBox, wpfScreen);

            TextBoxNumberValidationHandler(textBox);
        }

        /// <summary>
        /// Date Picker Skin used at Form Page
        /// </summary>
        /// <param name="datePicker"></param>
        public static void DatePickerSkinForm(DatePicker datePicker)
        {
            datePicker.HorizontalAlignment = HorizontalAlignment.Center;
            datePicker.VerticalAlignment = VerticalAlignment.Center;
        }

        /// <summary>
        /// Combo Box Skin used at Form Page
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="optionTab"></param>
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

        /// <summary>
        /// Combo Box Skin used at Form Page
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="optionTab"></param>
        public static void ComboBoxSkinForm(ComboBox comboBox, List<ListOption> optionTab)
        {
            comboBox.Items.Add("Select an Item");

            foreach (ListOption option in optionTab)
            {
                comboBox.Items.Add(option.Name);
            }

            if (comboBox.SelectedItem == null && comboBox.Items.Count > 0)
            {
                comboBox.SelectedItem = comboBox.Items[0];
            }

            comboBox.HorizontalAlignment = HorizontalAlignment.Center;
            comboBox.VerticalAlignment = VerticalAlignment.Center;
        }

        /// <summary>
        /// Label Skin used at Form Page
        /// </summary>
        /// <param name="label"></param>
        /// <param name="content"></param>
        public static void LabelSkinForm(Label label, string content)
        {
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.Content = content;
        }

        // Storage Viewer (Modify) //

        /// <summary>
        /// Text Box Skin used at Storage Viewer Page ( Modify Mode )
        /// </summary>
        /// <param name="textBox"></param>
        public static void TextBoxSkinModify(TextBox textBox, WpfScreen wpfScreen)
        {
            textBox.MinWidth = wpfScreen.PrimaryScreenSizeWidth() / 100 * 5;
            textBox.HorizontalAlignment = HorizontalAlignment.Center;
            textBox.VerticalAlignment = VerticalAlignment.Center;
        }

        /// <summary>
        /// Text Box Number Skin used at Storage Viewer Page ( Modify Mode )
        /// </summary>
        /// <param name="textBox"></param>
        public static void TextBoxNumberSkinModify(TextBox textBox, WpfScreen wpfScreen)
        {
            TextBoxSkinModify(textBox, wpfScreen);
            TextBoxNumberValidationHandler(textBox);
        }

        /// <summary>
        /// Date Picker Skin used at Storage Viewer Page ( Modify Mode )
        /// </summary>
        /// <param name="datePicker"></param>
        public static void DatePickerSkinModify(DatePicker datePicker)
        {
            datePicker.HorizontalAlignment = HorizontalAlignment.Center;
            datePicker.VerticalAlignment = VerticalAlignment.Center;
        }

        /// <summary>
        /// Combo Box Skin used at Storage Viewer Page ( Modify Mode )
        /// </summary>
        /// <param name="comboBox"></param>
        public static void ComboBoxSkinModify(ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null && comboBox.Items.Count < 0) { comboBox.SelectedItem = comboBox.Items[0]; }
            comboBox.HorizontalAlignment = HorizontalAlignment.Center;
            comboBox.VerticalAlignment = VerticalAlignment.Center;
        }

        /// <summary>
        /// Label Skin used at Storage Viewer Page ( Modify Mode )
        /// </summary>
        /// <param name="label"></param>
        public static void LabelSkinModify(Label label)
        {
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
        }

        /// <summary>
        /// Button Skin
        /// </summary>
        /// <param name="textBox"></param>
        public static void ButtonSkin(Button button, WpfScreen wpfScreen)
        {
            button.Width = wpfScreen.PrimaryScreenSizeWidth() / 4;
            button.Height = 40;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
        }

        // Other //

        /// <summary>
        /// Integration of testing fonction used by Text Box Number.
        /// </summary>
        /// <param name="textBox"></param>
        public static void TextBoxNumberValidationHandler(TextBox textBox)
        {
            textBox.PreviewTextInput += new TextCompositionEventHandler((object sender, TextCompositionEventArgs e) =>
            {
                NumberValidationTextBox(sender, e);
            });
        }

        /// <summary>
        /// Lock use of non-number input for Text Box Number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
