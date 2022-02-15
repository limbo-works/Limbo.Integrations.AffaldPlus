using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Limbo.Integrations.AffaldPlus.Models.Suggestions {

    public class AffaldPlusSuggestionList {

        [JsonProperty("items")]
        public AffaldPlusSuggestion[] Items { get; }

        public AffaldPlusSuggestionList(IEnumerable<AffaldPlusSuggestion> items) {
            Items = items.ToArray();
        }

    }

}