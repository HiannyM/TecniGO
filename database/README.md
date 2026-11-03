# 🗄️ TecniGO – Base de Datos (SQL Server)

Repositorio oficial de la base de datos del proyecto **TecniGO**, una plataforma web y móvil para la contratación y asignación automática de técnicos a domicilio.

Este repositorio contiene el diseño, la estructura y los scripts SQL necesarios para crear y mantener la base de datos del sistema.

---

## 📋 Descripción general

La base de datos de TecniGO está desarrollada en **Microsoft SQL Server** y consta de 20 tablas normalizadas que permiten gestionar las principales operaciones de la plataforma:

- Usuarios, perfiles y roles.
- Registro y validación de técnicos.
- Especialidades y zonas de cobertura.
- Catálogo de servicios.
- Solicitudes y asignaciones de técnicos.
- Disponibilidad y horarios.
- Cotizaciones y pagos.
- Calificaciones e historial de servicios.
- Disputas y auditoría del sistema.

Su estructura está diseñada para garantizar la integridad, consistencia y organización de los datos utilizados por el backend de TecniGO.

## 🛠️ Tecnologías utilizadas

| Tecnología | Descripción |
|---|---|
| Microsoft SQL Server | Sistema gestor de base de datos. |
| T-SQL | Lenguaje utilizado para definir y manipular los datos. |
| SSMS | Herramienta para ejecutar y administrar scripts SQL. |
| Git | Control de versiones local. |
| GitHub | Repositorio compartido del equipo. |

Se utilizan elementos de SQL Server como:

- DDL y DML.
- Claves primarias y foráneas.
- Restricciones de integridad (`CHECK`, `UNIQUE`, `NOT NULL`).
- Triggers.
- Procedimientos almacenados.

---

## 📂 Estructura del repositorio

```text
TecniGO/
├── database/                   <-- Carpeta principal de Base de Datos
│   ├── README.md               <-- Instrucciones para ejecutar la BD
│   ├── TecniGO_Schema.sql      <-- Script maestro con la estructura (20 tablas)
│   ├── SeedData.sql            <-- Script con datos de prueba (roles, categorías, servicios)
│   └── migrations/             <-- Opcional: cambios pequeños o updates futuros
│       └── 2026-04-14_nueva_tabla.sql
├── src/                        <-- Código del proyecto (Backend / Frontend)
└── .gitignore
```

### Archivo principal

**`TecniGO_Schema.sql`**

Contiene el script de creación de la base de datos `TecniGO_DB`, sus 20 tablas, relaciones, restricciones y datos iniciales de los roles del sistema.

---

## 🚀 Instalación de la base de datos

Cada integrante del equipo debe crear su propia copia local de la base de datos para trabajar durante el desarrollo.

### Requisitos previos

Antes de comenzar, asegúrate de tener instalado:

- Microsoft SQL Server.
- SQL Server Management Studio (SSMS).
- Git.
- Acceso al repositorio de TecniGO en GitHub.

### Paso 1. Obtener la última versión del repositorio

Si todavía no tienes el proyecto en tu computadora, clónalo:

```bash
git clone URL_DEL_REPOSITORIO
```

Si ya tienes el repositorio clonado, abre una terminal en la carpeta del proyecto y ejecuta:

```bash
git pull origin main
```

Esto descargará los últimos cambios disponibles en la rama `main`.

### Paso 2. Abrir el script SQL

1. Abre SQL Server Management Studio (SSMS).
2. Conéctate a tu instancia local de SQL Server.
3. Selecciona **File → Open → File**.
4. Busca el archivo `TecniGO_Schema.sql` dentro de la carpeta `database`.
5. Abre el archivo y revisa que el nombre de la base de datos sea `TecniGO_DB`.

### Paso 3. Ejecutar el script

1. Asegúrate de que estás conectado a la instancia correcta de SQL Server.
2. Pulsa **Execute** o presiona `F5`.
3. Espera a que finalice la ejecución.
4. Verifica que no se hayan producido errores en la ventana de resultados.

### Paso 4. Verificar la creación de la base de datos

En el Explorador de objetos de SSMS:

1. Expande la sección `Databases`.
2. Busca `TecniGO_DB`.
3. Expande `Tables`.
4. Comprueba que las tablas se hayan creado correctamente.

También puedes ejecutar:

