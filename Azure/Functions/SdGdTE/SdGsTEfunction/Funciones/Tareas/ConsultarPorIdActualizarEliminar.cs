using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

using Newtonsoft.Json;

using SdGsTEfunction.DataAccess;
using SdGsTEfunction.LogicaNegocio;
using SdGsTEfunction.Models;

using System.IO;
using System.Threading.Tasks;

namespace SdGsTEfunction.Funciones
{
    public class ConsultarPorIdActualizarEliminar
    {
        private readonly TareasBL _logicaTareas = new TareasBL(OrigenDatos.AzureEddye);

        [Function("ConsultarPorIdActualizaEliminaTarea")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "put", "delete", Route = "tareas/{tareaId}")] HttpRequestData req, int tareaId)
        {
            if (req.Method == HttpMethods.Get)
            {
                Tarea tarea = await _logicaTareas.Obtener(tareaId);
                if (tarea == null) return new NotFoundResult();

                return new OkObjectResult(tarea);
            }
            else if (req.Method == HttpMethods.Put)
            {
                string contenidoSolicitud = await new StreamReader(req.Body).ReadToEndAsync();
                Tarea tarea = JsonConvert.DeserializeObject<Tarea>(contenidoSolicitud);
                int res = await _logicaTareas.ActualizarAsync(tarea, tareaId);

                return new OkObjectResult(res);
            }
            else
            {
                Tarea tarea = await _logicaTareas.Obtener(tareaId);
                if (tarea == null) return new NotFoundResult();

                int res = await _logicaTareas.EliminarAsync(tareaId);
                return new OkObjectResult(res);
            }
        }
    }
}