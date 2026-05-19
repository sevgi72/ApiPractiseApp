using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Kullanıcı adı gerekli")]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre gerekli")]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}