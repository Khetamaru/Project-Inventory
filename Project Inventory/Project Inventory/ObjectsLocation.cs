using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Project_Inventory
{
    public class ObjectsLocation
    {
        public double globalWidth;
        public double globalHeight;
        public WpfScreen wpfScreen;

        public ObjectsLocation()
        {
            wpfScreen = new WpfScreen();

            globalWidth = wpfScreen.PrimaryScreenSizeWidth();
            globalHeight = wpfScreen.PrimaryScreenSizeHeight();
        }

        public Thickness RegularThicknessDynamicPlacement(int nbObjWidth, 
                                                          int nbObjHeight, 
                                                          int objPlaceInWidth, 
                                                          int objPlaceInHeight)
        {
            double oneWidth = globalWidth / nbObjWidth;
            double oneHeight = globalHeight / nbObjHeight;

            double halfOneWidth = oneWidth / 2;
            double halfOneHeight = oneHeight / 2;

            double marginWidth = halfOneWidth + oneWidth * (objPlaceInWidth - 1);
            double marginHeight = halfOneHeight + oneHeight * (objPlaceInHeight - 1);

            Thickness thickness = new Thickness(marginWidth, marginHeight, 0, 0);

            return thickness;
        }
    }
}
