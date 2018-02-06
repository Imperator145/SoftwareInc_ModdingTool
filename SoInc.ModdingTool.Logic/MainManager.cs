using SoInc.ModdingTool.Logic.Data;
using SoInc.ModdingTool.Logic.DataManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            get => softwareTypeManager = softwareTypeManager ?? new SoftwareTypeManager();
            set => softwareTypeManager = value;
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
            get => companyManager = companyManager ?? new CompanyManager();
            set => companyManager = value;
        }

        /// <summary>
        /// internal FIeld
        /// </summary>
        private CompanyTypeManager companyTypeManager;

        /// <summary>
        /// Gets or Sets the CompanyTypeManger
        /// </summary>
        public CompanyTypeManager CompanyTypeManager
        {
            get => companyTypeManager = companyTypeManager ?? new CompanyTypeManager();
            set => companyTypeManager = value;
        }

        /// <summary>
        /// internal Field
        /// </summary>
        private EventManager eventManager;

        /// <summary>
        /// Gets or Sets the EventManager
        /// </summary>
        public EventManager EventManager
        {
            get => eventManager = eventManager ?? new EventManager();
            set => eventManager = value;
        }

        /// <summary>
        /// internal Field
        /// </summary>
        private ScenarioManager scenarioManager;

        /// <summary>
        /// Gets or Sets the ScenarioManager
        /// </summary>
        public ScenarioManager ScenarioManager
        {
            get => scenarioManager = scenarioManager ?? new ScenarioManager();
            set => scenarioManager = value;
        }

        /// <summary>
        /// internal field
        /// </summary>
        private PersonalityManager personalityManager;

        /// <summary>
        /// Gets or sets the PersonalityManager
        /// </summary>
        public PersonalityManager PersonalityManager
        {
            get => personalityManager = personalityManager ?? new PersonalityManager();
            set => personalityManager = value;
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
            get => mod = mod ?? new Mod();
            set => mod = value;
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
            SoftwareTypeManager.SoftwareTypes.AddRange(IOManager.GetItems<SoftwareType>(path));

            //Companies
            path = Path.Combine(rootPath, "Companies");
            CompanyManager.Companies.AddRange(IOManager.GetItems<Company>(path));

            //CompanyTypes
            path = Path.Combine(rootPath, "CompanyTypes");
            CompanyTypeManager.CompanyTypes.AddRange(IOManager.GetItems<CompanyType>(path));

            //Events
            path = Path.Combine(rootPath, "Events");
            EventManager.Events.AddRange(IOManager.GetItems<Event>(path));

            //Scenarios
            path = Path.Combine(rootPath, "Scenarios");
            ScenarioManager.Scenarios.AddRange(IOManager.GetItems<Scenario>(path));

            //Personalities
            path = Path.Combine(rootPath, "Personalities.xml");
            PersonalityManager.List.AddRange(IOManager.GetItem<PersonalityGraph>(path));

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

                path = Path.Combine(Mod.Path, "SoftwareTypes", IOManager.CreatePathFriendlyName(st.Name) + ".xml");
                IOManager.WriteFileAsync(path, st);
            }

            //COmpanies
            foreach (var c in CompanyManager.Companies)
            {
                path = Path.Combine(mod.Path, "Companies", IOManager.CreatePathFriendlyName(c.Name) + ".xml");
                IOManager.WriteFileAsync(path, c);
            }

            //CompanyTypes
            foreach (var ct in CompanyTypeManager.CompanyTypes)
            {
                path = Path.Combine(mod.Path, "CompanyTypes", IOManager.CreatePathFriendlyName(ct.Specialization) + ".xml");
                IOManager.WriteFileAsync(path, ct);
            }

            //Events
            foreach (var e in EventManager.Events)
            {
                path = Path.Combine(mod.Path, "Events", IOManager.CreatePathFriendlyName(e.Name) + ".xml");
                IOManager.WriteFileAsync(path, e);
            }

            //Scenarios
            foreach (var s in ScenarioManager.Scenarios)
            {
                path = Path.Combine(mod.Path, "Scenarios", IOManager.CreatePathFriendlyName(s.Name) + ".xml");
                IOManager.WriteFileAsync(path, s);
            }

            //Personalities
            path = Path.Combine(mod.Path, "Personalities.xml");
            IOManager.WriteFileAsync(path, PersonalityManager.List);

            //Info-File
            path = Path.Combine(Mod.Path, "ReadMe.txt");
            if (!File.Exists(path))
                File.Create(path).Close();


            var rep = new List<Tuple<string, string>>
            {
                Tuple.Create("Year", DateTime.Now.Year.ToString()),
                Tuple.Create("Date", DateTime.Now.ToString("dd.MM.yyyy")),
                Tuple.Create("Creator", Mod.Creator),
                Tuple.Create("Name", Mod.Name)
            };


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
