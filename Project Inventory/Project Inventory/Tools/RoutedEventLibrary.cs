using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory.Tools
{
    public class RoutedEventLibrary
    {
        public RoutedEventHandler changePageEvent { get; set; }
        public RoutedEventHandler updateIdEvent { get; set; }
        public RoutedEventHandler optionalEventOne { get; set; }
        public RoutedEventHandler optionalEventTwo { get; set; }
        public RoutedEventHandler optionalEventThree { get; set; }
        public RoutedEventHandler resetPageEvent { get; set; }

        public RoutedEventLibrary()
        {
            changePageEvent = null;
            updateIdEvent = null;
            resetPageEvent = null;
        }

        private RoutedEventHandler[] LibraryToTab()
        {
            int i = 0;

            if (updateIdEvent != null) { i++; }
            if (resetPageEvent != null) { i++; }
            if (optionalEventOne != null) { i++; }
            if (optionalEventTwo != null) { i++; }
            if (optionalEventThree != null) { i++; }
            if (changePageEvent != null) { i++; }

            RoutedEventHandler[] routedEventHandlers = new RoutedEventHandler[i];

            i = 0;

            if (updateIdEvent != null) { routedEventHandlers[i] = updateIdEvent; i++; }
            if (resetPageEvent != null) { routedEventHandlers[i] = resetPageEvent; i++; }
            if (optionalEventOne != null) { routedEventHandlers[i] = optionalEventOne; i++; }
            if (optionalEventTwo != null) { routedEventHandlers[i] = optionalEventTwo; i++; }
            if (optionalEventThree != null) { routedEventHandlers[i] = optionalEventThree; i++; }
            if (changePageEvent != null) { routedEventHandlers[i] = changePageEvent; }

            return routedEventHandlers;
        }

        public void EventInjection(Button button)
        {
            RoutedEventHandler[] routedEventHandlers = LibraryToTab();

            for (int i = 0 ; i < routedEventHandlers.Length ; i++)
            {
                button.Click += routedEventHandlers[i];
            }
        }
    }
}
