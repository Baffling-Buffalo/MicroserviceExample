using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Models
{
    public class ListTreeNode
    {
        public List<ListTreeNode> Children = new List<ListTreeNode>();

        public bool ShouldSerializeChildren()
        {
            return (Children.Count > 0);
        }

        public ListTreeNode Parent { get; set; }

        public int Id { get; set; }

        public string ListName { get; set; }

        public string Description { get; set; }

        public int? NumberOfContacts { get; set; }

        public int? ParentId { get; set; }
    }
}
