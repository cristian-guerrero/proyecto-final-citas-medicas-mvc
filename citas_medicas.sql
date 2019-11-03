

CREATE TABLE [usuarios] (
[id] int NOT NULL,
[nombres] varchar(255) NULL,
[apellidos] varchar(255) NULL,
[perfil] varchar(255) NULL,
[direccion] varchar(255) NULL,
[telefono] int NULL,
[usuario] varchar(255) NULL,
[password] varchar(255) NULL,
PRIMARY KEY ([id]) 
)
GO
CREATE TABLE [citas] (
[id] int NOT NULL,
[medico] int NULL,
[paciente] int NULL,
[fecha] datetime2 NULL,
[estado] varchar(255) NULL,
PRIMARY KEY ([id]) 
)

GO

ALTER TABLE [citas] ADD CONSTRAINT [fk_citas_usuarios_1] FOREIGN KEY ([paciente]) REFERENCES [usuarios] ([id])
GO
ALTER TABLE [citas] ADD CONSTRAINT [fk_citas_usuarios_2] FOREIGN KEY ([medico]) REFERENCES [usuarios] ([id])
GO

