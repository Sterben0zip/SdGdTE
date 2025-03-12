using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

using Newtonsoft.Json;

using SdGsTEfunction.Models;

using System.IO;
using System.Threading.Tasks;

namespace SdGsTEfunction
{
    public class ConsultaInserta
    {
        [Function("ConsultaInserta")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "estudiantes")] HttpRequestData req)
        {
            DataAccess.AccesoDatos datos = new DataAccess.AccesoDatos();

            if (req.Method == HttpMethods.Post)
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var estudiante = JsonConvert.DeserializeObject<Estudiante>(requestBody);
                datos.Insertar(estudiante);
                return new CreatedResult("/estudiantes", estudiante);
            }

			var estudiantes = datos.GetEstudiantes();
			return new OkObjectResult(estudiantes);
		}
    }
}