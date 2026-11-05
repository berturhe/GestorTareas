# Gestor de Tareas Colaborativo (.NET 8)

Aplicación web para la gestión de tareas en equipo mediante un tablero Kanban interactivo, desarrollada como proyecto de portafolio.

🔗 **[Ver aplicación en vivo (Demo)](https://gestortareas-4u8f.onrender.com)**

## Tecnologías Utilizadas

*   **Backend:** C# / .NET 8 (Razor Pages)
*   **Base de Datos:** Entity Framework Core + SQLite
*   **Frontend:** HTML5, CSS3 nativo (Flexbox), JavaScript (Fetch API)
*   **Seguridad:** Tokens CSRF (AntiForgeryToken)

## Arquitectura

El proyecto utiliza una estructura en capas simplificada. Evité intencionalmente la sobreingeniería (como Clean Architecture pura con múltiples proyectos) para mantener el código legible, pero aplicando una separación estricta de responsabilidades:

*   **Models:** Clases que representan las tablas de la base de datos y sus reglas básicas.
*   **Services:** Aquí vive la lógica de negocio y las consultas a la base de datos (usando LINQ). De esta forma, los controladores no se acoplan directamente a Entity Framework.
*   **ViewModels:** Modelos exclusivos para la interfaz gráfica. Me permiten validar los datos del usuario con Data Annotations antes de tocar la base de datos.
*   **Pages (Razor Pages):** Los controladores de la vista. Reciben las peticiones, usan los servicios y devuelven la información al HTML. 

Para la interactividad del tablero (cambio de estados), el frontend utiliza JavaScript nativo (Fetch API) para comunicarse con el backend sin recargar la página, consumiendo un Handler específico en el PageModel.

## Ejecución Local

**Requisitos previos:** Tener instalado el SDK de .NET 8.

1. Clonar el repositorio.
2. Ejecutar `dotnet restore` para restaurar los paquetes NuGet.
3. Ejecutar `dotnet ef database update` para generar la base de datos e inyectar los datos semilla (Seed Data).
4. Ejecutar `dotnet run` y abrir la ruta indicada en la consola.

## Nota sobre el Despliegue y SQLite

El proyecto está desplegado en **Render** utilizando su capa gratuita. Es importante mencionar dos comportamientos propios de este entorno:

1.  **Cold Start (Modo de suspensión):** Si el servidor no recibe tráfico durante un tiempo, Render lo pone en pausa para ahorrar recursos. Cuando un usuario entra por primera vez, el servidor tarda unos segundos extra en "despertar" y cargar la aplicación. Es un comportamiento esperado.
2.  **Persistencia efímera:** La capa gratuita utiliza un sistema de archivos temporal. Como el proyecto usa SQLite (un archivo local), si el servidor se reinicia, la base de datos se borra. **Sin embargo**, para que la aplicación nunca se vea vacía o rota, configuré una Migración Automática y la inyección de datos semilla (Seed Data) en el `Program.cs`. Cada vez que el servidor despierta, SQLite se regenera automáticamente con los datos de prueba, garantizando que la app siempre esté funcional para su evaluación.

---
*Disclaimer: Proyecto desarrollado con fines de portafolio. Los datos mostrados son ficticios.*
