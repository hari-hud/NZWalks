using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class RegisterRequestDto 
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public String Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public String[] Roles {get; set;}
    }
}
