-- ============================================================
-- PROYECTO 3 IPC2
-- Script de creación de la base de datos SQLite
--
-- Instrucciones:
-- 1. Crear una base de datos SQLite vacía llamada sat.db en DBeaver.
-- 2. Abrir un editor SQL conectado a esa base de datos.
-- 3. Ejecutar este script por partes.
--
-- Importante:
-- - Este script crea las tablas e índices si todavía no existen.
-- - No elimina datos existentes.
-- - Las facturas con errores NO se guardan en la tabla Factura.
--   Sus errores se registran en Error_Validacion.
-- ============================================================

-- Activa la validación de llaves foráneas para ESTA conexión.
-- La API también debe activar esta opción en cada conexión SQLite.
PRAGMA foreign_keys = ON;


-- ============================================================
-- TABLA: Contribuyente
-- Almacena los contribuyentes válidos registrados en el RTU.
-- ============================================================
CREATE TABLE IF NOT EXISTS Contribuyente (
    -- Identificador interno autoincremental del contribuyente.
    id_contribuyente INTEGER PRIMARY KEY AUTOINCREMENT,

    -- NIT almacenado como texto para conservar ceros iniciales
    -- y el dígito verificador. No puede repetirse.
    nit TEXT NOT NULL UNIQUE,

    -- Nombre o razón social del contribuyente.
    nombre TEXT NOT NULL
);


-- ============================================================
-- TABLA: Factura
-- Almacena ÚNICAMENTE las facturas que fueron autorizadas.
-- Las solicitudes rechazadas no se insertan aquí.
-- ============================================================
CREATE TABLE IF NOT EXISTS Factura (
    -- Identificador interno autoincremental de la factura.
    id_factura INTEGER PRIMARY KEY AUTOINCREMENT,

    -- ID del contribuyente emisor (llave foránea).
    id_contr_e INTEGER NOT NULL,

    -- NIT del emisor tal como vino en el JSON.
    nit_emisor TEXT NOT NULL,

    -- ID del contribuyente receptor (llave foránea).
    id_contr_r INTEGER NOT NULL,

    -- NIT del receptor tal como vino en el JSON.
    nit_receptor TEXT NOT NULL,

    -- Fecha/hora de emisión normalizada:
    -- YYYY-MM-DD HH:MM:SS
    fecha TEXT NOT NULL,

    -- Referencia única entre las facturas autorizadas.
    -- La API debe detectar duplicados antes de insertar.
    referencia TEXT NOT NULL UNIQUE,

    -- Importe antes de IVA.
    valor NUMERIC NOT NULL,

    -- IVA esperado: redondear(valor * 0.12, 2).
    iva NUMERIC NOT NULL,

    -- Total esperado: valor + IVA.
    total NUMERIC NOT NULL,

    -- Código de autorización de exactamente 16 dígitos.
    -- Se guarda como TEXT para preservar el formato completo.
    codigo_apr TEXT NOT NULL UNIQUE,

    -- El emisor debe existir en Contribuyente.
    FOREIGN KEY (id_contr_e)
        REFERENCES Contribuyente(id_contribuyente),

    -- El receptor debe existir en Contribuyente.
    FOREIGN KEY (id_contr_r)
        REFERENCES Contribuyente(id_contribuyente)
);


-- ============================================================
-- TABLA: Error_Validacion
-- Guarda los errores de solicitudes rechazadas.
-- No tiene FK hacia Factura porque una factura con errores
-- no se almacena en la tabla Factura.
-- ============================================================
CREATE TABLE IF NOT EXISTS Error_Validacion (
    -- Identificador interno del error.
    id_error INTEGER PRIMARY KEY AUTOINCREMENT,

    -- Referencia que llegó en la solicitud, si está disponible.
    -- No es UNIQUE: una referencia duplicada puede producir
    -- errores en varias solicitudes recibidas.
    referencia TEXT NOT NULL,

    -- Fecha/hora en que la API recibió la solicitud, normalizada
    -- como YYYY-MM-DD HH:MM:SS. Permite reportar por día aunque
    -- la fecha de emisión enviada tenga un formato incorrecto.
    fecha TEXT NOT NULL,

    -- Código numérico del error:
    -- 0 = NIT emisor inválido
    -- 1 = NIT receptor inválido
    -- 2 = formato de fecha incorrecto
    -- 3 = IVA mal calculado
    -- 4 = total mal calculado
    -- 5 = referencia duplicada
    -- 6 = NIT emisor inexistente en RTU
    -- 7 = NIT receptor inexistente en RTU
    tipo INTEGER NOT NULL,

    -- Detalle del error: NIT, fecha recibida u otro dato
    -- que permita comprender qué provocó el rechazo.
    descripcion TEXT NOT NULL
);


-- ============================================================
-- TABLA: Correlativo
-- Controla el último correlativo utilizado en cada día.
-- El correlativo comienza en 1 cada día.
-- ============================================================
CREATE TABLE IF NOT EXISTS Correlativo (
    -- Identificador interno del registro diario.
    id_correlativo INTEGER PRIMARY KEY AUTOINCREMENT,

    -- Día del correlativo en formato YYYY-MM-DD.
    -- UNIQUE garantiza un solo registro por día.
    fecha TEXT NOT NULL UNIQUE,

    -- Último número asignado durante ese día.
    -- 0 significa que todavía no se ha autorizado una factura.
    ultimo INTEGER NOT NULL DEFAULT 0 CHECK (ultimo >= 0)
);


-- ============================================================
-- ÍNDICES
-- Ayudan a acelerar las consultas frecuentes por fecha,
-- emisor, receptor y errores.
-- ============================================================

-- Buscar facturas por fecha o rango de fechas.
CREATE INDEX IF NOT EXISTS IX_Factura_Fecha
    ON Factura(fecha);

-- Consultar facturas emitidas por un contribuyente y por fecha.
CREATE INDEX IF NOT EXISTS IX_Factura_Emisor_Fecha
    ON Factura(id_contr_e, fecha);

-- Consultar facturas recibidas por un contribuyente y por fecha.
CREATE INDEX IF NOT EXISTS IX_Factura_Receptor_Fecha
    ON Factura(id_contr_r, fecha);

-- Consultar errores por referencia.
CREATE INDEX IF NOT EXISTS IX_Error_Referencia
    ON Error_Validacion(referencia);

-- Generar reportes de errores por fecha y tipo.
CREATE INDEX IF NOT EXISTS IX_Error_Fecha_Tipo
    ON Error_Validacion(fecha, tipo);


-- ============================================================
-- FIN DEL SCRIPT
--
-- Comprobación opcional:
-- SELECT name FROM sqlite_master
-- WHERE type = 'table' AND name NOT LIKE 'sqlite_%'
-- ORDER BY name;
-- ============================================================
