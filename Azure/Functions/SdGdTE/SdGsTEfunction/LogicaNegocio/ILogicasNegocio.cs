using System.Collections.Generic;
using System.Threading.Tasks;

namespace SdGsTEfunction.LogicaNegocio
{
	internal interface ILogicasNegocio<T> where T : class
	{
		Task<int> CrearAsync(T tabla);
		Task<List<T>> EnlistarAsync();
		Task<T> Obtener(int tablaId);
		Task<int> ActualizarAsync(T tabla, int tablaId);
		Task<int> EliminarAsync(int tablaId);
	}
}