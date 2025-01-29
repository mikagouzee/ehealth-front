using System.Net.Http.Headers;

namespace PatientsMvc.Models
{
    public class Data : IData
    {
        public List<Patient> PatientsList { get; set; }
       
        
        
        
        
        public List<Patient> PatientsInitializeData()
        {
            PatientsList = new List<Patient>();


            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8082/api/getAllPatient");////
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            // Get data response
            var response = client.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body
                var dataObjects = response.Content.ReadFromJsonAsync<IEnumerable<Patient>>().Result;//////
                foreach (var d in dataObjects)
                { 
                    Console.WriteLine("{0}", d.Name);/////
                    PatientsList.Add(d);////////
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode,
                              response.ReasonPhrase);
            }




            return PatientsList;
        }
      
        
        
        
        
        
        
        public Patient GetPatientById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return PatientsList.SingleOrDefault(a => a.Id == id);
            }
        }
    }
}
