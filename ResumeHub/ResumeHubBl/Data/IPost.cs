using System;
using ResumeHub.DAL.Models;

namespace ResumeHub.BL.Data
{
	public interface IPost
	{
        Task<PostModel> GetPost(int postId);

        Task<int> AddOrUpdate(PostModel model);

        Task<List<PostContentModel>> GetPostItems(int postId);
        
        Task AddOrUpdateContentItems(IEnumerable<PostContentModel> items);
    }
}

