using System;
using System.Xml.Linq;
using Newtonsoft.Json;
using Skybrud.Essentials.Xml;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Integrations.AffaldPlus.Models.Content {

    public class AffaldPlusContentResult : XmlObjectBase {

        #region Properties

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("category")]
        public AffaldPlusContentCategory Category { get; }

        [JsonProperty("home")]
        public AffaldPlusContentLocation Home { get; }

        [JsonIgnore]
        public bool HasHome => Home != null;

        [JsonProperty("recycling")]
        public AffaldPlusContentLocation RecyclingStation { get; }

        [JsonIgnore]
        public bool HasRecyclingStation => RecyclingStation != null;

        #endregion

        public AffaldPlusContentResult(XElement xml) : base(xml) {

            Id = xml.GetElementValueAsInt32("SoegeOrdID");
            Name = xml.GetElementValue("SoegeOrd");
            Category = new AffaldPlusContentCategory(xml);

            Home = ParseHome(xml);
            RecyclingStation = ParseGenbrugsplads(xml);

        }

        private AffaldPlusContentLocation ParseHome(XElement xml) {

            string header = xml.GetElementValue("AfleveringsSted/AfleveringsStedHeader");
            string body = xml.GetElementValue("AfleveringsSted/AfleveringsStedBody");;

            if (String.IsNullOrWhiteSpace(header + body)) return null;

            return new AffaldPlusContentLocation(header, body, String.Empty, String.Empty);

        }

        private AffaldPlusContentLocation ParseGenbrugsplads(XElement xml) {

            string header = xml.GetElementValue("AfleveringsStedGenbrugspladsHeader");
            string body = xml.GetElementValue("AfleveringsStedGenbrugspladsBody");
            string link = xml.GetElementValue("AfleveringsStedGenbrugspladsLink");
            string billede = xml.GetElementValue("AfleveringsStedGenbrugspladsBillede");

            if (String.IsNullOrWhiteSpace(header + body + link + billede)) return null;

            return new AffaldPlusContentLocation(header, body, link, billede);

        }

    }

}