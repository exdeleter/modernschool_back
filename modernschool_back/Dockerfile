FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["modernschool_back/modernschool_back.csproj", "modernschool_back/"]
RUN dotnet restore "modernschool_back/modernschool_back.csproj"
COPY . .
WORKDIR "/src/modernschool_back"
RUN dotnet build "modernschool_back.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "modernschool_back.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "modernschool_back.dll"]
