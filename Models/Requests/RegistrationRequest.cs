namespace PatientsMvc.Models.Requests
{
    public class RegistrationRequest
    {
        public string Name {get; set;}
        public string eMail {get; set;}
        public string Password {get; set;}
        public string UserType {get; set;}
        public string Validation { get; set; }
    }
}
