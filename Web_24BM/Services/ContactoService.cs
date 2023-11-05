using System.Diagnostics;
using Web_24BM.Models;

namespace Web_24BM.Services
{
	public class ContactoService
	{

		private readonly BaseDbcurriculumContext base_de_datos;
		
		public ContactoService(BaseDbcurriculumContext base_de_datos)
		{
			this.base_de_datos = base_de_datos;
		}
        public string? GuardarArchivo(IFormFile archivo, string carpetaDestino)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return null; // No se proporcionó un archivo válido.
            }

            // Generar un nombre de archivo único con un uuid
            Guid uuid = Guid.NewGuid();
            
            string extension = Path.GetExtension(archivo.FileName);

            string nombreArchivo = $"{uuid}{extension}";

            // Combinar la ruta de destino con el nombre del archivo.
            string rutaCompleta = Path.Combine(carpetaDestino, nombreArchivo);

            // Crear el directorio de destino si no existe.
            if (!Directory.Exists(carpetaDestino))
            {
                Directory.CreateDirectory(carpetaDestino);
            }

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                archivo.CopyTo(stream);
            }

            return nombreArchivo;

        }

        public List<Contacto> getCurriculums()
        {
            return this.base_de_datos.Contactos.ToList();
        }
        public Contacto obtenerCurriculumPorId(int id)
        {
            return this.base_de_datos.Contactos.Where(c => c.Id == id).FirstOrDefault()!;
        }

        public bool ActualizarContacto(Contacto model)
        {
            try
            {
                var elemento = this.base_de_datos.Contactos.Where(c => c.Id == model.Id).FirstOrDefault();
                elemento.Nombre = model.Nombre;
                elemento.Apellidos = model.Apellidos;
                elemento.Objetivo = model.Objetivo;
                elemento.FechaNacimiento = model.FechaNacimiento;
                elemento.Direccion = model.Direccion;
                this.base_de_datos.Contactos.Update(elemento);
                this.base_de_datos.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool eliminarCurriculum(int id)
        {
            try
            {
                var elemento = this.base_de_datos.Contactos.Where(c => c.Id == id).FirstOrDefault();
                this.base_de_datos.Contactos.Remove(elemento);
                this.base_de_datos.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CrearContacto(Curriculum curriculum)
		{

            string carpetaDestino = Path.Combine("wwwroot", "uploads");

            string archivo = this.GuardarArchivo( curriculum.Foto , carpetaDestino)!;

            Contacto nuevoContacto = new Contacto()
			{
				Nombre = curriculum.Nombre,
				Apellidos = curriculum.Apellidos,
				Email = curriculum.Email,
				Direccion = curriculum.Direccion,
				FechaNacimiento = curriculum.FechaNacimiento,
				Objetivo = curriculum.Objetivo,
                Foto = archivo
            };

			this.base_de_datos.Contactos.Add(nuevoContacto);

			this.base_de_datos.SaveChanges();

		}
	}
}
