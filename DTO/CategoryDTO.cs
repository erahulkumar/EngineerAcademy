using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace DTO
{
    public class CategoryDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Please fill Category Name area")]
        public string CategoryName { get; set; }

    }
}
