create table materias(
	id serial primary key not null,
	nombre varchar(30) not null,
	id_carrera int,
	constraint fk_carrera
		foreign key (id_carrera)
		references carreras(id)
)