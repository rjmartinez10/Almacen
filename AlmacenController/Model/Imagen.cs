

namespace AlmacenController.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Imagen
    {
        public int ideImagen { get; set; }
        public string nombre { get; set; }
        public string contentType { get; set; }
        public string ruta { get; set; }
        public Nullable<bool> activo { get; set; }
        public Nullable<System.DateTime> fechaRegistro { get; set; }
        public Nullable<int> ideProducto { get; set; }
    }
}
