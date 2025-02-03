using Microsoft.AspNetCore.Mvc;
using PatientsMvc.Models;
using PatientsMvc.Service;
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


      /*  public IActionResult PatientDashboard()
        {
            List<Patient> listToShow = new ApiClient("http://localhost:8082").GetPatients();
            return View(listToShow);
        }*/

        //le patient peut voir la liste des docteurs
        public IActionResult DoctorDashboard()
        {
            List<Doctor> listToShow = new ApiClient("http://localhost:8080").GetDoctor();
            return View(listToShow);
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
