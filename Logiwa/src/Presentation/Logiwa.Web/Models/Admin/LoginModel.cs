using System.ComponentModel.DataAnnotations;

namespace Logiwa.Web.Models.Admin
{
    public class LoginModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
