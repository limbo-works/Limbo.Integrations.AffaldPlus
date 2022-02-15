using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Limbo.Integrations.AffaldPlus.Models.Suggestions;
using Skybrud.Essentials.Http;
using Skybrud.Essentials.Xml.Extensions;

namespace Limbo.Integrations.AffaldPlus.Responses {

    public class AffaldPlusGetSuggestionsResponse : AffaldPlusResponse<AffaldPlusSuggestionList> {
        
        #region Constructors

        private AffaldPlusGetSuggestionsResponse(IHttpResponse response) : base(response) {

            // Validate the response
            ValidateResponse(response);

            // Declare the namespaces used in the response body
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(new NameTable());
            nsmgr.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ns1", "urn:xmethods-delayed-quotes");

            // Parse the outer XML body
            XElement body = XElement.Parse(response.Body);

            // Parse the inner XML body
            XElement datapakke = XElement.Parse(body.GetElementValue("SOAP-ENV:Body/ns1:genXMLSoegeOrdResponse/GetDatapakke", nsmgr));

            // Parse the suggestions
            List<AffaldPlusSuggestion> temp = new List<AffaldPlusSuggestion>();
            foreach (XElement element in datapakke.GetElements("Data/Soegeord")) {
                int id = element.GetElementValueAsInt32("SoegeordID");
                string name = element.GetElementValue("SoegeordLabel");
                temp.Add(new AffaldPlusSuggestion(id, name));
            }

            Body = new AffaldPlusSuggestionList(temp);

        }

        #endregion

        #region Static methods

        public static AffaldPlusGetSuggestionsResponse ParseResponse(IHttpResponse response) {
            return response == null ? null : new AffaldPlusGetSuggestionsResponse(response);
        }

        #endregion

    }

}