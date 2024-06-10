#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BloodGL.MVC/BloodGL.MVC.csproj", "BloodGL.MVC/"]
COPY ["BloodGL.Application/BloodGL.Application.csproj", "BloodGL.Application/"]
COPY ["BloodGL.Domain/BloodGL.Domain.csproj", "BloodGL.Domain/"]
COPY ["BloodGL.Core/BloodGL.Core.csproj", "BloodGL.Core/"]
COPY ["BloodGL.Infrastructure/BloodGL.Infrastructure.csproj", "BloodGL.Infrastructure/"]
RUN dotnet restore "BloodGL.MVC/BloodGL.MVC.csproj"
COPY . .
WORKDIR "/src/BloodGL.MVC"
RUN dotnet build "BloodGL.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BloodGL.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BloodGL.MVC.dll"]