using System.ComponentModel.DataAnnotations;

namespace ResumeHub.ViewModels
{
	public class ProfileViewModel
	{
        public int? ProfileId { get; set; }
        [Required]
        public string? ProfileName { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }

        public string? ProfileImage { get; set; }
        
        public DAL.Models.ProfileStatusEnum ProfileStatus { get; set; }
    }
}

