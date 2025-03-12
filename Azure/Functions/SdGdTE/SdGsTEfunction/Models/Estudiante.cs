using System;
using System.Runtime.CompilerServices;

namespace SdGsTEfunction.Models
{
	public class Persona
	{
		public string Nombre { get; set; }
		public int Edad { get; set; }
	}

    public class Estudiante : Persona
    {
		public int Id { get; set; }
		public string Correo { get; set; }
		public string Password { get; set; }
		internal int numerito { get; set; }
	}
}