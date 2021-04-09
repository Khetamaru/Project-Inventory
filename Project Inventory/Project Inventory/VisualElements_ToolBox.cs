using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Project_Inventory
{
    class VisualElements_ToolBox
    {
        private NavigationService navServiceContext;
        private Window context;

        public VisualElements_ToolBox(NavigationService navService, Window context)
        {
            navServiceContext = navService;
            this.context = context;
        }

        public Button CreateButton(string content,
                                   HorizontalAlignment horizontalAlign,
                                   VerticalAlignment verticalAlign)
        {
            Button temp = new Button();

            temp.Content = content;
            temp.HorizontalAlignment = horizontalAlign;
            temp.VerticalAlignment = verticalAlign;

            return temp;
        }

        public Button CreateButtonWithNavigation(string content,
                                                  HorizontalAlignment horizontalAlign,
                                                  VerticalAlignment verticalAlign,
                                                  string nextPageName)
        {

            Button temp = CreateButton(content, horizontalAlign, verticalAlign);
            temp = AddOnClickButton(temp, nextPageName);

            return temp;

        }

        private Button AddOnClickButton(Button button, string nextPageName)
        {
            button.Click += (object sender, RoutedEventArgs e) => 
            {
                PageNavigation(sender, e, nextPageName); 
            };
            return button;
        }

        private void PageNavigation(object sender, RoutedEventArgs e, string nextPageName)
        {
            Uri uri = new Uri(nextPageName, UriKind.Relative);
            navServiceContext.Navigate(uri);
            context.Close();
        }

        public Grid ChangeGrid(Grid temp,
                               int columnNb,
                               int rowNb)
        {

            temp.Width = 400;
            temp.Height = 400;

            temp.HorizontalAlignment = HorizontalAlignment.Left;

            temp.VerticalAlignment = VerticalAlignment.Top;

            temp.ShowGridLines = true;

            temp.Background = new SolidColorBrush(Colors.LightSteelBlue);

            int i;

            for(i = 0; i < columnNb; i++)
            {
                temp.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (i = 0; i < rowNb; i++)
            {
                temp.RowDefinitions.Add(new RowDefinition());
            }

            return temp;
        }

        public Grid AddButtonToGrid(Grid grid, Button button)
        {
            grid.Children.Add(button);

            return grid;
        }

        public Grid CreateButtonToGrid(Grid grid,
                                       string content,
                                       HorizontalAlignment horizontalAlign,
                                       VerticalAlignment verticalAlign)
        {
            Button button = CreateButton(content, horizontalAlign, verticalAlign);

            grid = AddButtonToGrid(grid, button);

            return grid;
        }

        public Grid CreateButtonNavigateToGrid(Grid grid, 
                                               string content,
                                               HorizontalAlignment horizontalAlign,
                                               VerticalAlignment verticalAlign,
                                               string nextPageName)
        {
            Button button = CreateButtonWithNavigation(content, horizontalAlign, verticalAlign, nextPageName);

            grid = AddButtonToGrid(grid, button);

            return grid;
        }
    }
}
