using SdGsTEfunction.DataAccess;
using SdGsTEfunction.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SdGsTEfunction.LogicaNegocio
{
	internal class MateriasBL : ILogicasNegocio<Materia>
	{
		private readonly AccesoDatos _datos;

		public MateriasBL(OrigenDatos origen)
		{
			_datos = new AccesoDatos(origen);
		}

		public async Task<int> CrearAsync(Materia materia)
		{
			SqlCommand insertarCmd = new SqlCommand("INSERT INTO MATERIAS VALUES (@nombre, @descripcion)");
			insertarCmd.Parameters.AddWithValue("@nombre", materia.Nombre);
			insertarCmd.Parameters.AddWithValue("@descripcion", materia.Descripcion);

			int restultado = await _datos.EjecutarComando(insertarCmd);

			return restultado;
		}

		public async Task<List<Materia>> EnlistarAsync()
		{
			List<Materia> materias = new List<Materia>();
			SqlCommand consultarCmd = new SqlCommand("SELECT * FROM MATERIAS");
			DataTable materiasDt = await _datos.EjecutarConsulta(consultarCmd);

			foreach (DataRow fila in materiasDt.Rows)
			{
				materias.Add(new Materia
				{
					Id = Convert.ToInt32(fila["Id"]),
					Nombre = fila["Nombre"].ToString(),
					Descripcion = fila["Descripcion"].ToString()
				});
			}

			return materias;
		}

		public async Task<Materia> Obtener(int materiaId)
		{
			SqlCommand consultarCmd = new SqlCommand("SELECT * FROM MATERIAS WHERE Id = @id");
			consultarCmd.Parameters.AddWithValue("@id", materiaId);

			DataTable materiaDt = await _datos.EjecutarConsulta(consultarCmd);

			Materia materias = new Materia
			{
				Id = Convert.ToInt32(materiaDt.Rows[0]["Id"]),
				Nombre = materiaDt.Rows[0]["Nombre"].ToString(),
				Descripcion = materiaDt.Rows[0]["Descripcion"].ToString(),
			};

			return materias;
		}

		public async Task<int> ActualizarAsync(Materia materia, int materiaId)
		{
			SqlCommand actualizarCmd = new SqlCommand("UPDATE MATERIAS SET Nombre = @nombre, Descripcion = @descripcion WHERE Id = @id");
			actualizarCmd.Parameters.AddWithValue("@id", materiaId);
			actualizarCmd.Parameters.AddWithValue("@nombre", materia.Nombre);
			actualizarCmd.Parameters.AddWithValue("@descripcion", materia.Descripcion);

			int res = await _datos.EjecutarComando(actualizarCmd);

			return res;
		}

		public async Task<int> EliminarAsync(int materiaId)
		{
			SqlCommand eliminarCmd = new SqlCommand("DELETE FROM MATERIAS WHERE ID = @id");
			eliminarCmd.Parameters.AddWithValue("@id", materiaId);

			int res = await _datos.EjecutarComando(eliminarCmd);

			return res;
		}
	}
}