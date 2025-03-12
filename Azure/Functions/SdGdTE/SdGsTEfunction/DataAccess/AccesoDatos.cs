using SdGsTEfunction.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SdGsTEfunction.DataAccess
{
    public class AccesoDatos
    {
		private readonly string _cadenaEddye = "Server=tcp:sdgdte.database.windows.net,1433;Initial Catalog=SdGdTEDB;" +
				"User ID=sdgdte;Password=J2Tg6_gDdaMiJk-;" +
				"Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;" +
				"Connection Timeout=30;";
		private readonly string _cadenaChofen = "Server=tcp:learningmanagementsrvr.database.windows.net,1433;Initial Catalog=learningmanagementdb;" +
				"Persist Security Info=False;User ID=lmsa;Password=Lm@16022309Db;" +
				"MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;" +
				"Connection Timeout=30;";
        private readonly string _cadenaCon;
		private readonly SqlConnection _conexion;

		public AccesoDatos(OrigenDatos origen)
		{
			_cadenaCon = origen == OrigenDatos.AzureEddye ? _cadenaEddye : _cadenaChofen;
			_conexion = new SqlConnection(_cadenaCon);
		}

		internal async Task<int> EjecutarComando(SqlCommand cmd)
		{
			int res = 0;
			cmd.Connection = _conexion;

			try
			{
				await Task.Run(async () =>
				{
					cmd.Connection.Open();
					res = await cmd.ExecuteNonQueryAsync();
				});
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
			finally
			{
				if (cmd.Connection.State == ConnectionState.Open)
				{
					cmd.Connection.Close();
				}
			}

			return res;
		}

		internal async Task<DataTable> EjecutarConsulta(SqlCommand cmd)
		{
			SqlDataAdapter sda = new SqlDataAdapter(cmd);
			DataTable dtResult = new DataTable();
			cmd.Connection = _conexion;

			try
			{
				await Task.Run(() =>
				{
					cmd.Connection.Open();
					sda.Fill(dtResult);
				});
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
			finally
			{
				if (cmd.Connection.State == ConnectionState.Open)
				{
					cmd.Connection.Close();
				}
			}

			return dtResult;
		}
	}

	public enum OrigenDatos
	{
		AzureEddye,
		AzureChofen
	}
}