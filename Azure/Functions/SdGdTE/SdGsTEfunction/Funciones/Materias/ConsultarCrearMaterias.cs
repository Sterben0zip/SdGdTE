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
	public class ConsultarCrearMaterias
	{
		private readonly MateriasBL logicaMaterias = new MateriasBL(DataAccess.OrigenDatos.AzureEddye);

		[Function("ConsultarCrearMaterias")]
		public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "materias")] HttpRequestData req)
		{
			if (req.Method == HttpMethods.Post)
			{
				string contenidoSolicitud = await new StreamReader(req.Body).ReadToEndAsync();
				Materia materia = JsonConvert.DeserializeObject<Materia>(contenidoSolicitud);
				int restultado = await logicaMaterias.CrearAsync(materia);

				return new OkObjectResult(restultado);
			}

			List<Materia> materias = await logicaMaterias.EnlistarAsync();
			return new OkObjectResult(materias);
		}
	}
}