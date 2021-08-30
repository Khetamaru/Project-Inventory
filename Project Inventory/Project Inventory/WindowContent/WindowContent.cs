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

        public int actualUserId;
        public int actualStorageId;
        public int actualDataId;
        public int actualCustomListId;

        public WindowContent(ToolBox _toolBox, Router _router, RequestCenter _requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomListId)
        {
            toolBox = _toolBox;
            router = _router;
            requestCenter = _requestCenter;

            actualUserId = _actualUserId;
            actualStorageId = _actualStorageId;
            actualDataId = _actualDataId;
            actualCustomListId = _actualCustomListId;
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

        public void IDBachUps(out int _actualUserId, out int _actualStorageId, out int _actualDataId, out int _actualCustomListId)
        {
            _actualUserId = UserIDBackups();
            _actualStorageId = StorageIDBackups();
            _actualDataId = DataIDBackups();
            _actualCustomListId = CustomListIDBackups();
        }

        private int UserIDBackups()
        {
            return actualUserId;
        }

        private int StorageIDBackups()
        {
            return actualStorageId;
        }

        private int DataIDBackups()
        {
            return actualDataId;
        }

        private int CustomListIDBackups()
        {
            return actualCustomListId;
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
