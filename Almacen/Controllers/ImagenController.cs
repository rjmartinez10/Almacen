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
    public class ImagenController : ApiController
    {  
        [HttpGet]
        public string TraerImagen(int ideProducto) {

            using (AlmacenEntities entities = new AlmacenEntities())
            {
                var imgs = from i in entities.Imagen
                                where i.activo == true
                                select i;
                List<Imagen> imagenes = imgs.ToList();
                if (imagenes.Count == 0)
                {
                    return "";
                }
                else {
                    Imagen i = imagenes.FirstOrDefault();
                    byte[] imageArray = System.IO.File.ReadAllBytes(i.ruta);
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    return "data:"+i.contentType+";base64,"+base64ImageRepresentation;
                }
                
                
            }
            
        

        }
         

   
      
    }
}