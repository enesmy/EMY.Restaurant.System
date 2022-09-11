#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Presentation/EMY.Restaurant.Presentation.Web/EMY.Restaurant.Presentation.Web.csproj", "Presentation/EMY.Restaurant.Presentation.Web/"]
RUN dotnet restore "Presentation/EMY.Restaurant.Presentation.Web/EMY.Restaurant.Presentation.Web.csproj"
COPY . .
WORKDIR "/src/Presentation/EMY.Restaurant.Presentation.Web"
RUN dotnet build "EMY.Restaurant.Presentation.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EMY.Restaurant.Presentation.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EMY.Restaurant.Presentation.Web.dll"]