using Project_Inventory.Tools;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public class WindowContent
    {

        public ToolBox toolBox;
        public Router router;
        public RequestCenter requestCenter;

        public int actualStorageId;
        public int actualDataId;

        public WindowContent(ToolBox _toolBox, Router _router, RequestCenter _requestCenter, int _actualStorageId, int _actualDataId)
        {
            toolBox = _toolBox;
            router = _router;
            requestCenter = _requestCenter;

            actualStorageId = _actualStorageId;
            actualDataId = _actualDataId;
        }

        public void TopGridInit(Grid topGrid)
        {
            toolBox.EmptyGrid(topGrid);
        }

        public void CenterGridInit(Grid centerGrid)
        {
            toolBox.EmptyGrid(centerGrid);
        }

        public void BottomGridInit(Grid bottomGrid)
        {
            toolBox.EmptyGrid(bottomGrid);
        }

        public int StorageIDBackups()
        {
            return actualStorageId;
        }

        public int DataIDBackups()
        {
            return actualDataId;
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

        public void ButtonPlacer(Grid grid, int tabLength, int widthLimit, string skinName, string lengthName)
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

            toolBox.SetUpGrid(grid, j, i, skinName, lengthName);
        }
    }
}
