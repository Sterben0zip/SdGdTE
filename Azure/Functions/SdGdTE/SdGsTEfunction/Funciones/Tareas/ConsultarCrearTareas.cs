using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace SdGsTEfunction.Funciones.Tareas
{
    public class ConsultarCrearTareas
    {
        [Function("ConsultarCrearTareas")]
		public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "tareas")] HttpRequestData req)
		{
            
        }
    }
}