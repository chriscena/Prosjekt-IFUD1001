using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Phone.Controls;

namespace Sluttprosjekt.Helpers
{
    public class PhonePageBase : PhoneApplicationPage, IDialogService
    {
        public CustomMessageBox Dialog
        {
            get;
            protected set;
        }

        public PhonePageBase()
        {
            Loaded += PhonePageBaseLoaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                SimpleIoc.Default.Register<IDialogService>(() => this);
            }
            catch (InvalidOperationException)
            {
                SimpleIoc.Default.Unregister<IDialogService>();
                SimpleIoc.Default.Register<IDialogService>(() => this);
            }
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SimpleIoc.Default.Unregister<IDialogService>();
            base.OnNavigatingFrom(e);
        }

        #region Implementation of IDialogService

        public virtual void ShowError(string message, string title, string buttonText, Action hideCallback)
        {
            if (Dialog != null)
            {
                Dialog.IsShowingError = true;
                Dialog.Message = message;
                Dialog.Title = title;
                Dialog.ConfirmButtonText = buttonText;
                Dialog.CancelButtonText = null;
                Dialog.Show(hideCallback);
            }
#if DEBUG
            else
            {
                MessageBox.Show(message, title, MessageBoxButton.OK);
            }
#else
            throw new InvalidOperationException("Cannnot find Dialog control");
#endif
        }

        public virtual void ShowError(Exception error, string title, string buttonText, Action hideCallback)
        {
            if (Dialog != null)
            {
                Dialog.IsShowingError = true;
                Dialog.Message = error.Message;
                Dialog.Title = title;
                Dialog.ConfirmButtonText = buttonText;
                Dialog.CancelButtonText = null;
                Dialog.Show(hideCallback);
            }
#if DEBUG
            else
            {
                MessageBox.Show(error.Message, title, MessageBoxButton.OK);
            }
#else
            throw new InvalidOperationException("Cannnot find Dialog control");
#endif
        }

        public virtual void ShowMessage(string message, string title)
        {
            
            ShowMessage(message, title ?? string.Empty, null, null);
        }

        public virtual void ShowMessage(string message, string title, int timeoutSeconds)
        {
            if (Dialog != null)
            {
                Dialog.IsShowingError = false;
                Dialog.Message = message;
                Dialog.Title = title;
                Dialog.ConfirmButtonText = null;
                Dialog.CancelButtonText = null;
                Dialog.Show(null, timeoutSeconds);
            }
#if DEBUG
            else
            {
                ShowMessage(message, title);
            }
#else
            throw new InvalidOperationException("Cannnot find Dialog control");
#endif
        }

        public virtual void ShowMessage(string message, string title, string buttonText, Action hideCallback)
        {
            if (Dialog != null)
            {
                Dialog.IsShowingError = false;
                Dialog.Message = message;
                Dialog.Title = title;
                Dialog.ConfirmButtonText = buttonText;
                Dialog.CancelButtonText = null;
                Dialog.Show(hideCallback);
            }
#if DEBUG
            else
            {
                MessageBox.Show(message, title, MessageBoxButton.OK);
            }
#else
            throw new InvalidOperationException("Cannnot find Dialog control");
#endif
        }

        public virtual void ShowMessage(
            string message, string title, string confirmButtonText, string cancelButtonText, Action<bool> callback)
        {
            if (Dialog != null)
            {
                Dialog.IsShowingError = false;
                Dialog.Message = message;
                Dialog.Title = title;
                Dialog.ConfirmButtonText = confirmButtonText;
                Dialog.CancelButtonText = cancelButtonText;
                Dialog.Show(callback);
            }
#if DEBUG
            else
            {
                callback(MessageBox.Show(message, title, MessageBoxButton.OKCancel) == MessageBoxResult.OK);
            }
#else
            throw new InvalidOperationException("Cannnot find Dialog control");
#endif
        }

        public virtual void ShowMessageBox(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK);
        }

        #endregion

        private void PhonePageBaseLoaded(object sender, RoutedEventArgs e)
        {
            Dialog = FindName("DialogControl") as CustomMessageBox;
            Loaded -= PhonePageBaseLoaded;
        }
    }
}