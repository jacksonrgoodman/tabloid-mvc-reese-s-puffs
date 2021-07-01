using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        
        public int SubscriberUserProfileId { get; set; }

        public int ProviderUserProfileId { get; set; }

        public DateTime BeginDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
