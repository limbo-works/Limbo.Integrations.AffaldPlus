using Newtonsoft.Json;

namespace Skybrud.Integrations.AffaldPlus.Models.Suggestions {

    public class AffaldPlusSuggestion {

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("name")]
        public string Name { get; }

        public AffaldPlusSuggestion(int id, string name) {
            Id = id;
            Name = name;
        }

    }

}