using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();
        Tag GetTagById(int id);
        void AddTag(Tag tag);
        void AddTagToPost(int tag, int post);
        void RemoveTagFromPost(int tag, int post);
        void UpdateTag(Tag tag);
        void DeleteTag(int tagId);
    }
}
