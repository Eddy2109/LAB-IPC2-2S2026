# Modelo entidad-relación (ER) — API SAT

## 1. Descripción general

La base de datos SQLite almacena los contribuyentes registrados en el Registro Tributario Unificado (RTU), las facturas autorizadas, los errores detectados durante el procesamiento de solicitudes y el correlativo diario utilizado para generar códigos de autorización.

El modelo está compuesto por cuatro tablas:

- `Contribuyente`
- `Factura`
- `Error_Validacion`
- `Correlativo`

Las solicitudes que contienen errores no se almacenan en `Factura`. Sus errores se registran en `Error_Validacion`, de forma independiente de las facturas autorizadas.

> **Nota sobre el código de autorización:** se indicó que siempre tiene 16 dígitos. Se documenta como `TEXT` para conservar exactamente todos sus dígitos y evitar problemas de formato. Si se implementa como número, debe utilizarse un tipo entero de 64 bits (`INTEGER` en SQLite), nunca un entero de 32 bits.

![Diagrama ER de la base de datos](Modelo%20Entidad-Relacion.png)

## 2. Tipos de datos

SQLite utiliza un sistema de tipos flexible. En este diseño se emplean las siguientes convenciones:

- `INTEGER`: números enteros; también se utiliza para identificadores autoincrementales.
- `TEXT`: cadenas de texto, NIT, fechas normalizadas y códigos que deben conservar su formato.
- `NUMERIC`: importes monetarios, como valor, IVA y total.

Las fechas almacenadas como texto deben utilizar un formato uniforme para que las consultas por rango funcionen correctamente:
- Fecha: `YYYY-MM-DD`
- Fecha y hora: `YYYY-MM-DD HH:MM:SS`

## 3. Tabla `Contribuyente`

Almacena los contribuyentes válidos registrados en el RTU.

| Campo | Tipo | Restricciones | Descripción |
|---|---|---|---|
| `id_contribuyente` | `INTEGER` | PK, autoincremental | Identificador interno único del contribuyente. |
| `nit` | `TEXT` | NOT NULL, UNIQUE | NIT del contribuyente. Se almacena como texto para conservar el dígito verificador y posibles ceros iniciales. |
| `nombre` | `TEXT` | NOT NULL | Nombre o razón social del contribuyente. |

### Relación con `Factura`

Un contribuyente puede emitir muchas facturas y también puede recibir muchas facturas. La tabla `Factura` usa dos llaves foráneas distintas para representar esos roles.

## 4. Tabla `Factura`

Almacena exclusivamente las facturas que fueron autorizadas correctamente.

| Campo | Tipo | Restricciones | Descripción |
|---|---|---|---|
| `id_factura` | `INTEGER` | PK, autoincremental | Identificador interno de la factura. |
| `id_contr_e` | `INTEGER` | FK, NOT NULL | Identificador del contribuyente emisor; referencia a `Contribuyente.id_contribuyente`. |
| `nit_emisor` | `TEXT` | NOT NULL | NIT del emisor tal como aparece en el mensaje recibido. |
| `id_contr_r` | `INTEGER` | FK, NOT NULL | Identificador del contribuyente receptor; referencia a `Contribuyente.id_contribuyente`. |
| `nit_receptor` | `TEXT` | NOT NULL | NIT del receptor tal como aparece en el mensaje recibido. |
| `fecha` | `TEXT` | NOT NULL | Fecha y hora de emisión normalizadas como `YYYY-MM-DD HH:MM:SS`. |
| `referencia` | `TEXT` | NOT NULL, UNIQUE | Referencia interna de la factura. No debe repetirse entre las facturas autorizadas. |
| `valor` | `NUMERIC` | NOT NULL | Valor de la factura antes del IVA. |
| `iva` | `NUMERIC` | NOT NULL | IVA calculado conforme a las reglas del proyecto. |
| `total` | `NUMERIC` | NOT NULL | Total de la factura, correspondiente a valor más IVA. |
| `codigo_apr` | `TEXT` | NOT NULL, UNIQUE | Código de autorización de 16 dígitos. Se guarda como texto para preservar su representación exacta. |

### Llaves foráneas

- `id_contr_e` referencia `Contribuyente.id_contribuyente` como emisor.
- `id_contr_r` referencia `Contribuyente.id_contribuyente` como receptor.

Ambas relaciones apuntan a la misma tabla, pero representan funciones diferentes. Las llaves foráneas deben estar habilitadas en cada conexión de SQLite.

### Validaciones antes de insertar

La API debe validar la fecha, la referencia, los NIT, el IVA y el total antes de insertar una factura. Si la solicitud contiene errores, no se inserta en `Factura`; se registran los errores en `Error_Validacion`.

