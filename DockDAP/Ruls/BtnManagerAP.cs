using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DockDAP.Ruls
{
    public static class BtnManagerAP
    {
        public static void OpenLinkAP(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo(url) {UseShellExecute = true});
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show($"Error in Open the link : {url} || error message : {e.Message}");
            }
        }
    }
}
