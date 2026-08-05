# IntroProyecto1

Este proyecto es una aplicación de consola en C# que muestra dos ideas principales:

1. Una lista simple de robots.
2. Una matriz ortogonal para representar una cuadrícula con nodos enlazados horizontal y verticalmente.


## Archivos del proyecto

### `Program.cs`
Es el punto de entrada de la aplicación. Aquí se crean instancias de robots, se agregan a la lista y se imprime una cuadrícula de ejemplo usando la matriz ortogonal.

### `robot.cs`
Define la clase abstracta `Robot`, que sirve como base para los distintos tipos de robots. Contiene los atributos comunes `nombre` y `capacidad`, y obliga a implementar los métodos `Mover` y `RestarCapacidad`.

### `Fighter.cs`
Implementa el robot `Fighter`. Este robot tiene capacidad inicial configurable y puede moverse si supera la capacidad de la unidad militar indicada.

### `Recue.cs`
Implementa el robot `Recue`. Representa un robot de rescate; su capacidad es `0` y su comportamiento de movimiento está orientado a evitar unidades militares.

### `ListaRobot.cs`
Implementa una lista enlazada simple para almacenar robots. Incluye una clase interna `NodoRobot` y métodos para agregar robots al final de la lista y mostrarlos por consola.

### `Nodo.cs`
Define el nodo base usado por la matriz ortogonal. Cada nodo guarda un carácter, su posición (`fila` y `columna`) y referencias a los nodos vecinos: `siguiente`, `anterior`, `arriba` y `abajo`.

### `MatrizOrtogonal.cs`
Implementa la estructura de matriz ortogonal. Permite crear cabeceras de filas y columnas, insertar nodos en coordenadas específicas y mostrar la cuadrícula por consola.


## Resumen del funcionamiento

Al ejecutar el programa, se crean robots de ejemplo, se agregan a una lista y luego se construye una matriz con caracteres como `*`, `E` y `R` para mostrar cómo funciona la estructura ortogonal.
