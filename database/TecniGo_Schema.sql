-- ============================================================================
-- PROYECTO TECNIGO - SCRIPT DE CREACIÓN DE BASE DE DATOS Y TABLAS
-- Motor: Microsoft SQL Server
-- Descripción: Script completo basado en el diccionario de datos de las 20 tablas.
-- ============================================================================

CREATE DATABASE TecniGO_DB;
GO

USE TecniGO_DB;
GO

-- ============================================================================
-- 1. TABLA: Rol
-- ============================================================================
CREATE TABLE Rol (
    RolId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(30) NOT NULL UNIQUE
);
GO

-- Inserción de roles iniciales
INSERT INTO Rol (Nombre) VALUES ('Cliente'), ('Tecnico'), ('Administrador');
GO

-- ============================================================================
-- 2. TABLA: Usuario
-- ============================================================================
CREATE TABLE Usuario (
    UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
    RolId INT NOT NULL,
    Nombre NVARCHAR(80) NOT NULL,
    Apellido NVARCHAR(80) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Telefono NVARCHAR(20) NULL,
    Activo BIT NOT NULL CONSTRAINT DF_Usuario_Activo DEFAULT 1,
    IntentosFallidos INT NOT NULL CONSTRAINT DF_Usuario_Intentos DEFAULT 0,
    BloqueadoHasta DATETIME2 NULL,
    UltimoAcceso DATETIME2 NULL,
    FechaRegistro DATETIME2 NOT NULL CONSTRAINT DF_Usuario_FechaReg DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Usuario_Rol FOREIGN KEY (RolId) REFERENCES Rol(RolId)
);
GO

-- ============================================================================
-- 3. TABLA: RecuperacionPassword
-- ============================================================================
CREATE TABLE RecuperacionPassword (
    RecuperacionId INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    TokenHash NVARCHAR(255) NOT NULL,
    FechaExpiracion DATETIME2 NOT NULL,
    Usado BIT NOT NULL CONSTRAINT DF_Recuperacion_Usado DEFAULT 0,
    CONSTRAINT FK_Recuperacion_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId)
);
GO

-- ============================================================================
-- 4. TABLA: Zona
-- ============================================================================
CREATE TABLE Zona (
    ZonaId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Municipio NVARCHAR(80) NOT NULL,
    Provincia NVARCHAR(80) NOT NULL,
    Activo BIT NOT NULL CONSTRAINT DF_Zona_Activo DEFAULT 1
);
GO

-- ============================================================================
-- 5. TABLA: Direccion
-- ============================================================================
CREATE TABLE Direccion (
    DireccionId INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    ZonaId INT NOT NULL,
    Alias NVARCHAR(50) NOT NULL,
    Calle NVARCHAR(150) NOT NULL,
    Numero NVARCHAR(20) NULL,
    Sector NVARCHAR(100) NOT NULL,
    Referencia NVARCHAR(250) NULL,
    Principal BIT NOT NULL CONSTRAINT DF_Direccion_Principal DEFAULT 0,
    CONSTRAINT FK_Direccion_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId),
    CONSTRAINT FK_Direccion_Zona FOREIGN KEY (ZonaId) REFERENCES Zona(ZonaId)
);
GO

-- ============================================================================
-- 6. TABLA: Tecnico (Relación 1 a 1 con Usuario)
-- ============================================================================
CREATE TABLE Tecnico (
    TecnicoId INT NOT NULL PRIMARY KEY,
    Cedula NVARCHAR(15) NOT NULL UNIQUE,
    Biografia NVARCHAR(500) NULL,
    EstadoValidacion NVARCHAR(20) NOT NULL CONSTRAINT DF_Tecnico_EstadoVal DEFAULT 'Pendiente',
    ValidadoPor INT NULL,
    FechaValidacion DATETIME2 NULL,
    MotivoRechazo NVARCHAR(250) NULL,
    Disponible BIT NOT NULL CONSTRAINT DF_Tecnico_Disponible DEFAULT 1,
    CalificacionPromedio DECIMAL(3,2) NOT NULL CONSTRAINT DF_Tecnico_CalifProm DEFAULT 0.00,
    TotalServicios INT NOT NULL CONSTRAINT DF_Tecnico_TotalServ DEFAULT 0,
    CONSTRAINT FK_Tecnico_Usuario FOREIGN KEY (TecnicoId) REFERENCES Usuario(UsuarioId),
    CONSTRAINT FK_Tecnico_ValidadoPor FOREIGN KEY (ValidadoPor) REFERENCES Usuario(UsuarioId),
    CONSTRAINT CK_Tecnico_EstadoVal CHECK (EstadoValidacion IN ('Pendiente', 'Aprobado', 'Rechazado'))
);
GO

