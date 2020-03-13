FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/OkApp.Web.Host/OkApp.Web.Host.csproj", "src/OkApp.Web.Host/"]
COPY ["src/OkApp.Web.Core/OkApp.Web.Core.csproj", "src/OkApp.Web.Core/"]
COPY ["src/OkApp.Application/OkApp.Application.csproj", "src/OkApp.Application/"]
COPY ["src/OkApp.Core/OkApp.Core.csproj", "src/OkApp.Core/"]
COPY ["src/OkApp.EntityFrameworkCore/OkApp.EntityFrameworkCore.csproj", "src/OkApp.EntityFrameworkCore/"]
RUN dotnet restore "src/OkApp.Web.Host/OkApp.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/OkApp.Web.Host"
RUN dotnet build "OkApp.Web.Host.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OkApp.Web.Host.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OkApp.Web.Host.dll"]