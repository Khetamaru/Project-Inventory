using System;
using System.Windows;
using System.Windows.Input;
using Project_Inventory.Tools.NamesLibraries;

namespace Project_Inventory.Tools.FonctionalityCerters
{
    public static class KeyPressedEventCenter
    {
        /// <summary>
        /// Inject An Event when a specific Key is pressed on a specific UI Element
        /// </summary>
        /// <param name="routedEvent"></param>
        /// <param name="keyPressed"></param>
        /// <param name="uIElement"></param>
        public static void KeyPressedEventInjection(RoutedEventHandler routedEvent, KeyPressedName keyPressed, UIElement uIElement)
        {
            switch(keyPressed)
            {
                case KeyPressedName.EnterKey:

                    uIElement.KeyDown += new KeyEventHandler((object sender, KeyEventArgs e) => { KeyPressedEnter(sender, e, routedEvent); });
                    break;
            }
        }
        /// <summary>
        /// Inject An Event when a specific Key is pressed on a specific UI Element
        /// </summary>
        /// <param name="routedEventLibrary"></param>
        /// <param name="keyPressed"></param>
        /// <param name="uIElement"></param>
        public static void KeyPressedEventInjection(RoutedEventLibrary routedEventLibrary, KeyPressedName keyPressed, UIElement uIElement)
        {
            foreach(RoutedEventHandler routedEvent in routedEventLibrary.LibraryToTab())
            {
                switch (keyPressed)
                {
                    case KeyPressedName.EnterKey:

                        uIElement.KeyDown += new KeyEventHandler((object sender, KeyEventArgs e) => { KeyPressedEnter(sender, e, routedEvent); });
                        break;
                }
            }
        }

        /// <summary>
        /// Event for Enter Key Pressed Case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="routedEvent"></param>
        private static void KeyPressedEnter(Object sender, KeyEventArgs e, RoutedEventHandler routedEvent)
        {
            if (e.Key == Key.Enter)
            {
                routedEvent.Invoke(sender, e);
            }
        }
    }
}
