# transaction-api
Example application showcasing API that handles transactions and creating invoices to report on the transactions

For demonstration purposes, the API uses JSON files saved to the local disk as the repository. The save location is editable in the app settings.

## Usage

Run the application in Visual Studio to start the API with a Swagger UI frontend. The Swagger UI will prepopulate the schema and allow for quick testing.

## Technical Notes

The app was developed using Visual Studio 2022 and .NET 6 with ASP.NET MVC.

The app implements the Repository pattern and IUnitOfWork pattern. This allows for a decoupling of the app from the data store it uses so if the app were to use a database or database with ORM for the repository, the switch would be much easier and would not impact the code in the controller or business logic layer. 

## Requirements Notes

The creating of an invoice only takes on transactions that are unbilled and adds them to the invoice. This means transactions on an invoice or paid transactions won't end up on multiple invoices or be duplicated on any invoice.

For creating a transaction, the id and invoice id are not required.

For creating an invoice, the invoice id and the transaction list are not required but prepopulated by Swagger.
