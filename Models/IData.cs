namespace PatientsMvc.Models
{
    public interface IData
    {
        List<Patient> PatientsList { get; set; }
        List<Patient> PatientsInitializeData();
        Patient GetPatientById(int? id);
    }
}
 