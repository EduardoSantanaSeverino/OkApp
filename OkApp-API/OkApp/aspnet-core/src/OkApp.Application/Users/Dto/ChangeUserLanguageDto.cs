using System.ComponentModel.DataAnnotations;

namespace OkApp.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
