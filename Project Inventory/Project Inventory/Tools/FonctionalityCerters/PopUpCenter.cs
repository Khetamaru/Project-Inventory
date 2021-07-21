using System.Windows;

namespace Project_Inventory.Tools
{
    public static class PopUpCenter
    {
        /// <summary>
        /// A yes/no pop up to be sure user want to delete
        /// </summary>
        /// <returns></returns>
        public static bool ActionValidPopup()
        {
            string sMessageBoxText = "Are you sure ?";
            string sCaption = "Validation Pop Up";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    return true;

                case MessageBoxResult.No:
                    break;

                case MessageBoxResult.Cancel:
                    break;
            }

            return false;
        }
    }
}
