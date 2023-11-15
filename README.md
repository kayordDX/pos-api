# Kayord.Pos

Backend for kayord pos

```bash
dotnet ef migrations add Initial --project src/Kayord.Pos --startup-project src/Kayord.Pos --output-dir Data/Migrations

dotnet ef database update --project src/Kayord.Pos --startup-project src/Kayord.Pos

dotnet run --project src/Kayord.Pos
```

## Postgres

```bash
docker compose up -d
```
