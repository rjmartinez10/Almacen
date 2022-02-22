using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using AlmacenController.Model;
namespace Almacen.Controllers
{
    public class ProductoController : ApiController
    {

        public IEnumerable<Productos> Get() {
            using (AlmacenEntities entities = new AlmacenEntities()) {
                var productos = from p in entities.Productos
                                where p.activo == true
                                select p;

                //entities.Productos.Where(s=>s.activo==true);
                return productos.ToList();
            }        
        
        }

        public Productos Eliminar(Int32 ideProducto) {
            using (AlmacenEntities entities = new AlmacenEntities())
            {
                Productos c = (from x in entities.Productos
                              where x.ideProducto == ideProducto
                               select x).First();
                c.activo = false;
                entities.SaveChanges();
                return c;
            }
        }


        public Productos ActualizarNombre(Int32 ideProducto,string nombre)
        {
            using (AlmacenEntities entities = new AlmacenEntities())
            {
                Productos c = (from x in entities.Productos
                               where x.ideProducto == ideProducto
                               select x).First();
                c.nombre = nombre;
                entities.SaveChanges();
                return c;
            }
        }

        public Productos ActualizarDescripcion(Int32 ideProducto,string descripcion)
        {
            using (AlmacenEntities entities = new AlmacenEntities())
            {
                Productos c = (from x in entities.Productos
                               where x.ideProducto == ideProducto
                               select x).First();
                c.descripcion = descripcion;
                entities.SaveChanges();
                return c;
            }
        }




        public Productos Get(int Id)
        {
            using (AlmacenEntities entities = new AlmacenEntities())
            {
                return entities.Productos.Find(Id);
            }

        }

        
       


        public async Task<Imagen> CrearProducto(string nombre, string descripcion)
        {
            // Validar multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = "D:\\tmp\\";
            var provider = new MultipartFormDataStreamProvider(root);


            try
            {

                await Request.Content.ReadAsMultipartAsync(provider);

                Imagen imagen = null;

                Productos producto = new Productos
                {
                    nombre = nombre,
                    descripcion = descripcion,
                    activo = true,
                    fechaRegistro = DateTime.Now
                };
              

                /**
                 @TODO terminar de implementar carga multiple de archivos
                 */
                MultipartFileData file = provider.FileData.First();
                 
                imagen = new Imagen
                {
                    nombre = file.Headers.ContentDisposition.FileName.Replace("\"", ""),
                    contentType = file.Headers.ContentType.ToString(),
                    ruta = file.LocalFileName,
                    activo = true
                };

                using (AlmacenEntities entities = new AlmacenEntities())
                {
                    entities.Productos.Add(producto);
                    entities.SaveChanges();
                    imagen.ideProducto = producto.ideProducto;
                    entities.Imagen.Add(imagen);
                    entities.SaveChanges();
                }

               
                string stringJsonResponse = JsonSerializer.Serialize(imagen);
                return imagen;
            }
            catch (System.Exception e)
            {
                return null;
            }
        }
    }
}
