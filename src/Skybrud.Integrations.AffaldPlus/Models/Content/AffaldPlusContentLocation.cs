using System.Xml.Linq;
using Newtonsoft.Json;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Integrations.AffaldPlus.Models.Content {

    public class AffaldPlusContentLocation {

        [JsonProperty("header")]
        public string Header { get; }

        [JsonProperty("body")]
        public string Body { get; }

        [JsonProperty("link")]
        public AffaldPlusContentLink Link { get; }

        [JsonProperty("image")]
        public string Image { get; }

        public AffaldPlusContentLocation(XElement xml) {
            Header = xml.GetElementValue("AfleveringsStedHeader");
            Body = xml.GetElementValue("AfleveringsStedBody");
        }

        public AffaldPlusContentLocation(string header, string body, string link, string image) {
            Header = header;
            Body = body;
            Link = AffaldPlusContentLink.Parse(link);
            Image = image;
        }

    }

}