
CREATE TABLE usuarios (
    id SERIAL PRIMARY KEY,
	dni VARCHAR(8) NOT NULL,
    email VARCHAR(50) NOT NULL,
    password VARCHAR(60) NOT NULL,
    rol int NOT NULL
);
