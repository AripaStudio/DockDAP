using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Settings.Internal;
using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json;
using Process = EnvDTE.Process;

namespace DockDAP.Ruls
{
    public class DubConfigAP
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("authors")]
        public List<string> Authors { get; set; } = new List<string>();

        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("targetName")]
        public string TargetName { get; set; }

        [JsonProperty("targetType")]
        public string TargetType { get; set; }

        [JsonProperty("sourceFiles")]
        public List<string> SourceFiles { get; set; } = new List<string>();

        [JsonProperty("dependencies")]
        public Dictionary<string, string> Dependencies { get; set; } = new Dictionary<string, string>();

        [JsonProperty("buildConfigurations")] 
        public Dictionary<string, BuildConfiguration> BuildConfigurations { get; set; } = new Dictionary<string, BuildConfiguration>();

        [JsonExtensionData]
        public Dictionary<string, object> OtherData { get; set; } = new Dictionary<string, object>();

    }

    public class BuildConfiguration
    {
        [JsonProperty("dflags")]
        public List<string> DFlags { get; set; } = new List<string>();

        [JsonProperty("lflags")]
        public List<string> LFlags { get; set; } = new List<string>();
    }


    public static class DubManagerAP
    {

        public static DTE2 FindMainPathDte2AP()
        {
            return ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2;
        }

        public static string FindDubFileAP(DTE2 dte2)
        {
            if (dte2 == null || dte2.Solution == null || dte2.Solution.Projects == null)
            {
                return null;
            }

            foreach (EnvDTE.Project project in dte2.Solution.Projects)
            {
                string projectDirectory = System.IO.Path.GetDirectoryName(project.FullName);
                if (projectDirectory != null)
                {
                    string dubJsonPath = System.IO.Path.Combine(projectDirectory, "dub.json");

                    if (File.Exists(dubJsonPath))
                    {
                        return dubJsonPath;
                    }
                }
            }

            return null;

        }

        public static DubConfigAP ReadDubFileAP(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) { return null; }

            string jsonString = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<DubConfigAP>(jsonString);

        }

        public static DubConfigAP CreateDubFileWithDefaultsAP(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new DubConfigAP();
            }

            DubConfigAP config = new DubConfigAP
            {
                Name = "initial-DubFile-DockAP",
                Description = "A new Dub.json file by DockDAP , AripaParsStudio",
                Authors = new List<string>(){ "DockAPUser" },
            };
            string jsonString = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(path, jsonString);

            return config;

        }

        public static bool SaveFileDubAP(string path , DubConfigAP inputConfigAp)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            if (!File.Exists(path))
            {
                return false;
            }
            try
            {
                var jsonString = JsonConvert.SerializeObject(inputConfigAp, Formatting.Indented);
                File.WriteAllText(path, jsonString);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

    }

    public static class DubCommandAP
    {
        public static void BuildDubCommandAP(string projectPath, string arguments)
        {
            DTE2 dte = DubManagerAP.FindMainPathDte2AP();
            if (dte == null)
            {
                return;
            }

            Window outputWindow = dte.Windows.Item(Constants.vsWindowKindOutput);
            outputWindow.Visible = true;
            OutputWindowPane outputPane = dte.ToolWindows.OutputWindow.OutputWindowPanes.Add("Dub Manager");
            outputPane.Activate();


            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "dub.exe",
                Arguments = arguments,
                WorkingDirectory = projectPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using (System.Diagnostics.Process process =  System.Diagnostics.Process.Start(processStartInfo))
                {
                    process.OutputDataReceived += ProcessOnOutputDataReceived;
                    process.ErrorDataReceived += ProcessOnErrorDataReceived;

                    void ProcessOnErrorDataReceived(object sender, DataReceivedEventArgs e)
                    {
                        if (e.Data != null)
                        {
                            outputPane.OutputString(e.Data + Environment.NewLine);
                        }
                    }

                    void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs e)
                    {
                        if (e.Data != null)
                        {
                            outputPane.OutputString(e.Data + Environment.NewLine);
                        }
                    }

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                }
            }
            catch (Exception e)
            {
                outputPane.OutputString($"Error in run Command : {e.Message}{Environment.NewLine}");
            }
        }

        public static void OpenDubFileinVisualStudioAP(string path)
        {
            DTE2 dte = DubManagerAP.FindMainPathDte2AP();
            if (dte == null || string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return;
            }

            dte.ItemOperations.OpenFile(path);
        }
    }
    public static class APIdockAP
    {
        public static DubConfigAP CreateOrLoadDubFileAP()
        {
            DTE2 dte2 = DubManagerAP.FindMainPathDte2AP();
            if (dte2 == null)
            {
                return null;
            }

            string dubFilePath = DubManagerAP.FindDubFileAP(dte2);
            if (dubFilePath != null)
            {
                return DubManagerAP.ReadDubFileAP(dubFilePath);
            }
            else
            {
                string projectDirectory = System.IO.Path.GetDirectoryName(dte2.Solution.Projects.Item(1).FullName);
                string newFilePath = System.IO.Path.Combine(projectDirectory, "dub.json");
                return DubManagerAP.CreateDubFileWithDefaultsAP(newFilePath);
            }

        }
        public static void BuildDubAP()
        {
            var PathFile = DubManagerAP.FindMainPathDte2AP();
            if (PathFile == null)
            {
                return;
            }

            string FindDubFilePath = DubManagerAP.FindDubFileAP(PathFile);

            if (FindDubFilePath == null)
            {
                return;
            }

            string ProjectPath = System.IO.Path.GetDirectoryName(FindDubFilePath);

            DubCommandAP.BuildDubCommandAP(ProjectPath , "build");

        }

        public static void OpenDubFileAP()
        {
            var PathFile = DubManagerAP.FindMainPathDte2AP();
            if (PathFile == null)
            {
                return;
            }

            string FindDubFilePath = DubManagerAP.FindDubFileAP(PathFile);

            if (FindDubFilePath == null)
            {
                return;
            }

            DubCommandAP.OpenDubFileinVisualStudioAP(FindDubFilePath);
        }

        public static void SaveFileAP(DubConfigAP dubConfigAp)
        {
            var PathFile = DubManagerAP.FindMainPathDte2AP();
            if (PathFile == null)
            {
                return;
            }

            string FindDubFilePath = DubManagerAP.FindDubFileAP(PathFile);

            if (FindDubFilePath == null)
            {
                return;
            }

            DubManagerAP.SaveFileDubAP(FindDubFilePath , dubConfigAp);
        }
    }
}
