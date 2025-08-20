using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DockDAP.Ruls;

namespace DockDAP
{
    /// <summary>
    ///     Interaction logic for dmeAPControl.
    /// </summary>
    public partial class dmeAPControl : UserControl
    {
        private  BuildConfiguration CurrentBuildConfiguration = new BuildConfiguration();
        public DubConfigAP CurrentDubConfigAP;

        private  Dictionary<string, BuildConfiguration> DictionaryBuildConfigurations =
            new Dictionary<string, BuildConfiguration>();

        private  Dictionary<string, string> DictionaryDependencies = new Dictionary<string, string>();


        private  Dictionary<string, object> DictionaryOtherData = new Dictionary<string, object>();
        private  ObservableCollection<string> ListAuthors = new ObservableCollection<string>();
        private  ObservableCollection<string> ListSourceFile = new ObservableCollection<string>();


        public dmeAPControl()
        {
            InitializeComponent();
            CurrentDubConfigAP = APIdockAP.CreateOrLoadDubFileAP();
            ListBoxAuthors.ItemsSource = ListAuthors;
            ListBoxSourceFile.ItemsSource = ListSourceFile;
            ListBoxDependency.ItemsSource = DictionaryDependencies;
            ListBoxLflagsDubFile.ItemsSource = CurrentBuildConfiguration.LFlags;
            ListboxDflagsDubFile.ItemsSource = CurrentBuildConfiguration.DFlags;
            ListBoxVersionsDubFile.ItemsSource = CurrentBuildConfiguration.Versions;
            ListBoxBuildOptionsDubFile.ItemsSource = CurrentBuildConfiguration.BuildOptions;
            ListBoxConfigurationsDubFile.ItemsSource = DictionaryBuildConfigurations;


            DataContext = this;
        }

        private void AddDefaultBuildConfiguration()
        {
            var configurationName = "default";
            if (!DictionaryBuildConfigurations.ContainsKey(configurationName))
                DictionaryBuildConfigurations.Add(configurationName, CurrentBuildConfiguration);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter",
            Justification = "Default event handler naming pattern")]
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

        private void Btn_CreateDubFile_Click(object sender, RoutedEventArgs e)
        {
            var dubConfigAp = APIdockAP.CreateOrLoadDubFileAP();
            if (dubConfigAp == null) MessageBox.Show("Error in Create or Load Dub File");
        }

        private void BTNShowDubFileInVS_Click(object sender, RoutedEventArgs e)
        {
            APIdockAP.OpenDubFileAP();
        }

        private void BtnBuildDebugDub_Click(object sender, RoutedEventArgs e)
        {
            APIdockAP.BuildDebugDubAP();
        }

        private void BtnBuildReleaseDub_OnClick(object sender, RoutedEventArgs e)
        {
            APIdockAP.BuildReleaseDubAP();
        }

        private void BtnBuildDefaultDub_OnClick(object sender, RoutedEventArgs e)
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
            AddDefaultBuildConfiguration();

