using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;

using SdGsTEfunction.DataAccess;
using SdGsTEfunction.LogicaNegocio;
using SdGsTEfunction.Models;

using System.IO;
using System.Threading.Tasks;

namespace SdGsTEfunction.Funciones.Accounting
{
    public class Registrar
    {
		AccountingBL _logicaAccount = new AccountingBL(OrigenDatos.AzureChofen);

		[Function("Registrar")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "registro")] HttpRequestData req)
        {
			string contenidoSolicitud = await new StreamReader(req.Body).ReadToEndAsync();
			Estudiante estudiante = JsonConvert.DeserializeObject<Estudiante>(contenidoSolicitud);

			if(await _logicaAccount.Registrar(estudiante))
			{
				return new OkObjectResult(true);
			}

			return new ConflictObjectResult("Correo previamente registrado");
		}
    }
}