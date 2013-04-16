namespace Sluttprosjekt.Helpers
{
    public interface INavigationService
    {
        void GoBack();
        void Navigate(string pageKey);
        void Navigate(string pageKey, object parameter);
    }
}