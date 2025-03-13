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

		public Task<List<Tarea>> EnlistarAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<List<Tarea>> EnlistarAsync(int estudianteId)
		{
			List<Tarea> tareas = new List<Tarea>();
			SqlCommand consultarCmd = new SqlCommand("SELECT * FROM TAREAS WHERE EstudianteId = @estudianteId");
			consultarCmd.Parameters.AddWithValue("@estudianteId", estudianteId);

			DataTable tareasDt = await _datos.EjecutarConsulta(consultarCmd);

			foreach (DataRow fila in tareasDt.Rows)
			{
				tareas.Add(new Tarea
				{
					Id = Convert.ToInt32(fila["Id"]),
					Titulo = fila["Titulo"].ToString(),
					FechaEntrega = Convert.ToDateTime(fila["FechaEntrega"]),
					Completado = Convert.ToBoolean(fila["Completado"]),
					Descripcion = fila["Descripcion"].ToString(),
					EstudianteId = Convert.ToInt32(fila["EstudianteId"]),
					MateriaId = Convert.ToInt32(fila["MateriaId"])
				});
			}

			return tareas;
		}

		public async Task<Tarea> Obtener(int tareaId)
		{
			SqlCommand consultarCmd = new SqlCommand("SELECT * FROM TAREAS WHERE Id = @tareaId");
			consultarCmd.Parameters.AddWithValue("@tareaId", tareaId);
			DataTable tareaDt = await _datos.EjecutarConsulta(consultarCmd);

			Tarea tarea = new Tarea
			{
				Id = Convert.ToInt32(tareaDt.Rows[0]["Id"]),
				Titulo = tareaDt.Rows[0]["Titulo"].ToString(),
				FechaEntrega = Convert.ToDateTime(tareaDt.Rows[0]["FechaEntrega"]),
				Completado = Convert.ToBoolean(tareaDt.Rows[0]["Completado"]),
				Descripcion = tareaDt.Rows[0]["Descripcion"].ToString(),
				EstudianteId = Convert.ToInt32(tareaDt.Rows[0]["EstudianteId"]),
				MateriaId = Convert.ToInt32(tareaDt.Rows[0]["MateriaId"])
			};
			
			return tarea;
		}

		public async Task<int> ActualizarAsync(Tarea tarea, int tareaId)
		{
			SqlCommand actualizaCmd = new SqlCommand("UPDATE TAREAS SET Titulo= @titulo, FechaEntrega = @fechaEntrega, Completado = @completado, Descripcion = @descripcion WHERE Id = @tareaId");
			actualizaCmd.Parameters.AddWithValue("@tareaId", tareaId);
			actualizaCmd.Parameters.AddWithValue("@titulo", tarea.Titulo);
			actualizaCmd.Parameters.AddWithValue("@fechaEntrega", tarea.FechaEntrega);
			actualizaCmd.Parameters.AddWithValue("@completado", tarea.Completado);
			actualizaCmd.Parameters.AddWithValue("@descripcion", tarea.Descripcion);

			int res = await _datos.EjecutarComando(actualizaCmd);
			return res;
		}

		public async Task<int> EliminarAsync(int tareaId)
		{
			SqlCommand eliminarCmd = new SqlCommand("DELETE FROM TAREAS WHERE ID = @id");
			eliminarCmd.Parameters.AddWithValue("@id", tareaId);

			int res = await _datos.EjecutarComando(eliminarCmd);

			return res;
		}
	}
}