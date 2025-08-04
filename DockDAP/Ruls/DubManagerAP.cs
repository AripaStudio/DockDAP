using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Settings.Internal;
using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json;

namespace DockDAP.Ruls
{
    public class DubConfigAP
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Dependencies { get; set; } = new Dictionary<string, string>();
    }


    public static class DubManagerAP
    {
        public static bool CreateDubFileAP(DTE2 dte2)
        {
            if (dte2 == null || dte2.Solution == null || dte2.Solution.Projects == null)
            {
                return false;
            }

            Project firstProject = dte2.Solution.Projects.Item(1);
            if (firstProject == null || string.IsNullOrEmpty(firstProject.FullName))
            {
                return false;
            }

            string projectDirectory = System.IO.Path.GetDirectoryName(firstProject.FullName);
            if (projectDirectory == null)
            {
                return false;
            }

            string fullPath = Path.Combine(projectDirectory, "dub.json");
            if (File.Exists(fullPath))
            {
                return false;
            }

            using (FileStream fileStream = File.Create(fullPath))
            {
                string content = "{}";
                byte[] contentBytes = Encoding.UTF8.GetBytes(content);

                fileStream.Write(contentBytes, 0, contentBytes.Length);
            }
            return true;
        }

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
                string projectDirectory = Path.GetDirectoryName(project.FullName);
                if (projectDirectory != null)
                {
                    string dubJsonPath = Path.Combine(projectDirectory, "dub.json");

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
                Name = "WelcomeToDockDAP",
                Description = "A new Dub.json file by DockDAP , AripaParsStudio",
                Dependencies = new Dictionary<string, string>()
            };
            string jsonString = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(path, jsonString);

            return config;

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
                string projectDirectory = Path.GetDirectoryName(dte2.Solution.Projects.Item(1).FullName);
                string newFilePath = Path.Combine(projectDirectory, "dub.json");
                return DubManagerAP.CreateDubFileWithDefaultsAP(newFilePath);
            }

        }
    }
}
