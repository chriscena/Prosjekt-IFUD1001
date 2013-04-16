using System;
using System.Windows;
using GalaSoft.MvvmLight.Command;

namespace Sluttprosjekt
{
    public partial class CustomMessageBox
    {
        /// <summary>
        /// The <see cref = "CancelButtonText" /> dependency property's name.
        /// </summary>
        public const string CancelButtonTextPropertyName = "CancelButtonText";

        /// <summary>
        /// The <see cref = "CancelButtonVisibility" /> dependency property's name.
        /// </summary>
        public const string CancelButtonVisibilityPropertyName = "CancelButtonVisibility";

        /// <summary>
        /// The <see cref = "ConfirmButtonText" /> dependency property's name.
        /// </summary>
        public const string ConfirmButtonTextPropertyName = "ConfirmButtonText";

        /// <summary>
        /// The <see cref = "IsShowingError" /> dependency property's name.
        /// </summary>
        public const string IsShowingErrorPropertyName = "IsShowingError";

        /// <summary>
        /// The <see cref = "MessageElementsVisibility" /> dependency property's name.
        /// </summary>
        public const string MessageElementsVisibilityPropertyName = "MessageElementsVisibility";

        /// <summary>
        /// The <see cref = "Message" /> dependency property's name.
        /// </summary>
        public const string MessagePropertyName = "Message";

        /// <summary>
        /// The <see cref = "Title" /> dependency property's name.
        /// </summary>
        public const string TitlePropertyName = "Title";

        /// <summary>
        /// Identifies the <see cref = "CancelButtonText" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CancelButtonTextProperty = DependencyProperty.Register(
            CancelButtonTextPropertyName,
            typeof (string),
            typeof (CustomMessageBox),
            new PropertyMetadata(
                null,
                (s, e) =>
                {
                    var sender = (CustomMessageBox) s;
                    sender.CancelButtonVisibility = string.IsNullOrEmpty((string) e.NewValue)
                                                        ? Visibility.Collapsed
                                                        : Visibility.Visible;
                }));

        /// <summary>
        /// Identifies the <see cref = "CancelButtonVisibility" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CancelButtonVisibilityProperty = DependencyProperty.Register(
            CancelButtonVisibilityPropertyName,
            typeof (Visibility),
            typeof (CustomMessageBox),
            new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// Identifies the <see cref = "ConfirmButtonText" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ConfirmButtonTextProperty = DependencyProperty.Register(
            ConfirmButtonTextPropertyName,
            typeof (string),
            typeof (CustomMessageBox),
            new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref = "IsShowingError" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsShowingErrorProperty = DependencyProperty.Register(
            IsShowingErrorPropertyName,
            typeof (bool),
            typeof (CustomMessageBox),
            new PropertyMetadata(
                false,
                (s, e) =>
                {
                    var sender = (CustomMessageBox) s;
                    sender.MessageElementsVisibility = (bool) e.NewValue ? Visibility.Collapsed : Visibility.Visible;
                }));

        /// <summary>
        /// Identifies the <see cref = "MessageElementsVisibility" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MessageElementsVisibilityProperty = DependencyProperty.Register(
            MessageElementsVisibilityPropertyName,
            typeof (Visibility),
            typeof (CustomMessageBox),
            new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// Identifies the <see cref = "Message" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            MessagePropertyName,
            typeof (string),
            typeof (CustomMessageBox),
            new PropertyMetadata(string.Empty));

        /// <summary>
        /// Identifies the <see cref = "Title" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            TitlePropertyName,
            typeof (string),
            typeof (CustomMessageBox),
            new PropertyMetadata(string.Empty));

        private Action _callback;
        private Action<bool> _callbackWithBool;
        private bool _isVisible;
        private bool? _result;
        public event EventHandler Closed;

        public CustomMessageBox()
        {
            // Required to initialize variables
            InitializeComponent();

            HideBoxAnimation.Completed += HideBoxAnimationCompleted;

            OkCommand = new RelayCommand(
                () =>
                {
                    _result = true;
                    Hide();
                });

            CancelCommand = new RelayCommand(
                () =>
                {
                    _result = false;
                    Hide();
                });

            DataContext = this;
        }

        /// <summary>
        /// Gets or sets the value of the <see cref = "CancelButtonVisibility" />
        /// property. This is a dependency property.
        /// </summary>
        public Visibility CancelButtonVisibility
        {
            get
            {
                return (Visibility) GetValue(CancelButtonVisibilityProperty);
            }
            set
            {
                SetValue(CancelButtonVisibilityProperty, value);
            }
        }

        public RelayCommand CancelCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the value of the <see cref = "MessageElementsVisibility" />
        /// property. This is a dependency property.
        /// </summary>
        public Visibility MessageElementsVisibility
        {
            get
            {
                return (Visibility) GetValue(MessageElementsVisibilityProperty);
            }
            set
            {
                SetValue(MessageElementsVisibilityProperty, value);
            }
        }

        public RelayCommand OkCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the value of the <see cref = "CancelButtonText" />
        /// property. This is a dependency property.
        /// </summary>
        public string CancelButtonText
        {
            get
            {
                return (string) GetValue(CancelButtonTextProperty);
            }
            set
            {
                SetValue(CancelButtonTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the value of the <see cref = "ConfirmButtonText" />
        /// property. This is a dependency property.
        /// </summary>
        public string ConfirmButtonText
        {
            get
            {
                return (string) GetValue(ConfirmButtonTextProperty);
            }
            set
            {
                SetValue(ConfirmButtonTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the value of the <see cref = "IsShowingError" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsShowingError
        {
            get
            {
                return (bool) GetValue(IsShowingErrorProperty);
            }
            set
            {
                SetValue(IsShowingErrorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the value of the <see cref = "Message" />
        /// property. This is a dependency property.
        /// </summary>
        public string Message
        {
            get
            {
                return (string) GetValue(MessageProperty);
            }
            set
            {
                SetValue(MessageProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the value of the <see cref = "Title" />
        /// property. This is a dependency property.
        /// </summary>
        public string Title
        {
            get
            {
                return (string) GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public void Hide()
        {
            if (!_isVisible)
            {
                return;
            }

            _isVisible = false;
            ShowBoxAnimation.Stop();
            HideBoxAnimation.Begin();
        }

        public void Show(Action callback)
        {
            if (_isVisible)
            {
                return;
            }

            _callbackWithBool = null;
            _callback = callback;
            Show();
        }

        public void Show(Action<bool> callbackWithBool)
        {
            if (_isVisible)
            {
                return;
            }

            _callback = null;
            _callbackWithBool = callbackWithBool;
            Show();
        }

        private void HideBoxAnimationCompleted(object sender, EventArgs e)
        {
            HideBoxAnimation.Stop();
            Visibility = Visibility.Collapsed;

            Dispatcher.BeginInvoke(
                () =>
                {
                    if (_callbackWithBool != null)
                    {
                        _callbackWithBool(_result != null && _result.Value);
                    }
                    if (_callback != null)
                    {
                        _callback();
                    }
                    if (Closed != null)
                    {
                        Closed(this, EventArgs.Empty);
                    }
                });
        }

        private void OkTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            OkCommand.Execute(null);
        }

        private void Show()
        {
            _result = null;
            _isVisible = true;
            Visibility = Visibility.Visible;
            HideBoxAnimation.Stop();
            ShowBoxAnimation.Begin();
        }
        
        public void Show(Action callback, int timeoutSeconds)
        {
            throw new NotImplementedException();
        }

        private void CancelTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            CancelCommand.Execute(null);
        }
    }
}