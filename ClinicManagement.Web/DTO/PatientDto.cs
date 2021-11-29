using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicManagement.Web.DTO
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public CityDto City { get; set; }

        public int Age { get; set; }

        //public int Age
        //{
        //    get
        //    {
        //        var now = DateTime.Today;
        //        var age = now.Year - BirthDate.Year;
        //        if (BirthDate > now.AddYears(-age)) age--;
        //        return age;
        //    }

        //}
        public int DoctorId { get; set; }
        //public DoctorDto Doctor { get; set; }

        public DateTime DateTime { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
    }
}