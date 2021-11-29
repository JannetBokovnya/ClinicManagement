using ClinicManagement.Web.Controllers;
using ClinicManagement.Web.Controllers.Api;
using ClinicManagement.Web.Helpers;
using ClinicManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ClinicManagement.Web.ViewModel
{
    public class PatientFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public Gender Sex { get; set; }
        [Required]
        [ValidDate]
        public string BirthDate { get; set; }


        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }

        public int City { get; set; }

        public DateTime Date { get; set; }

        public string Heading { get; set; }
        public string Token { get; set; }


        public DateTime GetBirthDate()
        {
            //TODO: Validate BirthDate 

            //return DateTime.Parse(string.Format("{0}", BirthDate));
            //return DateTime.Parse(BirthDate);
            return DateTime.ParseExact(BirthDate, "dd/MM/yyyy", CultureInfo.CurrentCulture);
        }

        public IEnumerable<City> Cities { get; set; }



        public string Action
        {
            get
            {
                Expression<Func<Controllers.Api.PatientController, IHttpActionResult>> update = (c => c.Put(this));
                Expression<Func<Controllers.Api.PatientController, IHttpActionResult>> create = (c => c.Post(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;

            }
        }

        #region for dropdownlist

        public IEnumerable<SelectListItem> GendersList
        {
            get { return ClinicMgtHelpers.GenderToSelectList(); }
            set { }
        }
        #endregion
    }
}