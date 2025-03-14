using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

using Newtonsoft.Json;

using SdGsTEfunction.LogicaNegocio;
using SdGsTEfunction.Models;

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SdGsTEfunction.Funciones
{
    public class ConsultarCrearTareas
    {
        private readonly TareasBL _logicaTareas = new TareasBL(DataAccess.OrigenDatos.AzureEddye);
        [Function("ConsultarCrearTareas")]
		public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "tareas")] HttpRequestData req, int estudianteId)
		{
            if (req.Method == HttpMethods.Post)
            {
                string contenidoSolicitud = await new StreamReader(req.Body).ReadToEndAsync();
                Tarea tarea = JsonConvert.DeserializeObject<Tarea>(contenidoSolicitud);
                int res = await _logicaTareas.CrearAsync(tarea);

                return new OkObjectResult(res);
            }

            List<Tarea> tareas = await _logicaTareas.EnlistarAsync(estudianteId);
            return new OkObjectResult(tareas);

        }
    }
}