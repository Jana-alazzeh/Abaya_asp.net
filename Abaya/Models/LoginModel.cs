using System.ComponentModel.DataAnnotations;

namespace Abaya.Models
{
    public class LoginModel
    {

        [Display(Name = "تذكرني؟")]
        public bool RememberMe { get; set; }
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
        [EmailAddress]
        public string User_Email { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
