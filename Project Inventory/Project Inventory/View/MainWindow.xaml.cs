using Project_Inventory.Tools;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public partial class MainWindow : Window
    {
        public ToolBox toolBox;
        private double titleBarHeight;

        private Router router;
        private RequestCenter requestCenter;

        private MainMenu mainMenu;
        private StorageSelectionMenu storageSelectionMenu;
        private FormPage formPage;
        private StorageViewerPage storageViewerPage;

        public MainWindow()
        {
            DataContext = this;
            ResizeMode = ResizeMode.CanMinimize;

            InitializeComponent();

            titleBarHeight = SystemParameters.WindowCaptionHeight;

            toolBox = new ToolBox(this, titleBarHeight);

            router = InitRouters();
            requestCenter = new RequestCenter();

            Init();
        }

        private void Init()
        {
            MainMenuInit();
        }

        public void MainMenuInit()
        {
            mainMenu = new MainMenu(toolBox, router, requestCenter);
            mainMenu.TopGridInit(topGrid);
            mainMenu.CenterGridInit(centerGrid);
            mainMenu.BottomGridInit(bottomGrid);
        }

        public void StorageSelectionMenuInit()
        {
            storageSelectionMenu = new StorageSelectionMenu(toolBox, router, requestCenter);
            storageSelectionMenu.TopGridInit(topGrid);
            storageSelectionMenu.CenterGridInit(centerGrid);
            storageSelectionMenu.BottomGridInit(bottomGrid);
        }

        public void FormPageInit(string[] formElements, string[] labels)
        {
            formPage = new FormPage(toolBox, router, requestCenter, formElements, labels);
            formPage.TopGridInit(topGrid);
            formPage.CenterGridInit(centerGrid);
            formPage.BottomGridInit(bottomGrid);
        }

        public void storageViewerPageInit()
        {
            storageViewerPage = new StorageViewerPage(toolBox, router, requestCenter);
            storageViewerPage.TopGridInit(topGrid);
            storageViewerPage.CenterGridInit(centerGrid);
            storageViewerPage.BottomGridInit(bottomGrid);
        }

        public void WindowSwitch(object sender, RoutedEventArgs e, string windowName) 
        {
            switch(windowName)
            {
                case ("MainMenu"):
                    MainMenuInit();
                    break;

                case ("StorageSelectionMenu"):
                    StorageSelectionMenuInit();
                    break;

                case ("storageViewerPage"):
                    storageViewerPageInit();
                    break;
            }
        }

        public void WindowSwitch(object sender, RoutedEventArgs e,
                                 string formRoutersName, 
                                 string[] formElements, 
                                 string[] labels)
        {
            switch (formRoutersName)
            {
                case ("Add Storage"):
                    FormPageInit(formElements, labels);
                    break;
            }
        }

        public RoutedEventHandler EnventHandlerGenerator(string windowName)
        {
            RoutedEventHandler router = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            {
                WindowSwitch(sender, e, windowName);
            });

            return router;
        }

        public RoutedEventHandler[] EnventHandlerGeneratorByTab(string[] windowName, string[] formRoutersName, string[,] formElements, string[,] labels, string[] routersNameF)
        {
            RoutedEventHandler[] routers = new RoutedEventHandler[windowName.Length + formRoutersName.Length - 1];
            var i = 0;
            var j = 0;
            var string1 = string.Empty;
            string[] string2;
            string[] string3;

            foreach (string name in windowName)
            {
                if (name != "FormPage")
                {
                    routers[i] = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                    {
                        WindowSwitch(sender, e, name);
                    });
                    routersNameF[i] = name;
                }
                else
                {
                    foreach(string formName in formRoutersName)
                    {
                        string1 = formRoutersName[j];
                        string2 = Enumerable.Range(0, formElements.GetLength(1)).Select(x => formElements[j, x]).ToArray();
                        string3 = Enumerable.Range(0, labels.GetLength(1)).Select(x => labels[j, x]).ToArray();

                        routers[i + j] = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                        {
                            WindowSwitch(sender, e, string1, string2, string3);
                        });
                        routersNameF[i + j] = formName;

                        j++;
                    }
                }

                i++;
            }

            return routers;
        }

        public Router InitRouters()
        {
            string[] routersName = new string[] 
            { 
                "MainMenu", 
                "StorageSelectionMenu",
                "storageViewerPage",
                "FormPage"
            };

            string[] formRoutersName = new string[]
            {
                "Add Storage"
            };

            string[,] formElements = new string[,]
            {
                { "TextBox" }
                /*{ "TextBox", "TextBox", "TextBoxNumber", "DatePicker", "ListBox" }*/
            };

            string[,] labels = new string[,]
            {
                { "Storage's Name" }
                /*{ "Nom", "Prénom", "Numéro De Table", "Date De Naissance", "Choix Du Repas" }*/
            };

            string[] routersNameF = new string[routersName.Length - 1 + formRoutersName.Length];


            RoutedEventHandler[] routersRouter = EnventHandlerGeneratorByTab(routersName, formRoutersName, formElements, labels, routersNameF);

            return new Router(routersNameF, routersRouter);
        }
    }
}
