using System;
using System.Collections.Generic;

namespace ActividadIntegradora01
{
    public class Persona
    {
        public string DNI { get; set; } //Modificador de acceso publico
        public string Nombre { get; set; } //Modificador de acceso publico
        public string Apellido { get; set; } //Modificador de acceso publico
        public DateTime FechaIngreso { get; set; } //Modificador de acceso publico

        // Calcular la antigüedad en años y es solo lectura (solo uso el GET)
        public int Antiguedad //Modificador de acceso publico para poder ser llamado desde fuera de la clase pero pero de solo lectura
        {
            get
            {
                return CalcularAntiguedad();
            }
        }

        //Funcion que calcula la antiguedad del objeto persona
        public int CalcularAntiguedad()
        {
            int years = DateTime.Now.Year - FechaIngreso.Year;
            if (DateTime.Now.DayOfYear < FechaIngreso.DayOfYear)
                years--;
            return years;
        }

        // Constructor que lo vamos a usar cuando se instancie la clase agregando los datos en los inputs
        public Persona(string dni, string nombre, string apellido, DateTime fechaIngreso)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            FechaIngreso = fechaIngreso;
        }
        // Destructor
        ~Persona()
        {
            Console.WriteLine("Espacio liberado");
        }

        // Método estático para buscar una persona por su DNI en una lista de personas
        public static Persona BuscarPersonaPorDNI(string dni, List<Persona> lista)
        {
            return lista.Find(persona => persona.DNI == dni);
        }

        // Método estático para agregar una persona a una lista de personas
        public static void AgregarPersona(List<Persona> lista, Persona persona)
        {
            lista.Add(persona);
        }

             
    }
}
