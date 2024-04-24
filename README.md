# Kayord.Pos

Backend for kayord pos

```bash
dotnet ef migrations add Menu and MenuSections --project src/Kayord.Pos --startup-project src/Kayord.Pos --output-dir Data/Migrations

dotnet ef database update --project src/Kayord.Pos --startup-project src/Kayord.Pos

dotnet run --project src/Kayord.Pos
```

## Postgres

```bash
docker compose up -d
```

## Tools

```bash
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

dotnet tool install --global dotnet-outdated-tool
dotnet tool update --global dotnet-outdated-tool
```

## Secrets

```bash
dotnet user-secrets init --project src/Kayord.Pos
dotnet user-secrets set "Halo:MerchantId" "secret" --project src/Kayord.Pos
dotnet user-secrets set "Halo:XApiKey" "secret" --project src/Kayord.Pos
dotnet user-secrets set "Email:Host" "secret" --project src/Kayord.Pos
dotnet user-secrets set "Email:Email" "secret" --project src/Kayord.Pos
dotnet user-secrets set "Email:Password" "secret" --project src/Kayord.Pos
dotnet user-secrets list --project src/Kayord.Pos
```

## Google Admin SDK

Production
export GOOGLE_APPLICATION_CREDENTIALS=/service-account.json

Dev
create file in src/Kayord.Pos/private_key.json
