using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

using Newtonsoft.Json;

using SdGsTEfunction.LogicaNegocio;
using SdGsTEfunction.Models;

using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SdGsTEfunction.Funciones
{
    public class ConsultarPorIdActualizaEliminaMateria
    {
        private readonly MateriasBL _logicaMaterias = new MateriasBL(DataAccess.OrigenDatos.AzureEddye);

		[Function("ConsultarPorIdActualizaEliminaMateria")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "put", "delete", Route = "materias/{materiaId}")] HttpRequestData req, int materiaId)
        {
            if (req.Method == HttpMethods.Get)
            {
                Materia materia = await _logicaMaterias.Materia(materiaId);
                if (materia == null) return new NotFoundResult();

                return new OkObjectResult(materia);
            }
            else if (req.Method == HttpMethods.Put)
            {
                Materia materia = new Materia();

				string contenidoSolicitud = await new StreamReader(req.Body).ReadToEndAsync();
				materia = JsonConvert.DeserializeObject<Materia>(contenidoSolicitud);
				materia.Id = materiaId;

                int res = await _logicaMaterias.ActualizarMateria(materia, materiaId);
                return new OkObjectResult(res);
			}
            else
            {
                Materia materia = await _logicaMaterias.Materia(materiaId);
                if (materia == null) return new NotFoundResult();

                int res = await _logicaMaterias.EliminaMateria(materiaId);
                return new OkObjectResult(res);
            }
        }
    }
}