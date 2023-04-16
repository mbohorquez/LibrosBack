1. Importar el  proyecto ReservasCore6, esta hecho en NetCore6
2. Al abir se debe abrir la consola de administrador de paquetes 
3. Se debe realizar la migracion "El proyecto ya carga la data de prueba automaticamente de usuario y hotel" la data de reserva ser crea en el Post de service
   para esto se debe selecionar el proyecto predeterminado en la consola "ResrvasCore6" y dar el comando "EntityFrameworkCore\Add-Migration"
4. Cuando este se haya cargado se tiene que subir esto al sqlserver con el siguiente comando "EntityFrameworkCore\Update-Database"
5. Un usuario y contraseña para generar el token es email: 1email@dominio.com y pass: 123456
