using System.Collections.Generic;

namespace DataManager.XmlGenerator
{
    public interface IXmlGeneratorService<in T>
    {
        void GenerateXml(string path, IEnumerable<T> enumerable);
    }
}