using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

using Newtonsoft.Json;

using SdGsTEfunction.LogicaNegocio;
using SdGsTEfunction.Models;

using System.IO;
using System.Threading.Tasks;

namespace SdGsTEfunction.Funciones
{
    public class ConsultarPorIdActualizaEliminaEstudiante
    {
		private readonly EstudiantesBL logicaEstudiantes = new EstudiantesBL(DataAccess.OrigenDatos.AzureEddye);

		[Function("ConsultarPorIdActualizaEliminaEstudiante")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "put", "delete", Route = "estudiantes/{estudianteId}")] HttpRequestData req, int estudianteId)
        {
            if (req.Method == HttpMethods.Get)
            {
                Estudiante estudiante = await logicaEstudiantes.Obtener(estudianteId);
                if (estudiante == null) return new NotFoundResult();

                return new OkObjectResult(estudiante);
            }
            else if (req.Method == HttpMethods.Put)
            {
                string contenidoSolicitud = await new StreamReader(req.Body).ReadToEndAsync();
                Estudiante estudiante = JsonConvert.DeserializeObject<Estudiante>(contenidoSolicitud);
                estudiante.Id = estudianteId;

                int res = await logicaEstudiantes.ActualizarAsync(estudiante, estudianteId);
                return new OkObjectResult(res);
			}
            else
            {
                Estudiante estudiante = await logicaEstudiantes.Obtener(estudianteId);
                if (estudiante == null) return new NotFoundResult();

                int res = await logicaEstudiantes.EliminarAsync(estudianteId);
                return new OkObjectResult(res);
            }
        }
    }
}