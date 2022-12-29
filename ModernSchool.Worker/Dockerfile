FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ModernSchool.Worker/ModernSchool.Worker.csproj", "ModernSchool.Worker/"]
RUN dotnet restore "ModernSchool.Worker/ModernSchool.Worker.csproj"
COPY . .
WORKDIR "/src/ModernSchool.Worker"
RUN dotnet build "ModernSchool.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ModernSchool.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModernSchool.Worker.dll"]
