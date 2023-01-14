# demo-vuejs-syncfusion-grid-ef

The goal of this project is to show a sample of how the [Vue SyncFusion data grid control](https://ej2.syncfusion.com/vue/documentation/grid/getting-started/) can work with a remote data source using EF Core.  The example grid is located in:

* src/VueSyncFusionWebApi/ClientApp/src/components/HelloWorld.vue

## Technologies

* Mssql database
* AspNet Core 7 Web API backend
* Vue3
* EF Core 7
* SyncFusion Grid

## Projects

* VueSyncFusionWebApi - This is the AspNet Core Web API that services web requests
	* VueSyncFusionWebApi/ClientApp - Vuejs frontend client
* Northwind.Database - This is the library with nhibernate database entities and mappings

## Getting Started

1. Create the Northwind db by running the instnwnd.sql file in ssms or your favorite sql IDE.
2. Edit the connection string in VueSyncFusionWebApi/appsettings.json to point to Northwind db created
3. Debug the demo-vuejs-syncfusion-grid-ef Web Project in Visual Studio or Rider