using ClinicManagement.Web.Common;
using ClinicManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using ClinicManagement.Web.Services;

namespace ClinicManagement.Web.Controllers
{
    public class CityController : Controller
    {
        private readonly IClinicService<City> _clinicCityService;

        public CityController(IClinicService<City> clinicCityService)
        {
            _clinicCityService = clinicCityService;
        }

        // GET: City
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCity()
        {
            //var cityList = _clinicCityService.GetAll().ToList(); 
            return Json(new { data = _clinicCityService.GetAll().ToList() }, JsonRequestBehavior.AllowGet);

        }

 
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            if (id == 0)
            {
                return View(new City());
            }
            else
            {
                    return View(_clinicCityService.Get(id));
                //return View(db.Cities.Where(x => x.Id == id).FirstOrDefault<City>());
                
            }
            
        }

        [HttpPost]
        public ActionResult AddOrEdit( City city)
        {
            _clinicCityService.SaveOrUpdate(city, city.Id);
                if (city.Id == 0)
                {
                
                    return Json(new { success = true, message = "Saved Succssesfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);

                }
         }

 
        [HttpPost]
        public ActionResult Delete(int id)
        {
                int numberPatients = _clinicCityService.CountRelation(id);

                if (numberPatients == 0)
                {
                    _clinicCityService.Delete(id);
                    return Json(new { success = true, message = "Запись удаленв!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Нельзя удалить т.к. имеются связи" }, JsonRequestBehavior.AllowGet);
                }
        }
    }
}

//public static string GetPrimaryKey(object attribute)
//{
//    string strPrimary = string.Empty;
//    IdAttribute attr = attribute as IdAttribute;
//    switch (attr.Strategy)
//    {
//        case GenerationType.INDENTITY:
//            break;
//        case GenerationType.SEQUENCE:
//            strPrimary = System.Guid.NewGuid().ToString();
//            break;
//        case GenerationType.TABLE:
//            break;
//    }

//    return strPrimary;
//}

//возвращение значения ключевого поля
//private object GetPrimaryKey(DbEntityEntry<City> entry)
//{
//    var myObject = entry.Entity;
//    var property = myObject.GetType().GetProperties().FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

//    var stringReturnData = property.GetValue(myObject, null).ToString();

//    Guid returningGuid;
//    var ifItIsGuid = Guid.TryParse(stringReturnData, out returningGuid);
//    if (ifItIsGuid)
//    {
//        return (Guid)property.GetValue(myObject, null);
//    }

//    int returningInt;
//    var ifItIsInt = int.TryParse(stringReturnData, out returningInt);
//    if (ifItIsInt)
//    {
//        return (int)property.GetValue(myObject, null);
//    }

//    long returningLong;
//    var ifItIsLong = long.TryParse(stringReturnData, out returningLong);
//    if (ifItIsLong)
//    {
//        return (long)property.GetValue(myObject, null);
//    }

//    return null;

//}