using System;
using System.Xml.Linq;
using Newtonsoft.Json;
using Skybrud.Essentials.Xml;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Integrations.AffaldPlus.Models.Content {

    public class AffaldPlusContentLink {

        #region Properties

        [JsonProperty("host")]
        public string Host { get; }

        [JsonProperty("url")]
        public string Url { get; }

        #endregion

        private AffaldPlusContentLink(string url) {
            try {
                Uri uri = new Uri(url);
                Url = url;
                Host = uri.Host;
            } catch {
                Url = url;
            }
        }

        public static AffaldPlusContentLink Parse(string url) {
            return String.IsNullOrWhiteSpace(url) ? null : new AffaldPlusContentLink(url);
        }

    }

}