using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DTO
{
    public class SocialMediaDTO
    {
        //model of Social Media
        public int ID { get; set; }

        [Required(ErrorMessage ="Please fill the name area")]

        public string Name { get; set; }

        public string ImagePath { get; set; }

        [Required(ErrorMessage ="fill size of image")]

        public string Size { get; set; }

        [Required(ErrorMessage ="Please fill link area")]

        public string Link { get; set; }

        [Display(Name ="Image")]

        public HttpPostedFileBase SocialImage { get; set; }
    }
}
