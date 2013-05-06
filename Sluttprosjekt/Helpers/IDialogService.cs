using System;
using System.Threading.Tasks;

namespace Sluttprosjekt.Helpers
{
    /// <summary>
    /// The dialog service allows the ViewModel to trigger dialogs in the View.
    /// </summary>
    public interface IDialogService
    {
        void ShowError(
            string message,
            string title,
            string buttonText,
            Action afterHideCallback);

        void ShowError(
            Exception error,
            string title,
            string buttonText,
            Action afterHideCallback);

        void ShowMessage(
            string message,
            string title);

        void ShowMessage(
            string message,
            string title,
            string buttonText,
            Action afterHideCallback);

        void ShowMessage(
            string message,
            string title,
            string buttonConfirmText,
            string buttonCancelText,
            Action<bool> afterHideCallback);

        void ShowMessageBox(
            string message,
            string title);
    }
}