```sql
USE TecniGO_DB;
GO

SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
```

Si aparecen las tablas esperadas, la base de datos está lista para utilizarse.

> **Importante:** Antes de ejecutar el script nuevamente, verifica si la base de datos ya existe. El script inicial debe utilizarse para crear una instalación nueva, no para actualizar una base de datos existente.

---

## 🔄 Cómo realizar cambios en la base de datos

Cualquier modificación en la estructura de TecniGO debe ser comunicada al equipo para mantener sincronizado el desarrollo del backend.

### 1. Identificar el cambio

Antes de modificar la base de datos, informa al equipo qué deseas agregar, eliminar o modificar.

Ejemplos:

- Crear una nueva tabla.
- Agregar una columna.
- Modificar un tipo de dato.
- Crear una nueva relación.
- Agregar una restricción.
- Modificar un procedimiento almacenado.

### 2. Realizar y probar el cambio localmente

Ejecuta el cambio en tu instancia local de SQL Server y comprueba que funcione correctamente.

No realices modificaciones directamente en la base de datos compartida sin autorización del equipo.

### 3. Actualizar el script correspondiente

Los cambios deben quedar registrados en los archivos SQL del repositorio.

Para modificaciones posteriores a la creación inicial, se recomienda utilizar scripts de migración.

Ejemplo:

```text
database/
│
├── TecniGO_Schema.sql
│
├── migrations/
│   ├── 001_AddTelefonoToCliente.sql
│   ├── 002_AddEstadoToSolicitud.sql
│   └── 003_CreateProcedureAsignacion.sql
│
└── README.md
```

Cada migración debe contener únicamente los cambios necesarios para actualizar la estructura existente.

### 4. Compartir el cambio con GitHub

Una vez validado el cambio:

```bash
git add database
git commit -m "db: descripción del cambio"
git push origin main
```

Ejemplos de mensajes de commit:

```text
db: se añade tabla de disputas
db: se agrega columna telefono a cliente
db: se actualiza procedimiento de asignación
db: se modifica restricción de solicitud
```

### 5. Actualizar la base de datos de los demás integrantes

Los demás integrantes deben:

1. Descargar los últimos cambios del repositorio.
2. Revisar las migraciones pendientes.
3. Ejecutarlas en su instancia local de SQL Server.
4. Verificar que la estructura de su base de datos esté actualizada.

---

## 👥 Normas de colaboración

Para evitar conflictos y errores durante el desarrollo, todos los integrantes deben cumplir las siguientes reglas:

- **Comunicar los cambios:** cualquier modificación de tablas, columnas, relaciones o procedimientos debe informarse al equipo.
- **Mantener los scripts actualizados:** los cambios validados deben registrarse en GitHub.
- **No modificar directamente la base de datos de otro integrante:** cada desarrollador trabajará en su instancia local.
- **No sobrescribir cambios de otros miembros:** antes de subir modificaciones, actualizar el repositorio y revisar los cambios existentes.
- **Mantener compatibilidad con el backend:** los cambios en nombres de tablas, columnas o tipos de datos deben coordinarse con los desarrolladores backend.
- **Proteger la información:** no subir contraseñas, credenciales, datos personales reales ni configuraciones sensibles.
- **Probar antes de compartir:** todo script debe ejecutarse y verificarse antes de integrarse al repositorio.

---

## 🔗 Relación con el Backend

La base de datos TecniGO será utilizada por el backend desarrollado con **ASP.NET Core Web API**.

Los desarrolladores backend deben mantener sus entidades, modelos y consultas alineados con la estructura de SQL Server.

Cualquier cambio que afecte la estructura de la base de datos deberá coordinarse entre los responsables de SQL Server y backend.

---

## 📌 Consideraciones importantes

- `TecniGO_Schema.sql` representa el esquema inicial de la base de datos.
- Las migraciones permiten registrar y compartir modificaciones posteriores.
- GitHub es la fuente oficial de los scripts SQL del equipo.
- Cada integrante debe mantener su base de datos local sincronizada.
- Antes de aplicar cambios importantes, se recomienda realizar un respaldo de la base de datos.

---

**Proyecto:** TecniGO  
**Tecnología:** Microsoft SQL Server  
**Repositorio:** Base de datos oficial del proyecto  
**Desarrollo:** Equipo TecniGO
