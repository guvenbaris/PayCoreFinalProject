using System.ComponentModel.DataAnnotations;

namespace PayCore.Domain.Jwt
{
    public class TokenRequest
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
