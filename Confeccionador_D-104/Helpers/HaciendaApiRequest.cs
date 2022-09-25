using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Confeccionador_D_104.Model;
using System.Collections.Generic;

namespace Confeccionador_D_104.Helpers
{
    internal class HaciendaApiRequest
    {
        const string GET_CABYS_CODE_INFO = "https://api.hacienda.go.cr/fe/cabys";

        public async Task<List<CabysModel>>GetCabysDescription(string codes)
        {
            using HttpClient client = new HttpClient();

            using HttpResponseMessage response = await client.GetAsync(
                GET_CABYS_CODE_INFO + "?codigo=" + codes);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            List<CabysModel> result = JsonConvert.DeserializeObject<List<CabysModel>>(jsonResponse);

            return result;
        }
    }
}
