using Newtonsoft.Json;

namespace CustomDatagrid
{
    internal class API
    {
        public static List<Person> GetPeople(int pageNumber, int itemsPerPage, string search, string sort, string direction)
        {
            HttpClient client = new();
            if (!client.GetAsync($"http://localhost:5189/Table/People?pageNumber={pageNumber}&itemsPerPage={itemsPerPage}&search={search}&sort={sort}&direction={direction}").Result.IsSuccessStatusCode)
            {
                return new List<Person>();
            }
            string response = client.GetAsync($"http://localhost:5189/Table/People?pageNumber={pageNumber}&itemsPerPage={itemsPerPage}&search={search}&sort={sort}&direction={direction}").Result.Content.ReadAsStringAsync().Result;
            List<Person>? data = JsonConvert.DeserializeObject<List<Person>>(response);
            return data ?? new List<Person>();
        }

        public static int GetTotalPages(int itemsPerPage, string search)
        {
            HttpClient client = new();
            if (!client.GetAsync($"http://localhost:5189/Table/TotalPages?itemsPerPage={itemsPerPage}&search={search}").Result.IsSuccessStatusCode)
            {
                return 1;
            }
            string response = client.GetAsync($"http://localhost:5189/Table/TotalPages?itemsPerPage={itemsPerPage}&search={search}").Result.Content.ReadAsStringAsync().Result;
            int data = JsonConvert.DeserializeObject<int>(response);
            return data;
        }
    }
}
