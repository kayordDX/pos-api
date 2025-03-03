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
dotnet tool restore
```

## Secrets

```bash
dotnet user-secrets init --project src/Kayord.Pos
dotnet user-secrets set "Halo:MerchantId" "secret" --project src/Kayord.Pos
dotnet user-secrets set "Halo:XApiKey" "secret" --project src/Kayord.Pos
dotnet user-secrets set "Email:Host" "secret" --project src/Kayord.Pos
dotnet user-secrets set "Email:Email" "secret" --project src/Kayord.Pos
dotnet user-secrets set "Email:Password" "secret" --project src/Kayord.Pos
dotnet user-secrets set "Whatsapp:Host" "secret" --project src/Kayord.Pos
dotnet user-secrets set "Whatsapp:XApiKey" "secret" --project src/Kayord.Pos
dotnet user-secrets set "App:GeminiKey" "secret" --project src/Kayord.Pos 
dotnet user-secrets list --project src/Kayord.Pos
```

## Google Admin SDK

Production
export GOOGLE_APPLICATION_CREDENTIALS=/service-account.json

Dev
create file in src/Kayord.Pos/private_key.json

## Whatsapp Setup

We can send messages using WhatsApp.

```bash
# Get Status
curl --request GET \
  --url https://whatsapp.kayord.com/session/status/kayord \
  --header 'x-api-key: apiKey'

# Start
curl --request GET \
  --url https://whatsapp.kayord.com/session/start/kayord \
  --header 'x-api-key: apiKey'

# Generate QR Image to scan
curl --request GET \
  --url https://whatsapp.kayord.com/session/qr/kayord/image \
  --header 'x-api-key: apiKey'  
```

## TODO:

- [x] MenuItemStock
  - [x] Delete all items
  - [x] Should have StockItemId
  - [x] Quantity should become Amount
- [x] StockLocation
  - [x] Remove table completely
  - [x] Fix all references and replace with division
- [x] StockOrderItem
  - [x] Should have StockId instead of StockItemId
  - [ ] StockOrderItemStatus
  - [ ] StockerOrderItem Add Ordered amount and keep actual. Also Add status.
- [ ] StockItemAudit Table
  - [ ] Table should show who changed it
  - [ ] Date
  - [ ] Old Value and New Value
- [ ] StockAllocate
- [ ] StockAllocateItem
- [ ] StockAllocateStatus
  
## On Deployment
  
- [ ] Division Now has outlet
- [ ] RoleDivision Division was nullable

```sql
TRUNCATE TABLE "MenuItemStock"
DELETE FROM "Supplier"
DELETE FROM "SupplierPlatform"
DELETE FROM "Stock"

UPDATE "MenuItem" SET "DivisionId" = 1 WHERE "DivisionId" = 5
DELETE FROM "Division" WHERE "DivisionId" = 5


INSERT INTO "Division" ("DivisionName", "DivisionTypeId","OutletId")
SELECT 'Kitchen' "DivisionName", 1 "DivisionTypeId", "Id" "OutletId" FROM "Outlet"

INSERT INTO "Division" ("DivisionName", "DivisionTypeId","OutletId")
SELECT 'Bar' "DivisionName", 1 "DivisionTypeId", "Id" "OutletId" FROM "Outlet"


UPDATE "MenuItem" b
	SET "DivisionId" = a."DivisionId"
FROM (
  SELECT 
  	mi."MenuItemId",
  	mi."DivisionId" "OldDivisionId",
  	d."DivisionId"
  FROM "MenuItem" mi
  JOIN "MenuSection" ms
  ON mi."MenuSectionId" = ms."MenuSectionId"
  JOIN "Menu" m
  ON m."Id" = ms."MenuId"
  JOIN "Division" d
  ON "d"."OutletId" = m."OutletId"
) a
WHERE a."MenuItemId" = b."MenuItemId"
AND b."DivisionId" = a."OldDivisionId"


DELETE FROM "Division" WHERE "DivisionId" = 1
DELETE FROM "Division" WHERE "DivisionId" = 2

```
