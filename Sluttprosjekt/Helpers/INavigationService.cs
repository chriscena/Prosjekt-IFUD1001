namespace Sluttprosjekt.Helpers
{
    /// <summary>
    /// An interface to implement a navigation service allowing the view model to trigger navigation.
    /// </summary>
    public interface INavigationService
    {
        void GoBack();
        void Navigate(string pageKey);
        void Navigate(string pageKey, object parameter);
    }
}