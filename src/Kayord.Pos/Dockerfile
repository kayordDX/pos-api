FROM mcr.microsoft.com/dotnet/aspnet:7.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 5117

ENV ASPNETCORE_URLS=http://+:5117

FROM mcr.microsoft.com/dotnet/sdk:7.0-nanoserver-1809 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Kayord.POS/Kayord.Pos.csproj", "src/Kayord.POS/"]
RUN dotnet restore "src\Kayord.POS\Kayord.Pos.csproj"
COPY . .
WORKDIR "/src/src/Kayord.POS"
RUN dotnet build "Kayord.Pos.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Kayord.Pos.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kayord.Pos.dll"]
