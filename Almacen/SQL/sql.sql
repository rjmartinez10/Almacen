/**
drop table Imagen
drop table Categorias
drop table Productos
*/


/**
Categorias
*/
create table Categorias(
ideCategoria int identity (1,1),
nombre varchar(200),
activo bit,
fechaRegistro datetime default getdate(),
primary key(ideCategoria)
)

/**
Productos registrados
*/
create table Productos(
ideProducto int identity (1,1),
nombre varchar(100),
descripcion varchar(500),
activo bit,
fechaRegistro datetime default getdate(),
ideCategoria int,
primary key(ideProducto),
foreign key(ideCategoria) references Categorias(ideCategoria)
)




/**
Imagenes
*/
create table Imagen(
ideImagen int identity(1,1),
nombre varchar(100),
contentType varchar(100),
ruta varchar(500),
activo bit,
fechaRegistro datetime default getdate(),
ideProducto int,
foreign key(ideProducto) references Productos(ideProducto),
primary key(ideImagen)
)