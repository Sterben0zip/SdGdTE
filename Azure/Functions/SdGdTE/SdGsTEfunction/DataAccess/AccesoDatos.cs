using SdGsTEfunction.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SdGsTEfunction.DataAccess
{
    public class AccesoDatos
    {
        private readonly string _cadenaCon;
		private readonly SqlConnection _conexion;

		public AccesoDatos()
		{
			_cadenaCon = "Server=tcp:sdgdte.database.windows.net,1433;" +
				"Initial Catalog=SdGdTEDB;" +
				"Persist Security Info=False;" +
				"User ID=sdgdte;" +
				"Password=J2Tg6_gDdaMiJk-;" +
				"MultipleActiveResultSets=False;" +
				"Encrypt=True;" +
				"TrustServerCertificate=False;" +
				"Connection Timeout=30;";

			_conexion = new SqlConnection(_cadenaCon);
		}

		internal int Insertar(Estudiante estudiante)
		{
			int res = 0;

			SqlCommand insertarCmd = new SqlCommand("INSERT INTO ESTUDIANTES VALUES (@nombre, @correo, @password)", _conexion);
			insertarCmd.Parameters.AddWithValue("@nombre", estudiante.Nombre);
			insertarCmd.Parameters.AddWithValue("@correo", estudiante.Correo);
			insertarCmd.Parameters.AddWithValue("@password", estudiante.Password);

			try
			{
				insertarCmd.Connection.Open();
				res = insertarCmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
			finally
			{
				if (insertarCmd.Connection.State == ConnectionState.Open)
				{
					insertarCmd.Connection.Close();
				}
			}

			return res;
		}

		internal List<Estudiante> GetEstudiantes()
		{
			List<Estudiante> estudiantes = new List<Estudiante>();
			SqlCommand consultarCmd = new SqlCommand("SELECT * FROM ESTUDIANTES", _conexion);
			SqlDataAdapter sda = new SqlDataAdapter(consultarCmd);
			DataTable dtResult = new DataTable();
			
			try
			{
				consultarCmd.Connection.Open();
				sda.Fill(dtResult);

				foreach (DataRow row in dtResult.Rows)
				{
					estudiantes.Add(new Estudiante
					{
						Nombre = row["Nombre"].ToString(),
						Correo = row["Correo"].ToString(),
					});

				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
			finally
			{
				if (consultarCmd.Connection.State == ConnectionState.Open)
				{
					consultarCmd.Connection.Close();
				}
			}

			return estudiantes;
		}

		internal List<Estudiante> GetEstudiantes(int id)
		{
			return new List<Estudiante>();
		}
	}
}