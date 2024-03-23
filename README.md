## Proyecto TestGetechnologies

Es necesario tener instalado el SDK de .NET 6 para ejecutar los proyectos `https://dotnet.microsoft.com/en-us/download/dotnet/6.0`

**Como Ejecutar el proyecto con Visual Studio 2022**

-Clonar el proyecto.

- Abrir la solución `TestGetechnologies.sln` con Visual Studio 2022.

- Primero, ejecute el proyecto API dando clic derecho sobre el proyecto `TestGetechnologies.API`, luego dirigirse a la opción `Debug` y clic en `Start New Instance`.

- Después, ejecute el proyecto Cliente dando clic derecho sobre el proyecto `TestGetechnologies.Client`, luego dirigirse a la opción `Debug` y clic en `Start New Instance`.

- Una vez hecho esto, se abrirá el formulario para crear una Persona. Este formulario consume el endpoint `/api/DirectorioRestService/CreatePersona` del proyecto Web API.

Se pueden probar los enpoints del proyecto web API desde la página de Swagger incluida en el proyecto desde la dirección `http://localhost:5123/swagger/index.html` o desde Postman.

Se agregó un logger en el Endpoint `/api/DirectorioRestService/CreatePersona` y se puede ver en la consola cuando una Persona es creada correctamente.
