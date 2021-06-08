using System.Windows;

namespace Project_Inventory
{
    public class Router
    {
        public string[] routersName;
        public RoutedEventHandler[] routersRouter;

        public Router(string[] routersName, RoutedEventHandler[] routersRouter)
        {
            this.routersName = routersName;
            this.routersRouter = routersRouter;
        }
    }
}
