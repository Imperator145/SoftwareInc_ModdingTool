using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SoInc.ModdingTool.Logic
{
    public class XmlManager<T> :IOManager where T : class, new()
    {
        /// <summary>
        /// Reads the object from xml
        /// </summary>
        /// <param name="path"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public T ReadFile(string path, bool create = false)
        {
            try
            {
                if (CheckPath(path, create))
                {
                    var res = new T();

                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    res = (T)serializer.Deserialize(new FileStream(path, FileMode.Open));

                    return res;
                }
            }
            catch(System.Exception ex)
            {
                ErrorManager.Current.WriteError(ex);
            }

            return null;
        }

        /// <summary>
        /// saves the xml in the specified folder
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        public void WriteFile(string path, T data)
        {
            if (CheckPath(path, true))
            {
                if (!path.EndsWith(".xml"))
                    path += ".xml";

                System.Xml.Serialization.XmlSerializer serializer;
                XmlWriter writer = null;
                try
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    writer = XmlWriter.Create(path, new XmlWriterSettings() { Indent = true });
                    serializer.Serialize(writer, data);
                }
                catch (Exception ex)
                {
                    ErrorManager.Current.WriteError(ex);
                }
                finally
                {
                    if (writer != null && writer.WriteState != WriteState.Closed)
                        writer.Close();
                }
            }
        }
    }
}
