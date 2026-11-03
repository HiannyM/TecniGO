-- ============================================================================
-- PROYECTO TECNIGO - SCRIPT DE DATOS DE PRUEBA (SEED DATA)
-- Archivo: database/SeedData.sql
-- Descripci�n: Inserta datos iniciales de zonas, categor�as, servicios,
--              usuarios de prueba (Clientes, T�cnicos, Admin) y solicitudes.
-- ============================================================================

USE TecniGO_DB;
GO

-- ----------------------------------------------------------------------------
-- 1. ZONAS Y SECTORES DE COBERTURA
-- ----------------------------------------------------------------------------
INSERT INTO Zona (Nombre, Municipio, Provincia, Activo) VALUES
('Piantini', 'Santo Domingo de Guzm�n', 'Distrito Nacional', 1),
('Naco', 'Santo Domingo de Guzm�n', 'Distrito Nacional', 1),
('Bella Vista', 'Santo Domingo de Guzm�n', 'Distrito Nacional', 1),
('Alma Rosa I', 'Santo Domingo Este', 'Santo Domingo', 1),
('Las Am�ricas', 'Santo Domingo Este', 'Santo Domingo', 1);
GO

-- ----------------------------------------------------------------------------
-- 2. CATEGORiAS DE SERVICIO
-- ----------------------------------------------------------------------------
INSERT INTO Categoria (Nombre, Descripcion, Activo) VALUES
('Electricidad', 'Instalaciones el�ctricas, reparaci�n de cortocircuitos y paneles', 1),
('Plomer�a', 'Reparaci�n de tuber�as, filtraciones, tinacos y bombas de agua', 1),
('Aire Acondicionado', 'Mantenimiento, instalaci�n y reparaci�n de equipos Split e Inverter', 1),
('Inform�tica & Redes', 'Reparaci�n de PC, configuraci�n de Wi-Fi y cableado estructurado', 1);
GO

-- ----------------------------------------------------------------------------
-- 3. SERVICIOS Y TARIFAS BASE
-- ----------------------------------------------------------------------------
INSERT INTO Servicio (CategoriaId, Nombre, Descripcion, TarifaBase, PrecioVisita, Activo) VALUES
(1, 'Diagn�stico El�ctrico General', 'Revisi�n general del sistema el�ctrico e identificaci�n de fallas', 1500.00, 500.00, 1),
(1, 'Instalaci�n de L�mparas / Abanicos', 'Instalaci�n y fijaci�n de luminarias o abanicos de techo', 1200.00, 400.00, 1),
(2, 'Reparaci�n de Filtraci�n / Fuga', 'Detecci�n y correcci�n de fugas en tuber�as visibles o empotradas', 2000.00, 600.00, 1),
(2, 'Mantenimiento de Bomba de Agua', 'Revisi�n de autom�tico, sellos y motor de la bomba de agua', 1800.00, 500.00, 1),
(3, 'Mantenimiento Preventivo A/C Split', 'Limpieza profunda de consola interna y condensador externo', 1500.00, 500.00, 1),
(4, 'Diagn�stico y Mantenimiento de PC', 'Limpieza f�sica, cambio de pasta t�rmica y optimizaci�n del SO', 1500.00, 500.00, 1);
GO

-- ----------------------------------------------------------------------------
-- 4. USUARIOS (Passwords en hash gen�rico de ejemplo)
-- Nota: RolId (1 = Cliente, 2 = T�cnico, 3 = Administrador)
-- ----------------------------------------------------------------------------
INSERT INTO Usuario (RolId, Nombre, Apellido, Email, PasswordHash, Telefono, Activo) VALUES
(3, 'Admin', 'TecniGO', 'admin@tecnigo.com', 'AQAAAAEAACcQAAAAEHashEjemploAdmin123==', '8095550000', 1),
(1, 'Carlos', 'Mendoza', 'carlos.mendoza@email.com', 'AQAAAAEAACcQAAAAEHashEjemploCliente123==', '8095551111', 1),
(2, 'Juan', 'P�rez', 'juan.perez@tecnigo.com', 'AQAAAAEAACcQAAAAEHashEjemploTecnico123==', '8095552222', 1),
(2, 'Mar�a', 'Rodr�guez', 'maria.rodriguez@tecnigo.com', 'AQAAAAEAACcQAAAAEHashEjemploTecnico456==', '8095553333', 1);
GO

