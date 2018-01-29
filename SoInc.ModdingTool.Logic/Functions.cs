using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SoInc.ModdingTool.Logic
{
    /// <summary>
    /// Provides General FUnctions
    /// </summary>
    public static class Functions
    {
        /// <summary>
        /// reads the Text from the path and replaces the text
        /// </summary>
        /// <param name="path"></param>
        /// <param name="replacements">list of the replacements with this format: ##text##</param>
        /// <returns></returns>
        public static string CreateTemplateText(string path, List<Tuple<string,string>> replacements)
        {
            if (File.Exists(path))
            {
                var text = File.ReadAllText(path);
                ReplaceTemplate(text, replacements);
            }
            return "";
        }

        /// <summary>
        /// replaces the replacements with the text
        /// </summary>
        /// <param name="templateText"></param>
        /// <param name="replacements"></param>
        /// <returns></returns>
        public static string ReplaceTemplate(string templateText, List<Tuple<string, string>> replacements)
        {
            var text = templateText;
            foreach (var r in replacements)
            {
                text = text.Replace("##" + r.Item1 + "##", r.Item2);
            }
            return text;
        }

        /// <summary>
        /// Returns the CUrrent Version
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentVersion()
        {
            return System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
    }
}
