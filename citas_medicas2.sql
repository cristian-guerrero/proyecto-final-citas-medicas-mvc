

DROP TABLE citas;
DROP  TABLE usuarios;

CREATE TABLE [usuarios] (
[id] int NOT NULL IDENTITY(1,1),
[nombres] varchar(255) NULL,
[apellidos] varchar(255) NULL,
[perfil] varchar(255) NOT NULL 
CHECK (perfil in ('medico','paciente','administrador')),
[direccion] varchar(255) NULL,
[telefono]  varchar(255) NULL,
[usuario] varchar(255) NULL,
[password] varchar(255) NULL,
PRIMARY KEY ([id]) 
)
GO
CREATE TABLE [citas] (
[id] int NOT NULL IDENTITY(1,1) ,
[medico] int NULL,
[paciente] int NULL,
[fecha] datetime2 NULL,
[estado] varchar(255) NULL,
[detalles] VARCHAR(MAX)
PRIMARY KEY ([id]) 
)

GO

ALTER TABLE [citas] ADD CONSTRAINT [fk_citas_usuarios_1] FOREIGN KEY ([paciente]) REFERENCES [usuarios] ([id])
GO
ALTER TABLE [citas] ADD CONSTRAINT [fk_citas_usuarios_2] FOREIGN KEY ([medico]) REFERENCES [usuarios] ([id])
GO

