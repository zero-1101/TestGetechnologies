## Proyecto TestGetechnologies

Es necesario tener instalado el SDK de .NET 6 para ejecutar los proyectos `https://dotnet.microsoft.com/en-us/download/dotnet/6.0`

**Como Ejecutar el proyecto con Visual Studio 2022**

-Clonar el proyecto.

- Abrir la solucion `TestGetechnologies.sln` con visual studio 2022.

- Primero ejecutar el proyecto API dando clic derecho sobre el proyecto `TestGetechnologies.API`, luego dirigirse a la opcion `Debug` y clic en `Start New Instance`.

- Despues ejecutar el proyecto Cliente dando clic derecho sobre el proyecto `TestGetechnologies.Client`, luego dirigirse a la opcion `Debug` y clic en `Start New Instance`.

- Una vez echo esto se abrira el formulario para crear una Persona, este formulario consume el endpoint `/api/DirectorioRestService/CreatePersona` del proyecto Web Api.

Se pueden probar los enpoints del proyecto web Api desde la pagina de Swagger incluida en el proyecto desde la direccion `http://localhost:5123/swagger/index.html` o desde Postman.

Se agrego un logger en el Endpoint `/api/DirectorioRestService/CreatePersona` y se puede ver en la consola cuando una Persona es creada correctamente.
