using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class CommentViewModel
    {
        public Post Post { get; set; }
        public Comments Comment { get; set; }

        public List<Comments> Comments { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
