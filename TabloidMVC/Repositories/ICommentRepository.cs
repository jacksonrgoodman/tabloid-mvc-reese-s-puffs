using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
       
        List<Comments> GetCommentsByPostId(int id);
        Comments GetCommentById(int id);

        void AddComment(Comments comment);

        void DeleteComment(int id);
    }
}
