using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class SubscriptionRepository : BaseRepository, ISubscriptionRepository
    {
        public SubscriptionRepository(IConfiguration config) : base(config) { }

        public void CreateSubscription(Subscription subscription)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Subscription (SubscriberUserProfileId, ProviderUserProfileId)
                        OUTPUT INSERTED.ID
                        VALUES (@subscriptionUserProfileId, @providerUserProfileId)";

                    cmd.Parameters.AddWithValue("@subscriptionUserProfileId", subscription.SubscriberUserProfileId);
                    cmd.Parameters.AddWithValue("@providerUserProfileId", subscription.ProviderUserProfileId);
                    

                    int id = (int)cmd.ExecuteScalar();

                    subscription.Id = id;
                }
            }
        }
    }
}
