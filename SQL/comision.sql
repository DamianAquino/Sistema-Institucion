create table comision(
	id serial primary key not null,
	id_materia int not null,
	id_carrera int not null,
	constraint fk_materia
		foreign key (id_materia)
		references materias(id),
	constraint fk_carrera
		foreign key (id_carrera)
		references carreras(id)
)