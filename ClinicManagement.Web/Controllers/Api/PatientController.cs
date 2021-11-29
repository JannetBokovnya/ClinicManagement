using AutoMapper;
using ClinicManagement.Web.DTO;
using ClinicManagement.Web.Models;
using ClinicManagement.Web.Services;
using ClinicManagement.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClinicManagement.Web.Controllers.Api
{
    public class PatientController : ApiController
    {
        private readonly IClinicService<Patient> _patientService;
        private readonly IMapper _mapper;

        public PatientController(IClinicService<Patient> patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }
        public IHttpActionResult GetPatients()
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Patient, PatientDto>());
            //var mapper = new Mapper(config);
            //var patientsList = mapper.Map<List<PatientDto>>(_patientService.GetAll().ToList());

            // Здесь мы преобразуем объект типа Patient в объект типа PatientDto при помощи Automapper.
            // Настройку маппинга проводим в отдельном файле (MappingProfile) 
            var patientsList = _mapper.Map<List<PatientDto>>(_patientService.GetAllWithInclude().ToList());
            return Ok(patientsList);
        }

        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Post([FromBody]PatientFormViewModel patientViewModel)
        {

            //if (!ModelState.IsValid)
            //{
            //    //patientViewModel.Cities = _unitOfWork.Cities.GetCities();
            //    //return View("PatientForm", viewModel);

            //}

            var patient = new Patient
            {
                FirstName = patientViewModel.FirstName,
                LastName = patientViewModel.LastName,
                Phone = patientViewModel.Phone,
                Address = patientViewModel.Address,
                DateTime = DateTime.Now,
                BirthDate = patientViewModel.GetBirthDate(),
                Height = patientViewModel.Height,
                Weight = patientViewModel.Weight,
                CityId = patientViewModel.City,
                Sex = patientViewModel.Sex,
                Id = patientViewModel.Id,
                Token = (2020 + _patientService.GetAll().Count()).ToString().PadLeft(7, '0')

        };

           
            _patientService.SaveOrUpdate(patient, patient.Id);

            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.Created, patient);
            // var msg = Request.CreateResponse(HttpStatusCode.BadRequest);
            // Location заголовок стоит создавать, если новый элемент был создан
           // msg.Headers.Location = new Uri(Request.RequestUri + "/" + (_fruits.Count - 1));
            //return msg;
            return Json(new { success = true, message = "Сохранено!" });
        }

        public IHttpActionResult Put([FromBody]PatientFormViewModel patientViewModel)
        {
            var patient = new Patient
            {
                FirstName = patientViewModel.FirstName,
                LastName = patientViewModel.LastName,
                Phone = patientViewModel.Phone,
                Address = patientViewModel.Address,
                DateTime = DateTime.Now,
                BirthDate = patientViewModel.GetBirthDate(),
                Height = patientViewModel.Height,
                Weight = patientViewModel.Weight,
                CityId = patientViewModel.City,
                Sex = patientViewModel.Sex,
                Id = patientViewModel.Id,
                Token = patientViewModel.Token
            };

            _patientService.SaveOrUpdate(patient, patient.Id);
            return Json(new { success = true, message = "Сохранено!" });
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _patientService.Delete(id);
            return Json(new { success = true, message = "Запись удалена!" });
        }
        //public IHttpActionResult Delete(int id)
        //{
        //    //int numberDoctors = _clinicSpecService.CountRelation(id);

        //    //if (numberDoctors == 0)
        //    //{
        //    //    _clinicSpecService.Delete(id);
        //    //    return Json(new { success = true, message = "Запись удалена!" });
        //    //}
        //    //else
        //    //{
        //    //    return Json(new { success = false, message = "Нельзя удалить т.к. имеются связи" });
        //    //}


        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(PatientFormViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        viewModel.Cities = _unitOfWork.Cities.GetCities();
        //        return View("PatientForm", viewModel);

        //    }

        //    var patient = new Patient
        //    {
        //        Name = viewModel.Name,
        //        Phone = viewModel.Phone,
        //        Address = viewModel.Address,
        //        DateTime = DateTime.Now,
        //        BirthDate = viewModel.GetBirthDate(),
        //        Height = viewModel.Height,
        //        Weight = viewModel.Weight,
        //        CityId = viewModel.City,
        //        Sex = viewModel.Sex,
        //        Token = (2018 + _unitOfWork.Patients.GetPatients().Count()).ToString().PadLeft(7, '0')
        //    };

        //    _unitOfWork.Patients.Add(patient);
        //    _unitOfWork.Complete();
        //    return RedirectToAction("Index", "Patients");

        //    // TODO: BUG redirect to detail 
        //    //return RedirectToAction("Details", new { id = viewModel.Id });
        //}

        //[HttpPost]
        //public ActionResult AddOrEdit(Specialization spec)
        //{
        //    _clinicSpecService.SaveOrUpdate(spec, spec.Id);
        //    if (spec.Id == 0)
        //    {

        //        return Json(new { success = true, message = "Saved Succssesfully" }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);

        //    }

        //}
    }
}
