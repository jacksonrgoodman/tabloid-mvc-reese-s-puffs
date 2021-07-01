using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TabloidMVC.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserProfileId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        [DisplayName("Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDateTime { get; set; } 
        public UserProfile UserProfile { get; set; }
        public Post Post { get; set; }

        public string DateString()
        {
            return CreateDateTime.ToShortDateString();
        }
    }
}
