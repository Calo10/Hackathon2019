### Nombre
Gestor de Programas Sociales (GEPROS - CR)

### Descripci�n y contexto
---
La Tesorer�a Nacional de Costa Rica, tiene la necesidad de implementar un sistema que permita operar los pagos para los programas sociales de transferencias monetarias. Este objetivo lo comparte con todas las agencias gubernamentales que administran dichos programas.

El GEPROS es una herramienta que debe permitir conectar a todas las agencias de gobierno relacionadas al proceso de definici�n de beneficiarios, aprobaci�n de transferencias, aprobaci�n de presupuesto, y ejecuci�n de mecanismos de pago de programas sociales, simplificando la administraci�n de estos beneficios. Se conecta con Entidades Due�as de programas sociales, el sistema SINIRUBE, el Sistema de Tesorer�a Digital de la Tesorer�a Nacional, y el Sistema Nacional de Pago Electr�nico (SINPE) a trav�s de la Tesorer�a Digital.

El GEPROS es un sistema en desarrollo que da servicio a 4 tipos de usuario: i) Configuraci�n de Reglas de Beneficiarios, ii) Visualizador del Flujo de Transferencias, iii) Administrador de la Plataforma, iv) Usuario con Acceso a Consulta. Actualmente los usuarios coexisten en la misma pantalla, con el objeto �nico de mostrar el proceso completo de las solicitudes de transferencias.

### Gu�a de usuario
---
La pantalla inicial de la plataforma contiene distintas pesta�as donde se puede ir revisando cada uno de los pasos del proceso de aprobaci�n de las solicitudes individuales de transferencias desde que la solicitud es creada por la entidad due�a del programa hasta que la transferencia es solicitada al SINPE.

#### Configuraci�n de Reglas de Beneficiarios
La Tesorer�a Nacional tiene acceso a parametrizar las reglas de acuerdo a las cuales la poblaci�n es elegibles para ser beneficiaria de los programas de transferencias sociales que son operados a trav�s del sistema.

#### Ingreso de Solicitudes de Transferencias
La entidad due�a del programa social debe comunicarse con la plataforma via un API que se consume desde el sistema que genera el listado de los beneficiarios y los montos a transferir por cada programa. Este es el primer paso para el uso del sistema; sin embargo a nivel de ejemplo, la plataforma hoy trabaja con datos consumidos en formato RAW.

#### Seguimiento a los Pasos de Aprobaci�n
Los distintos pasos de aprobaci�n realizados por el sistema pueden ser revisados en cada una de las pesta�as del sistema. El sistema tendr� un indicador de las transacciones exitosas y las transacciones rechazadas, pudiendo revisar el motivo del rechazo.
 

### Autor/es
---
Carlos
Bryan
Rafael
Alexsander

### Informaci�n adicional
---
Este proyecto fue desarrollado para el Hackathon Gesti�n Financiera Publica y Tecnolog�a - Costa Rica.