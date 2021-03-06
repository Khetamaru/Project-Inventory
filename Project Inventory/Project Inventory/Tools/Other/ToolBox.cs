using Project_Inventory.BDD;
using Project_Inventory.Tools;
using Project_Inventory.Tools.FonctionalityCerters;
using Project_Inventory.Tools.NamesLibraries;
using System;
using System.Collections.Generic;
using System.Reflection;
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
        public double titleBarHeight;

        public ToolBox(double titleBarHeight, Window windowRef)
        {
            wpfScreen = WpfScreen.GetScreenFrom(windowRef);
            this.titleBarHeight = titleBarHeight;

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
                                   SkinName skinName,
                                   SkinLocation skinPosition)
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
                                   SkinName skinName,
                                   SkinLocation skinPosition,
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
                case ImageSizesName.OneForTwoHorizontal:
                    height = 32;
                    width = 64;
                    break;
            }

            temp.Content = new Image
            {
                Source = new BitmapImage(new Uri(@"pack://application:,,,/" + Assembly.GetCallingAssembly().GetName().Name + ";component/Images/" + imagesName.ToString() + ".png", UriKind.Absolute)),
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
                                         SkinName skinName,
                                         SkinLocation skinPosition)
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
                                         SkinName skinName,
                                         SkinLocation skinPosition,
                                         ImageSizesName imageSizesName)
        {
            Button temp = CreateButton(imagesName, skinName, skinPosition, imageSizesName);
            AddOnClickButton(temp, router);

            return temp;
        }

        /// <summary>
        /// Insert a UIElement in a created grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="uIElement"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="uIElementsName"></param>
        /// <param name="skinLocation"></param>
        internal void InsertUIElementInGrid(Grid grid, UIElement uIElement, int row, int column, UIElementsName uIElementsName, SkinLocation skinLocation)
        {
            Grid.SetRow(uIElement, row);
            Grid.SetColumn(uIElement, column);

            switch (uIElementsName)
            {
                case UIElementsName.ComboBox:
                    UIElementSkin.ComboBoxSkinModify(uIElement as ComboBox);
                    break;

                case UIElementsName.DatePicker:
                    UIElementSkin.DatePickerSkinModify(uIElement as DatePicker);
                    break;

                case UIElementsName.TextBox:
                    UIElementSkin.TextBoxSkinModify(uIElement as TextBox, wpfScreen);
                    break;

                case UIElementsName.TextBoxNumber:
                    UIElementSkin.TextBoxNumberSkinModify(uIElement as TextBox, wpfScreen);
                    break;

                case UIElementsName.Button:

                    LoadButtonPosition(uIElement as Button, skinLocation);
                    LoadButtonSkin(uIElement as Button, SkinName.StandartLittleMargin);
                    break;

                case UIElementsName.Label:

                    UIElementSkin.LabelSkinForm(uIElement as Label, (uIElement as Label).Content as string);
                    break;
            }

            StorageViewerSkin.LoadSkinPosition(uIElement, skinLocation);

            grid.Children.Add(uIElement);
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
        private void LoadButtonPosition(Button button, SkinLocation skinPosition)
        {
            switch (skinPosition)
            {
                case SkinLocation.TopLeft:
                    ButtonSkins.TopLeft(button);
                    break;

                case SkinLocation.TopCenter:
                    ButtonSkins.TopCenter(button);
                    break;

                case SkinLocation.TopRight:
                    ButtonSkins.TopRight(button);
                    break;

                case SkinLocation.CenterLeft:
                    ButtonSkins.CenterLeft(button);
                    break;

                case SkinLocation.CenterCenter:
                    ButtonSkins.CenterCenter(button);
                    break;

                case SkinLocation.CenterRight:
                    ButtonSkins.CenterRight(button);
                    break;

                case SkinLocation.BottomLeft:
                    ButtonSkins.BottomLeft(button);
                    break;

                case SkinLocation.BottomCenter:
                    ButtonSkins.BottomCenter(button);
                    break;

                case SkinLocation.BottomRight:
                    ButtonSkins.BottomRight(button);
                    break;
            }
        }

        /// <summary>
        /// Set up button skin
        /// </summary>
        /// <param name="button"></param>
        /// <param name="skinName"></param>
        private void LoadButtonSkin(Button button, SkinName skinName)
        {
            switch (skinName)
            {
                case SkinName.Standart:
                    ButtonSkins.StandartButtonSkin(button);
                    break;

                case SkinName.StandartLittleMargin:
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
                               SkinLocation skinName,
                               SkinSize lengthName)
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
        public void LoadGridLocation(Grid grid, SkinLocation skinName)
        {
            switch (skinName)
            {
                case SkinLocation.TopLeft:
                    GridSkins.TopLeft(grid);
                    break;

                case SkinLocation.StretchLeft:
                    GridSkins.StretchLeft(grid);
                    break;

                case SkinLocation.BottomLeft:
                    GridSkins.BottomLeft(grid);
                    break;

                case SkinLocation.TopStretch:
                    GridSkins.TopStretch(grid);
                    break;

                case SkinLocation.BottomStretch:
                    GridSkins.BottomStretch(grid);
                    break;

                case SkinLocation.StretchStretch:
                    GridSkins.StretchStretch(grid);
                    break;

                case SkinLocation.CenterCenter:
                    GridSkins.CenterCenter(grid);
                    break;

                case SkinLocation.StretchCenter:
                    GridSkins.StretchCenter(grid);
                    break;

                case SkinLocation.CenterStretch:
                    GridSkins.CenterStretch(grid);
                    break;

                case SkinLocation.TopRight:
                    GridSkins.TopRight(grid);
                    break;

                case SkinLocation.StretchRight:
                    GridSkins.StretchRight(grid);
                    break;

                case SkinLocation.BottomRight:
                    GridSkins.BottomRight(grid);
                    break;
            }
        }

        /// <summary>
        /// Get size percentage of the screen
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="lengthName"></param>
        public void LoadGridLength(Grid grid, SkinSize lengthName)
        {
            switch (lengthName)
            {
                case SkinSize.WidthOneTier:
                    GridSkins.WidthOneTier(grid, windowWidth - titleBarHeight);
                    break;

                case SkinSize.WidthFiftyPercent:
                    GridSkins.WidthFiftyPercent(grid, windowWidth - titleBarHeight);
                    break;

                case SkinSize.WidthTwoTier:
                    GridSkins.WidthTwoTier(grid, windowWidth - titleBarHeight);
                    break;

                case SkinSize.HeightOneTier:
                    GridSkins.HeightOneTier(grid, windowHeight - titleBarHeight);
                    break;

                case SkinSize.HeightTwoTier:
                    GridSkins.HeightTwoTier(grid, windowHeight - titleBarHeight);
                    break;

                case SkinSize.HeightTenPercent:
                    GridSkins.HeightTenPercent(grid, windowHeight - titleBarHeight);
                    break;

                case SkinSize.HeightEightPercent:
                    GridSkins.HeightEightPercent(grid, windowHeight - titleBarHeight);
                    break;

                case SkinSize.HeightNintyPercent:
                    GridSkins.HeightNintyPercent(grid, windowHeight - titleBarHeight);
                    break;
            }
        }

        internal UIElementsName GetUIElementType(string column)
        {
            if (column == UIElementsName.DatePicker.ToString())
            {
                return UIElementsName.DatePicker;
            }
            else if (column == UIElementsName.TextBox.ToString())
            {
                return UIElementsName.TextBox;
            }
            else if (column == UIElementsName.TextBoxNumber.ToString())
            {
                return UIElementsName.TextBoxNumber;
            }
            return UIElementsName.None;
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
                                       SkinName skinName,
                                       SkinLocation skinPosition)
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
                                               SkinName skinName,
                                               SkinLocation skinPosition)
        {
            Button button = CreateSwitchButton(content, router, skinName, skinPosition);

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
        public void CreateSwitchButtonsToGridByTab(Grid grid, string[] buttonsTab, SkinName buttonsSkin, SkinLocation skinPosition)
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
        public void CreateSwitchButtonsToGridByTab(Grid grid, string[] buttonsTab, RoutedEventLibrary[] routerTab, SkinName buttonsSkin, SkinLocation skinPosition)
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
                    if (buttonsTab.Length > k)
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
        public void CreateSwitchButtonsToGridByTab(Grid grid, Storage[] buttonsTab, RoutedEventLibrary[] routerTab, SkinName buttonsSkin, SkinLocation skinPosition)
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
                        z = j;
                    }

                    if (buttonsTab.Length > z)
                    {
                        CreateSwitchButtonToGrid(grid, buttonsTab[k].Name, routerTab[k], i, j, buttonsSkin, skinPosition);
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
        public void CreateSwitchButtonsToGridByTab(Grid grid, CustomList[] buttonsTab, RoutedEventLibrary[] routerTab, SkinName buttonsSkin, SkinLocation skinPosition)
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

                    if (buttonsTab.Length >= z)
                    {
                        CreateSwitchButtonToGrid(grid, buttonsTab[k].Name, routerTab[k], i, j, buttonsSkin, skinPosition);
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
        public void CreateLabelToGridByTab(Grid grid, List<User> buttonsTab, SkinLocation skinPosition)
        {
            int i;
            int j;
            int k = 0;
            int z;
            Label label;

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
                        z = j;
                    }

                    if (buttonsTab.Count > z)
                    {
                        label = new Label();
                        label.Content = buttonsTab[k].Name;
                        InsertUIElementInGrid(grid, label, i, j, UIElementsName.Label, skinPosition);
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
        public void CreateSwitchButtonsToGridByTab(Grid grid, string[] buttonsTab, RoutedEventLibrary[] routerTab, SkinName[] buttonsSkin, SkinLocation[] skinPosition)
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
                        if (buttonsTab[k] != string.Empty)
                        {
                            CreateSwitchButtonToGrid(grid, buttonsTab[k], routerTab[k], i, j, buttonsSkin[k], skinPosition[k]);
                        }
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
        public void CreateFormToGridByTab(Grid grid, UIElementsName[] formElements, string[] labels, ComboBoxNames listBoxName, CustomList[] customList, RoutedEventLibrary routedEventLibrary)
        {
            Label label;
            UIElement uIElement;
            int i = 0;

            if (grid.ColumnDefinitions.Count >= 2)
            {
                grid.ColumnDefinitions[0].Width = new GridLength(windowWidth * 0.2, GridUnitType.Pixel);
                grid.ColumnDefinitions[1].Width = new GridLength(windowWidth * 0.8, GridUnitType.Pixel);
            }
            else
            {
                grid.ColumnDefinitions[0].Width = new GridLength(windowWidth, GridUnitType.Pixel);
            }

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
                        KeyPressedEventCenter.KeyPressedEventInjection(routedEventLibrary, KeyPressedName.EnterKey, uIElement);
                        UIElementSkin.TextBoxSkinForm(uIElement as TextBox, wpfScreen);
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as TextBox);

                        break;

                    case (UIElementsName.TextBoxNumber):

                        uIElement = new TextBox();
                        KeyPressedEventCenter.KeyPressedEventInjection(routedEventLibrary, KeyPressedName.EnterKey, uIElement);
                        UIElementSkin.TextBoxSkinForm(uIElement as TextBox, wpfScreen);
                        UIElementSkin.TextBoxNumberSkinForm(uIElement as TextBox, wpfScreen);
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
                                string[] standartStrings = new string[] { "TextBox", "TextBoxNumber", "DatePicker" };

                                string[] comboBoxStrings = new string[standartStrings.Length + customList.Length];

                                for (var j = 0; j < comboBoxStrings.Length; j++)
                                {
                                    if (j < standartStrings.Length)
                                    {
                                        comboBoxStrings[j] = standartStrings[j];
                                    }
                                    else
                                    {
                                        comboBoxStrings[j] = customList[j - standartStrings.Length].Name;
                                    }
                                }

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
        /// Set Up the grid for custom list's options
        /// </summary>
        /// <param name="embededGrid"></param>
        /// <param name="options"></param>
        public void SetUpCustomListGrid(Grid embededGrid, List<ListOption> options, List<Button> deleteButtons, List<Button> upButtons, List<Button> downButtons, RoutedEventLibrary routedEventLibrary)
        {
            TextBox textBoxTemp;
            int i;

            for (i = 0; i < options.Count; i++)
            {
                InsertUIElementInGrid(embededGrid, deleteButtons[i], i, 0, UIElementsName.None, SkinLocation.CenterCenter);

                textBoxTemp = new TextBox();
                textBoxTemp.Text = options[i].Name;
                KeyPressedEventCenter.KeyPressedEventInjection(routedEventLibrary, KeyPressedName.EnterKey, textBoxTemp);
                InsertUIElementInGrid(embededGrid, textBoxTemp, i, 1, UIElementsName.TextBox, SkinLocation.CenterCenter);

                InsertUIElementInGrid(embededGrid, upButtons[i], i, 2, UIElementsName.None, SkinLocation.TopCenter);
                InsertUIElementInGrid(embededGrid, downButtons[i], i, 2, UIElementsName.None, SkinLocation.BottomCenter);
            }
            textBoxTemp = new TextBox();
            KeyPressedEventCenter.KeyPressedEventInjection(routedEventLibrary, KeyPressedName.EnterKey, textBoxTemp);
            InsertUIElementInGrid(embededGrid, textBoxTemp, i, 1, UIElementsName.TextBox, SkinLocation.CenterCenter);
        }

        /// <summary>
        /// Set Up the grid for global storage research
        /// </summary>
        /// <param name="embededGrid"></param>
        /// <param name="datas"></param>
        public void SetUpGlobalResearchGrid(Grid embededGrid, List<Data> datas, List<Button> button, List<Storage> storages)
        {
            int i;
            Label labelStorage;
            Label labelCodeBar;

            for (i = 0; i < datas.Count; i++)
            {
                labelStorage = new Label();

                foreach(Storage storage in storages)
                {
                    if(datas[i].StorageId == storage.id)
                    {
                        labelStorage.Content = storage.Name;
                    }
                }

                InsertUIElementInGrid(embededGrid, labelStorage, i, 0, UIElementsName.Label, SkinLocation.CenterCenter);

                labelCodeBar = new Label();
                labelCodeBar.Content = datas[i].CodeBar;
                InsertUIElementInGrid(embededGrid, labelCodeBar, i, 1, UIElementsName.Label, SkinLocation.CenterCenter);

                InsertUIElementInGrid(embededGrid, button[i], i, 2, UIElementsName.Button, SkinLocation.CenterCenter);
            }
        }

        /// <summary>
        /// Set Up the grid for Data details
        /// </summary>
        /// <param name="embededGrid"></param>
        /// <param name="datas"></param>
        public void SetUpDataDetailsGrid(Grid embededGrid, Data data, List<List<ListOption>> listOptions, List<int> customListIds, Data header)
        {
            int i;
            SkinLocation skinLocation = SkinLocation.CenterCenter;

            for (i = 0; i < data.DataText.Count; i++)
            {
                CreateTabCellToGrid(embededGrid, header.DataText[i], i, 0, skinLocation);

                if (IsCustomList(data.DataType[i]))
                {
                    CreateTabCellToGrid(embededGrid, data.DataText[i], i, 1, skinLocation, GetListOptionInList(listOptions, Int32.Parse(data.DataType[i]), customListIds));
                }
                else
                {
                    CreateTabCellToGrid(embededGrid, data.DataText[i], data.DataType[i], i, 1, skinLocation);
                }
            }

            CreateTabCellToGrid(embededGrid, "Code Bar", i, 0, skinLocation);
            CreateTabCellToGrid(embededGrid, data.CodeBar, UIElementsName.TextBox.ToString(), i, 1, skinLocation);
        }

        /// <summary>
        /// Set Up the grid for Data transfert
        /// </summary>
        /// <param name="embededGrid"></param>
        /// <param name="datas"></param>
        public void SetUpDataTransfertGrid(Grid embededGrid, Data data, List<List<ListOption>> listOptions, List<int> customListIds, Data header)
        {
            int i;
            SkinLocation skinLocation = SkinLocation.CenterCenter;

            for (i = 0; i < data.DataText.Count; i++)
            {
                CreateTabCellToGrid(embededGrid, header.DataText[i], i, 0, skinLocation);

                if (IsCustomList(data.DataType[i]))
                {
                    CreateTabCellToGrid(embededGrid, data.DataText[i], i, 1, skinLocation, GetListOptionInList(listOptions, Int32.Parse(data.DataType[i]), customListIds));
                }
                else
                {
                    CreateTabCellToGrid(embededGrid, data.DataText[i], data.DataType[i], i, 1, skinLocation);
                }
            }

            CreateTabCellToGrid(embededGrid, "Code Bar", i, 0, skinLocation);
            CreateTabCellToGrid(embededGrid, data.CodeBar, UIElementsName.TextBox.ToString(), i, 1, skinLocation);
        }

        /// <summary>
        /// Create UIElements in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="stringTab"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabToGrid(Grid grid, Data[] stringTab, SkinLocation skinPosition, string[] indicTab, List<List<ListOption>> listOptions, List<int> customListIds, List<Button> sortButtonList)
        {
            int i;
            int j;

            int intResult;

            int rowNb = grid.RowDefinitions.Count - 1;
            int columnNb = grid.ColumnDefinitions.Count;

            for (j = 0; j < columnNb - 1; j++)
            {
                Grid.SetRow(sortButtonList[j], 0);
                Grid.SetColumn(sortButtonList[j], j);
                grid.Children.Add(sortButtonList[j]);
            }

            for (i = 0; i < rowNb; i++)
            {
                for (j = 0; j < columnNb; j++)
                {
                    if (stringTab[i].DataText.Count > j)
                    {
                        if (IsCustomList(indicTab[j]) && i != 0)
                        {
                            Int32.TryParse(stringTab[i].DataText[j], out intResult);
                            CreateTabCellToGrid(grid, intResult, i + 1, j, skinPosition, GetListOptionInList(listOptions, Int32.Parse(indicTab[j]), customListIds));
                        }
                        else
                        {
                            CreateTabCellToGrid(grid, stringTab[i].DataText[j], i + 1, j, skinPosition);
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            CreateTabCellToGrid(grid, "Code Bar", i + 1, j, skinPosition);
                        }
                        else
                        {
                            CreateTabCellToGrid(grid, stringTab[i].CodeBar, i + 1, j, skinPosition);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Create UIElements in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="stringTab"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabToGrid(Grid grid, List<Log> logs, List<User> users, SkinLocation skinPosition)
        {
            int i = 0;

            foreach (Log log in logs)
            {
                CreateTabCellToGrid(grid, "(" + GetUser(log.UserId, users).Name + ") : " + log.Message + " /// " + log.Date.ToString(), i, 0, skinPosition);

                i++;
            }
        }

        /// <summary>
        /// Create UIElements in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="stringTab"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabToGrid(Grid grid, List<Bug> bugs, List<User> users, SkinLocation skinPosition, List<Button> deleteButtons, List<Button> handleButtons)
        {
            int i = 0;
            User userTemp = null;

            foreach (Bug bug in bugs)
            {
                foreach(User user in users)
                {
                    if (user.id == bug.UserId)
                    {
                        userTemp = user;
                    }
                }
                InsertUIElementInGrid(grid, deleteButtons[i], i, 0, UIElementsName.Button, SkinLocation.CenterCenter);
                CreateTabCellToGrid(grid, userTemp.Name, i, 1, skinPosition);
                CreateTabCellToGrid(grid, bug.Description, i, 2, skinPosition);

                if(bug.Handled)
                {
                    CreateTabCellToGrid(grid, "En cours de gestion", i, 3, skinPosition);
                }
                else
                {
                    CreateTabCellToGrid(grid, "En attente", i, 3, skinPosition);
                }

                if (!bug.Handled)
                {
                    InsertUIElementInGrid(grid, handleButtons[i], i, 4, UIElementsName.Button, SkinLocation.CenterCenter);
                }

                i++;
            }
        }

        /// <summary>
        /// Get the good user in a list of users
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public User GetUser(int userId, List<User> users)
        {
            foreach(User user in users)
            {
                if(user.id == userId)
                {
                    return user;
                }
            }
            return null;
        }

        /// <summary>
        /// Create UIElements in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="stringTab"></param>
        /// <param name="indicTab"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabToGrid(Grid grid, Data[] stringTab, string[] indicTab, SkinLocation skinPosition, List<Button> buttonList, List<List<ListOption>> listOptions, List<int> customListIds, List<Button> sortButtonList, RoutedEventLibrary routedEventLibrary)
        {
            int i;
            int j;
            int k = 0;

            int rowNb = grid.RowDefinitions.Count - 2;
            int columnNb = grid.ColumnDefinitions.Count - 1;

            string[] addElemString = new string[columnNb];

            for (j = 0; j < columnNb - 1; j++)
            {
                Grid.SetRow(sortButtonList[j], 0);
                Grid.SetColumn(sortButtonList[j], j + 1);
                grid.Children.Add(sortButtonList[j]);
            }

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
                        if (stringTab[i].DataText.Count >= (i + 1) * (j + 1))
                        {
                            CreateHeaderToGrid(grid, stringTab[i].DataText[j], i + 1, j + 1, skinPosition, routedEventLibrary);
                            k++;
                        }
                        else
                        {
                            CreateHeaderToGrid(grid, "Code Bar", i + 1, j + 1, skinPosition);
                        }
                    }
                }
                else
                {
                    Grid.SetRow(buttonList[i], i + 1);
                    Grid.SetColumn(buttonList[i], 0);
                    grid.Children.Add(buttonList[i]);

                    for (j = 0; j < columnNb; j++)
                    {
                        if (stringTab[i].DataText.Count > j)
                        {
                            if (IsCustomList(stringTab[i].DataType[j]))
                            {
                                CreateTabCellToGrid(grid, stringTab[i].DataText[j], i + 1, j + 1, skinPosition, GetListOptionInList(listOptions, Int32.Parse(indicTab[j]), customListIds));
                            }
                            else
                            {
                                CreateTabCellToGrid(grid, stringTab[i].DataText[j], indicTab[j], i + 1, j + 1, skinPosition, routedEventLibrary);
                            }
                            k++;
                        }
                        else
                        {
                            CreateTabCellToGrid(grid, stringTab[i].CodeBar, UIElementsName.TextBox.ToString(), i + 1, j + 1, skinPosition, routedEventLibrary);
                        }
                    }
                }
            }

            for (int l = 0; l < addElemString.Length; l++)
            {
                if (indicTab.Length > l)
                {
                    if (IsCustomList(stringTab[0].DataType[l]))
                    {
                        CreateTabCellToGrid(grid, addElemString[l], rowNb + 2, l + 1, skinPosition, GetListOptionInList(listOptions, Int32.Parse(indicTab[l]), customListIds));
                    }
                    else
                    {
                        CreateTabCellToGrid(grid, addElemString[l], indicTab[l], rowNb + 2, l + 1, skinPosition, routedEventLibrary);
                    }
                }
                else
                {
                    CreateTabCellToGrid(grid, addElemString[l], UIElementsName.TextBox.ToString(), rowNb + 2, l + 1, skinPosition, routedEventLibrary);
                }
            }
        }

        /// <summary>
        /// Find the good list option in a list
        /// </summary>
        /// <param name="listOptions"></param>
        /// <param name="id"></param>
        /// <param name="customListIds"></param>
        /// <returns></returns>
        private List<ListOption> GetListOptionInList(List<List<ListOption>> listOptions, int id, List<int> customListIds)
        {
            int i = 0;

            foreach (List<ListOption> options in listOptions)
            {
                if (customListIds[i] == id)
                {
                    return options;
                }

                i++;
            }

            return null;
        }

        /// <summary>
        /// Create UIElements in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="storageTab"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabToGrid(Grid grid, Storage[] storageTab, SkinLocation skinPosition, RoutedEventLibrary routedEventLibrary)
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
                        CreateHeaderToGrid(grid, storageTab[j + (i * 5)].Name, i, j, skinPosition, routedEventLibrary);
                        k = j;
                    }
                }
            }
            k++;

            if (k >= 5) { k = 0; }

            CreateHeaderToGrid(grid, addElemString, rowNb, k, skinPosition, routedEventLibrary);
        }

        /// <summary>
        /// Create UIElements in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="storageTab"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabToGrid(Grid grid, CustomList[] storageTab, SkinLocation skinPosition, RoutedEventLibrary routedEventLibrary)
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
                        CreateHeaderToGrid(grid, storageTab[j + (i * 5)].Name, i, j, skinPosition, routedEventLibrary);
                        k = j;
                    }
                }
            }
            k++;

            if (k >= 5) { k = 0; }

            CreateHeaderToGrid(grid, addElemString, rowNb, k, skinPosition, routedEventLibrary);
        }

        /// <summary>
        /// Create UIElements in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="storageTab"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabToGrid(Grid grid, List<User> storageTab, SkinLocation skinPosition, RoutedEventLibrary routedEventLibrary)
        {
            int i = storageTab.Count;
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
                    if (storageTab.Count > j + (i * 5))
                    {
                        CreateHeaderToGrid(grid, storageTab[j + (i * 5)].Name, i, j, skinPosition, routedEventLibrary);
                        k = j;
                    }
                }
            }
            k++;

            if (k >= 5) { k = 0; }

            CreateHeaderToGrid(grid, addElemString, rowNb, k, skinPosition, routedEventLibrary);
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
        public void CreateTabCellToGrid(Grid grid, string text, string indication, int row, int column, SkinLocation skinPosition)
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
                UIElementSkin.TextBoxValidationHandler(uiElement);
                uiElement.Text = text;

                UIElementSkin.TextBoxSkinModify(uiElement, wpfScreen);
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

                UIElementSkin.TextBoxNumberSkinModify(uiElement, wpfScreen);
                StorageViewerSkin.LoadSkinPosition(uiElement, skinPosition);

                Grid.SetRow(uiElement, row);
                Grid.SetColumn(uiElement, column);

                grid.Children.Add(uiElement);
            }

            if (indication == UIElementsName.ComboBox.ToString())
            {
                ComboBox uiElement = new ComboBox();
                string[] comboBoxStrings = new string[] { "Selectionnez une option", "TextBox", "TextBoxNumber", "DatePicker", "ComboBox"};
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
        /// Create UIElements in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="text"></param>
        /// <param name="indication"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabCellToGrid(Grid grid, string text, string indication, int row, int column, SkinLocation skinPosition, RoutedEventLibrary routedEventLibrary)
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
                UIElementSkin.TextBoxValidationHandler(uiElement);
                uiElement.Text = text;
                KeyPressedEventCenter.KeyPressedEventInjection(routedEventLibrary, KeyPressedName.EnterKey, uiElement);

                UIElementSkin.TextBoxSkinModify(uiElement, wpfScreen);
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
                KeyPressedEventCenter.KeyPressedEventInjection(routedEventLibrary, KeyPressedName.EnterKey, uiElement);

                UIElementSkin.TextBoxNumberSkinModify(uiElement, wpfScreen);
                StorageViewerSkin.LoadSkinPosition(uiElement, skinPosition);

                Grid.SetRow(uiElement, row);
                Grid.SetColumn(uiElement, column);

                grid.Children.Add(uiElement);
            }

            if (indication == UIElementsName.ComboBox.ToString())
            {
                ComboBox uiElement = new ComboBox();
                string[] comboBoxStrings = new string[] { "Selectionnez une option", "TextBox", "TextBoxNumber", "DatePicker", "ComboBox" };
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
        /// Create UIElements in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="text"></param>
        /// <param name="indication"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabCellToGrid(Grid grid, string text, int row, int column, SkinLocation skinPosition, List<ListOption> listOptions)
        {
            int intResult;
            ComboBox uiElement = new ComboBox();
            UIElementSkin.ComboBoxSkinForm(uiElement, listOptions);

            if (Int32.TryParse(text, out intResult))
            {
                foreach(ListOption option in listOptions)
                {
                    if (option.id == intResult)
                    {
                        uiElement.SelectedItem = uiElement.Items[option.Index + 1];
                    }
                }
            }
            else
            {
                if (uiElement.Items.Count > 0)
                {
                    uiElement.SelectedItem = uiElement.Items[0];
                }
            }

            UIElementSkin.ComboBoxSkinModify(uiElement);
            StorageViewerSkin.LoadSkinPosition(uiElement, skinPosition);

            // insert potential clickEvent

            Grid.SetRow(uiElement, row);
            Grid.SetColumn(uiElement, column);

            grid.Children.Add(uiElement);
        }

        /// <summary>
        /// Create Labels in the scroll grid for viewer mode
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabCellToGrid(Grid grid, string text, int row, int column, SkinLocation skinPosition)
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
        /// Create Labels in the scroll grid for viewer mode
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="listOptionId"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="skinPosition"></param>
        public void CreateTabCellToGrid(Grid grid, int listOptionId, int row, int column, SkinLocation skinPosition, List<ListOption> listOptions)
        {
            Label label = new Label();

            foreach(ListOption option in listOptions)
            {
                if (option.id == listOptionId)
                {
                    label.Content = option.Name;
                }
            }

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
        public void CreateHeaderToGrid(Grid grid, string text, int row, int column, SkinLocation skinPosition)
        {
            TextBox textbox = new TextBox();
            textbox.Text = text;

            UIElementSkin.TextBoxSkinModify(textbox, wpfScreen);
            StorageViewerSkin.LoadSkinPosition(textbox, skinPosition);

            Grid.SetRow(textbox, row);
            Grid.SetColumn(textbox, column);

            grid.Children.Add(textbox);
        }

        /// <summary>
        /// Create the header line in the scroll grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="skinPosition"></param>
        public void CreateHeaderToGrid(Grid grid, string text, int row, int column, SkinLocation skinPosition, RoutedEventLibrary routedEventLibrary)
        {
            TextBox textbox = new TextBox();
            textbox.Text = text;
            KeyPressedEventCenter.KeyPressedEventInjection(routedEventLibrary, KeyPressedName.EnterKey, textbox);

            UIElementSkin.TextBoxSkinModify(textbox, wpfScreen);
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
        public void ButtonPlacer(Grid grid, int tabLength, int widthLimit, SkinLocation skinName, SkinSize lengthName)
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

        /// <summary>
        /// Set up Scrollable Grid for Custom List's Options
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="embededGrid"></param>
        /// <param name="gridSkin"></param>
        /// <param name="skinHeight"></param>
        /// <param name="options"></param>
        public void CustomListViewer(Grid grid, Grid embededGrid, List<ListOption> options, List<Button> deleteButtons, List<Button> upButtons, List<Button> downButtons, RoutedEventLibrary routedEventLibrary)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpNewGrid(embededGrid, options.Count + 1, SkinLocation.TopLeft);

            ScrollFormInit(embededGrid, options.Count + 1, scrollViewer);

            ScrollGridInit(embededGrid);

            SetUpCustomListGrid(embededGrid, options, deleteButtons, upButtons, downButtons, routedEventLibrary);

            EmbedScrollableGrid(grid, embededGrid, scrollViewer);
        }

        /// <summary>
        /// Set up Scrollable Grid for global Storage research page
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="embededGrid"></param>
        /// <param name="datas"></param>
        public void GlobalStorageResearchGrid(Grid grid, Grid embededGrid, List<Data> datas, List<Button> button, List<Storage> storages)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpNewGrid(embededGrid, datas.Count, 3, SkinLocation.TopLeft);

            ScrollFormInit(embededGrid, datas.Count, scrollViewer);

            ScrollGridInit(embededGrid);

            SetUpGlobalResearchGrid(embededGrid, datas, button, storages);

            EmbedScrollableGrid(grid, embededGrid, scrollViewer);
        }

        /// <summary>
        /// Set up Scrollable Grid for Data details page
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="embededGrid"></param>
        /// <param name="datas"></param>
        public void GlobalDataDetailsGrid(Grid grid, Grid embededGrid, Data data, List<List<ListOption>> listOptions, List<int> customListIds, Data header)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpNewGrid(embededGrid, data.DataText.Count + 1, 2, SkinLocation.TopLeft);

            ScrollFormInit(embededGrid, data.DataText.Count + 1, scrollViewer);

            ScrollGridInit(embededGrid);

            SetUpDataDetailsGrid(embededGrid, data, listOptions, customListIds, header);

            EmbedScrollableGrid(grid, embededGrid, scrollViewer);
        }

        /// <summary>
        /// Set up Scrollable Grid for Data transfert page
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="embededGrid"></param>
        /// <param name="datas"></param>
        public void DataTransfertGrid(Grid grid, out Grid gridLeft, out Grid gridRight, Data data, Data newData, Data header, Data newHeader, List<List<ListOption>> listOptions, List<int> customListIds)
        {
            ScrollViewer scrollViewerLeft = new ScrollViewer();
            ScrollViewer scrollViewerRight = new ScrollViewer();

            gridLeft = new Grid();
            gridRight = new Grid();

            SetUpNewGrid(gridLeft,  data.DataText.Count + 1,    2, SkinLocation.TopLeft);
            SetUpNewGrid(gridRight, newData.DataText.Count + 1, 2, SkinLocation.TopRight);

            LoadGridLength(gridLeft,  SkinSize.WidthFiftyPercent);
            LoadGridLength(gridRight, SkinSize.WidthFiftyPercent);

            ScrollFormInit(gridLeft,  data.DataText.Count + 1,    scrollViewerLeft);
            ScrollFormInit(gridRight, newData.DataText.Count + 1, scrollViewerRight);

            scrollViewerLeft.Width = windowWidth / 2;
            scrollViewerRight.Width = windowWidth / 2;

            scrollViewerLeft.HorizontalAlignment = HorizontalAlignment.Left;
            scrollViewerRight.HorizontalAlignment = HorizontalAlignment.Right;

            SetUpDataTransfertGrid(gridLeft,  data,    listOptions, customListIds, header);
            SetUpDataTransfertGrid(gridRight, newData, listOptions, customListIds, newHeader);

            EmbedScrollableGrid(grid, gridLeft,  scrollViewerLeft);
            EmbedScrollableGrid(grid, gridRight, scrollViewerRight);
        }

        public int GetDataTextColumn(RequestCenter requestCenter, int listOptionId)
        {
            return JsonCenter.GetListOption(requestCenter, listOptionId).Index;
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
        public void CreateScrollableForm(Grid grid, Grid embededGrid, int gridRowOne, int gridColumnOne, int gridRowTwo, int gridColumnTwo, SkinLocation gridSkin, SkinSize skinHeight, UIElementsName[] stringTab, string[] indicTab, ComboBoxNames listBoxNames, CustomList[] customList, RoutedEventLibrary routedEventLibrary)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpGrid(grid, gridRowOne, gridColumnOne, gridSkin, skinHeight);

            SetUpNewGrid(embededGrid, gridRowTwo, gridColumnTwo, SkinLocation.TopLeft);

            ScrollFormInit(embededGrid, gridRowTwo, scrollViewer);

            CreateFormToGridByTab(embededGrid, stringTab, indicTab, listBoxNames, customList, routedEventLibrary);

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

            if (gridRowTwo > windowHeight / (wpfScreen.PrimaryScreenSizeHeight() / 5))
            {
                ScrollViewerVertical(scrollViewer);
            }
            else
            {
                ScrollViewerNoOne(scrollViewer);
            }
        }

        /// <summary>
        /// Init scroll form sizes personalisly
        /// </summary>
        /// <param name="embededGrid"></param>
        /// <param name="gridRowTwo"></param>
        /// <param name="scrollViewer"></param>
        public void ScrollFormInit(Grid embededGrid, int gridRowTwo, int limit, ScrollViewer scrollViewer)
        {
            SetScrollFormHeight(embededGrid);

            if (gridRowTwo > limit)
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
                GridSkins.RowHeightFifteenPercent(row, wpfScreen.PrimaryScreenSizeHeight());
            }
        }

        public void SetScrollFormWidth(Grid grid)
        {
            foreach (ColumnDefinition column in grid.ColumnDefinitions)
            {
                GridSkins.ColumnHeightTier(column, wpfScreen.PrimaryScreenSizeWidth());
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
        public void CreateScrollableGrid(Grid grid, Grid embededGrid, int gridRowOne, int gridColumnOne, int gridRowTwo, int gridColumnTwo, SkinLocation gridSkin, SkinSize skinHeight, SkinLocation tabPos, Data[] stringTab, string[] indicTab, List<List<ListOption>> listOptions, List<int> customListIds, List<Button> sortButtonList)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpGrid(grid, gridRowOne, gridColumnOne, gridSkin, skinHeight);

            SetUpNewGrid(embededGrid, gridRowTwo, gridColumnTwo, SkinLocation.TopStretch);

            ScrollGridInit(embededGrid, gridRowTwo, gridColumnTwo, scrollViewer);

            CreateTabToGrid(embededGrid, stringTab, tabPos, indicTab, listOptions, customListIds, sortButtonList);

            EmbedScrollableGrid(grid, embededGrid, scrollViewer);
        }

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
        public void CreateScrollGrid(Grid grid, Grid embededGrid, int gridRowOne, int gridColumnOne, int gridRowTwo, int gridColumnTwo, SkinLocation gridSkin, SkinSize skinHeight, SkinLocation tabPos, List<Log> logs, int rowLimit, List<User> users)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpGrid(grid, gridRowOne, gridColumnOne, gridSkin, skinHeight);

            SetUpNewGrid(embededGrid, gridRowTwo, gridColumnTwo, SkinLocation.TopStretch);

            ScrollGridInit(embededGrid, gridRowTwo, gridColumnTwo, scrollViewer, rowLimit);

            CreateTabToGrid(embededGrid, logs, users, tabPos);

            EmbedScrollableGrid(grid, embededGrid, scrollViewer);
        }

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
        public void CreateScrollGrid(Grid grid, Grid embededGrid, int gridRowOne, int gridColumnOne, int gridRowTwo, int gridColumnTwo, SkinLocation gridSkin, SkinSize skinHeight, SkinLocation tabPos, List<Bug> bugs, int rowLimit, List<User> users, List<Button> deleteButtons, List<Button> handleButtons)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpGrid(grid, gridRowOne, gridColumnOne, gridSkin, skinHeight);

            SetUpNewGrid(embededGrid, gridRowTwo, gridColumnTwo, SkinLocation.TopStretch);

            ScrollGridInit(embededGrid, gridRowTwo, gridColumnTwo, scrollViewer, rowLimit);

            CreateTabToGrid(embededGrid, bugs, users, tabPos, deleteButtons, handleButtons);

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
        public void CreateScrollableGridModfiable(Grid grid, Grid embededGrid, int gridRowOne, int gridColumnOne, int gridRowTwo, int gridColumnTwo, SkinLocation gridSkin, SkinSize skinHeight, SkinLocation tabPos, Data[] stringTab, string[] indicTab, List<Button> buttonList, List<List<ListOption>> listOptions, List<int> customListIds, List<Button> sortButtonList, RoutedEventLibrary routedEventLibrary)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpGrid(grid, gridRowOne, gridColumnOne, gridSkin, skinHeight);

            SetUpNewGrid(embededGrid, gridRowTwo, gridColumnTwo, SkinLocation.StretchStretch);

            ScrollGridInit(embededGrid, gridRowTwo, gridColumnTwo, scrollViewer);

            CreateTabToGrid(embededGrid, stringTab, indicTab, tabPos, buttonList, listOptions, customListIds, sortButtonList, routedEventLibrary);

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
                           SkinLocation skinName)
        {
            CreateRowsInGrid(grid, rowNb);
            CreateColumnsInGrid(grid, columnNb);

            LoadGridLocation(grid, skinName);
        }

        /// <summary>
        /// Set up the grid that embed the scroll bar
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="optionNumber"></param>
        /// <param name="columnNb"></param>
        /// <param name="skinName"></param>
        /// <param name="lengthName"></param>
        public void SetUpNewGrid(Grid grid,
                           int optionNumber,
                           SkinLocation skinName)
        {
            CreateRowsInGrid(grid, optionNumber);
            CreateColumnsInGrid(grid, 3);

            LoadGridLocation(grid, skinName);
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

            if (gridRowTwo > windowHeight / (wpfScreen.PrimaryScreenSizeHeight() / 7))
            {
                if (gridColumnTwo > windowWidth / (wpfScreen.PrimaryScreenSizeWidth() / 9))
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
                if (gridColumnTwo > windowWidth / (wpfScreen.PrimaryScreenSizeWidth() / 9))
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
        /// Init the scroll grid
        /// </summary>
        /// <param name="embededGrid"></param>
        /// <param name="gridRowTwo"></param>
        /// <param name="gridColumnTwo"></param>
        /// <param name="scrollViewer"></param>
        public void ScrollGridInit(Grid embededGrid, int gridRowTwo, int gridColumnTwo, ScrollViewer scrollViewer, int rowLimit)
        {
            SetScrollGridHeight(embededGrid);

            if (gridRowTwo > rowLimit)
            {
                ScrollViewerVertical(scrollViewer);
            }
            else
            {
                ScrollViewerNoOne(scrollViewer);
            }
        }

        /// <summary>
        /// Init the scroll grid
        /// </summary>
        /// <param name="embededGrid"></param>
        /// <param name="gridRowTwo"></param>
        /// <param name="gridColumnTwo"></param>
        /// <param name="scrollViewer"></param>
        public void ScrollGridInit(Grid embededGrid)
        {
            SetCustomListScrollGridHeight(embededGrid);
            SetCustomListScrollGridWidth(embededGrid);
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
                GridSkins.ColumnHeightTenPercent(column, wpfScreen.PrimaryScreenSizeWidth());
            }
        }

        /// <summary>
        /// Set up scroll grid horizontal size
        /// </summary>
        /// <param name="grid"></param>
        public void SetCustomListScrollGridWidth(Grid grid)
        {
            foreach (ColumnDefinition column in grid.ColumnDefinitions)
            {
                GridSkins.ColumnHeightTier(column, wpfScreen.PrimaryScreenSizeWidth());
            }
        }

        /// <summary>
        /// Set up scroll grid horizontal size
        /// </summary>
        /// <param name="grid"></param>
        public void SetScrollGridWidth(Grid grid, SkinSize skinsName)
        {
            switch (skinsName)
            {
                case SkinSize.HeightTwentyPercent:
                    foreach (ColumnDefinition column in grid.ColumnDefinitions)
                    {
                        GridSkins.ColumnHeightTwentyPercent(column, wpfScreen.PrimaryScreenSizeWidth());
                    }
                    break;
                case SkinSize.HeightTenPercent:
                    foreach (ColumnDefinition column in grid.ColumnDefinitions)
                    {
                        GridSkins.ColumnHeightTenPercent(column, wpfScreen.PrimaryScreenSizeWidth());
                    }
                    break;
            }
        }

        /// <summary>
        /// Set up scroll grid vertical size
        /// </summary>
        /// <param name="grid"></param>
        public void SetCustomListScrollGridHeight(Grid grid)
        {
            foreach (RowDefinition row in grid.RowDefinitions)
            {
                GridSkins.RowHeightFifteenPercent(row, wpfScreen.PrimaryScreenSizeHeight());
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
                    GridSkins.RowHeightTenPercent(row, wpfScreen.PrimaryScreenSizeHeight());
                }
                else
                {
                    GridSkins.RowHeightFifteenPercent(row, wpfScreen.PrimaryScreenSizeHeight());
                }
            }
        }

        /// <summary>
        /// Set up scroll grid vertical size
        /// </summary>
        /// <param name="grid"></param>
        public void SetScrollGridHeight(Grid grid, SkinSize skinsName)
        {
            switch (skinsName)
            {
                case SkinSize.HeightFifteenPercent:
                    foreach (RowDefinition row in grid.RowDefinitions)
                    {
                        GridSkins.RowHeightFifteenPercent(row, wpfScreen.PrimaryScreenSizeHeight());
                    }
                    break;
                case SkinSize.HeightTwentyPercent:
                    foreach (RowDefinition row in grid.RowDefinitions)
                    {
                        GridSkins.RowHeightTwentyPercent(row, wpfScreen.PrimaryScreenSizeHeight());
                    }
                    break;
                case SkinSize.HeightTenPercent:
                    foreach (RowDefinition row in grid.RowDefinitions)
                    {
                        GridSkins.RowHeightTenPercent(row, wpfScreen.PrimaryScreenSizeHeight());
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
        public List<int> GetUIElements(List<UIElement> uiElementList, Data[] data, out Data optionalAdd, List<List<ListOption>> listOptions)
        {
            int rowNb = data.Length;
            int columnNb = data[0].DataText.Count + 1;

            List<Data> gridData = ConvertGridChildrenToDataList(uiElementList, rowNb, columnNb, data[0].StorageId, data[0].DataType, out optionalAdd, listOptions);

            List<int> changesList = new List<int>();
            bool trigger;
            int i;

            for (i = 0; i < rowNb; i++)
            {
                trigger = false;

                for (int j = 0; j < columnNb - 1; j++)
                {

                    if (gridData[i].DataText[j] != data[i].DataText[j])
                    {
                        if (IsCustomList(data[i].DataType[j]))
                        {
                            gridData[i].DataText[j] = IsDataAListOption(gridData[i].DataText[j], data[0].DataType[j], listOptions).ToString();

                            if (gridData[i].DataText[j] != data[i].DataText[j])
                            {
                                trigger = true;
                            }
                        }
                        else
                        {
                            trigger = true;
                        }
                    }
                }

                if (gridData[i].CodeBar != data[i].CodeBar && i != 0)
                {
                    trigger = true;
                }

                if (trigger)
                {
                    changesList.Add(i);
                    data[i].DataText = gridData[i].DataText;
                    data[i].CodeBar = gridData[i].CodeBar;
                }
            }

            return changesList;
        }

        /// <summary>
        /// get all UIElements result
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="data"></param>
        /// <param name="indicationTab"></param>
        /// <returns></returns>
        public bool GetUIElements(List<UIElement> uiElementList, Data data, out Data output, List<List<ListOption>> listOptions)
        {
            int rowNb = data.DataText.Count;

            output = ConvertGridChildrenToDataList(uiElementList, rowNb, data.StorageId, data.DataType, listOptions);
            output.id = data.id;

            if (output.DataText != data.DataText || output.CodeBar != data.CodeBar)
            {
                return true;
            }

            return false;
        }

        public bool IsCustomList(string dataType)
        {
            if (dataType == UIElementsName.ComboBox.ToString()) 
            { 
                return false; 
            }
            else if (dataType == UIElementsName.DatePicker.ToString()) 
            { 
                return false; 
            }
            else if(dataType == UIElementsName.TextBox.ToString()) 
            { 
                return false; 
            }
            else if(dataType == UIElementsName.TextBoxNumber.ToString()) 
            { 
                return false; 
            }
            else 
            { 
                return true; 
            }
        }

        /// <summary>
        /// Catch Id Value of ListOption from Name Value
        /// </summary>
        /// <param name="dataListOptionId"></param>
        /// <param name="dataTextListOption"></param>
        /// <param name="dataCustomList"></param>
        /// <param name="listOptions"></param>
        /// <returns></returns>
        public int IsDataAListOption(string dataTextListOption, string dataCustomList, List<List<ListOption>> listOptions)
        {
            int dataCustomListId = Int32.Parse(dataCustomList);
            List<ListOption> list = new List<ListOption>();

            foreach(List<ListOption> options in listOptions)
            {
                if(options[0].CustomListId == dataCustomListId)
                {
                    list = options;
                }
            }

            foreach(ListOption option in list)
            {
                if (option.id.ToString() == dataTextListOption)
                {
                    return option.id;
                }
            }

            return 42;
        }

        /// <summary>
        /// get all UIElements result of modifiable tab in modify mode
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="storage"></param>
        /// <returns></returns>
        public List<int> GetUIElements(List<UIElement> uiElementList, Storage[] storage, out Storage optionalStorage)
        {
            List<Storage> gridStorage = ConvertGridChildrenToStorageList(uiElementList, storage.Length, out optionalStorage);

            List<int> changesList = new List<int>();

            for (int i = 0; i < storage.Length; i++)
            {
                if (gridStorage[i].Name != storage[i].Name)
                {
                    changesList.Add(i);
                    storage[i].Name = gridStorage[i].Name;
                }
            }

            return changesList;
        }

        /// <summary>
        /// get all UIElements result of modifiable tab in modify mode
        /// </summary>
        /// <param name="uiElementList"></param>
        /// <param name="customList"></param>
        /// <returns></returns>
        public List<int> GetUIElements(List<UIElement> uiElementList, CustomList[] customList, out CustomList optionalCustomList)
        {
            List<CustomList> gridCustomList = ConvertGridChildrenToCustomListList(uiElementList, customList.Length, out optionalCustomList);

            List<int> changesList = new List<int>();

            for (int i = 0; i < customList.Length; i++)
            {
                if (gridCustomList[i].Name != customList[i].Name)
                {
                    changesList.Add(i);
                    customList[i].Name = gridCustomList[i].Name;
                }
            }

            return changesList;
        }

        /// <summary>
        /// get all UIElements result of modifiable tab in modify mode
        /// </summary>
        /// <param name="uiElementList"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<int> GetUIElements(List<UIElement> uiElementList, List<User> user, out User optionalUser)
        {
            List<User> gridUser = ConvertGridChildrenToUserList(uiElementList, user.Count, out optionalUser);

            List<int> changesList = new List<int>();

            for (int i = 0; i < user.Count; i++)
            {
                if (gridUser[i].Name != user[i].Name)
                {
                    changesList.Add(i);
                    user[i].Name = gridUser[i].Name;
                }
            }

            return changesList;
        }

        /// <summary>
        /// get all UIElements result of custom list details
        /// </summary>
        /// <param name="uiElementList"></param>
        /// <param name="listOption"></param>
        /// <returns></returns>
        public bool GetUIElements(List<UIElement> uiElementList, List<ListOption> listOption, out string optionalListOption)
        {
            List<string> gridListOption = ConvertGridChildrenToCustomList(uiElementList, listOption.Count, out optionalListOption);

            bool trigger = false;

            for (int i = 0; i < gridListOption.Count; i++)
            {
                if (gridListOption[i] != listOption[i].Name)
                {
                    listOption[i].Name = gridListOption[i];
                    trigger = true;
                }
            }

            return trigger;
        }

        /// <summary>
        /// Convert Grid Children To list of data
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public List<Data> ConvertGridChildrenToDataList(List<UIElement> uIElements, int rowNb, int columnNb, int storageId, List<string> dataType, out Data optionalData, List<List<ListOption>> listOptions)
        {
            List<Data> gridData = new List<Data>();
            int j;
            int gridIndex = 0;

            optionalData = null;
            bool trigger = false;

            List<string> dataText;
            List<string> optionalDataText;
            string codeBar = string.Empty;

            optionalDataText = new List<string>();

            for (int i = 0; i < rowNb; i++)
            {
                dataText = new List<string>();

                for (j = 0 ; j < (uIElements.Count / (rowNb + 1)) ; j++)
                {
                    if (j < (uIElements.Count / (rowNb + 1)) - 1)
                    {
                        if (i == 0)
                        {
                            dataText.Add((uIElements[gridIndex] as TextBox).Text);
                        }
                        else
                        {
                            TypeCatch(dataType[j], uIElements[gridIndex], dataText, listOptions);
                        }
                    }
                    else
                    {
                        if (i != 0)
                        {
                            codeBar = (uIElements[gridIndex] as TextBox).Text;
                        }
                    }

                    gridIndex++;
                }

                if (i == 0)
                {
                    gridData.Add(new Data(storageId, dataText, dataType, true));
                }
                else
                {
                    gridData.Add(new Data(storageId, dataText, dataType, false, codeBar));
                }
            }

            if (uIElements[gridIndex] != null)
            {
                for (j = 0; j < (uIElements.Count / (rowNb + 1)); j++)
                {
                    if(j < (uIElements.Count / (rowNb + 1)) - 1)
                    {
                        TypeCatchOptional(dataType[j], uIElements[gridIndex], optionalDataText, listOptions);
                    }
                    else
                    {
                        codeBar = (uIElements[gridIndex] as TextBox).Text;
                    }

                    gridIndex++;
                }

                optionalData = new Data(storageId, optionalDataText, dataType, false, codeBar);

                for (j = 0; j < optionalData.DataType.Count; j++)
                {
                    if (optionalData.DataText[j] != string.Empty)
                    {
                        trigger = true;
                    }
                }

                if (!trigger)
                {
                    optionalData = null;
                }
            }

            return gridData;
        }

        /// <summary>
        /// Convert Grid children to Data
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public Data ConvertGridChildrenToDataList(List<UIElement> uIElements, int rowNb, int storageId, List<string> dataType, List<List<ListOption>> listOptions)
        {
            int j;

            List<string> dataText;
            string codeBar = string.Empty;

            dataText = new List<string>();

            for (j = 0; j < uIElements.Count; j++)
            {
                if (j < uIElements.Count - 1)
                {
                    TypeCatch(dataType[j], uIElements[j], dataText, listOptions);
                }
                else
                {
                    codeBar = (uIElements[j] as TextBox).Text;
                }
            }

            return new Data(storageId, dataText, dataType, false, codeBar);
        }

        /// <summary>
        /// Convert Grid Children To list of storage
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public List<Storage> ConvertGridChildrenToStorageList(List<UIElement> uIElements, int rowNb, out Storage optionalStorage)
        {
            List<Storage> gridStorage = new List<Storage>();
            int gridIndex = 0;

            optionalStorage = null;

            string text = string.Empty;

            for (int i = 0; i < rowNb; i++)
            {
                text = (uIElements[gridIndex] as TextBox).Text;

                gridIndex++;

                gridStorage.Add(new Storage(text));
            }

            text = string.Empty;

            if (uIElements[gridIndex] != null)
            {
                text = (uIElements[gridIndex] as TextBox).Text;

                optionalStorage = new Storage(text);

                if (text == string.Empty)
                {
                    optionalStorage = null;
                }
            }

            return gridStorage;
        }

        /// <summary>
        /// Convert Grid Children To list of custom list
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public List<CustomList> ConvertGridChildrenToCustomListList(List<UIElement> uIElements, int rowNb, out CustomList optionalAdd)
        {
            List<CustomList> gridStorage = new List<CustomList>();
            int gridIndex = 0;

            optionalAdd = null;

            string text = string.Empty;

            for (int i = 0; i < rowNb; i++)
            {
                text = (uIElements[gridIndex] as TextBox).Text;

                gridIndex++;

                gridStorage.Add(new CustomList(text));
            }

            text = string.Empty;

            if (uIElements[gridIndex] != null)
            {
                text = (uIElements[gridIndex] as TextBox).Text;

                optionalAdd = new CustomList(text);

                if (text == string.Empty)
                {
                    optionalAdd = null;
                }
            }

            return gridStorage;
        }

        /// <summary>
        /// Convert Grid Children To list of custom list
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public List<User> ConvertGridChildrenToUserList(List<UIElement> uIElements, int rowNb, out User optionalAdd)
        {
            List<User> gridStorage = new List<User>();
            int gridIndex = 0;

            optionalAdd = null;

            string text = string.Empty;

            for (int i = 0; i < rowNb; i++)
            {
                text = (uIElements[gridIndex] as TextBox).Text;

                gridIndex++;

                gridStorage.Add(new User(text, 0, true));
            }

            text = string.Empty;

            if (uIElements[gridIndex] != null)
            {
                text = (uIElements[gridIndex] as TextBox).Text;

                optionalAdd = new User(text, 0, true);

                if (text == string.Empty)
                {
                    optionalAdd = null;
                }
            }

            return gridStorage;
        }

        /// <summary>
        /// Convert Grid Children To custom list
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public List<string> ConvertGridChildrenToCustomList(List<UIElement> uIElements, int rowNb, out string optionalAdd)
        {
            List<string> listOption = new List<string>();
            int gridIndex = 0;

            optionalAdd = string.Empty;

            string text;

            for (int i = 0; i < rowNb; i++)
            {
                text = (uIElements[gridIndex] as TextBox).Text;

                gridIndex++;

                listOption.Add(text);
            }

            if (uIElements[gridIndex] != null)
            {
                text = (uIElements[gridIndex] as TextBox).Text;

                optionalAdd = text;

                if (text == string.Empty)
                {
                    optionalAdd = string.Empty;
                }
            }

            return listOption;
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
            int j = data.Length * data[0].DataType.Count + data.Length - 1;
            bool trigger = false;

            for (int i = 0; i < optionnalAdd.DataType.Count; i++)
            {
                if (optionnalAdd.DataType[i] == UIElementsName.TextBox.ToString())
                {

                    optionnalAdd.DataText.Add((grid.Children[j + i] as TextBox).Text);
                }
                else if (optionnalAdd.DataType[i] == UIElementsName.TextBoxNumber.ToString())
                {

                    optionnalAdd.DataText.Add((grid.Children[j + i] as TextBox).Text);
                }
                else if (optionnalAdd.DataType[i] == UIElementsName.DatePicker.ToString())
                {

                    optionnalAdd.DataText.Add((grid.Children[j + i] as DatePicker).Text);
                }
                else if (optionnalAdd.DataType[i] == UIElementsName.ComboBox.ToString())
                {
                    if ((grid.Children[j + i] as ComboBox).SelectedItem != (grid.Children[j + i] as ComboBox).Items[0])
                    {
                        optionnalAdd.DataText.Add((grid.Children[j + i] as ComboBox).SelectedItem.ToString());
                    }
                    else
                    {
                        optionnalAdd.DataText.Add(string.Empty);
                    }
                }

                if (optionnalAdd.DataText[i] != string.Empty)
                {
                    trigger = true;
                }
            }

            return trigger;
        }

        /// <summary>
        /// Add a +1 line if user want to add a new storage
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="storage"></param>
        /// <param name="optionnalAdd"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add a +1 line if user want to add a new custom list
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="customList"></param>
        /// <param name="optionnalAdd"></param>
        /// <returns></returns>
        public bool OptionnalAdd(Grid grid, CustomList[] customList, CustomList optionnalAdd)
        {
            bool trigger = false;

            optionnalAdd.Name = (grid.Children[customList.Length] as TextBox).Text;

            if (optionnalAdd.Name != string.Empty)
            {
                trigger = true;
            }

            return trigger;
        }

        /// <summary>
        /// catch string in every type of UIElement
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="uIElement"></param>
        /// <param name="dataText"></param>
        public void TypeCatch(string dataType, UIElement uIElement, List<string> dataText, List<List<ListOption>> listOptions)
        {
            if (dataType == UIElementsName.TextBox.ToString())
            {

                dataText.Add((uIElement as TextBox).Text);
            }
            else if (dataType == UIElementsName.TextBoxNumber.ToString())
            {

                dataText.Add((uIElement as TextBox).Text);
            }
            else if (dataType == UIElementsName.DatePicker.ToString())
            {

                dataText.Add((uIElement as DatePicker).Text);
            }
            else if (dataType == UIElementsName.ComboBox.ToString())
            {
                if ((uIElement as ComboBox).SelectedItem != (uIElement as ComboBox).Items[0])
                {
                    dataText.Add((uIElement as ComboBox).SelectedItem.ToString());
                }
                else
                {
                    dataText.Add(string.Empty);
                }
            }
            else
            {
                if ((uIElement as ComboBox).SelectedItem != (uIElement as ComboBox).Items[0])
                {
                    List<ListOption> list = new List<ListOption>();

                    foreach (List<ListOption> options in listOptions)
                    {
                        if (options[0].CustomListId == Int32.Parse(dataType))
                        {
                            list = options;
                        }
                    }

                    foreach(ListOption option in list)
                    {
                        if ((uIElement as ComboBox).SelectedItem.ToString() == option.Name) {

                            dataText.Add(option.id.ToString());
                        }
                    }
                }
                else
                {
                    dataText.Add(string.Empty);
                }
            }
        }

        /// <summary>
        /// catch string in every type of UIElement
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="uIElement"></param>
        /// <param name="dataText"></param>
        public void TypeCatchOptional(string dataType, UIElement uIElement, List<string> dataText, List<List<ListOption>> listOptions)
        {
            if (dataType == UIElementsName.TextBox.ToString())
            {
                dataText.Add((uIElement as TextBox).Text);
            }
            else if (dataType == UIElementsName.TextBoxNumber.ToString())
            {

                dataText.Add((uIElement as TextBox).Text);
            }
            else if (dataType == UIElementsName.DatePicker.ToString())
            {

                dataText.Add((uIElement as DatePicker).Text);
            }
            else if (dataType == UIElementsName.ComboBox.ToString())
            {
                if ((uIElement as ComboBox).SelectedItem != (uIElement as ComboBox).Items[0])
                {
                    dataText.Add((uIElement as ComboBox).SelectedItem.ToString());
                }
                else
                {
                    dataText.Add(string.Empty);
                }
            }
            else
            {
                if ((uIElement as ComboBox).SelectedItem != (uIElement as ComboBox).Items[0])
                {
                    List<ListOption> list = new List<ListOption>();

                    foreach (List<ListOption> options in listOptions)
                    {
                        if (options[0].CustomListId == Int32.Parse(dataType))
                        {
                            list = options;
                        }
                    }

                    foreach (ListOption option in list)
                    {
                        if ((uIElement as ComboBox).SelectedItem.ToString() == option.Name)
                        {

                            dataText.Add(option.id.ToString());
                        }
                    }
                }
                else
                {
                    dataText.Add(string.Empty);
                }
            }
        }

        /// <summary>
        /// Extract all exploitable infos
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public List<UIElement> ExtractFormInfos(Grid grid)
        {
            List<UIElement> elementList = new List<UIElement>();

            foreach( UIElement uIElement in grid.Children)
            {
                if (uIElement.GetType().FullName.Equals(new TextBox().GetType().FullName))
                {
                    elementList.Add(uIElement);
                }
                else if (uIElement.GetType().FullName.Equals(new ComboBox().GetType().FullName))
                {
                    elementList.Add(uIElement);
                }
                else if (uIElement.GetType().FullName.Equals(new DatePicker().GetType().FullName))
                {
                    elementList.Add(uIElement);
                }
                else if (uIElement.GetType().FullName.Equals(new ListBox().GetType().FullName))
                {
                    elementList.Add(uIElement);
                }
            }

            return elementList;
        }
    }
}