## 5. Tabla `Error_Validacion`

Almacena los errores detectados en solicitudes rechazadas. Esta tabla no tiene una llave foránea hacia `Factura`, porque una solicitud rechazada no genera una fila en esa tabla.

| Campo | Tipo | Restricciones | Descripción |
|---|---|---|---|
| `id_error` | `INTEGER` | PK, autoincremental | Identificador único del registro de error. |
| `referencia` | `TEXT` | NOT NULL | Referencia recibida para identificar la solicitud cuando esté disponible. |
| `fecha` | `TEXT` | NOT NULL | Fecha y hora de recepción de la solicitud, normalizada como `YYYY-MM-DD HH:MM:SS`. Se usa para reportes diarios, incluso si la fecha de emisión es inválida. |
| `tipo` | `INTEGER` | NOT NULL | Código numérico que identifica la categoría del error. |
| `descripcion` | `TEXT` | NOT NULL | Detalle del error, como el NIT inválido, la fecha recibida o el dato que no coincide. |

### Catálogo de errores

| Código | Significado |
|---:|---|
| `0` | NIT del emisor inválido. |
| `1` | NIT del receptor inválido. |
| `2` | Formato de fecha incorrecto. |
| `3` | IVA mal calculado. |
| `4` | Total mal calculado. |
| `5` | Referencia duplicada. |
| `6` | NIT del emisor inexistente en el RTU. |
| `7` | NIT del receptor inexistente en el RTU. |

Los códigos `6` y `7` distinguen un NIT con formato o dígito verificador inválido de un NIT válido que no está registrado en el RTU.

Una solicitud puede generar varios registros en `Error_Validacion`. Todos deben compartir el mismo `id_solicitud`, de modo que los reportes puedan contar solicitudes rechazadas sin confundir la cantidad de solicitudes con la cantidad de errores.

Si la referencia está ausente o no se puede extraer, el diseño actual necesita permitir representar esa situación; en ese caso se puede hacer `referencia` nullable o guardar una cadena vacía acompañada por `id_solicitud`.

## 6. Tabla `Correlativo`

Controla el último correlativo utilizado para cada día. El correlativo comienza en `1` cada día y no debe repetirse dentro de esa fecha.

| Campo | Tipo | Restricciones | Descripción |
|---|---|---|---|
| `id_correlativo` | `INTEGER` | PK, autoincremental | Identificador interno del registro de correlativo diario. |
| `fecha` | `TEXT` | NOT NULL, UNIQUE | Día al que corresponde el correlativo, en formato `YYYY-MM-DD`. Solo puede existir un registro por día. |
| `ultimo` | `INTEGER` | NOT NULL, DEFAULT 0 | Último número de correlativo asignado en ese día. Antes de la primera autorización, su valor puede ser `0`. |

### Generación del código de autorización

El código se forma concatenando:

- La fecha de autorización en formato `YYYYMMDD` (8 dígitos).
- El correlativo diario con ocho dígitos, completado con ceros a la izquierda.

Ejemplo: para el 14 de octubre de 2026 y el correlativo `1`, el código es `2026101400000001`.

La asignación del correlativo y la inserción de la factura autorizada deben ejecutarse de forma atómica para evitar códigos repetidos si llegan solicitudes simultáneas.

## 7. Resumen de relaciones

| Relación | Cardinalidad | Significado |
|---|---|---|
| `Contribuyente` → `Factura` mediante `id_contr_e` | 1 a muchos | Un contribuyente puede emitir muchas facturas. |
| `Contribuyente` → `Factura` mediante `id_contr_r` | 1 a muchos | Un contribuyente puede recibir muchas facturas. |
| `Error_Validacion` → `Factura` | Sin relación directa | Los errores corresponden a solicitudes rechazadas y no requieren una factura almacenada. |
| `Correlativo` → `Factura` | Sin relación directa | El correlativo se utiliza para generar el código de autorización; no es necesario almacenar una FK del correlativo en cada factura. |

## 8. Consideraciones para los reportes

- **Total de solicitudes recibidas:** contar los identificadores únicos `id_solicitud` del día. Para que este conteo incluya solicitudes sin errores, la implementación también debe conservar el registro de las solicitudes autorizadas o mantener un contador de todas las solicitudes recibidas.
- **Cantidad de errores por tipo:** contar los registros de `Error_Validacion` agrupados por `tipo`.
- **Cantidad de facturas autorizadas:** contar las filas de `Factura` por fecha.
- **Emisores y receptores distintos:** contar los identificadores o NIT distintos en las facturas autorizadas.
- **Consultas por rango:** utilizar fechas normalizadas en formato ISO 8601 y comparar desde la fecha inicial inclusive hasta el día posterior a la fecha final, exclusivo.

