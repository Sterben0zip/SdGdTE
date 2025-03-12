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
	public class ConsultarCrearEstudiantes
	{
		private readonly EstudiantesBL logicaEstudiantes = new EstudiantesBL(DataAccess.OrigenDatos.AzureEddye);

		[Function("ConsultarCrearEstudiantes")]
		public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "estudiantes")] HttpRequestData req)
		{
			if (req.Method == HttpMethods.Post)
			{
				string contenidoSolicitud = await new StreamReader(req.Body).ReadToEndAsync();
				Estudiante estudiante = JsonConvert.DeserializeObject<Estudiante>(contenidoSolicitud);
				int restultado = await logicaEstudiantes.CrearEstudiante(estudiante);

				return new OkObjectResult(restultado);
			}

			List<Estudiante> estudiantes = await logicaEstudiantes.Estudiantes();
			return new OkObjectResult(estudiantes);
		}
	}
}