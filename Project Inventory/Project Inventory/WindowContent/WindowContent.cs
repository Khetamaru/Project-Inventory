using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public class WindowContent
    {

        public VisualElements_ToolBox toolBox;
        public Router router;

        public WindowContent(VisualElements_ToolBox visualElements_ToolBox, Router _router)
        {
            toolBox = visualElements_ToolBox;
            router = _router;
        }

        public Grid TopGridInit(Grid topGrid)
        {
            return null;
        }

        public Grid CenterGridInit(Grid centerGrid)
        {
            return null;
        }
        public Grid BottomGridInit(Grid bottomGrid)
        {
            return null;
        }

        public RoutedEventHandler GetEventHandler(string routerName)
        {
            var i = 0;

            foreach(string name in router.routersName)
            {
                if(name == routerName)
                {
                    return router.routersRouter[i];
                }

                i++;
            }

            return null;
        }

        public Grid ButtonPlacer(Grid grid, int tabLength, int widthLimit, string skinName, string lengthName)
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

            return toolBox.SetUpGrid(grid, j, i, skinName, lengthName);
        }
    }
}
