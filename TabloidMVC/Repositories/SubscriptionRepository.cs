using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class SubscriptionRepository : BaseRepository, ICategoryRepository
    {
        public SubscriptionRepository(IConfiguration config) : base(config) { }

        public void CreateSubscription(Subscription subscription)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Subscription (SubscriberUserProfileId, )
                        OUTPUT INSERTED.ID
                        VALUES (@subscriptionUserProfileId)";

                    cmd.Parameters.AddWithValue("@name", subscription.SubscriberUserProfileId);

                    int id = (int)cmd.ExecuteScalar();

                    subscription.Id = id;
                }
            }
        }
    }
}
