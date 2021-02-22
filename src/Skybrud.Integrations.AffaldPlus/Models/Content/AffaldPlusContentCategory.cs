using System.Xml.Linq;
using Newtonsoft.Json;
using Skybrud.Essentials.Xml;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Integrations.AffaldPlus.Models.Content {

    public class AffaldPlusContentCategory : XmlObjectBase {

        [JsonProperty("header")]
        public string Header { get; }

        [JsonProperty("body")]
        public string Body { get; }

        [JsonProperty("image")]
        public string Image { get; }

        public AffaldPlusContentCategory(XElement xml) : base(xml) {
            Header = xml.GetElementValue("KategoriHeader");
            Body = xml.GetElementValue("KategoriBody");
            Image = xml.GetElementValue("KategoriBillede");
        }

    }

}