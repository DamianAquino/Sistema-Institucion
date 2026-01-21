
CREATE TABLE usuarios (
    id SERIAL PRIMARY KEY,
	dni VARCHAR(8) NOT NULL,
	nombre VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    password VARCHAR(60) NOT NULL,
	carrera VARCHAR(30),
	ruta_foto VARCHAR(60) NOT NULL,
    rol VARCHAR(10) NOT NULL
);
