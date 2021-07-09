using Project_Inventory.Tools;
using System.Windows;

namespace Project_Inventory
{
    /// <summary>
    /// Class to store all routing stored procedure
    /// </summary>
    public class Router
    {
        public WindowsName[] routersName;
        public RoutedEventHandler[] routersRouter;

        public Router(WindowsName[] routersName, RoutedEventHandler[] routersRouter)
        {
            this.routersName = routersName;
            this.routersRouter = routersRouter;
        }
    }
}
