using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

using Newtonsoft.Json;

using SdGsTEfunction.DataAccess;
using SdGsTEfunction.LogicaNegocio;
using SdGsTEfunction.Models;

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SdGsTEfunction.Funciones
{
	public class ConsultarCrearEstudiantes
	{
		private readonly EstudiantesBL _logicaEstudiantes = new EstudiantesBL(OrigenDatos.AzureEddye);

		[Function("ConsultarCrearEstudiantes")]
		public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "estudiantes")] HttpRequestData req)
		{
			if (req.Method == HttpMethods.Post)
			{
				string contenidoSolicitud = await new StreamReader(req.Body).ReadToEndAsync();
				Estudiante estudiante = JsonConvert.DeserializeObject<Estudiante>(contenidoSolicitud);
				int restultado = await _logicaEstudiantes.CrearAsync(estudiante);

				return new OkObjectResult(restultado);
			}

			List<Estudiante> estudiantes = await _logicaEstudiantes.EnlistarAsync();
			return new OkObjectResult(estudiantes);
		}
	}
}