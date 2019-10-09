using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Services.ModelDTOs
{
    public class ComboTreeNodeDTO
    {
        [JsonProperty(PropertyName = "subs")]
        public List<ComboTreeNodeDTO> Children = new List<ComboTreeNodeDTO>();

        public bool ShouldSerializeChildren()
        {
            return (Children.Count > 0);
        }

        public ComboTreeNodeDTO Parent { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "parentId")]
        public int? ParentId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Name { get; set; }
    }
}
