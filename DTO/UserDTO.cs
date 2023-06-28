using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DTO
{
    public class UserDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please Enter a Valid Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please Enter a Valid Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter a Valid E-Mail")]
        public string Email { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Please Enter fill Name Area")]
        public string Name { get; set; }
        public bool isAdmin { get; set; }
        [Display(Name="User Image")]
        public HttpPostedFileBase UserImage { get; set; }
    }
}
