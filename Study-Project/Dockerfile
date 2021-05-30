#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Study-Project/Study-Project.csproj", "Study-Project/"]
RUN dotnet restore "Study-Project/Study-Project.csproj"
COPY . .
WORKDIR "/src/Study-Project"
RUN dotnet build "Study-Project.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Study-Project.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Study-Project.dll"]