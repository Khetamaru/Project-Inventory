using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory.Tools
{
    /// <summary>
    /// Class to Stock All stored procedure of a button
    /// </summary>
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

        /// <summary>
        /// Prepare all stored procedure for insertion in the button
        /// </summary>
        /// <returns></returns>
        public RoutedEventHandler[] LibraryToTab()
        {
            int i = 0;

            if (updateIdEvent != null) { i++; }
            if (optionalEventOne != null) { i++; }
            if (optionalEventTwo != null) { i++; }
            if (optionalEventThree != null) { i++; }
            if (resetPageEvent != null) { i++; }
            if (changePageEvent != null) { i++; }

            RoutedEventHandler[] routedEventHandlers = new RoutedEventHandler[i];

            i = 0;

            if (updateIdEvent != null) { routedEventHandlers[i] = updateIdEvent; i++; }
            if (optionalEventOne != null) { routedEventHandlers[i] = optionalEventOne; i++; }
            if (optionalEventTwo != null) { routedEventHandlers[i] = optionalEventTwo; i++; }
            if (optionalEventThree != null) { routedEventHandlers[i] = optionalEventThree; i++; }
            if (resetPageEvent != null) { routedEventHandlers[i] = resetPageEvent; i++; }
            if (changePageEvent != null) { routedEventHandlers[i] = changePageEvent; }

            return routedEventHandlers;
        }

        /// <summary>
        /// Insert all stored procedure in the button
        /// </summary>
        /// <param name="button"></param>
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
