using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form.API.Models
{
    public class ComboTreeNode
    {
        [JsonProperty(PropertyName = "subs")]
        public List<ComboTreeNode> Children = new List<ComboTreeNode>();

        public bool ShouldSerializeChildren()
        {
            return (Children.Count > 0);
        }

        public ComboTreeNode Parent { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "parentId")]
        public int? ParentId { get; set; }
    }
}
