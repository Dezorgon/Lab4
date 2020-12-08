using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml.Serialization;

namespace DataManager.XmlGenerator
{
    public class XmlGeneratorService<T> : IXmlGeneratorService<T>
    {
        public void GenerateXml(string path, IEnumerable<T> enumerable)
        {
            var xmlSerializer = new XmlSerializer(typeof(IEnumerable<T>));
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, enumerable);
            }
        }
    }
}