-- ============================================================================
-- 7. TABLA: DocumentoTecnico
-- ============================================================================
CREATE TABLE DocumentoTecnico (
    DocumentoId INT IDENTITY(1,1) PRIMARY KEY,
    TecnicoId INT NOT NULL,
    Tipo NVARCHAR(30) NOT NULL,
    NombreArchivo NVARCHAR(200) NOT NULL,
    RutaArchivo NVARCHAR(300) NOT NULL,
    Estado NVARCHAR(20) NOT NULL CONSTRAINT DF_DocTecnico_Estado DEFAULT 'Pendiente',
    FechaCarga DATETIME2 NOT NULL CONSTRAINT DF_DocTecnico_FechaCarga DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_DocTecnico_Tecnico FOREIGN KEY (TecnicoId) REFERENCES Tecnico(TecnicoId),
    CONSTRAINT CK_DocTecnico_Tipo CHECK (Tipo IN ('Cedula', 'Certificacion', 'Referencia')),
    CONSTRAINT CK_DocTecnico_Estado CHECK (Estado IN ('Pendiente', 'Aprobado', 'Rechazado'))
);
GO

-- ============================================================================
-- 8. TABLA: Categoria
-- ============================================================================
CREATE TABLE Categoria (
    CategoriaId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(80) NOT NULL UNIQUE,
    Descripcion NVARCHAR(250) NULL,
    Activo BIT NOT NULL CONSTRAINT DF_Categoria_Activo DEFAULT 1
);
GO

-- ============================================================================
-- 9. TABLA: Servicio
-- ============================================================================
CREATE TABLE Servicio (
    ServicioId INT IDENTITY(1,1) PRIMARY KEY,
    CategoriaId INT NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(300) NULL,
    TarifaBase DECIMAL(10,2) NOT NULL,
    PrecioVisita DECIMAL(10,2) NOT NULL,
    Activo BIT NOT NULL CONSTRAINT DF_Servicio_Activo DEFAULT 1,
    CONSTRAINT FK_Servicio_Categoria FOREIGN KEY (CategoriaId) REFERENCES Categoria(CategoriaId),
    CONSTRAINT CK_Servicio_TarifaBase CHECK (TarifaBase >= 0),
    CONSTRAINT CK_Servicio_PrecioVisita CHECK (PrecioVisita >= 0)
);
GO

-- ============================================================================
-- 10. TABLA: TecnicoCategoria (Muchos a Muchos)
-- ============================================================================
CREATE TABLE TecnicoCategoria (
    TecnicoId INT NOT NULL,
    CategoriaId INT NOT NULL,
    PRIMARY KEY (TecnicoId, CategoriaId),
    CONSTRAINT FK_TecCat_Tecnico FOREIGN KEY (TecnicoId) REFERENCES Tecnico(TecnicoId),
    CONSTRAINT FK_TecCat_Categoria FOREIGN KEY (CategoriaId) REFERENCES Categoria(CategoriaId)
);
GO

-- ============================================================================
-- 11. TABLA: TecnicoZona (Muchos a Muchos)
-- ============================================================================
CREATE TABLE TecnicoZona (
    TecnicoId INT NOT NULL,
    ZonaId INT NOT NULL,
    PRIMARY KEY (TecnicoId, ZonaId),
    CONSTRAINT FK_TecZona_Tecnico FOREIGN KEY (TecnicoId) REFERENCES Tecnico(TecnicoId),
    CONSTRAINT FK_TecZona_Zona FOREIGN KEY (ZonaId) REFERENCES Zona(ZonaId)
);
GO

-- ============================================================================
-- 12. TABLA: Disponibilidad
-- ============================================================================
CREATE TABLE Disponibilidad (
    DisponibilidadId INT IDENTITY(1,1) PRIMARY KEY,
    TecnicoId INT NOT NULL,
    DiaSemana TINYINT NOT NULL,
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL,
    Activo BIT NOT NULL CONSTRAINT DF_Disponibilidad_Activo DEFAULT 1,
    CONSTRAINT FK_Disponibilidad_Tecnico FOREIGN KEY (TecnicoId) REFERENCES Tecnico(TecnicoId),
    CONSTRAINT CK_Disponibilidad_Dia CHECK (DiaSemana BETWEEN 1 AND 7),
    CONSTRAINT CK_Disponibilidad_Horas CHECK (HoraInicio < HoraFin)
);
GO

