using SdGsTEfunction.DataAccess;
using SdGsTEfunction.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SdGsTEfunction.LogicaNegocio
{
	internal class TareasBL : ILogicasNegocio<Tarea>
	{
		private readonly AccesoDatos _datos;

		public TareasBL(OrigenDatos origen)
		{
			_datos = new AccesoDatos(origen);
		}

		public async Task<int> CrearAsync(Tarea tarea)
		{
			SqlCommand insertarCmd = new SqlCommand("INSERT INTO TAREAS VALUES (@titulo, @fechaEntrega, @completado, @descripcion, @estudianteId, @materiaId)");
			insertarCmd.Parameters.AddWithValue("@titulo", tarea.Titulo);
			insertarCmd.Parameters.AddWithValue("@fechaEntrega", tarea.FechaEntrega);
			insertarCmd.Parameters.AddWithValue("@completado", tarea.Completado);
			insertarCmd.Parameters.AddWithValue("@descripcion", tarea.Descripcion);
			insertarCmd.Parameters.AddWithValue("@estudianteId", tarea.EstudianteId);
			insertarCmd.Parameters.AddWithValue("@materiaId", tarea.MateriaId);

			int res = await _datos.EjecutarComando(insertarCmd);
			return res;
		}

		public async Task<List<Tarea>> EnlistarAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<List<Tarea>> EnlistarAsync(int estudianteId, int materiaId)
		{
			List<Tarea> tareas = new List<Tarea>();
			SqlCommand consultarCmd = new SqlCommand("SELECT * FROM TAREAS WHERE EstudianteId = @estudianteId AND Materia = @materiaId");
			consultarCmd.Parameters.AddWithValue("@estudianteId", estudianteId);
			consultarCmd.Parameters.AddWithValue("@materiaId", materiaId);

			DataTable tareasDt = await _datos.EjecutarConsulta(consultarCmd);

			foreach (DataRow fila in tareasDt.Rows)
			{
				tareas.Add(new Tarea
				{
					Id = (int)fila["Id"],
					Titulo = (string)fila["Titulo"],
					FechaEntrega = (DateTime)fila["FechaEntrega"],
					Completado = (bool)fila["Completado"],
					Descripcion = (string)fila["Descripcion"],
					EstudianteId = (int)fila["EstudianteId"],
					MateriaId = (int)fila["MateriaId"]
				});
			}

			return tareas;
		}

		public Task<Tarea> Obtener(int tareaId)
		{
			throw new NotImplementedException();
		}

		public Task<int> ActualizarAsync(Tarea tarea, int tareaId)
		{
			throw new NotImplementedException();
		}



		public Task<int> EliminarAsync(int tareaId)
		{
			throw new NotImplementedException();
		}
	}
}
