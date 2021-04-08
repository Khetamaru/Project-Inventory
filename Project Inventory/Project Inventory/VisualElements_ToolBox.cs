using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    class VisualElements_ToolBox
    {
        private Button CreateButton(string content,
                                    Thickness margin,
                                    HorizontalAlignment horizontalAlign,
                                    VerticalAlignment verticalAlign)
        {
            Button temp = new Button();

            temp.Content = content;
            temp.Margin = margin;
            temp.HorizontalAlignment = horizontalAlign;
            temp.VerticalAlignment = verticalAlign;

            return temp;
        }

        private Grid CreateGrid(int columnNb,
                                int rowNb)
        {
            Grid temp = new Grid();

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
    }
}
