using System.Windows;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace Sluttprosjekt.Model
{
    //public class Friend : INotifyPropertyChanged
    public class Friend : ViewModelBase
    {
        [JsonProperty("id")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// The <see cref="FirstName" /> property's name.
        /// </summary>
        public const string FirstNamePropertyName = "FirstName";

        private string _firstName;

        /// <summary>
        /// Sets and gets the FirstName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>lize(FieldName = "first_name")]
        [JsonProperty("first_name")]
        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                if (_firstName == value)
                {
                    return;
                }

                _firstName = value;
                RaisePropertyChanged(FirstNamePropertyName);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="LastName" /> property's name.
        /// </summary>
        public const string LastNamePropertyName = "LastName";

        private string _lastName;

        /// <summary>
        /// Sets and gets the LastName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                if (_lastName == value)
                {
                    return;
                }

                _lastName = value;
                RaisePropertyChanged(LastNamePropertyName);
                IsDirty = true;
            }
        }

        [JsonProperty("picture")]
        public string PictureUrl
        {
            get;
            set;
        }

        [JsonProperty("location")]
        public string Location
        {
            get;
            set;
        }

        private Point _parsedLocation;

        public Point ParsedLocation
        {
            get
            {
                if (_parsedLocation.X < 0.01)
                {
                    var elements = Location.Split(';');
                    _parsedLocation = new Point(
                        double.Parse(elements[0]), 
                        double.Parse(elements[1]));
                }

                return _parsedLocation;
            }
        }

        [JsonProperty("message")]
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// The <see cref="IsDirty" /> property's name.
        /// </summary>
        public const string IsDirtyPropertyName = "IsDirty";

        private bool _isDirty;

        /// <summary>
        /// Sets and gets the IsDirty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }

            set
            {
                if (_isDirty == value)
                {
                    return;
                }

                _isDirty = value;
                RaisePropertyChanged(IsDirtyPropertyName);
            }
        }

        public void Update(Friend updatedFriend)
        {
            FirstName = updatedFriend.FirstName;
            LastName = updatedFriend.LastName;
        }

        public Friend()
        {
#if DEBUG
            if (IsInDesignMode)
            {
                FirstName = "Laurent";
                LastName = "Bugnion";
                Message = "This is a custom message";
                PictureUrl = "http://www.galasoft.ch/labs/friends/Data/LogoHead128.png";
            }
#endif
        }

        public override string ToString()
        {
            return string.Format(
                "{0},{1},{2},{3}",
                Id,
                _firstName,
                _lastName,
                PictureUrl);
        }

        public Friend(string serial)
        {
            var items = serial.Split(',');

            Id = items[0];
            FirstName = items[1];
            LastName = items[2];
            PictureUrl = items[3];
        }
    }
}