            var DubConfig = APIdockAP.AddToConfigAp(Name, Description, ListAuthors.ToList(), license, targetName,
                targetType, ListSourceFile.ToList(), DictionaryDependencies, DictionaryBuildConfigurations,
                DictionaryOtherData);
            CurrentDubConfigAP = DubConfig;
            SaveFile();
        }


        private async Task SaveFile()
        {
            await Task.Run(() => { APIdockAP.SaveFileAP(CurrentDubConfigAP); });
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

        private void TXTConfigurationsAddDubFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHover(TXTConfigurationsAddKeyDubFile);
        }

        private void TXTConfigurationsAddValueDubFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHover(TXTConfigurationsAddValueDubFile);
        }

        private void TXTbuildOptionsDubFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHover(TXTbuildOptionsDubFile);
        }

        private void TXTVersionsDubFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHover(TXTVersionsDubFile);
        }


        private void Btn_AddAuthors_OnClick(object sender, RoutedEventArgs e)
        {
            var authors = TXTauthorsDubFile.Text;
            if (string.IsNullOrEmpty(authors))
            {
                System.Windows.Forms.MessageBox.Show(
                    "Authors TextBox is Empty or is Null , Please Enter a Text in TextBox");
                return;
            }

            ListAuthors.Add(authors);
        }

        private void Btn_DeleteAuthors_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListBoxAuthors.SelectedItem;

            if (selectedItem != null) ListAuthors.Remove(selectedItem as string);
        }

        private void BtnAddSourceFile_OnClick(object sender, RoutedEventArgs e)
        {
            var sourceFile = TXTsourceFilesDubFile.Text;
            if (string.IsNullOrEmpty(sourceFile))
            {
                MessageBox.Show("sourceFile TextBox is Empty or is Null , Please Enter a Text in TextBox");
                return;
            }

            ListSourceFile.Add(sourceFile);
        }

        private void BtnDeleteSourceFile_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListBoxSourceFile.SelectedItem;

            if (selectedItem != null) ListSourceFile.Remove(selectedItem as string);
        }

        private void Btn_AddDependency_OnClick(object sender, RoutedEventArgs e)
        {
            var DependencyKey = TXTdependenciesKeyDubFile.Text;
            var DependencyValue = TXTdependenciesValueDubFile.Text;
            if (string.IsNullOrEmpty(DependencyKey) || string.IsNullOrEmpty(DependencyValue))
            {
                MessageBox.Show("Dependency TextBox Key/Value is Empty or is Null , Please Enter a Text in TextBox");
                return;
            }

            DictionaryDependencies.Add(DependencyKey, DependencyValue);
        }

        private void Btn_DeleteDependency_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListBoxDependency.SelectedItem;
            if (selectedItem != null)
            {
                var dependency = (KeyValuePair<string, string>)selectedItem;
                var dependencyKey = dependency.Key;
                DictionaryDependencies.Remove(dependencyKey);
            }
        }


        private void BtndflagsDubFileAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var dflags = TXTdflagsDubFile.Text;
            if (string.IsNullOrEmpty(dflags))
            {
                MessageBox.Show("dflags TextBox is Empty or is Null , Please Enter a Text in TextBox");
                return;
            }


            CurrentBuildConfiguration.DFlags.Add(dflags);
        }

        private void BtndflagsDubFileRemove_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListboxDflagsDubFile.SelectedItem;
            if (selectedItem != null) CurrentBuildConfiguration.DFlags.Remove(selectedItem as string);
        }

        private void BtnlflagsDubFileAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var lflags = TXTlflagsDubFile.Text;
            if (string.IsNullOrEmpty(lflags))
            {
                MessageBox.Show("lflags TextBox is Empty or is Null , Please Enter a Text in TextBox");
                return;
            }

            CurrentBuildConfiguration.LFlags.Add(lflags);
        }

        private void BtnlflagsDubFileRemove_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListBoxLflagsDubFile.SelectedItem;
            if (selectedItem != null) CurrentBuildConfiguration.LFlags.Remove(selectedItem as string);
        }

        private void BtnConfigurationsAddDubFile_OnClick(object sender, RoutedEventArgs e)
        {
            var addOtherConfigurationsKey = TXTConfigurationsAddKeyDubFile.Text;
            var addOtherConfigurationsValue = TXTConfigurationsAddValueDubFile.Text;
            if (string.IsNullOrEmpty(addOtherConfigurationsKey) || string.IsNullOrEmpty(addOtherConfigurationsValue))
            {
                MessageBox.Show("Build Configurations TextBox is Empty or is Null , Please Enter a Text in TextBox");
                return;
            }

            CurrentBuildConfiguration.OtherDataBuildConfiguration.Add(addOtherConfigurationsKey,
                addOtherConfigurationsValue);
        }

        private void BtnConfigurationsRemoveDubFile_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListBoxConfigurationsDubFile.SelectedItem;
            if (selectedItem != null)
            {
                var GetItem = (KeyValuePair<string, object>)selectedItem;
                var key = GetItem.Key;
                CurrentBuildConfiguration.OtherDataBuildConfiguration.Remove(key);
            }
        }

        private void BtnBuildOptionsDubFileAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var addBuildOptions = TXTbuildOptionsDubFile.Text;
            if (string.IsNullOrEmpty(addBuildOptions))
            {
                MessageBox.Show("Build Options TextBox is Empty or is Null , Please Enter a Text in TextBox");
                return;
            }

            CurrentBuildConfiguration.BuildOptions.Add(addBuildOptions);
        }

        private void BtnBuildOptionsDubFileRemove_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListBoxBuildOptionsDubFile.SelectedItem;
            if (selectedItem != null) CurrentBuildConfiguration.BuildOptions.Remove(selectedItem as string);
        }


        private void BtnVersionsDubFileAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var addVersion = TXTVersionsDubFile.Text;
            if (string.IsNullOrEmpty(addVersion))
            {
                MessageBox.Show("Versions TextBox is Empty or is Null , Please Enter a Text in TextBox");
                return;
            }

            CurrentBuildConfiguration.Versions.Add(addVersion);
        }

        private void BtnVersionsDubFileRemove_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListBoxVersionsDubFile.SelectedItem;
            if (selectedItem != null) CurrentBuildConfiguration.Versions.Remove(selectedItem as string);
        }
    }
}