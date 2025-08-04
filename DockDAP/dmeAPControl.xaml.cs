using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using DockDAP.Ruls;

namespace DockDAP
{
    /// <summary>
    /// Interaction logic for dmeAPControl.
    /// </summary>
    public partial class dmeAPControl : UserControl
    {
        public dmeAPControl()
        {
            this.InitializeComponent();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "dmeAP");
        }

        private void Btn_Telegram_Click(object sender, RoutedEventArgs e)
        {
            BtnManagerAP.OpenLinkAP("https://t.me/AripaStudio");
        }

        private void Btn_Website_Click(object sender, RoutedEventArgs e)
        {
            BtnManagerAP.OpenLinkAP("https://aripastudio.github.io/AripaStudio/");
        }

        private void Btn_Github_Click(object sender, RoutedEventArgs e)
        {
            BtnManagerAP.OpenLinkAP("https://github.com/AripaStudio");
        }

        private void Btn_DockDAPrepository_Click(object sender, RoutedEventArgs e)
        {
            BtnManagerAP.OpenLinkAP("https://github.com/AripaStudio/DockDAP");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DubConfigAP dubConfigAp = APIdockAP.CreateOrLoadDubFileAP();
            if (dubConfigAp == null)
            {
                MessageBox.Show("Error in Create or Load Dub File");
            }
        }
    }
}