using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project_Inventory
{
    /// <summary>
    /// Tool box class to store all most usefull functions of the program to be useable everywhere.
    /// </summary>
    public class ToolBox
    {
        private WpfScreen wpfScreen;

        public double windowWidth;
        public double windowHeight;

        public ToolBox(double titleBarHeight)
        {
            wpfScreen = new WpfScreen();

            windowWidth = wpfScreen.PrimaryScreenSizeWidth();
            windowHeight = wpfScreen.PrimaryScreenSizeHeight() - titleBarHeight;
        }

        // BUTTON PART //

        /// <summary>
        /// Generate UIElement Button without any event on click
        /// </summary>
        /// <param name="content"></param>
        /// <param name="skinName"></param>
        /// <param name="skinPosition"></param>
        /// <returns></returns>
        public Button CreateButton(string content,
                                   SkinsName skinName,
                                   SkinsName skinPosition)
        {
            Button temp = new Button();

            temp.Content = content;

            LoadButtonPosition(temp, skinPosition);
            LoadButtonSkin(temp, skinName);

            return temp;
        }

        /// <summary>
        /// Generate UIElement Button image without any event on click
        /// </summary>
        /// <param name="content"></param>
        /// <param name="skinName"></param>
        /// <param name="skinPosition"></param>
        /// <returns></returns>
        public Button CreateButton(ImagesName imagesName,
                                   SkinsName skinName,
                                   SkinsName skinPosition,
                                   ImageSizesName imageSizesName)
        {
            Button temp = new Button();
            string startupPath = Environment.CurrentDirectory;

            int height = 0;
            int width = 0;

            switch (imageSizesName)
            {
                case ImageSizesName.Small:

                    height = width = 32;
                    break;
                case ImageSizesName.Medium:

                    height = width = 64;
                    break;
                case ImageSizesName.Large:

                    height = width = 128;
                    break;
                case ImageSizesName.Logo:

                    height = width = 256;
                    break;
            }

            temp.Content = new Image
            {
                Source = new BitmapImage(new Uri(startupPath + "..\\..\\..\\..\\Images\\" + imagesName.ToString() + ".png", UriKind.Absolute)),
                Stretch = Stretch.Fill,
                Height = height,
                Width = width
            };

            LoadButtonPosition(temp, skinPosition);
            LoadButtonSkin(temp, skinName);
            temp.BorderBrush = new SolidColorBrush(Colors.Transparent);
            temp.Background = new SolidColorBrush(Colors.Transparent);

            return temp;
        }

        /// <summary>
        /// Generate UIElement Button with event(s) on click
        /// </summary>
        /// <param name="content"></param>
        /// <param name="router"></param>
        /// <param name="skinName"></param>
        /// <param name="skinPosition"></param>
        /// <returns></returns>
        public Button CreateSwitchButton(string content,
                                         RoutedEventLibrary router,
                                         SkinsName skinName,
                                         SkinsName skinPosition)
        {
            Button temp = CreateButton(content, skinName, skinPosition);
            AddOnClickButton(temp, router);

            return temp;
        }

        /// <summary>
        /// Generate UIElement Button image with event(s) on click
        /// </summary>
        /// <param name="content"></param>
        /// <param name="router"></param>
        /// <param name="skinName"></param>
        /// <param name="skinPosition"></param>
        /// <returns></returns>
        public Button CreateSwitchButtonImage(ImagesName imagesName,
                                         RoutedEventLibrary router,
                                         SkinsName skinName,
                                         SkinsName skinPosition,
                                         ImageSizesName imageSizesName)
        {
            Button temp = CreateButton(imagesName, skinName, skinPosition, imageSizesName);
            AddOnClickButton(temp, router);

            return temp;
        }

        /// <summary>
        /// Inject an event on click of the button
        /// </summary>
        /// <param name="button"></param>
        /// <param name="routedEventLibrary"></param>
        private void AddOnClickButton(Button button, RoutedEventLibrary routedEventLibrary)
        {
            routedEventLibrary.EventInjection(button);
        }

        /// <summary>
        /// Set up button position in the grid
        /// </summary>
        /// <param name="button"></param>
        /// <param name="skinPosition"></param>
        private void LoadButtonPosition(Button button, SkinsName skinPosition)
        {
            switch (skinPosition)
            {
                case SkinsName.TopLeft:
                    ButtonSkins.TopLeft(button);
                    break;

                case SkinsName.TopCenter:
                    ButtonSkins.TopCenter(button);
                    break;

                case SkinsName.TopRight:
                    ButtonSkins.TopRight(button);
                    break;

                case SkinsName.CenterLeft:
                    ButtonSkins.CenterLeft(button);
                    break;

                case SkinsName.CenterCenter:
                    ButtonSkins.CenterCenter(button);
                    break;

                case SkinsName.CenterRight:
                    ButtonSkins.CenterRight(button);
                    break;

                case SkinsName.BottomLeft:
                    ButtonSkins.BottomLeft(button);
                    break;

                case SkinsName.BottomCenter:
                    ButtonSkins.BottomCenter(button);
                    break;

                case SkinsName.BottomRight:
                    ButtonSkins.BottomRight(button);
                    break;
            }
        }

        /// <summary>
        /// Set up button skin
        /// </summary>
        /// <param name="button"></param>
        /// <param name="skinName"></param>
        private void LoadButtonSkin(Button button, SkinsName skinName)
        {
            switch (skinName)
            {
                case SkinsName.Standart:
                    ButtonSkins.StandartButtonSkin(button);
                    break;

                case SkinsName.StandartLittleMargin:
                    ButtonSkins.StandartLittleMargin(button);
                    break;
            }
        }

        // GRID PART //

        /// <summary>
        /// Set up grid number of lines verticals and horizontals, size percentage of the screen and it location
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rowNb"></param>
        /// <param name="columnNb"></param>
        /// <param name="skinName"></param>
        /// <param name="lengthName"></param>
        public void SetUpGrid(Grid grid,
                               int rowNb,
                               int columnNb,
                               SkinsName skinName,
                               SkinsName lengthName)
        {
            EmptyGrid(grid);

            CreateRowsInGrid(grid, rowNb);
            CreateColumnsInGrid(grid, columnNb);

            LoadGridLocation(grid, skinName);
            LoadGridLength(grid, lengthName);
        }


        /// <summary>
        /// Clean UI grid before show a new screen
        /// </summary>
        /// <param name="grid"></param>
        public void EmptyGrid(Grid grid)
        {
            while (grid.Children.Count >= 1)
            {
                grid.Children.Remove(grid.Children[0]);
            }

            while (grid.RowDefinitions.Count >= 1)
            {
                grid.RowDefinitions.Remove(grid.RowDefinitions[0]);
            }

            while (grid.ColumnDefinitions.Count >= 1)
            {
                grid.ColumnDefinitions.Remove(grid.ColumnDefinitions[0]);
            }

            //grid.Width = 0;
            grid.Height = 0;
        }

        /// <summary>
        /// Load grid location
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="skinName"></param>
        public void LoadGridLocation(Grid grid, SkinsName skinName)
        {
            switch (skinName)
            {
                case SkinsName.TopLeft:
                    GridSkins.TopLeft(grid);
                    break;

                case SkinsName.StretchLeft:
                    GridSkins.StretchLeft(grid);
                    break;

                case SkinsName.BottomLeft:
                    GridSkins.BottomLeft(grid);
                    break;

                case SkinsName.TopStretch:
                    GridSkins.TopStretch(grid);
                    break;

                case SkinsName.BottomStretch:
                    GridSkins.BottomStretch(grid);
                    break;

                case SkinsName.StretchStretch:
                    GridSkins.StretchStretch(grid);
                    break;

                case SkinsName.CenterCenter:
                    GridSkins.CenterCenter(grid);
                    break;

                case SkinsName.StretchCenter:
                    GridSkins.StretchCenter(grid);
                    break;

                case SkinsName.CenterStretch:
                    GridSkins.CenterStretch(grid);
                    break;

                case SkinsName.TopRight:
                    GridSkins.TopRight(grid);
                    break;

                case SkinsName.StretchRight:
                    GridSkins.StretchRight(grid);
                    break;

                case SkinsName.BottomRight:
                    GridSkins.BottomRight(grid);
                    break;
            }
        }

        /// <summary>
        /// Get size percentage of the screen
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="lengthName"></param>
        public void LoadGridLength(Grid grid, SkinsName lengthName)
        {
            switch (lengthName)
            {
                case SkinsName.WidthOneTier:
                    GridSkins.WidthOneTier(grid, windowWidth);
                    break;

                case SkinsName.WidthTwoTier:
                    GridSkins.WidthTwoTier(grid, windowWidth);
                    break;

                case SkinsName.HeightOneTier:
                    GridSkins.HeightOneTier(grid, windowHeight);
                    break;

                case SkinsName.HeightTwoTier:
                    GridSkins.HeightTwoTier(grid, windowHeight);
                    break;

                case SkinsName.HeightTenPercent:
                    GridSkins.HeightTenPercent(grid, windowHeight);
                    break;

                case SkinsName.HeightEightPercent:
                    GridSkins.HeightEightPercent(grid, windowHeight);
                    break;

                case SkinsName.HeightNintyPercent:
                    GridSkins.HeightNintyPercent(grid, windowHeight);
                    break;
            }
        }

        /// <summary>
        /// Insert button in the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="button"></param>
        /// <param name="rowNb"></param>
        /// <param name="columnNb"></param>
        public void AddButtonToGrid(Grid grid, Button button, int rowNb, int columnNb)
        {
            Grid.SetRow(button, rowNb);
            Grid.SetColumn(button, columnNb);
            grid.Children.Add(button);
        }

        /// <summary>
        /// add button in the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="content"></param>
        /// <param name="rowNb"></param>
        /// <param name="columnNb"></param>
        /// <param name="skinName"></param>
        /// <param name="skinPosition"></param>
        public void CreateButtonToGrid(Grid grid,
                                       string content,
                                       int rowNb,
                                       int columnNb,
                                       SkinsName skinName,
                                       SkinsName skinPosition)
        {
            Button button = CreateButton(content, skinName, skinPosition);

            AddButtonToGrid(grid, button, rowNb, columnNb);
        }

        /// <summary>
        /// Add switch button in the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="content"></param>
        /// <param name="router"></param>
        /// <param name="rowNb"></param>
        /// <param name="columnNb"></param>
        /// <param name="skinName"></param>
        /// <param name="skinPosition"></param>
        public void CreateSwitchButtonToGrid(Grid grid,
                                               string content,
                                               RoutedEventLibrary router,
                                               int rowNb,
                                               int columnNb,
                                               SkinsName skinName,
                                               SkinsName skinPosition)
        {
            Button button = CreateSwitchButton(content, router, skinName, skinPosition);

            AddButtonToGrid(grid, button, rowNb, columnNb);
        }

        /// <summary>
        /// Add switch button in the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="storage"></param>
        /// <param name="router"></param>
        /// <param name="rowNb"></param>
        /// <param name="columnNb"></param>
        /// <param name="skinName"></param>
        /// <param name="skinPosition"></param>
        public void CreateSwitchButtonToGrid(Grid grid,
                                               Storage storage,
                                               RoutedEventLibrary router,
                                               int rowNb,
                                               int columnNb,
                                               SkinsName skinName,
                                               SkinsName skinPosition)
        {
            Button button = CreateSwitchButton(storage.Name, router, skinName, skinPosition);

            AddButtonToGrid(grid, button, rowNb, columnNb);
        }

        /// <summary>
        /// Generate the good number of horizontals lanes
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="nbRows"></param>
        public void CreateRowsInGrid(Grid grid, int nbRows)
        {
            int i;

            for (i = 0; i < nbRows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        /// <summary>
        /// Generate the good number of verticals lanes
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="nbColumns"></param>
        public void CreateColumnsInGrid(Grid grid, int nbColumns)
        {
            int i;

            for (i = 0; i < nbColumns; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        /// <summary>
        /// Add multiple buttons in the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="buttonsTab"></param>
        /// <param name="buttonsSkin"></param>
        /// <param name="skinPosition"></param>
        public void CreateSwitchButtonsToGridByTab(Grid grid, string[] buttonsTab, SkinsName buttonsSkin, SkinsName skinPosition)
        {
            int i;
            int j;
            int k = 0;

            int rowNb = grid.RowDefinitions.Count;
            int columnNb = grid.ColumnDefinitions.Count;

            for (i = 0; i < rowNb; i++)
            {
                for (j = 0; j < columnNb; j++)
                {
                    if (buttonsTab.Length >= (i + 1) * (j + 1))
                    {
                        CreateButtonToGrid(grid, buttonsTab[k], i, j, buttonsSkin, skinPosition);
                        k++;
                    }
                }
            }
        }

        /// <summary>
        /// Add multiple buttons in the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="buttonsTab"></param>
        /// <param name="routerTab"></param>
        /// <param name="buttonsSkin"></param>
        /// <param name="skinPosition"></param>
        public void CreateSwitchButtonsToGridByTab(Grid grid, string[] buttonsTab, RoutedEventLibrary[] routerTab, SkinsName buttonsSkin, SkinsName skinPosition)
        {
            int i;
            int j;
            int k = 0;

            int rowNb = grid.RowDefinitions.Count;
            int columnNb = grid.ColumnDefinitions.Count;

            for (i = 0; i < rowNb; i++)
            {
                for (j = 0; j < columnNb; j++)
                {
                    if (buttonsTab.Length >= (i + 1) * (j + 1))
                    {
                        CreateSwitchButtonToGrid(grid, buttonsTab[k], routerTab[k], i, j, buttonsSkin, skinPosition);
                        k++;
                    }
                }
            }
        }

        /// <summary>
        /// Add multiple buttons in the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="buttonsTab"></param>
        /// <param name="routerTab"></param>
        /// <param name="buttonsSkin"></param>
        /// <param name="skinPosition"></param>
        public void CreateSwitchButtonsToGridByTab(Grid grid, Storage[] buttonsTab, RoutedEventLibrary[] routerTab, SkinsName buttonsSkin, SkinsName skinPosition)
        {
            int i;
            int j;
            int k = 0;
            int z;

            int rowNb = grid.RowDefinitions.Count;
            int columnNb = grid.ColumnDefinitions.Count;

            for (i = 0; i < rowNb; i++)
            {
                for (j = 0; j < columnNb; j++)
                {
                    if (i >= 1)
                    {
                        if (j >= 1) { z = (i * 5) + j; }
                        else { z = i * 5; }
                    }
                    else
                    {
                        if (j >= 1) { z = j; }
                        else { z = 1; }
                    }

                    if (buttonsTab.Length > z)
                    {
                        CreateSwitchButtonToGrid(grid, buttonsTab[k], routerTab[k], i, j, buttonsSkin, skinPosition);
                        k++;
                    }
                }
            }
        }

        /// <summary>
        /// Add multiple buttons in the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="buttonsTab"></param>
        /// <param name="routerTab"></param>
        /// <param name="buttonsSkin"></param>
        /// <param name="skinPosition"></param>
        public void CreateSwitchButtonsToGridByTab(Grid grid, string[] buttonsTab, RoutedEventLibrary[] routerTab, SkinsName[] buttonsSkin, SkinsName[] skinPosition)
        {
            int i;
            int j;
            int k = 0;

            int rowNb = grid.RowDefinitions.Count;
            int columnNb = grid.ColumnDefinitions.Count;

            for (i = 0; i < rowNb; i++)
            {
                for (j = 0; j < columnNb; j++)
                {
                    if (buttonsTab.Length >= (i + 1) * (j + 1))
                    {
                        CreateSwitchButtonToGrid(grid, buttonsTab[k], routerTab[k], i, j, buttonsSkin[k], skinPosition[k]);
                        k++;
                    }
                }
            }
        }

        /// <summary>
        /// Add multiple UIElements of the form in the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="formElements"></param>
        /// <param name="labels"></param>
        /// <param name="listBoxName"></param>
        public void CreateFormToGridByTab(Grid grid, UIElementsName[] formElements, string[] labels, ComboBoxNames listBoxName)
        {
            Label label;
            UIElement uIElement;
            int i = 0;

            grid.ColumnDefinitions[0].Width = new GridLength(windowWidth * 0.2, GridUnitType.Pixel);
            grid.ColumnDefinitions[1].Width = new GridLength(windowWidth * 0.8, GridUnitType.Pixel);

            foreach (UIElementsName name in formElements)
            {
                label = new Label();
                UIElementSkin.LabelSkinForm(label, labels[i]);
                Grid.SetRow(label, i);
                Grid.SetColumn(label, 0);
                grid.Children.Add(label);

                switch (name)
                {
                    case (UIElementsName.TextBox):

                        uIElement = new TextBox();
                        UIElementSkin.TextBoxSkinForm(uIElement as TextBox);
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as TextBox);

                        break;

                    case (UIElementsName.TextBoxNumber):

                        uIElement = new TextBox();
                        UIElementSkin.TextBoxSkinForm(uIElement as TextBox);
                        UIElementSkin.TextBoxNumberSkinForm(uIElement as TextBox);
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as TextBox);

                        break;

                    case (UIElementsName.DatePicker):

                        uIElement = new DatePicker();
                        UIElementSkin.DatePickerSkinForm(uIElement as DatePicker);
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as DatePicker);

                        break;

                    case (UIElementsName.ComboBox):

                        uIElement = new ComboBox();

                        switch (listBoxName)
                        {
                            case ComboBoxNames.UIElementsType:
                                string[] comboBoxStrings = comboBoxStrings = new string[] { "TextBox", "TextBoxNumber", "DatePicker", "ComboBox" };
                                UIElementSkin.ComboBoxSkinForm(uIElement as ComboBox, comboBoxStrings);
                                break;
                        }

                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as ComboBox);

                        break;
                }

                i++;
            }
        }

        /// <summary>
        /// Create UIElements in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="stringTab"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabToGrid(Grid grid, string[,] stringTab, SkinsName skinPosition)
        {
            int i;
            int j;
            int k = 0;

            int rowNb = grid.RowDefinitions.Count;
            int columnNb = grid.ColumnDefinitions.Count;

            for (i = 0; i < rowNb; i++)
            {
                for (j = 0; j < columnNb; j++)
                {
                    if (stringTab.Length >= (i + 1) * (j + 1))
                    {
                        CreateTabCellToGrid(grid, stringTab[i, j], i, j, skinPosition);
                        k++;
                    }
                }
            }
        }

        /// <summary>
        /// Create UIElements in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="stringTab"></param>
        /// <param name="indicTab"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabToGrid(Grid grid, string[,] stringTab, string[,] indicTab, SkinsName skinPosition, List<Button> buttonList)
        {
            int i;
            int j;
            int k = 0;

            int rowNb = grid.RowDefinitions.Count - 1;
            int columnNb = grid.ColumnDefinitions.Count - 1;

            string[] addElemString = new string[columnNb];

            for (i = 0; i < addElemString.Length; i++)
            {
                addElemString[i] = string.Empty;
            }

            for (i = 0; i < rowNb; i++)
            {
                if (i == 0)
                {
                    for (j = 0; j < columnNb; j++)
                    {
                        if (stringTab.Length >= (i + 1) * (j + 1))
                        {
                            CreateHeaderToGrid(grid, stringTab[i, j], i, j + 1, skinPosition);
                            k++;
                        }
                    }
                }
                else
                {
                    Grid.SetRow(buttonList[i], i);
                    Grid.SetColumn(buttonList[i], 0);
                    grid.Children.Add(buttonList[i]);

                    for (j = 0; j < columnNb; j++)
                    {
                        if (stringTab.Length >= (i + 1) * (j + 1))
                        {
                            CreateTabCellToGrid(grid, stringTab[i, j], indicTab[i, j], i, j + 1, skinPosition);
                            k++;
                        }
                    }
                }
            }

            for (int l = 0; l < addElemString.Length; l++)
            {
                CreateTabCellToGrid(grid, addElemString[l], indicTab[0, l], rowNb + 1, l + 1, skinPosition);
            }
        }

        /// <summary>
        /// Create UIElements in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="storageTab"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabToGrid(Grid grid, Storage[] storageTab, SkinsName skinPosition)
        {
            int i = storageTab.Length;
            int j = 1;
            int k = 0;
            while (i >= 5)
            {
                i -= 5;
                j++;
            }

            int rowNb = j;

            string addElemString = string.Empty;

            for (i = 0; i < rowNb; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    if (storageTab.Length > j + (i * 5))
                    {
                        CreateHeaderToGrid(grid, storageTab[j + (i * 5)].Name, i, j, skinPosition);
                        k = j;
                    }
                }
            }
            k++;

            if (k >= 5) { k = 0; }

            CreateHeaderToGrid(grid, addElemString, rowNb, k, skinPosition);
        }

        /// <summary>
        /// Create UIElements in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="text"></param>
        /// <param name="indication"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabCellToGrid(Grid grid, string text, string indication, int row, int column, SkinsName skinPosition)
        {
            if (indication == UIElementsName.DatePicker.ToString())
            {
                DatePicker uiElement = new DatePicker();
                UIElementSkin.DatePickerSkinForm(uiElement);
                if (text != "")
                {
                    uiElement.SelectedDate = DateTime.Parse(text);
                }

                UIElementSkin.DatePickerSkinModify(uiElement);
                StorageViewerSkin.LoadSkinPosition(uiElement, skinPosition);

                // insert potential clickEvent

                Grid.SetRow(uiElement, row);
                Grid.SetColumn(uiElement, column);

                grid.Children.Add(uiElement);
            }

            if (indication == UIElementsName.TextBox.ToString())
            {
                TextBox uiElement = new TextBox();
                uiElement.Text = text;

                UIElementSkin.TextBoxSkinModify(uiElement);
                StorageViewerSkin.LoadSkinPosition(uiElement, skinPosition);

                // insert potential clickEvent

                Grid.SetRow(uiElement, row);
                Grid.SetColumn(uiElement, column);

                grid.Children.Add(uiElement);
            }

            if (indication == UIElementsName.TextBoxNumber.ToString())
            {
                TextBox uiElement = new TextBox();
                UIElementSkin.TextBoxNumberValidationHandler(uiElement);
                uiElement.Text = text;

                UIElementSkin.TextBoxNumberSkinModify(uiElement);
                StorageViewerSkin.LoadSkinPosition(uiElement, skinPosition);

                // insert potential clickEvent

                Grid.SetRow(uiElement, row);
                Grid.SetColumn(uiElement, column);

                grid.Children.Add(uiElement);
            }

            if (indication == UIElementsName.ComboBox.ToString())
            {
                ComboBox uiElement = new ComboBox();
                string[] comboBoxStrings = new string[] { "TextBox", "TextBoxNumber", "DatePicker", "ComboBox" };
                UIElementSkin.ComboBoxSkinForm(uiElement, comboBoxStrings);

                if (text != null)
                {
                    uiElement.SelectedItem = text;
                }
                else
                {
                    uiElement.SelectedItem = uiElement.Items[0];
                }

                UIElementSkin.ComboBoxSkinModify(uiElement);
                StorageViewerSkin.LoadSkinPosition(uiElement, skinPosition);

                // insert potential clickEvent

                Grid.SetRow(uiElement, row);
                Grid.SetColumn(uiElement, column);

                grid.Children.Add(uiElement);
            }
        }

        /// <summary>
        /// Create Labels in the scroll grid for viewer mode
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabCellToGrid(Grid grid, string text, int row, int column, SkinsName skinPosition)
        {
            Label label = new Label();
            label.Content = text;

            UIElementSkin.LabelSkinModify(label);
            StorageViewerSkin.LoadSkinPosition(label, skinPosition);

            // insert potential clickEvent

            Grid.SetRow(label, row);
            Grid.SetColumn(label, column);

            grid.Children.Add(label);
        }

        /// <summary>
        /// Create the header line in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="skinPosition"></param>
        public void CreateHeaderToGrid(Grid grid, string text, int row, int column, SkinsName skinPosition)
        {
            TextBox textbox = new TextBox();
            textbox.Text = text;

            UIElementSkin.TextBoxSkinModify(textbox);
            StorageViewerSkin.LoadSkinPosition(textbox, skinPosition);

            Grid.SetRow(textbox, row);
            Grid.SetColumn(textbox, column);

            grid.Children.Add(textbox);
        }

        /// <summary>
        /// Adapt view about number of button to show
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="tabLength"></param>
        /// <param name="widthLimit"></param>
        /// <param name="skinName"></param>
        /// <param name="lengthName"></param>
        public void ButtonPlacer(Grid grid, int tabLength, int widthLimit, SkinsName skinName, SkinsName lengthName)
        {
            int i;
            int j = 1;

            if (tabLength > widthLimit)
            {
                do
                {
                    i = widthLimit;
                    j++;
                    tabLength -= widthLimit;
                }
                while (tabLength > widthLimit);
            }
            else
            {
                i = tabLength;
            }

            SetUpGrid(grid, j, i, skinName, lengthName);
        }

        // Form //

        /// <summary>
        /// Generate a form scrollable
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="embededGrid"></param>
        /// <param name="gridRowOne"></param>
        /// <param name="gridColumnOne"></param>
        /// <param name="gridRowTwo"></param>
        /// <param name="gridColumnTwo"></param>
        /// <param name="gridSkin"></param>
        /// <param name="skinHeight"></param>
        /// <param name="stringTab"></param>
        /// <param name="indicTab"></param>
        /// <param name="listBoxNames"></param>
        public void CreateScrollableForm(Grid grid, Grid embededGrid, int gridRowOne, int gridColumnOne, int gridRowTwo, int gridColumnTwo, SkinsName gridSkin, SkinsName skinHeight, UIElementsName[] stringTab, string[] indicTab, ComboBoxNames listBoxNames)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpGrid(grid, gridRowOne, gridColumnOne, gridSkin, skinHeight);

            SetUpNewGrid(embededGrid, gridRowTwo, gridColumnTwo, SkinsName.TopLeft, SkinsName.None);

            ScrollFormInit(embededGrid, gridRowTwo, scrollViewer);

            CreateFormToGridByTab(embededGrid, stringTab, indicTab, listBoxNames);

            EmbedScrollableGrid(grid, embededGrid, scrollViewer);
        }

        /// <summary>
        /// Init scroll form sizes
        /// </summary>
        /// <param name="embededGrid"></param>
        /// <param name="gridRowTwo"></param>
        /// <param name="scrollViewer"></param>
        public void ScrollFormInit(Grid embededGrid, int gridRowTwo, ScrollViewer scrollViewer)
        {
            SetScrollFormHeight(embededGrid);

            if (gridRowTwo > 5)
            {
                ScrollViewerVertical(scrollViewer);
            }
            else
            {
                ScrollViewerNoOne(scrollViewer);
            }
        }

        /// <summary>
        /// Set scroll form verticaly
        /// </summary>
        /// <param name="grid"></param>
        public void SetScrollFormHeight(Grid grid)
        {
            foreach (RowDefinition row in grid.RowDefinitions)
            {
                GridSkins.RowHeightFifteenPercent(row, windowHeight);
            }
        }

        /// <summary>
        /// Get all fields results of the form
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="uiReselt"></param>
        /// <param name="formElements"></param>
        public void GetUiElementResult(Grid grid, string[] uiReselt, UIElementsName[] formElements)
        {
            int i = 0;
            int j = 0;

            foreach (UIElement element in grid.Children)
            {
                if (i % 2 != 0)
                {
                    switch (formElements[j])
                    {
                        case (UIElementsName.TextBox):

                            uiReselt[j] = (element as TextBox).Text;

                            break;

                        case (UIElementsName.TextBoxNumber):

                            uiReselt[j] = (element as TextBox).Text;

                            break;

                        case (UIElementsName.DatePicker):

                            uiReselt[j] = (element as DatePicker).Text;

                            break;

                        case (UIElementsName.ComboBox):

                            uiReselt[j] = (element as ComboBox).SelectedItem.ToString();

                            break;
                    }

                    j++;
                }

                i++;
            }
        }

        /// <summary>
        /// Verify that the fields of the form has been correctly filled
        /// </summary>
        /// <param name="uiElements"></param>
        /// <param name="formElements"></param>
        /// <returns></returns>
        public bool FormResultValidation(string[] uiElements, UIElementsName[] formElements)
        {
            int i = 0;
            int j = 0;

            foreach (string element in uiElements)
            {
                switch (formElements[i])
                {
                    case (UIElementsName.TextBox):

                        for (j = 0; j < element.Length; j++)
                        {
                            if (element[j] == '~' ||
                               element[j] == ',' ||
                               element[j] == '{' ||
                               element[j] == '}')
                            {
                                element.Remove(j, 1);
                            }
                        }

                        break;

                    case (UIElementsName.TextBoxNumber):

                        break;

                    case (UIElementsName.DatePicker):

                        break;

                    case (UIElementsName.ComboBox):

                        break;
                }

                if (element == "" || element == null)
                {
                    return false;
                }

                i++;
            }

            return true;
        }

        // StorageViewer //

        /// <summary>
        /// Generate a scrollable grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="embededGrid"></param>
        /// <param name="gridRowOne"></param>
        /// <param name="gridColumnOne"></param>
        /// <param name="gridRowTwo"></param>
        /// <param name="gridColumnTwo"></param>
        /// <param name="gridSkin"></param>
        /// <param name="skinHeight"></param>
        /// <param name="tabPos"></param>
        /// <param name="stringTab"></param>
        /// <param name="indicTab"></param>
        public void CreateScrollableGrid(Grid grid, Grid embededGrid, int gridRowOne, int gridColumnOne, int gridRowTwo, int gridColumnTwo, SkinsName gridSkin, SkinsName skinHeight, SkinsName tabPos, string[,] stringTab, string[,] indicTab)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpGrid(grid, gridRowOne, gridColumnOne, gridSkin, skinHeight);

            SetUpNewGrid(embededGrid, gridRowTwo, gridColumnTwo, SkinsName.StretchStretch, skinHeight);

            ScrollGridInit(embededGrid, gridRowTwo, gridColumnTwo, scrollViewer);

            CreateTabToGrid(embededGrid, stringTab, tabPos);

            EmbedScrollableGrid(grid, embededGrid, scrollViewer);
        }

        /// <summary>
        /// Generate a scrollable grid to modify datas
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="embededGrid"></param>
        /// <param name="gridRowOne"></param>
        /// <param name="gridColumnOne"></param>
        /// <param name="gridRowTwo"></param>
        /// <param name="gridColumnTwo"></param>
        /// <param name="gridSkin"></param>
        /// <param name="skinHeight"></param>
        /// <param name="tabPos"></param>
        /// <param name="stringTab"></param>
        /// <param name="indicTab"></param>
        public void CreateScrollableGridModfiable(Grid grid, Grid embededGrid, int gridRowOne, int gridColumnOne, int gridRowTwo, int gridColumnTwo, SkinsName gridSkin, SkinsName skinHeight, SkinsName tabPos, string[,] stringTab, string[,] indicTab, List<Button> buttonList)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpGrid(grid, gridRowOne, gridColumnOne, gridSkin, skinHeight);

            SetUpNewGrid(embededGrid, gridRowTwo, gridColumnTwo, SkinsName.StretchStretch, skinHeight);

            ScrollGridInit(embededGrid, gridRowTwo, gridColumnTwo, scrollViewer);

            CreateTabToGrid(embededGrid, stringTab, indicTab, tabPos, buttonList);

            EmbedScrollableGrid(grid, embededGrid, scrollViewer);
        }

        /// <summary>
        /// Set up the grid that embed the scroll bar
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rowNb"></param>
        /// <param name="columnNb"></param>
        /// <param name="skinName"></param>
        /// <param name="lengthName"></param>
        public void SetUpNewGrid(Grid grid,
                           int rowNb,
                           int columnNb,
                           SkinsName skinName,
                           SkinsName lengthName)
        {
            CreateRowsInGrid(grid, rowNb);
            CreateColumnsInGrid(grid, columnNb);

            LoadGridLocation(grid, skinName);
            LoadGridLength(grid, lengthName);
        }

        /// <summary>
        /// insert the scroll bar in the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="embededGrid"></param>
        /// <param name="scrollViewer"></param>
        public void EmbedScrollableGrid(Grid grid, Grid embededGrid, ScrollViewer scrollViewer)
        {
            scrollViewer.Content = embededGrid;
            grid.Children.Add(scrollViewer);
        }

        /// <summary>
        /// Init the scroll grid
        /// </summary>
        /// <param name="embededGrid"></param>
        /// <param name="gridRowTwo"></param>
        /// <param name="gridColumnTwo"></param>
        /// <param name="scrollViewer"></param>
        public void ScrollGridInit(Grid embededGrid, int gridRowTwo, int gridColumnTwo, ScrollViewer scrollViewer)
        {
            SetScrollGridHeight(embededGrid);
            SetScrollGridWidth(embededGrid);

            if (gridRowTwo > 6)
            {
                if (gridColumnTwo > 9)
                {
                    ScrollViewerBoth(scrollViewer);
                }
                else
                {
                    ScrollViewerVertical(scrollViewer);
                }
            }
            else
            {
                if (gridColumnTwo > 9)
                {
                    ScrollViewerHorizontal(scrollViewer);
                }
                else
                {
                    ScrollViewerNoOne(scrollViewer);
                }
            }
        }

        /// <summary>
        /// Init scroll bar verticaly and horizontaly
        /// </summary>
        /// <param name="scrollViewer"></param>
        public void ScrollViewerBoth(ScrollViewer scrollViewer)
        {
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
        }

        /// <summary>
        /// Init scroll bar verticaly
        /// </summary>
        /// <param name="scrollViewer"></param>
        public void ScrollViewerVertical(ScrollViewer scrollViewer)
        {
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }

        /// <summary>
        /// Init scroll bar horizontaly
        /// </summary>
        /// <param name="scrollViewer"></param>
        public void ScrollViewerHorizontal(ScrollViewer scrollViewer)
        {
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
        }

        /// <summary>
        /// Init scroll bar
        /// </summary>
        /// <param name="scrollViewer"></param>
        public void ScrollViewerNoOne(ScrollViewer scrollViewer)
        {
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }

        /// <summary>
        /// Set up scroll grid horizontal size
        /// </summary>
        /// <param name="grid"></param>
        public void SetScrollGridWidth(Grid grid)
        {
            foreach (ColumnDefinition column in grid.ColumnDefinitions)
            {
                GridSkins.ColumnHeightTenPercent(column, windowWidth);
            }
        }

        /// <summary>
        /// Set up scroll grid horizontal size
        /// </summary>
        /// <param name="grid"></param>
        public void SetScrollGridWidth(Grid grid, SkinsName skinsName)
        {
            switch (skinsName)
            {
                case SkinsName.HeightTwentyPercent:
                    foreach (ColumnDefinition column in grid.ColumnDefinitions)
                    {
                        GridSkins.ColumnHeightTwentyPercent(column, windowHeight);
                    }
                    break;
                case SkinsName.HeightTenPercent:
                    foreach (ColumnDefinition column in grid.ColumnDefinitions)
                    {
                        GridSkins.ColumnHeightTenPercent(column, windowHeight);
                    }
                    break;
            }
        }

        /// <summary>
        /// Set up scroll grid vertical size
        /// </summary>
        /// <param name="grid"></param>
        public void SetScrollGridHeight(Grid grid)
        {
            foreach (RowDefinition row in grid.RowDefinitions)
            {
                if (row.Equals(grid.RowDefinitions[0]))
                {
                    GridSkins.RowHeightTenPercent(row, windowHeight);
                }
                else
                {
                    GridSkins.RowHeightFifteenPercent(row, windowHeight);
                }
            }
        }

        /// <summary>
        /// Set up scroll grid vertical size
        /// </summary>
        /// <param name="grid"></param>
        public void SetScrollGridHeight(Grid grid, SkinsName skinsName)
        {
            switch (skinsName)
            {
                case SkinsName.HeightFifteenPercent:
                    foreach (RowDefinition row in grid.RowDefinitions)
                    {
                        GridSkins.RowHeightFifteenPercent(row, windowHeight);
                    }
                    break;
                case SkinsName.HeightTwentyPercent:
                    foreach (RowDefinition row in grid.RowDefinitions)
                    {
                        GridSkins.RowHeightTwentyPercent(row, windowHeight);
                    }
                    break;
                case SkinsName.HeightTenPercent:
                    foreach (RowDefinition row in grid.RowDefinitions)
                    {
                        GridSkins.RowHeightTenPercent(row, windowHeight);
                    }
                    break;
            }
        }

        /// <summary>
        /// get all UIElements result of modifiable tab in modify mode
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="data"></param>
        /// <param name="indicationTab"></param>
        /// <returns></returns>
        public List<int> GetUIElements(Grid grid, Data[] data, string[,] indicationTab)
        {
            int rowNb = data.Length;
            int columnNb = data[0].DataText.Length + 1;

            List<Data> gridData = ConvertGridChildrenToDataList(grid, rowNb, columnNb, data[0].StorageId, data[0].DataType);

            List<int> changesList = new List<int>();
            bool trigger = false;
            int i;

            for (i = 0; i < rowNb; i++)
            {
                trigger = false;

                for (int j = 0; j < columnNb - 1; j++)
                {

                    if (gridData[i].DataText[j] != data[i].DataText[j])
                    {
                        trigger = true;
                    }
                }

                if (trigger)
                {
                    changesList.Add(i);
                    data[i].DataText = gridData[i].DataText;
                }
            }

            return changesList;
        }

        /// <summary>
        /// Convert Grid Children To list of data
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public List<Data> ConvertGridChildrenToDataList(Grid grid, int rowNb, int columnNb, int storageId, string[] dataType)
        {
            List<Data> gridData = new List<Data>();
            int j;
            int k;
            int gridIndex = 0;

            string[] dataText;

            for (int i = 0; i < rowNb; i++)
            {
                dataText = new string[columnNb];
                k = 0;

                for (j = 0; j < columnNb; j++)
                {
                    if (j != 0)
                    {
                        if (i == 0)
                        {
                            dataText[k] = (grid.Children[gridIndex] as TextBox).Text;
                        }
                        else
                        {
                            if (dataType[k] == UIElementsName.TextBox.ToString())
                            {

                                dataText[k] = (grid.Children[gridIndex] as TextBox).Text;
                            }
                            else if (dataType[k] == UIElementsName.TextBoxNumber.ToString())
                            {

                                dataText[k] = (grid.Children[gridIndex] as TextBox).Text;
                            }
                            else if (dataType[k] == UIElementsName.DatePicker.ToString())
                            {

                                dataText[k] = (grid.Children[gridIndex] as DatePicker).Text;
                            }
                            else if (dataType[k] == UIElementsName.ComboBox.ToString())
                            {

                                dataText[k] = (grid.Children[gridIndex] as ComboBox).SelectedItem.ToString();
                            }
                        }

                        k++;
                        gridIndex++;
                    }
                    else
                    {
                        if (i != 0)
                        {
                            gridIndex++;
                        }
                    }

                }

                if (i == 0)
                {
                    gridData.Add(new Data(storageId, dataText, dataType, true));
                }
                else
                {
                    gridData.Add(new Data(storageId, dataText, dataType, false));
                }
            }

            return gridData;
        }

        /// <summary>
        /// get all UIElements result of modifiable tab in modify mode
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="storage"></param>
        /// <returns></returns>
        public List<int> GetUIElements(Grid grid, Storage[] storage)
        {
            string text;
            List<int> changesList = new List<int>();

            for (int i = 0; i < storage.Length; i++)
            {
                text = (grid.Children[i] as TextBox).Text;

                if (text != storage[i].Name)
                {
                    changesList.Add(i);
                    storage[i].Name = text;
                }
            }

            return changesList;
        }

        /// <summary>
        /// Add a +1 line if user want to add a new data
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="data"></param>
        /// <param name="optionnalAdd"></param>
        /// <returns></returns>
        public bool OptionnalAdd(Grid grid, Data[] data, Data optionnalAdd)
        {
            int j = data.Length * data[0].DataText.Length + data.Length - 1;
            bool trigger = false;

            for (int i = 0; i < optionnalAdd.DataText.Length; i++)
            {
                if (optionnalAdd.DataType[i] == UIElementsName.TextBox.ToString())
                {

                    optionnalAdd.DataText[i] = (grid.Children[j + i] as TextBox).Text;
                }
                else if (optionnalAdd.DataType[i] == UIElementsName.TextBoxNumber.ToString())
                {

                    optionnalAdd.DataText[i] = (grid.Children[j + i] as TextBox).Text;
                }
                else if (optionnalAdd.DataType[i] == UIElementsName.DatePicker.ToString())
                {

                    optionnalAdd.DataText[i] = (grid.Children[j + i] as DatePicker).Text;
                }
                else if (optionnalAdd.DataType[i] == UIElementsName.ComboBox.ToString())
                {

                    optionnalAdd.DataText[i] = (grid.Children[j + i] as ComboBox).SelectedItem.ToString();
                }

                if (optionnalAdd.DataText[i] != string.Empty)
                {
                    trigger = true;
                }
            }

            return trigger;
        }

        public bool OptionnalAdd(Grid grid, Storage[] storage, Storage optionnalAdd)
        {
            bool trigger = false;

            optionnalAdd.Name = (grid.Children[storage.Length] as TextBox).Text;

            if (optionnalAdd.Name != string.Empty)
            {
                trigger = true;
            }

            return trigger;
        }
    }
}
