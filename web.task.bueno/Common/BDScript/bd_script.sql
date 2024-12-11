/*
CREATE DATABASE TestTaskBueno
GO
*/

USE TestTaskBueno
GO

--DROP TABLE Usuario
CREATE TABLE Usuario (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	NombreCompleto VARCHAR(160),
	Correo VARCHAR(150),
	Clave VARCHAR(200),
	Activo BIT DEFAULT '0',
	FechaCreacion DATETIME DEFAULT GETDATE(),
	FechaModificacion DATETIME
)

--DROP TABLE Tarea
CREATE TABLE Tarea (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdUsuario INT,
	Titulo VARCHAR(150),
	Descripcion VARCHAR(200),
	FechaCreacion DATETIME DEFAULT GETDATE(),
	FechaModificacion DATETIME
	CONSTRAINT FK_Usuario_Temp FOREIGN KEY (IdUsuario)
    REFERENCES Usuario(Id)
)


SELECT * FROM Tarea
SELECT * FROM Usuario
