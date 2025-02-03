using PatientsMvc.Models;
using PatientsMvc.Models.Requests;
namespace PatientsMvc.Service
{
    public class ApiClient
    {
        public string baseUri { get; set; }
        public ApiClient(string uri)
        {
            baseUri = uri;
        }

        private HttpResponseMessage Get(string endpoint)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseUri + endpoint);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client.GetAsync("").Result;
        }

        private HttpResponseMessage Post<T>(string endpoint, T body)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseUri + endpoint);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TransferEncodingChunked = false;
            var attempt = client.PostAsJsonAsync(endpoint, body);
            return attempt.Result;
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

        
    }
}
