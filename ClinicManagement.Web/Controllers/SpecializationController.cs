using ClinicManagement.Web.Common;
using ClinicManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ClinicManagement.Web.Services;

namespace ClinicManagement.Web.Controllers
{
    public class SpecializationController : Controller
    {

        private readonly IClinicService<Specialization> _clinicSpecService;

        public SpecializationController(IClinicService<Specialization> clinicSpecService)
        {
            _clinicSpecService = clinicSpecService;
        }

        // GET: Specialization
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSpecialization()
        {

            return Json(new { data = _clinicSpecService.GetAll().ToList() }, JsonRequestBehavior.AllowGet);
            //using (ClinicContext db = new ClinicContext())
            //{
            //    List<Specialization> specList = db.Specializations.ToList<Specialization>();

            //    return Json(new { data = specList }, JsonRequestBehavior.AllowGet);
            //}

        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Specialization());
            }
            else
            {
                return View(_clinicSpecService.Get(id));
                //using (ClinicContext db = new ClinicContext())
                //{
                //    return View(db.Specializations.Where(x => x.Id == id).FirstOrDefault<Specialization>());
                //}

            }

        }

        [HttpPost]
        public ActionResult AddOrEdit(Specialization spec)
        {
            _clinicSpecService.SaveOrUpdate(spec, spec.Id);
            if (spec.Id == 0)
            {

                return Json(new { success = true, message = "Saved Succssesfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);

            }

        }

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{

        //    int numberDoctors = _clinicSpecService.CountRelation(id);

        //    if (numberDoctors == 0)
        //    {
        //        _clinicSpecService.Delete(id);
        //        return Json(new { success = true, message = "Запись удаленв!" }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Нельзя удалить т.к. имеются связи" }, JsonRequestBehavior.AllowGet);
        //    }
        //    //using (ClinicContext db = new ClinicContext())
        //    //{

        //    //    int numberDoctors = db.Doctors.Count(x => x.SpecializationId == id);

        //    //    if (numberDoctors == 0)
        //    //    {
        //    //        Specialization spec = db.Specializations.Where(x => x.Id == id).FirstOrDefault<Specialization>();
        //    //        db.Specializations.Remove(spec);
        //    //        db.SaveChanges();
        //    //        return Json(new { success = true, message = "Запись удалена!" }, JsonRequestBehavior.AllowGet);

        //    //    }
        //    //    else
        //    //    {
        //    //        return Json(new { success = false, message = "Нельзя удалить т.к. имеются связи" }, JsonRequestBehavior.AllowGet);
        //    //    }


        //    //}
        //}
    }
}