using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.FormGroup
{
    public class GroupTreeNode
    {
        public List<GroupTreeNode> Children = new List<GroupTreeNode>();

        public bool ShouldSerializeChildren()
        {
            return (Children.Count > 0);
        }

        public GroupTreeNode Parent { get; set; }

        public int Id { get; set; }

        public string GroupName { get; set; }

        public string Description { get; set; }

        public int? NumberOfForms { get; set; }

        public int? ParentId { get; set; }
    }
}
