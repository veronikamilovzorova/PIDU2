using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PIDU2.Models
{
    public class Peo
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Sisesta nimi")]

        public string Nimi { get; set; }


        public bool? WillAttend { get; set; }
    }
}