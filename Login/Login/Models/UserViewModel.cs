using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class UserViewModel
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PassWord { get; set; }

        [Compare("PassWord", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}
