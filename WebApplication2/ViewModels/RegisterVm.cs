using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
    public class RegisterVm
    {
        [Required(ErrorMessage = "Ad Soyad gerekli")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı gerekli")]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email gerekli")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi girin")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gerekli")]
        [StringLength(100, MinimumLength = 6,
            ErrorMessage = "Şifre en az 6 karakter olmalı")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre onayı gerekli")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}