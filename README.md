# Sistema de Gestión de Cine

Este proyecto es una aplicación de escritorio desarrollada en C# con Windows Forms. Está diseñada para gestionar información sobre películas, incluyendo la creación, modificación y eliminación de registros de películas en una base de datos.

## Características

- **Gestión de Películas**: Permite añadir, editar, buscar y eliminar películas.
- **Ordenamiento y Búsqueda**: Implementa algoritmos de ordenamiento y búsqueda para manejar eficientemente los datos de películas.
- **Interfaz de Usuario**: Ofrece una interfaz gráfica para interactuar con la base de datos y realizar las operaciones de gestión.

## Dependencias

Este proyecto requiere las siguientes dependencias para su ejecución:

- **.NET Framework**: Versión 4.7.2 o superior.
- **Entity Framework**: Utilizado para la interacción con la base de datos.
- **SQLite**: Como sistema de gestión de base de datos.

## Configuración y Ejecución

### Clonar el repositorio

```bash
git clone 
cd proyecto_cine
```

### Configuración de la Base de Datos

1. Asegúrate de tener SQLite instalado y configurado.
2. Inicializa la base de datos utilizando los scripts SQL proporcionados en el directorio `Database`.

### Compilación y Ejecución

Utiliza Visual Studio para abrir el archivo de solución `.sln`.

1. Abre Visual Studio.
2. Navega a `Archivo` > `Abrir` > `Proyecto/Solución`.
3. Selecciona el archivo de solución `.sln` del directorio clonado.
4. Compila el proyecto yendo a `Compilar` > `Compilar Solución`.
5. Ejecuta el proyecto presionando `F5` o haciendo clic en `Iniciar`.

## Uso de la Aplicación

La aplicación tiene una interfaz de usuario basada en formularios donde puedes seleccionar diferentes opciones como agregar, editar, buscar y eliminar películas desde el menú principal.