-- ----------------------------------------------------------------------------
-- 5. DIRECCIONES DE CLIENTES
-- ----------------------------------------------------------------------------
INSERT INTO Direccion (UsuarioId, ZonaId, Alias, Calle, Numero, Sector, Referencia, Principal) VALUES
(2, 1, 'Casa', 'Calle Porfirio Herrera', '12', 'Piantini', 'Frente al Edificio Profesional', 1);
GO

-- ----------------------------------------------------------------------------
-- 6. PERFILES DE T�CNICOS Y DOCUMENTOS
-- ----------------------------------------------------------------------------
-- Juan P�rez (T�cnico de Electricidad y A/C)
INSERT INTO Tecnico (TecnicoId, Cedula, Biografia, EstadoValidacion, ValidadoPor, FechaValidacion, Disponible, CalificacionPromedio, TotalServicios) VALUES
(3, '00112345678', 'T�cnico certificado en refrigeraci�n e instalaciones el�ctricas residenciales con m�s de 5 a�os de experiencia.', 'Aprobado', 1, SYSUTCDATETIME(), 1, 4.85, 12);

-- Mar�a Rodr�guez (T�cnica en Plomer�a e Inform�tica)
INSERT INTO Tecnico (TecnicoId, Cedula, Biografia, EstadoValidacion, ValidadoPor, FechaValidacion, Disponible, CalificacionPromedio, TotalServicios) VALUES
(4, '00198765432', 'Especialista en plomer�a residencial avanzada y redes de comunicaci�n.', 'Aprobado', 1, SYSUTCDATETIME(), 1, 5.00, 8);
GO

-- Especialidades de los t�cnicos
INSERT INTO TecnicoCategoria (TecnicoId, CategoriaId) VALUES
(3, 1), -- Juan -> Electricidad
(3, 3), -- Juan -> Aire Acondicionado
(4, 2), -- Mar�a -> Plomer�a
(4, 4); -- Mar�a -> Inform�tica
GO

-- Zonas de cobertura de los t�cnicos
INSERT INTO TecnicoZona (TecnicoId, ZonaId) VALUES
(3, 1), (3, 2), (3, 3), -- Juan -> Piantini, Naco, Bella Vista
(4, 4), (4, 5);          -- Mar�a -> Alma Rosa I, Las Am�ricas
GO

-- Horarios de disponibilidad (1=Lunes a 5=Viernes)
INSERT INTO Disponibilidad (TecnicoId, DiaSemana, HoraInicio, HoraFin, Activo) VALUES
(3, 1, '08:00:00', '17:00:00', 1),
(3, 2, '08:00:00', '17:00:00', 1),
(3, 3, '08:00:00', '17:00:00', 1),
(4, 1, '09:00:00', '18:00:00', 1),
(4, 2, '09:00:00', '18:00:00', 1);
GO

-- ----------------------------------------------------------------------------
-- 7. SOLICITUD DE PRUEBA Y ASIGNACI�N
-- ----------------------------------------------------------------------------
INSERT INTO Solicitud (ClienteId, DireccionId, ServicioId, Descripcion, FechaProgramada, Urgente, Estado, FechaCreacion) VALUES
(2, 1, 5, 'El aire del cuarto principal no est� enfriando bien y bota un poco de agua.', DATEADD(day, 2, SYSUTCDATETIME()), 0, 'Asignado', SYSUTCDATETIME());

-- Registrar la asignaci�n oferta al t�cnico Juan P�rez
INSERT INTO Asignacion (SolicitudId, TecnicoId, Puntaje, Estado, FechaOferta, FechaRespuesta) VALUES
(1, 3, 95.50, 'Aceptada', SYSUTCDATETIME(), SYSUTCDATETIME());

-- Historial de estado inicial
INSERT INTO HistorialEstado (SolicitudId, Estado, CambiadoPor, Fecha, Comentario) VALUES
(1, 'Pendiente', 2, SYSUTCDATETIME(), 'Solicitud creada por el cliente'),
(1, 'Asignado', 3, SYSUTCDATETIME(), 'T�cnico acept� la solicitud de servicio');
GO