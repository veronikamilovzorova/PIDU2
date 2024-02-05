using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace PIDU2.Models

{
    public class Guest
    {
        public int Id { get; set; }
        [Required(ErrorMessage= "sisestage")]
        public string Name { get; set; }
        [Required(ErrorMessage = "oma andmed")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "-")]

        public string Email { get; set; }
        [Required(ErrorMessage = "kutse saatmiseks")]
        [RegularExpression(@"\+372.+", ErrorMessage = "- ")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "AITÄH!")]

        public bool? WillAttend { get; set; }
    }
}
