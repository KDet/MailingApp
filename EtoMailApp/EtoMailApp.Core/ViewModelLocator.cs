using GalaSoft.MvvmLight.Ioc;
using MailApp.Core.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace MailApp.Core
{
    public class ViewModelLocator
    {
        private static ViewModelLocator _locator;

        public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());
        public MainFormViewModel Main => ServiceLocator.Current.GetInstance<MainFormViewModel>();

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
        }
    }
}
