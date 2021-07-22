using Project_Inventory.Tools;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    /// <summary>
    /// Mother class of pages class
    /// </summary>
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

        /// <summary>
        /// Dispay top part of the UI if exist
        /// </summary>
        /// <param name="topGrid"></param>
        public void TopGridInit(Grid topGrid)
        {
            toolBox.EmptyGrid(topGrid);
        }

        /// <summary>
        /// Dispay center part of the UI if exist
        /// </summary>
        /// <param name="centerGrid"></param>
        public void CenterGridInit(Grid centerGrid)
        {
            toolBox.EmptyGrid(centerGrid);
        }

        /// <summary>
        /// Dispay bottom part of the UI if exist
        /// </summary>
        /// <param name="bottomGrid"></param>
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

        /// <summary>
        /// Give stored procedure needed
        /// </summary>
        /// <param name="routerName"></param>
        /// <returns></returns>
        public RoutedEventHandler GetEventHandler(WindowsName routerName)
        {
            var i = 0;

            foreach(WindowsName name in router.routersName)
            {
                if(name == routerName)
                {
                    return router.routersRouter[i];
                }

                i++;
            }

            return null;
        }

        /// <summary>
        /// Init all routed event libraries
        /// </summary>
        /// <param name="routedEventLibrary"></param>
        public void RoutedEventLibrariesInit(RoutedEventLibrary[] routedEventLibrary)
        {
            for( int i = 0 ; i < routedEventLibrary.Length ; i++)
            {
                routedEventLibrary[i] = new RoutedEventLibrary();
            }
        }
    }
}
