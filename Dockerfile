FROM --platform=${BUILDPLATFORM} mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM --platform=${BUILDPLATFORM} mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
WORKDIR /src

COPY ["src/SD.Mini.ZooManagement.Domain/SD.Mini.ZooManagement.Domain.csproj", "SD.Mini.ZooManagement.Domain/"]
COPY ["src/SD.Mini.ZooManagement.Application/SD.Mini.ZooManagement.Application.csproj", "SD.Mini.ZooManagement.Application/"]
COPY ["src/SD.Mini.ZooManagement.Infrastructure/SD.Mini.ZooManagement.Infrastructure.csproj", "SD.Mini.ZooManagement.Infrastructure/"]
COPY ["src/SD.Mini.ZooManagement.Api/SD.Mini.ZooManagement.Api.csproj", "SD.Mini.ZooManagement.Api/"]

RUN dotnet restore "SD.Mini.ZooManagement.Api/SD.Mini.ZooManagement.Api.csproj" --arch ${TARGETARCH}
COPY src/. .
RUN dotnet build "SD.Mini.ZooManagement.Api/SD.Mini.ZooManagement.Api.csproj" -c Release -o /app/build

WORKDIR "/src/SD.Mini.ZooManagement.Api"
FROM build AS publish
RUN dotnet publish "SD.Mini.ZooManagement.Api.csproj" --arch ${TARGETARCH}  -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SD.Mini.ZooManagement.Api.dll"]