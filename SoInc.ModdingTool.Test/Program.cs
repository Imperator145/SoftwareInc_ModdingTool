using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoInc.ModdingTool.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var xml = new Logic.XmlManager<Logic.Data.SoftwareType>();

            xml.ReadFile("\\\\WDMYCLOUD\\Stefan\\Projekte\\Programmieren\\Eigene Projekte\\SoInc.ModdingTool\\TestMod\\SoftwareTypes\\Test.xml");


            Logic.ErrorManager.Current.WriteError(new Exception("HAllo"));
            System.Console.Read();
        }
    }
}
