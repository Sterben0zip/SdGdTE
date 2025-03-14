using SdGsTEfunction.DataAccess;
using SdGsTEfunction.Models;

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SdGsTEfunction.LogicaNegocio
{
	internal class AccountingBL
	{
		private readonly AccesoDatos _datos;
		private readonly EstudiantesBL _logicaEstudiantes;

		public AccountingBL(OrigenDatos origen)
		{
			_datos = new AccesoDatos(origen);
			_logicaEstudiantes = new EstudiantesBL(origen);
		}

		internal async Task<Estudiante> Login(Estudiante login)
		{
			Estudiante estudiante = await _logicaEstudiantes.Obtener(login.Correo);

			if (estudiante == null)
			{
				return null;
			}

			return estudiante;
		}

		internal async Task<bool> Registrar(Estudiante buscar)
		{
			Estudiante estudiante = await _logicaEstudiantes.Obtener(buscar.Correo);
			
			if (estudiante != null)
			{
				return false;
			}

			int res = await _logicaEstudiantes.CrearAsync(buscar);
			return res > 0;
		}

		private static string DecryptString(string cipherText)
		{
			string key = "7f963202-24fc-44";
			byte[] keyBytes = Encoding.UTF8.GetBytes(key);
			byte[] cipherBytes = Convert.FromBase64String(cipherText);

			using (Aes aes = Aes.Create())
			{
				aes.Key = keyBytes;
				aes.Mode = CipherMode.ECB;
				aes.Padding = PaddingMode.Zeros;

				using (ICryptoTransform decryptor = aes.CreateDecryptor())
				{
					using (MemoryStream ms = new MemoryStream(cipherBytes))
					{
						using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
						{
							using (StreamReader reader = new StreamReader(cs))
							{
								return reader.ReadToEnd();
							}
						}
					}
				}
			}
		}
	}
}