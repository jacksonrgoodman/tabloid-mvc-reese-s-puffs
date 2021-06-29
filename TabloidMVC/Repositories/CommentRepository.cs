﻿using Microsoft.Data.SqlClient;
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
        public List<Comments> GetCommentsByPostId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select up.DisplayName as AuthorName, c.Subject, c.Content,
                    c.CreateDateTime as Date, p.Title as Title  
                    from Comment c
                    left Join Post p on c.PostId = p.Id
                    left join UserProfile up on c.UserProfileId = up.Id
                    where p.id = @id
                    order by c.CreateDateTime desc;";

                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Comments> comments = new List<Comments>();
                    while (reader.Read())
                    {
                        Comments comment = new Comments()
                        {
                            //Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            //PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("Date")),
                            UserProfile = new UserProfile
                            {
                                DisplayName = reader.GetString(reader.GetOrdinal("AuthorName"))
                            }

                        };
                        comments.Add(comment);
                    }
                        
                    reader.Close();
                    return comments;
                }
            }
        }
        public void AddComment(Comments comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Comment (PostId, Subject, Content, CreateDateTime, UserProfile.DisplayName) OUTPUT INSTERTED.ID VALUES (@PostId, @Subject, @Content, @CreateDateTime, @UserProfile.DisplayName)";
                    cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                    cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", comment.CreateDateTime);
                    cmd.Parameters.AddWithValue("@UserProfile.DisplayName", comment.UserProfile.DisplayName);

                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

    }

}
