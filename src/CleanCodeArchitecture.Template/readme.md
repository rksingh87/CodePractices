 # Clean Architecture Solution Template

<br/>

This is a solution template for creating WebAPI & Worker Service Project

## Technologies

* ASP.NET Core 3.1


## Getting Started

The easiest way to get started is to install the [NuGet package] and run `dotnet new CleanCodeArchitecture`:

* Steps 1:
	* Clone this repository
	* Create local nuget package using command ` nuget pack `
	* Install template using command ` dotnet new --install CleanCodeArchitecture.1.0.0.nupkg `
	* Create a new solution using command ` dotnet new CleanCodeArchitecture -o <output_folder_and_namespace_name> `


## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### Presentation

This layer contains all Hostable components for example: WebAPI/WorkerService. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.