using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DockDAP.Ruls;

namespace DockDAP
{
    /// <summary>
    /// Interaction logic for dmeAPControl.
    /// </summary>
    
    
    public partial class dmeAPControl : UserControl
    {
        public DubConfigAP CurrentDubConfigAP;

        public dmeAPControl()
        {
            this.InitializeComponent();
            CurrentDubConfigAP = APIdockAP.CreateOrLoadDubFileAP();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]

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

        private void BTNShowDubFileInVS_Click(object sender, RoutedEventArgs e)
        {
            APIdockAP.OpenDubFileAP();
        }

        private void BtnBuildDub_Click(object sender, RoutedEventArgs e)
        {
            APIdockAP.BuildDubAP();
        }

        private void BtnSaveDubFile_OnClick(object sender, RoutedEventArgs e)
        {

            SaveFile();
        }

        private async Task SaveFile()
        {
            await Task.Run(() =>
            {
                APIdockAP.SaveFileAP(CurrentDubConfigAP);
            });
        }


        private void AnimationHover(FrameworkElement input)
        {
            AnimationManagerAP.AddHoverAnimation(input, 5, Colors.Black, Colors.CadetBlue, TimeSpan.FromSeconds(5));
        }

        private void TXTnameDubFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHover(TXTnameDubFile);
        }

        private void TXTdescriptionDubFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHover(TXTdescriptionDubFile);
        }

        private void TXTauthorsDubFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHover(TXTauthorsDubFile);
        }

        private void TXTlicenseDubFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHover(TXTlicenseDubFile);
        }

        private void TXTtargetNameDubFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHover(TXTtargetNameDubFile);
        }

        private void TXTtargetTypeDubFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHover(TXTtargetTypeDubFile);
        }

        private void TXTsourceFilesDubFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHover(TXTsourceFilesDubFile);
        }

        private void TXTdflagsDubFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHover(TXTdflagsDubFile);
        }

        private void TXTlflagsDubFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHover(TXTlflagsDubFile);
        }
    }



}