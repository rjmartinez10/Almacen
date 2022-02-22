using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlmacenController.Model;

namespace Almacen.Controllers
{
    public class CategoriasController : ApiController
    {


        public IEnumerable<Categorias> Get()
        {
            using (AlmacenEntities entities = new AlmacenEntities())
            {
                return entities.Categorias.ToList();
            }

        }



        public Categorias Get(int Id)
        {
            using (AlmacenEntities entities = new AlmacenEntities())
            {
                return entities.Categorias.Find(Id);
            }

        }


        public Categorias CrearCategoria(string nombre)
        {
            using (AlmacenEntities entities = new AlmacenEntities())
            {
                Categorias categoria = new Categorias
                {
                    nombre = nombre,
                    fechaRegistro = DateTime.Now,
                    activo = true
                };
                entities.Categorias.Add(categoria);
                entities.SaveChanges();
                return categoria;
            }

        }

    }
}