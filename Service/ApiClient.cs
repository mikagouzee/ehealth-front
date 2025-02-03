using PatientsMvc.Models;
namespace PatientsMvc.Service
{
    public class ApiClient
    {
        public string basUri { get; set; }
        public ApiClient(string uri)
        {
            basUri = uri;
        }
        public HttpResponseMessage CallBackend(string endpoint)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(basUri+endpoint);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client.GetAsync("").Result;///à revoir 
        }
        public List<Doctor> GetDoctor()
        {
            List<Doctor> result = null;
            string patientEndpont = "/user/doctors";
            var serverResponse = CallBackend(patientEndpont);
            if (serverResponse.IsSuccessStatusCode)
            {
                var dataObjects = serverResponse.Content.ReadFromJsonAsync<IEnumerable<Doctor>>().Result;
                foreach(var d in dataObjects)
                {
                    Console.WriteLine("{0}", d.Name);
                }
                result = dataObjects.ToList();
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)serverResponse.StatusCode, serverResponse.ReasonPhrase);
            }
            return result;
        }
    }
}
