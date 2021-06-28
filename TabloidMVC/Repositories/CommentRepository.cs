using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;


namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) :base(config) { }
        public List<Comments> GetAllComments()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Comment";

                    var reader = cmd.ExecuteReader();

                    var comments = new List<Comments>();

                    while (reader.Read())
                    {
                        comments.Add(NewCommentFromUser(reader));
                    }
                    reader.Close();

                    return comments;
                }
            }
        }
    }
}
