using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClinicManagement.Web.Models
{
    //справочник город
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public string Name { get; set; }
    }
}