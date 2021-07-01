using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostEditViewModel
    {
        public Post post { get; set; }
        public List <Category> categories { get; set; }

    }
}
