using Limbo.Integrations.AffaldPlus.Responses;

namespace Limbo.Integrations.AffaldPlus {

    public class AffaldPlusService {

        public AffaldPlusClient Client { get; }

        public AffaldPlusService() {
            Client = new AffaldPlusClient();
        }

        public AffaldPlusGetSuggestionsResponse GetSuggestions(int municipalityId, string text) {
            return AffaldPlusGetSuggestionsResponse.ParseResponse(Client.GetSuggestions(municipalityId, text));
        }

        public AffaldPlusGetContentResponse GetContent(int municipalityId, int contentId) {
            return AffaldPlusGetContentResponse.ParseResponse(Client.GetContent(municipalityId, contentId));
        }

    }

}