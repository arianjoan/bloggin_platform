# Bloggin Platform

## Aclaraciones 🚀

* Se utlizó inyección de dependencia para generar la arquitectura.
* Se crearon 2 proyectos diferentes, uno para la aplicación en sí, y otro para los tests unitarios.
* Se aprovechó la ventaja de la inyección de dependencia para hacer el código reutilizable y extensible por ejemplo al poder utilizar tanto base de datos en memoria, como sql, solo modificando una linea en el startup.
* Se creó una clase llamada "Unit of Work" para en caso futuro, tener que hacer una transacción en la base de datos utilizando distintos repositorios, poder extraer el método que permite inyectar todas las queries al mismo tiempo.
* Se utilizaron los middlewares que ya trae por defecto .Net para la autenticación y autentificación del usuario.

