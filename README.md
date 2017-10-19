# CustomerGET
A project of three parts to demonstrate communication inbetween a 3rd party API, a backend Service API and a frontend MVC website. Written in C# and using Azure as the host.

## Roadmap
1. Release to Production (completed)
2. Scale Implementation (50%) - Service accepts pagination, website neds to send the parameter.
3. Implement IOC in Service - Concreate Objects created in the Azure functions.

## Getting Started
The website is running on Azure which can be reached at the following link: customerget.azurewebsites.net
The backend is running as an Azure function which can be reached at the following link: customergetservice20171015.azurewebsites.net/api/GetAll or Customer?id=<GUID>
The code can be run by hitting F5 in Visual Studio 2017. Nuget will fetch the required libraries for you.

### Prerequisites
Visual Studio 2017 with Cloud Development libraries.
Nuget

### Installing
1. Open the CustomerGet.sln file in Visual Studio
2. Set the CustomerGet.Website to be the Startup project, this is done by right clicking the CustomerGet.Website project in the Solution Explorer window.
3. Run the project with F5. 
4. If Nuget does not fetch the required libraries, in the website project right click Dependencies and select Manage Nuget Packages. In the Nuget window you can choose to download the required packages.
5. Steps 2-4 can be repeated for running the CustomerGet.Service code.

## Running the tests
In Visual Studio, in the Test menu -> Windows -> Test Explorer, a window will appear which detects any unit tests in the Solution.  Clicking Run All will execute the tests or individual tests can be clicked on and run.

### Service Tests
Two Live intergration tests are commented out in the CustomerServiceTests class, their TestMethod tags can be uncommented to execute a test run against the Live api.
Tests can be found in CustomerGet.Server.Tests.Services

### Website Tests
In the root of the website project there is a Bootstrapper class, you can replace `LiveCustomerDataFactory` as the DataFactory with `TestCustomerDataFactory` to read responses from a JSon file rather than the API.
Tests can be found in CustomerGet.Website.Tests

## Deployment
Code can be deployed to an Azure account by right clicking the Service or Website project, choosing publish.  You will need to be logged into Azure and have a resource group setup.

## Authors
* **Simon Lawson** - *Supreme Lead Architect*

## Acknowledgments
* StackOverflow as par
