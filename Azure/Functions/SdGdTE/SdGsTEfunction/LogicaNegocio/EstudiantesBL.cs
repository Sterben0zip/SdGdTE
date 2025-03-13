using SdGsTEfunction.DataAccess;
using SdGsTEfunction.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SdGsTEfunction.LogicaNegocio
{
	internal class EstudiantesBL : ILogicasNegocio<Estudiante>
	{
		private readonly AccesoDatos _datos;

		public EstudiantesBL(OrigenDatos origen)
		{
			_datos = new AccesoDatos(origen);
		}

		public async Task<int> CrearAsync(Estudiante estudiante)
		{
			SqlCommand insertarCmd = new SqlCommand("INSERT INTO ESTUDIANTES VALUES (@nombre, @correo, @password)");
			insertarCmd.Parameters.AddWithValue("@nombre", estudiante.Nombre);
			insertarCmd.Parameters.AddWithValue("@correo", estudiante.Correo);
			insertarCmd.Parameters.AddWithValue("@password", estudiante.Password);

			int restultado = await _datos.EjecutarComando(insertarCmd);

			return restultado;
		}

		public async Task<List<Estudiante>> EnlistarAsync()
		{
			List<Estudiante> estudiantes = new List<Estudiante>();
			SqlCommand consultarCmd = new SqlCommand("SELECT * FROM ESTUDIANTES");
			DataTable estudiantesDt = await _datos.EjecutarConsulta(consultarCmd);

			foreach (DataRow fila in estudiantesDt.Rows)
			{
				estudiantes.Add(new Estudiante
				{
					Id = Convert.ToInt32(fila["Id"]),
					Nombre = fila["Nombre"].ToString(),
					Correo = fila["Correo"].ToString()
				});
			}

			return estudiantes;
		}

		public async Task<Estudiante> Obtener(int estudianteId)
		{
			Estudiante estudiante = null;
			SqlCommand consultarCmd = new SqlCommand("SELECT * FROM ESTUDIANTES WHERE Id = @id");
			consultarCmd.Parameters.AddWithValue("@id", estudianteId);

			DataTable estudianteDt = await _datos.EjecutarConsulta(consultarCmd);

			if (estudianteDt.Rows.Count > 0)
			{
				estudiante = new Estudiante
				{
					Id = Convert.ToInt32(estudianteDt.Rows[0]["Id"]),
					Nombre = estudianteDt.Rows[0]["Nombre"].ToString(),
					Correo = estudianteDt.Rows[0]["Correo"].ToString(),
				};
			}

			return estudiante;
		}

		public async Task<Estudiante> Obtener(string correo)
		{
			Estudiante estudiante = null;
			SqlCommand cmd = new SqlCommand("SELECT * FROM ESTUDIANTES WHERE Correo = @correo");
			cmd.Parameters.AddWithValue("@correo", correo);
			DataTable estudianteDt = await _datos.EjecutarConsulta(cmd);

			if (estudianteDt.Rows.Count > 0)
			{
				estudiante = new Estudiante
				{
					Id = Convert.ToInt32(estudianteDt.Rows[0]["Id"]),
					Nombre = estudianteDt.Rows[0]["Nombre"].ToString(),
					Correo = estudianteDt.Rows[0]["Correo"].ToString(),
					Password = estudianteDt.Rows[0]["Password"].ToString()
				};
			}

			return estudiante;
		}

		public async Task<int> ActualizarAsync(Estudiante estudiante, int estudianteId)
		{
			SqlCommand actualizarCmd = new SqlCommand("UPDATE ESTUDIANTES SET Nombre = @nombre, Correo = @correo,  WHERE Id = @id");
			actualizarCmd.Parameters.AddWithValue("@id", estudianteId);
			actualizarCmd.Parameters.AddWithValue("@nombre", estudiante.Nombre);
			actualizarCmd.Parameters.AddWithValue("@correo", estudiante.Correo);

			int res = await _datos.EjecutarComando(actualizarCmd);

			return res;
		}

		public async Task<int> EliminarAsync(int estudianteId)
		{
			SqlCommand eliminarCmd = new SqlCommand("DELETE FROM ESTUDIANTES WHERE ID = @id");
			eliminarCmd.Parameters.AddWithValue("@id", estudianteId);

			int res = await _datos.EjecutarComando(eliminarCmd);

			return res;
		}
	}
}