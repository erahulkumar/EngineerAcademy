using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class AddressDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Please fill Address in area")]
        public string AddressContent { get; set; }
        [Required(ErrorMessage = "Please fill Email in area")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please fill Phone in area")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please fill Large Map in area")]
        public string MapPathLarge { get; set; }
        [Required(ErrorMessage = "Please fill Small Map in area")]
        public string MapPathSmall { get; set; }

    }
}
