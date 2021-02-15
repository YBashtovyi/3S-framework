# Single Software Solution (3S)

Single Software Solution - demo 

# How to install
 
After cloning the repository :

1. Install postgres from https://www.postgresql.org. 
2. Install redis
   2.1 On linux: sudo apt-get install redis-server
   2.2 On Windows: see this article https://redislabs.com/blog/redis-on-windows-10/
3. In repository folder open src\App.Api\appsettings.Development.json and edit connection strings:
   3.1 DefaultConnection - change user. User should have rights to create database. Change ip and port of your postgres instance if needed
   3.2 Redis - change redis ip and port if needed. Or leave it by default if you haven't changed default redis settings
4. Open Visual Studio and press F5 (or use cli commands 'dotnet build' and 'dotnet run')
5. Run in browser localhost:5050 (5050 - Url value in appsettings)