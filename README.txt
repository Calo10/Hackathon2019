### Nombre
Gestor de Programas Sociales (GEPROS - CR)

### Descripción y contexto
---
La Tesorería Nacional de Costa Rica, tiene la necesidad de implementar un sistema que permita operar los pagos para los programas sociales de transferencias monetarias. Este objetivo lo comparte con todas las agencias gubernamentales que administran dichos programas.

El GEPROS es una herramienta que debe permitir conectar a todas las agencias de gobierno relacionadas al proceso de definición de beneficiarios, aprobación de transferencias, aprobación de presupuesto, y ejecución de mecanismos de pago de programas sociales, simplificando la administración de estos beneficios. Se conecta con Entidades Dueñas de programas sociales, el sistema SINIRUBE, el Sistema de Tesorería Digital de la Tesorería Nacional, y el Sistema Nacional de Pago Electrónico (SINPE) a través de la Tesorería Digital.

El GEPROS es un sistema en desarrollo que da servicio a 4 tipos de usuario: i) Configuración de Reglas de Beneficiarios, ii) Visualizador del Flujo de Transferencias, iii) Administrador de la Plataforma, iv) Usuario con Acceso a Consulta. Actualmente los usuarios coexisten en la misma pantalla, con el objeto único de mostrar el proceso completo de las solicitudes de transferencias.

### Guía de usuario
---
La pantalla inicial de la plataforma contiene distintas pestañas donde se puede ir revisando cada uno de los pasos del proceso de aprobación de las solicitudes individuales de transferencias desde que la solicitud es creada por la entidad dueña del programa hasta que la transferencia es solicitada al SINPE.

#### Configuración de Reglas de Beneficiarios
La Tesorería Nacional tiene acceso a parametrizar las reglas de acuerdo a las cuales la población es elegibles para ser beneficiaria de los programas de transferencias sociales que son operados a través del sistema.

#### Ingreso de Solicitudes de Transferencias
La entidad dueña del programa social debe comunicarse con la plataforma via un API que se consume desde el sistema que genera el listado de los beneficiarios y los montos a transferir por cada programa. Este es el primer paso para el uso del sistema; sin embargo a nivel de ejemplo, la plataforma hoy trabaja con datos consumidos en formato RAW.

#### Seguimiento a los Pasos de Aprobación
Los distintos pasos de aprobación realizados por el sistema pueden ser revisados en cada una de las pestañas del sistema. El sistema tendrá un indicador de las transacciones exitosas y las transacciones rechazadas, pudiendo revisar el motivo del rechazo.
 

### Autor/es
---
Carlos
Bryan
Rafael
Alexsander

### Información adicional
---
Este proyecto fue desarrollado para el Hackathon Gestión Financiera Publica y Tecnología - Costa Rica.