# BusApp

Este proyecto es una aplicación web desarrollada con **.NET** para el backend y **Angular** para el frontend.

## Tecnologías utilizadas

- [.NET 9](https://dotnet.microsoft.com/)
- [Angular 20](https://angular.io/)
- C#
- TypeScript
- HTML/CSS

## Arquitectura del Backend

La solución del backend está desarrollada con **.NET 9**, implementando el patrón **CQRS (Command and Query Responsibility Segregation)**.  
A diferencia de otras implementaciones, **no se separó la solución en distintos proyectos** para comandos y consultas; en su lugar, se estructura internamente dentro de un único proyecto, manteniendo la separación lógica mediante carpetas y convenciones de nombres.

El backend hace uso de varias librerías complementarias para estructurar la solución de forma robusta y escalable:

- Se utiliza **MediatR** para implementar el patrón CQRS mediante el uso de handlers desacoplados
- **FluentValidation** para la validación de sobre queries y comamnds de forma clara y extensible
- **Entity Framework Core** con **SQLite** como motor de persistencia
- La solución incluye un proyecto de **tests unitarios** desarrollada con **xUnit**, utilizando la librería **Moq** para la creación de mocks.

## Arquitectura del Frontend

El frontend está desarrollado con **Angular**, siguiendo una arquitectura modular basada en componentes y servicios.  
El manejo de rutas está realizao con **@angular/router**, los formularios está realzados usando **@angular/forms** facilitando la validación y el control de estado y **@angular/material** que provee una base de componentes de UI.

La aplicación está diseñada con principios de separación de responsabilidades:

- **Componentes**: Representan la interfaz de usuario y manejan la interacción con el usuario.
- **Servicios**: Manejan el consumo de APIs externas.
- **Models**: Contienen las interfaces y tipos TypeScript utilizados para representar datos y facilitar el tipado fuerte en toda la aplicación.
- **Utils**: Incluye funciones y helpers reutilizables que permiten abstraer lógica común y evitar duplicación de código.

## Requisitos previos

Para poder correr la aplicacion, es necesario tener instalados:

- [.NET SDK 9](https://dotnet.microsoft.com/)
- [Node.js 22+](https://nodejs.org/)
- [Angular CLI](https://angular.dev/tools/cli)
- [Docker](https://www.docker.com/)

---

## Ejecución del proyecto

### Ejecución local (sin Docker)

#### Backend

```bash
cd backend
dotnet restore
dotnet run
```

Corre en: https://localhost:7295

#### Frontend

```bash
cd frontend
npm install
ng serve
```

Corre en: http://localhost:4200/

#### Ejecución con Docker

#### Backend

Para buildear la imagen, pararse en la carpeta donde esta el Dockerfile del backend y correr:

```bash
docker build -t busapp-api .
```

Luego, para correr la Api ejecutar:

```bash
docker run -d -p 7777:80 --name busapp-api-container busapp-api
```

Corre en: http://localhost:7777

#### Frontend

Para hacer build de la imagen, pararse donde está el Dockerfile correspondiente al frontend y correr:

```bash
docker build --no-cache -t busapp-angular .
```

Luego, para correr la aplicacion ejecutar:

```bash
docker run -p 5555:4200 --name busapp-angular-container busapp-angular
```

Corre en: http://localhost:5555

## Posibles Extensiones

A continuación, quería detallar algunas ideas para extender y mejorar la solución en futuras iteraciones:

- **Sepeación del backend en diferentes proyectos**: Separar las carpetas creadas en proyectos es un pendiente.

- **Agregar logs con Serilog**: Integrar Serilog en el backend como herramiento de logging, facilitando el monitoreo y la trazabilidad de eventos y errores.

- **Mejorar el formato de los mensajes de error en el frontend**: Aplicar una estructura uniforme para mostrar errores al usuario, con mensajes claros, amigables y útiles, diferenciando errores de validación, del servidor y de red.

- **Refinar el manejo de errores en backend y frontend**: Implementar middlewares de captura global de errores en el backend y estrategias de manejo centralizado en Angular, para garantizar respuestas consistentes y una experiencia de usuario más robusta.

- **Mejorar la muestra de los errores de validacion en los formularios**: Crear componentes para el manejo de la muestra de los errores. Actualmente la lógica está repetida en varios componentes.

- **Internacionalización (i18n)**: Adaptar el frontend para soportar múltiples idiomas.

- **Autenticación y autorización**: Incorporar mecanismos de seguridad como JWT y guards en Angular para proteger rutas y recursos sensibles.