-- ============================================================================
-- 13. TABLA: Solicitud
-- ============================================================================
CREATE TABLE Solicitud (
    SolicitudId INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL,
    DireccionId INT NOT NULL,
    ServicioId INT NOT NULL,
    Descripcion NVARCHAR(1000) NOT NULL,
    FechaProgramada DATETIME2 NOT NULL,
    Urgente BIT NOT NULL CONSTRAINT DF_Solicitud_Urgente DEFAULT 0,
    Estado NVARCHAR(20) NOT NULL CONSTRAINT DF_Solicitud_Estado DEFAULT 'Pendiente',
    MotivoCancelacion NVARCHAR(250) NULL,
    FechaCreacion DATETIME2 NOT NULL CONSTRAINT DF_Solicitud_FechaCrea DEFAULT SYSUTCDATETIME(),
    FechaCierre DATETIME2 NULL,
    CONSTRAINT FK_Solicitud_Cliente FOREIGN KEY (ClienteId) REFERENCES Usuario(UsuarioId),
    CONSTRAINT FK_Solicitud_Direccion FOREIGN KEY (DireccionId) REFERENCES Direccion(DireccionId),
    CONSTRAINT FK_Solicitud_Servicio FOREIGN KEY (ServicioId) REFERENCES Servicio(ServicioId),
    CONSTRAINT CK_Solicitud_Estado CHECK (Estado IN ('Pendiente', 'Asignado', 'EnCamino', 'EnSitio', 'EnProgreso', 'Completado', 'Cancelado'))
);
GO

-- ============================================================================
-- 14. TABLA: Asignacion
-- ============================================================================
CREATE TABLE Asignacion (
    AsignacionId INT IDENTITY(1,1) PRIMARY KEY,
    SolicitudId INT NOT NULL,
    TecnicoId INT NOT NULL,
    Puntaje DECIMAL(6,2) NOT NULL CONSTRAINT DF_Asignacion_Puntaje DEFAULT 0,
    Estado NVARCHAR(20) NOT NULL CONSTRAINT DF_Asignacion_Estado DEFAULT 'Ofertada',
    FechaOferta DATETIME2 NOT NULL CONSTRAINT DF_Asignacion_FechaOferta DEFAULT SYSUTCDATETIME(),
    FechaRespuesta DATETIME2 NULL,
    MotivoRechazo NVARCHAR(250) NULL,
    CONSTRAINT FK_Asignacion_Solicitud FOREIGN KEY (SolicitudId) REFERENCES Solicitud(SolicitudId),
    CONSTRAINT FK_Asignacion_Tecnico FOREIGN KEY (TecnicoId) REFERENCES Tecnico(TecnicoId),
    CONSTRAINT CK_Asignacion_Estado CHECK (Estado IN ('Ofertada', 'Aceptada', 'Rechazada', 'Expirada'))
);
GO

-- ============================================================================
-- 15. TABLA: HistorialEstado
-- ============================================================================
CREATE TABLE HistorialEstado (
    HistorialId INT IDENTITY(1,1) PRIMARY KEY,
    SolicitudId INT NOT NULL,
    Estado NVARCHAR(20) NOT NULL,
    CambiadoPor INT NOT NULL,
    Fecha DATETIME2 NOT NULL CONSTRAINT DF_Historial_Fecha DEFAULT SYSUTCDATETIME(),
    Comentario NVARCHAR(250) NULL,
    CONSTRAINT FK_Historial_Solicitud FOREIGN KEY (SolicitudId) REFERENCES Solicitud(SolicitudId),
    CONSTRAINT FK_Historial_Usuario FOREIGN KEY (CambiadoPor) REFERENCES Usuario(UsuarioId)
);
GO

-- ============================================================================
-- 16. TABLA: Cotizacion
-- ============================================================================
CREATE TABLE Cotizacion (
    CotizacionId INT IDENTITY(1,1) PRIMARY KEY,
    SolicitudId INT NOT NULL,
    TecnicoId INT NOT NULL,
    ManoObra DECIMAL(10,2) NOT NULL CONSTRAINT DF_Cotizacion_ManoObra DEFAULT 0,
    Materiales DECIMAL(10,2) NOT NULL CONSTRAINT DF_Cotizacion_Materiales DEFAULT 0,
    Total AS (ManoObra + Materiales) PERSISTED,
    Descripcion NVARCHAR(500) NULL,
    Estado NVARCHAR(20) NOT NULL CONSTRAINT DF_Cotizacion_Estado DEFAULT 'Pendiente',
    FechaCreacion DATETIME2 NOT NULL CONSTRAINT DF_Cotizacion_FechaCrea DEFAULT SYSUTCDATETIME(),
    FechaRespuesta DATETIME2 NULL,
    CONSTRAINT FK_Cotizacion_Solicitud FOREIGN KEY (SolicitudId) REFERENCES Solicitud(SolicitudId),
    CONSTRAINT FK_Cotizacion_Tecnico FOREIGN KEY (TecnicoId) REFERENCES Tecnico(TecnicoId),
    CONSTRAINT CK_Cotizacion_Estado CHECK (Estado IN ('Pendiente', 'Aprobada', 'Rechazada'))
);
GO

