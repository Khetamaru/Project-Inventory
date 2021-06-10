using Project_Inventory.Tools;
using System.Windows;

namespace Project_Inventory
{
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
