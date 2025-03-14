using System;

namespace SdGsTEfunction.Models
{
    public class Tarea
    {
		public int Id { get; set; }
		public string Titulo { get; set; }
		public DateTime FechaEntrega { get; set; }
		public bool Completado { get; set; }
		public string Descripcion { get; set; }
		public int EstudianteId { get; set; }
		public int MateriaId { get; set; }

		public virtual Materia Materia { get; set; }
	}
}