-- ============================================================================
-- 17. TABLA: Pago
-- ============================================================================
CREATE TABLE Pago (
    PagoId INT IDENTITY(1,1) PRIMARY KEY,
    CotizacionId INT NOT NULL,
    Monto DECIMAL(10,2) NOT NULL,
    Metodo NVARCHAR(20) NOT NULL,
    Estado NVARCHAR(20) NOT NULL CONSTRAINT DF_Pago_Estado DEFAULT 'Pendiente',
    Referencia NVARCHAR(100) NULL,
    RegistradoPor INT NOT NULL,
    FechaPago DATETIME2 NOT NULL CONSTRAINT DF_Pago_FechaPago DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Pago_Cotizacion FOREIGN KEY (CotizacionId) REFERENCES Cotizacion(CotizacionId),
    CONSTRAINT FK_Pago_RegistradoPor FOREIGN KEY (RegistradoPor) REFERENCES Usuario(UsuarioId),
    CONSTRAINT CK_Pago_Monto CHECK (Monto > 0),
    CONSTRAINT CK_Pago_Metodo CHECK (Metodo IN ('Efectivo', 'Tarjeta', 'Transferencia')),
    CONSTRAINT CK_Pago_Estado CHECK (Estado IN ('Pendiente', 'Registrado', 'Reembolsado'))
);
GO

-- ============================================================================
-- 18. TABLA: Calificacion
-- ============================================================================
CREATE TABLE Calificacion (
    CalificacionId INT IDENTITY(1,1) PRIMARY KEY,
    SolicitudId INT NOT NULL UNIQUE,
    ClienteId INT NOT NULL,
    TecnicoId INT NOT NULL,
    Puntuacion TINYINT NOT NULL,
    Comentario NVARCHAR(500) NULL,
    Fecha DATETIME2 NOT NULL CONSTRAINT DF_Calificacion_Fecha DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Calificacion_Solicitud FOREIGN KEY (SolicitudId) REFERENCES Solicitud(SolicitudId),
    CONSTRAINT FK_Calificacion_Cliente FOREIGN KEY (ClienteId) REFERENCES Usuario(UsuarioId),
    CONSTRAINT FK_Calificacion_Tecnico FOREIGN KEY (TecnicoId) REFERENCES Tecnico(TecnicoId),
    CONSTRAINT CK_Calificacion_Puntuacion CHECK (Puntuacion BETWEEN 1 AND 5)
);
GO

-- ============================================================================
-- 19. TABLA: Disputa
-- ============================================================================
CREATE TABLE Disputa (
    DisputaId INT IDENTITY(1,1) PRIMARY KEY,
    SolicitudId INT NOT NULL,
    UsuarioId INT NOT NULL,
    Motivo NVARCHAR(500) NOT NULL,
    Estado NVARCHAR(20) NOT NULL CONSTRAINT DF_Disputa_Estado DEFAULT 'Abierta',
    Resolucion NVARCHAR(500) NULL,
    ResueltaPor INT NULL,
    FechaCreacion DATETIME2 NOT NULL CONSTRAINT DF_Disputa_FechaCrea DEFAULT SYSUTCDATETIME(),
    FechaResolucion DATETIME2 NULL,
    CONSTRAINT FK_Disputa_Solicitud FOREIGN KEY (SolicitudId) REFERENCES Solicitud(SolicitudId),
    CONSTRAINT FK_Disputa_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId),
    CONSTRAINT FK_Disputa_ResueltaPor FOREIGN KEY (ResueltaPor) REFERENCES Usuario(UsuarioId),
    CONSTRAINT CK_Disputa_Estado CHECK (Estado IN ('Abierta', 'EnRevision', 'Resuelta'))
);
GO

-- ============================================================================
-- 20. TABLA: Auditoria
-- ============================================================================
CREATE TABLE Auditoria (
    AuditoriaId BIGINT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NULL,
    Entidad NVARCHAR(50) NOT NULL,
    EntidadId INT NOT NULL,
    Accion NVARCHAR(20) NOT NULL,
    Detalle NVARCHAR(MAX) NULL,
    DireccionIP NVARCHAR(45) NULL,
    Fecha DATETIME2 NOT NULL CONSTRAINT DF_Auditoria_Fecha DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Auditoria_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId),
    CONSTRAINT CK_Auditoria_Accion CHECK (Accion IN ('Insertar', 'Actualizar', 'Eliminar'))
);
GO