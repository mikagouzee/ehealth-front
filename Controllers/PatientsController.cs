using Microsoft.AspNetCore.Mvc;
using PatientsMvc.Models;
namespace PatientsMvc.Controllers
{
    public class PatientsController : Controller
    {


        private IData _tempData;


        public PatientsController(IData tempData)
        {
            _tempData = tempData;
        }




        public IActionResult Index()
        {
            List<Patient> patients = _tempData.PatientsInitializeData();

            IndexViewModel indexViewModel = new IndexViewModel
            {
                Patients = patients,
            };

            return View(indexViewModel);
        }

        public IActionResult Details(int? id)
        {

            var model = _tempData.GetPatientById(id);
            if (model == null)
            {
                return NotFound();
            }


            return View(model);


        }
    }
}
