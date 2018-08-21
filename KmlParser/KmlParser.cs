using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace KmlUtilities
{
    public class KmlParser
    {
        public string FixBaseCampKml(string kmlFilePath)
        {
            var kml = XDocument.Load(kmlFilePath);
            XNamespace ns = "http://www.opengis.net/kml/2.2";

            kml.Descendants($"{ns}name")
                .Where(d => d.Value == "Via Points")
                .Select(d => d.Parent)
                .Remove();

            var routesFolder = kml.Descendants($"{ns}name")
                .Single(d => d.Value == "Routes")
                .Parent;

            var routes = routesFolder
                .Descendants($"{ns}Placemark")
                .ToList();

            routesFolder.Descendants($"{ns}Folder")
                .Remove();

            var newRoutesFolder = new XElement($"{ns}Folder");
            newRoutesFolder.Add(new XElement($"{ns}name", Path.GetFileNameWithoutExtension(kmlFilePath)));
            newRoutesFolder.Add(routes);
            routesFolder.Add(newRoutesFolder);

            var newFilePath = Path.Combine(Path.GetDirectoryName(kmlFilePath),
                Path.GetFileNameWithoutExtension(kmlFilePath) + " (Updated).kml");

            kml.Save(newFilePath);

            return newFilePath;
        }
    }
}
