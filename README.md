🏠 Inmobiliaria API – Sistema de Gestión Inmobiliaria - API REST

API REST desarrollada en .NET 10 para la gestión de propiedades inmobiliarias, agentes y reportes, aplicando Clean Architecture, DDD, CQRS, JWT Authentication 
y control de roles y acceso a datos mediante procedimientos almacenados.

📌 Tecnologías y patrones utilizados
.NET / ASP.NET Core Web API
Clean Architecture
DDD (Domain-Driven Design)
CQRS + Mediator Pattern
JWT Authentication
Autorización por Roles (Admin / User)
SQL Server
Stored Procedures
Swagger (OpenAPI)
Postman (colección local)
Custom ServiceResult y manejo centralizado de errores

🌱 Seed Automático de Datos InicialesAl ejecutar la aplicación por primera vez, se ejecutará automáticamente un proceso de seed que creará:
Roles del sistema:

Admin - Rol de administrador con permisos completos
User - Rol de usuario regular con permisos de solo lectura

🏘️ Módulo de Propiedades
PropertyType El tipo de propiedad está manejado como un enum en la base de datos se guarda como int y se mapea directamente al enum en el dominio.

public enum PropertyType
{
    Casa = 1,
    Departamento = 2,
    Oficina = 3,
    Terreno = 4,
    LocalComercial = 5
}

🚀 Cómo levantar el proyecto

1️⃣ Clonar el repositorio
2️⃣ Restaurar la base de datos
3️⃣ Configurar la cadena de conexión
4️⃣ Levantar la API
5️⃣ Swagger

📮 Postman
Se incluye una colección de Postman para pruebas locales.

🧪 Flujo recomendado de pruebas

Login (Admin)
Crear Agent
Crear Property
Listar propiedades
Etc.

📝 Notas finales
Este proyecto fue desarrollado como prueba técnica, priorizando:
Claridad

Buenas prácticas
Arquitectura limpia
Escalabilidad

## Decisiones Técnicas
### Acceso a Datos

Se implementó el acceso a datos utilizando **ADO.NET puro** sin ORM, cumpliendo estrictamente con el requisito de que "el acceso a la base de datos deberá ser únicamente por procedimientos almacenados".

#### Patrón implementado:
- **BaseRepository**: Clase abstracta que encapsula operaciones comunes.

#### Nota sobre Dapper:
Aunque Dapper es técnicamente un "micro-ORM", es simplemente un mapper de datos que: Se utilizo solo para la carga de roles, usuarios en el 🌱 Seed Automático.
