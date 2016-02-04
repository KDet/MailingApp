using Eto.Forms;
using Eto.Serialization.Xaml;
using MailApp.Core;

namespace EtoMailApp.UI.EtoPortable.View
{
    public class MainForm : Form
    {
        public MainForm()
        {
            DataContext = ViewModelLocator.Locator.Main;
            XamlReader.Load(this);
        }
    }
}