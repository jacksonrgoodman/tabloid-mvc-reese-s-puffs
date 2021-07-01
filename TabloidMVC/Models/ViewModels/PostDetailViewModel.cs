using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostDetailViewModel
    {
        public Post Post { get; set; }
        public Tag Tag { get; set; }
        public List<Tag> Tags { get; set; }
        public List<PostTag> AssociatedTags { get; set; }
    }
}
