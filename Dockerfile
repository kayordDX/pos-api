FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5117
ENV ASPNETCORE_URLS=http://+:5117
RUN apt update && apt install -y curl && rm -rf /var/lib/apt/lists/*

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Kayord.Pos/Kayord.Pos.csproj", "Directory.Build.props", "Directory.Packages.props", "src/Kayord.Pos/"]
RUN dotnet restore "src/Kayord.Pos/Kayord.Pos.csproj"
COPY . .
WORKDIR "/src/src/Kayord.Pos"
RUN dotnet build "Kayord.Pos.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Kayord.Pos.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kayord.Pos.dll"]
