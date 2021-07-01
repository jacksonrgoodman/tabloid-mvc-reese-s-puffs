using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        void DeletePost(int id);
        void UpdatePost(Post post);
        Post GetPostById(int id);
        List<Post> GetAllPublishedPosts();
        List<PostTag> GetTagByPostId(int PostId);
        Post GetPublishedPostById(int id);
        Post GetUserPostById(int id, int userProfileId);
        void AddTagToPost(int tag, int post);
        void RemoveTagFromPost(int tag, int post);
        List<Post> GetAllPostsFromUser(int userProfileId);
    }
}