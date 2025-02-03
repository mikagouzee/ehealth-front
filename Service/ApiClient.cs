using PatientsMvc.Models;
using PatientsMvc.Models.Requests;
namespace PatientsMvc.Service
{
    public class ApiClient
    {
        public string basUri { get; set; }
        public ApiClient(string uri)
        {
            basUri = uri;
        }

        private HttpResponseMessage Get(string endpoint)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(basUri + endpoint);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client.GetAsync("").Result;
        }

        private HttpResponseMessage Post<T>(string endpoint, T body)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(basUri + endpoint);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client.PostAsJsonAsync<T>(endpoint, body).Result;
        }
        
        public List<Doctor> GetDoctor()
        {
            List<Doctor> result = null;
            string patientEndpont = "/user/doctors";
            var serverResponse = Get(patientEndpont);
            if (serverResponse.IsSuccessStatusCode)
            {
                var dataObjects = serverResponse.Content.ReadFromJsonAsync<IEnumerable<Doctor>>().Result;
                foreach (var d in dataObjects)
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

        public void Register(RegistrationRequest request)
        {
            string endpoint = "/user/create";
            var serverResponse = Post(endpoint, request);
            
            if (serverResponse.IsSuccessStatusCode)
            {
                Console.Write("Registration success!");
            }
            else
            {
                Console.WriteLine("Registration Failure!");
            }
        }

        public void Login(LoginRequest request)
        {
            string endpoint = "/user/login";
            var serverResponse = Post(endpoint, request);

            if (serverResponse.IsSuccessStatusCode)
            {
                Console.Write("Login success!");
            }
            else
            {
                Console.WriteLine("Login Failure!");
            }
        }
    }
}
