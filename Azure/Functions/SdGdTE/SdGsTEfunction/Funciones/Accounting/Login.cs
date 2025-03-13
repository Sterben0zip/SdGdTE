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
    public class Login
    {
        AccountingBL _logicaAccount = new AccountingBL(OrigenDatos.AzureChofen);

        [Function("Login")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "login")] HttpRequestData req)
        {
            string contenidoSolicitud = await new StreamReader(req.Body).ReadToEndAsync();
            Estudiante estudiante = JsonConvert.DeserializeObject<Estudiante>(contenidoSolicitud);
            estudiante = await _logicaAccount.Login(estudiante);

            if (estudiante == null)
            {
                return new NotFoundObjectResult(-1);
            }

            return new OkObjectResult(estudiante.Id);
        }
    }
}