# transaction-api
Example application showcasing API that handles transactions and creating invoices to report on the transactions

For demonstration purposes, the API uses JSON files saved to the local disk as the repository. The save location is editable in the app settings.

## Usage

Run the application in Visual Studio to start the API with a Swagger UI frontend. The Swagger UI will prepopulate the schema and allow for quick testing.

## Technical Notes

The app was developed using Visual Studio 2022 and .NET 6 with ASP.NET MVC.

The app implements the Repository pattern and IUnitOfWork pattern. This allows for a decoupling of the app from the data store it uses so if the app were to use a database or database with ORM for the repository, the switch would be much easier and would not impact the code in the controller or business logic layer. 