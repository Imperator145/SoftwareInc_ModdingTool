using SoInc.ModdingTool.Logic.Data;
using SoInc.ModdingTool.Logic.DataManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO: NeedUpdate bei verschiendensten Dingen in der Logik verwenden -> Listen

namespace SoInc.ModdingTool.Logic
{
    /// <summary>
    /// Represents the Infrastructure Object
    /// </summary>
    public class MainManager
    {
        /// <summary>
        /// fires when the whole window needs to be Updated
        /// </summary>
        public event EventHandler NeedUpdate;

        /// <summary>
        /// fires the NeedUpdate event
        /// </summary>
        protected void OnNeedUpdate() => this.NeedUpdate?.Invoke(this, new EventArgs());

        /// <summary>
        /// Internal Field
        /// </summary>
        private SoftwareTypeManager softwareTypeManager;

        /// <summary>
        /// Gets or Sets the SoftwareTypeManager
        /// </summary>
        public SoftwareTypeManager SoftwareTypeManager
        {
            get
            {
                if (softwareTypeManager == null)
                {
                    softwareTypeManager = new SoftwareTypeManager();
                }
                return softwareTypeManager;
            }
            set { softwareTypeManager = value; }
        }

        /// <summary>
        /// Internal Field
        /// </summary>
        private CompanyManager companyManager;

        /// <summary>
        /// Gets or Sets the CompanyManager
        /// </summary>
        public CompanyManager CompanyManager
        {
            get {
                if (companyManager == null)
                    companyManager = new CompanyManager();
                return companyManager;
            }
            set { companyManager = value; }
        }


        /// <summary>
        /// Internal Field
        /// </summary>
        private Mod mod;

        /// <summary>
        /// gets or sets the Current Mod
        /// </summary>
        public Mod Mod
        {
            get
            {
                if (mod == null)
                {
                    mod = new Mod();
                }
                return mod;
            }
            set { mod = value; }
        }

        /// <summary>
        /// gets the Template for the infofile
        /// </summary>
        public string InfoTextTemplate { get; set; }

        /// <summary>
        /// Internal Field
        /// </summary>
        private static MainManager current;

        /// <summary>
        /// Gets the Current MainManager
        /// </summary>
        public static MainManager Current
        {
            get
            {
                if (current == null)
                {
                    current = new MainManager();
                }
                return current;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MainManager()
        {

        }

        /// <summary>
        /// opens the Mod From the Path
        /// </summary>
        public void Open()
        {
            this.Open(Mod.Path);
            OnNeedUpdate();
        }

        /// <summary>
        /// opens the Mod From the path
        /// </summary>
        /// <param name="path"></param>
        public void Open(string rootPath)
        {
            Mod.Path = rootPath;

            //SoftwareTypes
            var path = Path.Combine(rootPath, "SoftwareTypes");

            if (Functions.CheckDirectoryOrFile(path))
            {
                var files = Directory.GetFiles(path);

                var xmlManager = new XmlManager<SoftwareType>();
                foreach (var f in files)
                {
                    SoftwareType st = xmlManager.ReadFile(f);
                    SoftwareTypeManager.SoftwareTypes.Add(st);
                }
            }

            //Companies
            path = Path.Combine(rootPath, "Companies");
            if(Functions.CheckDirectoryOrFile(path))
            {
                var files = Directory.GetFiles(path);

                var xmlManager = new XmlManager<Company>();
                foreach (var f in files)
                {
                    Company c = xmlManager.ReadFile(f);
                    CompanyManager.Companies.Add(c);
                }
            }


            //TODO: Read Other Components


            OnNeedUpdate();
        }

        /// <summary>
        /// Saves the Whole Mod inside the Path
        /// </summary>
        public void Save()
        {
            string path = "";
            //SoftwareTypes
            foreach (var st in SoftwareTypeManager.SoftwareTypes)
            {
                path = Path.Combine(Mod.Path, "SoftwareTypes", Functions.CreatePathFriendlyName(st.Name)+".xml");
                var xmlManager = new XmlManager<SoftwareType>();
                xmlManager.WriteFile(path, st);
            }

            //COmpanies
            foreach(var c in CompanyManager.Companies)
            {
                path = Path.Combine(mod.Path, "Companies", Functions.CreatePathFriendlyName(c.Name) + ".xml");
                var xmlManager = new XmlManager<Company>();
                xmlManager.WriteFile(path, c);
            }



            //TODO: Write Other Components



            //Info-File
            path = Path.Combine(Mod.Path, "Info.txt");
            if (!File.Exists(path)) File.Create(path).Close();
            

            var rep = new List<Tuple<string, string>>();
            rep.Add(Tuple.Create("Year", DateTime.Now.Year.ToString()));
            rep.Add(Tuple.Create("Date", DateTime.Now.ToString("dd.MM.yyyy")));
            rep.Add(Tuple.Create("Creator", Mod.Creator));
            rep.Add(Tuple.Create("Name", Mod.Name));


            var text = Functions.ReplaceTemplate(InfoTextTemplate, rep);
            File.WriteAllText(path, text);
        }

        /// <summary>
        /// clears the whole Mod without saving any changes
        /// </summary>
        public void Clear()
        {
            Mod.Path = "";
            SoftwareTypeManager = null;
            Mod = null;
            OnNeedUpdate();
        }

        /// <summary>
        /// forces The Update-Event
        /// </summary>
        public void Update()
        {
            OnNeedUpdate();
        }
    }
}
