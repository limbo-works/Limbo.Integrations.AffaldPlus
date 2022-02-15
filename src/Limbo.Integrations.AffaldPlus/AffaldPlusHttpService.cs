using Limbo.Integrations.AffaldPlus.Responses;

namespace Limbo.Integrations.AffaldPlus {

    public class AffaldPlusHttpService {

        public AffaldPlusHttpClient Client { get; }

        public AffaldPlusHttpService() {
            Client = new AffaldPlusHttpClient();
        }

        public AffaldPlusGetSuggestionsResponse GetSuggestions(int municipalityId, string text) {
            return AffaldPlusGetSuggestionsResponse.ParseResponse(Client.GetSuggestions(municipalityId, text));
        }

        public AffaldPlusGetContentResponse GetContent(int municipalityId, int contentId) {
            return AffaldPlusGetContentResponse.ParseResponse(Client.GetContent(municipalityId, contentId));
        }

    }

}