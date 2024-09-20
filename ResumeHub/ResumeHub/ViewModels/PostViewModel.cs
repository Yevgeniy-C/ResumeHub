using System.ComponentModel.DataAnnotations;
using ResumeHub.DAL.Models;

namespace ResumeHub.ViewModels
{
	public class PostContentItemViewModel
	{
        public enum ContentItemTypeEnum { Text, Image, Title }        

		public int? PostContentId { get; set; }

		public int ContentItemType { get; set; }
		
        public string? Value { get; set; }
	}

	public class PostViewModel
	{
		public int? PostId { get; set; }

		[Required (ErrorMessage = "Title required")]
		public string? Title { get; set; }

		[Required (ErrorMessage = "Description required")]
		public string? Intro { get; set; }

		public List<PostContentItemViewModel> ContentItems { get; set; } = new List<PostContentItemViewModel>();

		public PostStatusEnum Status { get; set; }
    }
}

