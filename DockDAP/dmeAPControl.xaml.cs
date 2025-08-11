using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using DockDAP.Ruls;
using Microsoft.VisualStudio.CommandBars;
using Microsoft.VisualStudio.Shell.Interop;

namespace DockDAP
{
    /// <summary>
    /// Interaction logic for dmeAPControl.
    /// </summary>
    
    
    public partial class dmeAPControl : UserControl
    {
        public DubConfigAP CurrentDubConfigAP;
        private ObservableCollection<string> ListAuthors = new ObservableCollection<string>();
        private ObservableCollection<string> ListSourceFile = new ObservableCollection<string>();
        private Dictionary<string , string> DictionaryDependencies = new Dictionary<string , string>();
        private Dictionary<string , BuildConfiguration> DictionaryBuildConfigurations = new Dictionary<string , BuildConfiguration>();
        private Dictionary<string, object> DictionaryOtherData = new Dictionary<string, object>();
        

        public dmeAPControl()
        {
            this.InitializeComponent();
            CurrentDubConfigAP = APIdockAP.CreateOrLoadDubFileAP();
            ListBoxAuthors.ItemsSource = ListAuthors;
            ListBoxSourceFile.ItemsSource = ListSourceFile;
            ListBoxDependency.ItemsSource = DictionaryDependencies;


            DataContext = this;
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
            var Name = TXTnameDubFile.Text;
            var Description = TXTdescriptionDubFile.Text;
            var license = TXTlicenseDubFile.Text;
            var targetName = TXTtargetNameDubFile.Text;
            var targetType = TXTtargetTypeDubFile.Text;

            APIdockAP.AddToConfigAp(Name, Description, ListAuthors.ToList(), license, targetName,
                targetType, ListSourceFile.ToList() , DictionaryDependencies, DictionaryBuildConfigurations, DictionaryOtherData);
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

        private void Btn_AddAuthors_OnClick(object sender, RoutedEventArgs e)
        {
            var authors = TXTauthorsDubFile.Text;
            if (string.IsNullOrEmpty(authors))
            {
                System.Windows.Forms.MessageBox.Show(
                    "Authors TextBox is Empty or is Null , Please Enter a Text in TextBox");
            }

            ListAuthors.Add(authors);
        }

        private void Btn_DeleteAuthors_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListBoxAuthors.SelectedItem;

            if (selectedItem != null)
            {
                ListAuthors.Remove(selectedItem as string);
            }
        }

        private void BtnAddSourceFile_OnClick(object sender, RoutedEventArgs e)
        {
            var sourceFile = TXTsourceFilesDubFile.Text;
            if (string.IsNullOrEmpty(sourceFile))
            {
                MessageBox.Show("sourceFile TextBox is Empty or is Null , Please Enter a Text in TextBox");
            }
            ListSourceFile.Add(sourceFile);
        }

        private void BtnDeleteSourceFile_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListBoxSourceFile.SelectedItem;

            if (selectedItem != null)
            {
                ListSourceFile.Remove(selectedItem as string);
            }
        }
    }

   

}