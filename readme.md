# Bloggin Platform

## Aclaraciones 馃殌

* Se utliz贸 inyecci贸n de dependencia para generar la arquitectura.
* Se crearon 2 proyectos diferentes, uno para la aplicaci贸n en s铆, y otro para los tests unitarios.
* Se aprovech贸 la ventaja de la inyecci贸n de dependencia para hacer el c贸digo reutilizable y extensible por ejemplo al poder utilizar tanto base de datos en memoria, como sql, solo modificando una linea en el startup.
* Se cre贸 una clase llamada "Unit of Work" para en caso futuro, tener que hacer una transacci贸n en la base de datos utilizando distintos repositorios, poder extraer el m茅todo que permite inyectar todas las queries al mismo tiempo.
* Se utilizaron los middlewares que ya trae por defecto .Net para la autenticaci贸n y autentificaci贸n del usuario.

