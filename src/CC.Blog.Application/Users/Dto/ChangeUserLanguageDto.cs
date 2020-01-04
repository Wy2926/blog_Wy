using System.ComponentModel.DataAnnotations;

namespace CC.Blog.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}