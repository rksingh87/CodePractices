#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Presentation/CleanCodeArchitecture.Presentation.WorkerService/CleanCodeArchitecture.Presentation.WorkerService.csproj", "src/Presentation/CleanCodeArchitecture.Presentation.WorkerService/"]
RUN dotnet restore "src/Presentation/CleanCodeArchitecture.Presentation.WorkerService/CleanCodeArchitecture.Presentation.WorkerService.csproj"
COPY . .
WORKDIR "/src/src/Presentation/CleanCodeArchitecture.Presentation.WorkerService"
RUN dotnet build "CleanCodeArchitecture.Presentation.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleanCodeArchitecture.Presentation.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanCodeArchitecture.Presentation.WorkerService.dll"]