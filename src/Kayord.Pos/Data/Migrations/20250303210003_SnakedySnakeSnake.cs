using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class SnakedySnakeSnake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adjustment_AdjustmentType_AdjustmentTypeId",
                table: "Adjustment");

            migrationBuilder.DropForeignKey(
                name: "FK_Adjustment_TableBooking_TableBookingId",
                table: "Adjustment");

            migrationBuilder.DropForeignKey(
                name: "FK_AdjustmentTypeOutlet_AdjustmentType_AdjustmentTypeId",
                table: "AdjustmentTypeOutlet");

            migrationBuilder.DropForeignKey(
                name: "FK_AdjustmentTypeOutlet_Outlet_OutletId",
                table: "AdjustmentTypeOutlet");

            migrationBuilder.DropForeignKey(
                name: "FK_CashUp_SalesPeriod_SalesPeriodId",
                table: "CashUp");

            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItem_CashUpUserItemType_CashUpUserItemTypeId",
                table: "CashUpUserItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItem_CashUpUser_CashUpUserId",
                table: "CashUpUserItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItemType_AdjustmentType_AdjustmentTypeId",
                table: "CashUpUserItemType");

            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItemType_CashUpConfig_CashupConfigId",
                table: "CashUpUserItemType");

            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItemType_PaymentType_PaymentTypeId",
                table: "CashUpUserItemType");

            migrationBuilder.DropForeignKey(
                name: "FK_Clock_Outlet_OutletId",
                table: "Clock");

            migrationBuilder.DropForeignKey(
                name: "FK_Clock_User_UserId",
                table: "Clock");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Table_TableId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Extra_ExtraGroup_ExtraGroupId",
                table: "Extra");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraStock_Extra_ExtraId",
                table: "ExtraStock");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraStock_Stock_StockId",
                table: "ExtraStock");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraStock_Unit_UnitId",
                table: "ExtraStock");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Outlet_OutletId",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Division_DivisionId",
                table: "MenuItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_MenuSection_MenuSectionId",
                table: "MenuItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemExtraGroup_ExtraGroup_ExtraGroupId",
                table: "MenuItemExtraGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemExtraGroup_MenuItem_MenuItemId",
                table: "MenuItemExtraGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOptionGroup_MenuItem_MenuItemId",
                table: "MenuItemOptionGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOptionGroup_OptionGroup_OptionGroupId",
                table: "MenuItemOptionGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemStock_MenuItem_MenuItemId",
                table: "MenuItemStock");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemStock_StockItem_StockItemId",
                table: "MenuItemStock");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuSection_MenuSection_ParentId",
                table: "MenuSection");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuSection_Menu_MenuId",
                table: "MenuSection");

            migrationBuilder.DropForeignKey(
                name: "FK_Option_OptionGroup_OptionGroupId",
                table: "Option");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionStock_Option_OptionId",
                table: "OptionStock");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionStock_Stock_StockId",
                table: "OptionStock");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionStock_Unit_UnitId",
                table: "OptionStock");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_MenuItem_MenuItemId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_OrderGroup_OrderGroupId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_OrderItemStatus_OrderItemStatusId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_TableBooking_TableBookingId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemExtra_Extra_ExtraId",
                table: "OrderItemExtra");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemExtra_OrderItem_OrderItemId",
                table: "OrderItemExtra");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemOption_Option_OptionId",
                table: "OrderItemOption");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemOption_OrderItem_OrderItemId",
                table: "OrderItemOption");

            migrationBuilder.DropForeignKey(
                name: "FK_Outlet_Business_BusinessId",
                table: "Outlet");

            migrationBuilder.DropForeignKey(
                name: "FK_OutletExtraGroup_ExtraGroup_ExtraGroupId",
                table: "OutletExtraGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_OutletExtraGroup_Outlet_OutletId",
                table: "OutletExtraGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_OutletPaymentType_Outlet_OutletId",
                table: "OutletPaymentType");

            migrationBuilder.DropForeignKey(
                name: "FK_OutletPaymentType_PaymentType_PaymentTypeId",
                table: "OutletPaymentType");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_PaymentType_PaymentTypeId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_TableBooking_TableBookingId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleDivision_Division_DivisionId",
                table: "RoleDivision");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesPeriod_Outlet_OutletId",
                table: "SalesPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Outlet_OutletId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_StockCategory_StockCategoryId",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Unit_UnitId",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_StockAllocate_Division_FromDivisionId",
                table: "StockAllocate");

            migrationBuilder.DropForeignKey(
                name: "FK_StockAllocate_Division_ToDivisionId",
                table: "StockAllocate");

            migrationBuilder.DropForeignKey(
                name: "FK_StockAllocate_StockAllocateStatus_StockAllocateStatusId",
                table: "StockAllocate");

            migrationBuilder.DropForeignKey(
                name: "FK_StockAllocate_User_AssignedUserUserId",
                table: "StockAllocate");

            migrationBuilder.DropForeignKey(
                name: "FK_StockAllocate_User_FromUserUserId",
                table: "StockAllocate");

            migrationBuilder.DropForeignKey(
                name: "FK_StockAllocateItem_Division_DivisionId",
                table: "StockAllocateItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockAllocateItem_StockAllocateItemStatus_StockAllocateItem~",
                table: "StockAllocateItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockAllocateItem_StockAllocate_StockAllocateId",
                table: "StockAllocateItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockAllocateItem_Stock_StockId",
                table: "StockAllocateItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItem_Division_DivisionId",
                table: "StockItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItem_Stock_StockId",
                table: "StockItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItemAudit_StockItemAuditType_StockItemAuditTypeId",
                table: "StockItemAudit");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrder_Division_DivisionId",
                table: "StockOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrder_StockOrderStatus_StockOrderStatusId",
                table: "StockOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrder_Supplier_SupplierId",
                table: "StockOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrderItem_StockOrderItemStatus_StockOrderItemStatusId",
                table: "StockOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrderItem_StockOrder_StockOrderId",
                table: "StockOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrderItem_Stock_StockId",
                table: "StockOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_Section_SectionId",
                table: "Table");

            migrationBuilder.DropForeignKey(
                name: "FK_TableBooking_CashUpUser_CashUpUserId",
                table: "TableBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_TableBooking_SalesPeriod_SalesPeriodId",
                table: "TableBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_TableBooking_Table_TableId",
                table: "TableBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_TableBooking_User_UserId",
                table: "TableBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_MenuItem_MenuItemId",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleOutlet_Outlet_OutletId",
                table: "UserRoleOutlet");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleOutlet_Role_RoleId",
                table: "UserRoleOutlet");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleOutlet_User_UserId",
                table: "UserRoleOutlet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Table",
                table: "Table");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Section",
                table: "Section");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Printer",
                table: "Printer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Outlet",
                table: "Outlet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Option",
                table: "Option");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu",
                table: "Menu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Extra",
                table: "Extra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Division",
                table: "Division");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clock",
                table: "Clock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Business",
                table: "Business");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adjustment",
                table: "Adjustment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleOutlet",
                table: "UserRoleOutlet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOutlet",
                table: "UserOutlet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableBooking",
                table: "TableBooking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockOrderStatus",
                table: "StockOrderStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockOrderItemStatus",
                table: "StockOrderItemStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockOrderItem",
                table: "StockOrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockOrder",
                table: "StockOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockItemAuditType",
                table: "StockItemAuditType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockItemAudit",
                table: "StockItemAudit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockItem",
                table: "StockItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockCategory",
                table: "StockCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockAllocateStatus",
                table: "StockAllocateStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockAllocateItemStatus",
                table: "StockAllocateItemStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockAllocateItem",
                table: "StockAllocateItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockAllocate",
                table: "StockAllocate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesPeriod",
                table: "SalesPeriod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleDivision",
                table: "RoleDivision");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentType",
                table: "PaymentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OutletPaymentType",
                table: "OutletPaymentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OutletExtraGroup",
                table: "OutletExtraGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItemStatusAudit",
                table: "OrderItemStatusAudit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItemStatus",
                table: "OrderItemStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItemOption",
                table: "OrderItemOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItemExtra",
                table: "OrderItemExtra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderGroup",
                table: "OrderGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OptionStock",
                table: "OptionStock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OptionGroup",
                table: "OptionGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationUser",
                table: "NotificationUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationLog",
                table: "NotificationLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuSection",
                table: "MenuSection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemStock",
                table: "MenuItemStock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemOptionGroup",
                table: "MenuItemOptionGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemExtraGroup",
                table: "MenuItemExtraGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItem",
                table: "MenuItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HaloReference",
                table: "HaloReference");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HaloLog",
                table: "HaloLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HaloConfig",
                table: "HaloConfig");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtraStock",
                table: "ExtraStock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtraGroup",
                table: "ExtraGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailLog",
                table: "EmailLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DivisionType",
                table: "DivisionType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashUpUserItemType",
                table: "CashUpUserItemType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashUpUserItem",
                table: "CashUpUserItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashUpUser",
                table: "CashUpUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashUpConfig",
                table: "CashUpConfig");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashUp",
                table: "CashUp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdjustmentTypeOutlet",
                table: "AdjustmentTypeOutlet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdjustmentType",
                table: "AdjustmentType");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "unit");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "tag");

            migrationBuilder.RenameTable(
                name: "Table",
                newName: "table");

            migrationBuilder.RenameTable(
                name: "Supplier",
                newName: "supplier");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "stock");

            migrationBuilder.RenameTable(
                name: "Section",
                newName: "section");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "role");

            migrationBuilder.RenameTable(
                name: "Printer",
                newName: "printer");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "payment");

            migrationBuilder.RenameTable(
                name: "Outlet",
                newName: "outlet");

            migrationBuilder.RenameTable(
                name: "Option",
                newName: "option");

            migrationBuilder.RenameTable(
                name: "Menu",
                newName: "menu");

            migrationBuilder.RenameTable(
                name: "Extra",
                newName: "extra");

            migrationBuilder.RenameTable(
                name: "Division",
                newName: "division");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "customer");

            migrationBuilder.RenameTable(
                name: "Clock",
                newName: "clock");

            migrationBuilder.RenameTable(
                name: "Business",
                newName: "business");

            migrationBuilder.RenameTable(
                name: "Adjustment",
                newName: "adjustment");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "address");

            migrationBuilder.RenameTable(
                name: "UserRoleOutlet",
                newName: "user_role_outlet");

            migrationBuilder.RenameTable(
                name: "UserOutlet",
                newName: "user_outlet");

            migrationBuilder.RenameTable(
                name: "TableBooking",
                newName: "table_booking");

            migrationBuilder.RenameTable(
                name: "StockOrderStatus",
                newName: "stock_order_status");

            migrationBuilder.RenameTable(
                name: "StockOrderItemStatus",
                newName: "stock_order_item_status");

            migrationBuilder.RenameTable(
                name: "StockOrderItem",
                newName: "stock_order_item");

            migrationBuilder.RenameTable(
                name: "StockOrder",
                newName: "stock_order");

            migrationBuilder.RenameTable(
                name: "StockItemAuditType",
                newName: "stock_item_audit_type");

            migrationBuilder.RenameTable(
                name: "StockItemAudit",
                newName: "stock_item_audit");

            migrationBuilder.RenameTable(
                name: "StockItem",
                newName: "stock_item");

            migrationBuilder.RenameTable(
                name: "StockCategory",
                newName: "stock_category");

            migrationBuilder.RenameTable(
                name: "StockAllocateStatus",
                newName: "stock_allocate_status");

            migrationBuilder.RenameTable(
                name: "StockAllocateItemStatus",
                newName: "stock_allocate_item_status");

            migrationBuilder.RenameTable(
                name: "StockAllocateItem",
                newName: "stock_allocate_item");

            migrationBuilder.RenameTable(
                name: "StockAllocate",
                newName: "stock_allocate");

            migrationBuilder.RenameTable(
                name: "SalesPeriod",
                newName: "sales_period");

            migrationBuilder.RenameTable(
                name: "RoleDivision",
                newName: "role_division");

            migrationBuilder.RenameTable(
                name: "PaymentType",
                newName: "payment_type");

            migrationBuilder.RenameTable(
                name: "OutletPaymentType",
                newName: "outlet_payment_type");

            migrationBuilder.RenameTable(
                name: "OutletExtraGroup",
                newName: "outlet_extra_group");

            migrationBuilder.RenameTable(
                name: "OrderItemStatusAudit",
                newName: "order_item_status_audit");

            migrationBuilder.RenameTable(
                name: "OrderItemStatus",
                newName: "order_item_status");

            migrationBuilder.RenameTable(
                name: "OrderItemOption",
                newName: "order_item_option");

            migrationBuilder.RenameTable(
                name: "OrderItemExtra",
                newName: "order_item_extra");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                newName: "order_item");

            migrationBuilder.RenameTable(
                name: "OrderGroup",
                newName: "order_group");

            migrationBuilder.RenameTable(
                name: "OptionStock",
                newName: "option_stock");

            migrationBuilder.RenameTable(
                name: "OptionGroup",
                newName: "option_group");

            migrationBuilder.RenameTable(
                name: "NotificationUser",
                newName: "notification_user");

            migrationBuilder.RenameTable(
                name: "NotificationLog",
                newName: "notification_log");

            migrationBuilder.RenameTable(
                name: "MenuSection",
                newName: "menu_section");

            migrationBuilder.RenameTable(
                name: "MenuItemStock",
                newName: "menu_item_stock");

            migrationBuilder.RenameTable(
                name: "MenuItemOptionGroup",
                newName: "menu_item_option_group");

            migrationBuilder.RenameTable(
                name: "MenuItemExtraGroup",
                newName: "menu_item_extra_group");

            migrationBuilder.RenameTable(
                name: "MenuItem",
                newName: "menu_item");

            migrationBuilder.RenameTable(
                name: "HaloReference",
                newName: "halo_reference");

            migrationBuilder.RenameTable(
                name: "HaloLog",
                newName: "halo_log");

            migrationBuilder.RenameTable(
                name: "HaloConfig",
                newName: "halo_config");

            migrationBuilder.RenameTable(
                name: "ExtraStock",
                newName: "extra_stock");

            migrationBuilder.RenameTable(
                name: "ExtraGroup",
                newName: "extra_group");

            migrationBuilder.RenameTable(
                name: "EmailLog",
                newName: "email_log");

            migrationBuilder.RenameTable(
                name: "DivisionType",
                newName: "division_type");

            migrationBuilder.RenameTable(
                name: "CashUpUserItemType",
                newName: "cash_up_user_item_type");

            migrationBuilder.RenameTable(
                name: "CashUpUserItem",
                newName: "cash_up_user_item");

            migrationBuilder.RenameTable(
                name: "CashUpUser",
                newName: "cash_up_user");

            migrationBuilder.RenameTable(
                name: "CashUpConfig",
                newName: "cash_up_config");

            migrationBuilder.RenameTable(
                name: "CashUp",
                newName: "cash_up");

            migrationBuilder.RenameTable(
                name: "AdjustmentTypeOutlet",
                newName: "adjustment_type_outlet");

            migrationBuilder.RenameTable(
                name: "AdjustmentType",
                newName: "adjustment_type");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "user",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "user",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "user",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "user",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "user",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "user",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "user",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "unit",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "unit",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tag",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "MenuItemId",
                table: "tag",
                newName: "menu_item_id");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "tag",
                newName: "tag_id");

            migrationBuilder.RenameIndex(
                name: "IX_Tag_MenuItemId",
                table: "tag",
                newName: "ix_tag_menu_item_id");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "table",
                newName: "position");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "table",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "table",
                newName: "capacity");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "table",
                newName: "section_id");

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "table",
                newName: "table_id");

            migrationBuilder.RenameIndex(
                name: "IX_Table_SectionId",
                table: "table",
                newName: "ix_table_section_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "supplier",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "supplier",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "supplier",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "supplier",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "ContactNumber",
                table: "supplier",
                newName: "contact_number");

            migrationBuilder.RenameColumn(
                name: "ContactName",
                table: "supplier",
                newName: "contact_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "stock",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "stock",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "stock",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "stock",
                newName: "unit_id");

            migrationBuilder.RenameColumn(
                name: "StockCategoryId",
                table: "stock",
                newName: "stock_category_id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "stock",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "stock",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "stock",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "stock",
                newName: "created_by");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_UnitId",
                table: "stock",
                newName: "ix_stock_unit_id");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_StockCategoryId",
                table: "stock",
                newName: "ix_stock_stock_category_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "section",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "section",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "section",
                newName: "outlet_id");

            migrationBuilder.RenameIndex(
                name: "IX_Section_OutletId",
                table: "section",
                newName: "ix_section_outlet_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "role",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "role",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "isFrontLine",
                table: "role",
                newName: "is_front_line");

            migrationBuilder.RenameColumn(
                name: "isBackOffice",
                table: "role",
                newName: "is_back_office");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "role",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "role",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "Port",
                table: "printer",
                newName: "port");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "printer",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "printer",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PrinterName",
                table: "printer",
                newName: "printer_name");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "printer",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "LineCharacters",
                table: "printer",
                newName: "line_characters");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "printer",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "printer",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "IsEnabled",
                table: "printer",
                newName: "is_enabled");

            migrationBuilder.RenameColumn(
                name: "IPAddress",
                table: "printer",
                newName: "ip_address");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "printer",
                newName: "device_id");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "printer",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "payment",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "payment",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "payment",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TableBookingId",
                table: "payment",
                newName: "table_booking_id");

            migrationBuilder.RenameColumn(
                name: "PaymentTypeId",
                table: "payment",
                newName: "payment_type_id");

            migrationBuilder.RenameColumn(
                name: "PaymentReference",
                table: "payment",
                newName: "payment_reference");

            migrationBuilder.RenameColumn(
                name: "DateReceived",
                table: "payment",
                newName: "date_received");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_TableBookingId",
                table: "payment",
                newName: "ix_payment_table_booking_id");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_PaymentTypeId",
                table: "payment",
                newName: "ix_payment_payment_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_PaymentReference",
                table: "payment",
                newName: "ix_payment_payment_reference");

            migrationBuilder.RenameColumn(
                name: "Registration",
                table: "outlet",
                newName: "registration");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "outlet",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "outlet",
                newName: "logo");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "outlet",
                newName: "company");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "outlet",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "outlet",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "VATNumber",
                table: "outlet",
                newName: "vat_number");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "outlet",
                newName: "business_id");

            migrationBuilder.RenameIndex(
                name: "IX_Outlet_BusinessId",
                table: "outlet",
                newName: "ix_outlet_business_id");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "option",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "option",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "option",
                newName: "position_id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "option",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "OptionGroupId",
                table: "option",
                newName: "option_group_id");

            migrationBuilder.RenameColumn(
                name: "OptionId",
                table: "option",
                newName: "option_id");

            migrationBuilder.RenameIndex(
                name: "IX_Option_OptionGroupId",
                table: "option",
                newName: "ix_option_option_group_id");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "menu",
                newName: "position");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "menu",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "menu",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "menu",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "menu",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "menu",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "menu",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "menu",
                newName: "created_by");

            migrationBuilder.RenameIndex(
                name: "IX_Menu_OutletId",
                table: "menu",
                newName: "ix_menu_outlet_id");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "extra",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "extra",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "extra",
                newName: "position_id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "extra",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "ExtraGroupId",
                table: "extra",
                newName: "extra_group_id");

            migrationBuilder.RenameColumn(
                name: "ExtraId",
                table: "extra",
                newName: "extra_id");

            migrationBuilder.RenameIndex(
                name: "IX_Extra_ExtraGroupId",
                table: "extra",
                newName: "ix_extra_extra_group_id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "division",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "DivisionTypeId",
                table: "division",
                newName: "division_type_id");

            migrationBuilder.RenameColumn(
                name: "DivisionName",
                table: "division",
                newName: "division_name");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "division",
                newName: "division_id");

            migrationBuilder.RenameColumn(
                name: "Orders",
                table: "customer",
                newName: "orders");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "customer",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "customer",
                newName: "table_id");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "customer",
                newName: "customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_TableId",
                table: "customer",
                newName: "ix_customer_table_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "clock",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "clock",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "clock",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "clock",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "clock",
                newName: "end_date");

            migrationBuilder.RenameIndex(
                name: "IX_Clock_UserId",
                table: "clock",
                newName: "ix_clock_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Clock_OutletId",
                table: "clock",
                newName: "ix_clock_outlet_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "business",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "business",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "adjustment",
                newName: "note");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "adjustment",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "adjustment",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "adjustment",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TableBookingId",
                table: "adjustment",
                newName: "table_booking_id");

            migrationBuilder.RenameColumn(
                name: "AdjustmentTypeId",
                table: "adjustment",
                newName: "adjustment_type_id");

            migrationBuilder.RenameColumn(
                name: "AdjustmentId",
                table: "adjustment",
                newName: "adjustment_id");

            migrationBuilder.RenameIndex(
                name: "IX_Adjustment_TableBookingId",
                table: "adjustment",
                newName: "ix_adjustment_table_booking_id");

            migrationBuilder.RenameIndex(
                name: "IX_Adjustment_AdjustmentTypeId",
                table: "adjustment",
                newName: "ix_adjustment_adjustment_type_id");

            migrationBuilder.RenameColumn(
                name: "Suburb",
                table: "address",
                newName: "suburb");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "address",
                newName: "province");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "address",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "address",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StreetName",
                table: "address",
                newName: "street_name");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "address",
                newName: "postal_code");

            migrationBuilder.RenameColumn(
                name: "HouseNr",
                table: "address",
                newName: "house_nr");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "user_role_outlet",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user_role_outlet",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_role_outlet",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "user_role_outlet",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "user_role_outlet",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "user_role_outlet",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "user_role_outlet",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "user_role_outlet",
                newName: "created_by");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleOutlet_UserId",
                table: "user_role_outlet",
                newName: "ix_user_role_outlet_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleOutlet_RoleId",
                table: "user_role_outlet",
                newName: "ix_user_role_outlet_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleOutlet_OutletId",
                table: "user_role_outlet",
                newName: "ix_user_role_outlet_outlet_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user_outlet",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_outlet",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "user_outlet",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "IsCurrent",
                table: "user_outlet",
                newName: "is_current");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "table_booking",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "table_booking",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "table_booking",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TotalTips",
                table: "table_booking",
                newName: "total_tips");

            migrationBuilder.RenameColumn(
                name: "TotalPayments",
                table: "table_booking",
                newName: "total_payments");

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "table_booking",
                newName: "table_id");

            migrationBuilder.RenameColumn(
                name: "SalesPeriodId",
                table: "table_booking",
                newName: "sales_period_id");

            migrationBuilder.RenameColumn(
                name: "CloseDate",
                table: "table_booking",
                newName: "close_date");

            migrationBuilder.RenameColumn(
                name: "CashUpUserId",
                table: "table_booking",
                newName: "cash_up_user_id");

            migrationBuilder.RenameColumn(
                name: "BookingName",
                table: "table_booking",
                newName: "booking_name");

            migrationBuilder.RenameColumn(
                name: "BookingDate",
                table: "table_booking",
                newName: "booking_date");

            migrationBuilder.RenameIndex(
                name: "IX_TableBooking_UserId",
                table: "table_booking",
                newName: "ix_table_booking_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_TableBooking_TableId",
                table: "table_booking",
                newName: "ix_table_booking_table_id");

            migrationBuilder.RenameIndex(
                name: "IX_TableBooking_SalesPeriodId",
                table: "table_booking",
                newName: "ix_table_booking_sales_period_id");

            migrationBuilder.RenameIndex(
                name: "IX_TableBooking_CashUpUserId",
                table: "table_booking",
                newName: "ix_table_booking_cash_up_user_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "stock_order_status",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "stock_order_status",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "stock_order_status",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "stock_order_status",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "stock_order_status",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "stock_order_status",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "stock_order_item_status",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "stock_order_item_status",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "stock_order_item_status",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "stock_order_item_status",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "stock_order_item_status",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "stock_order_item_status",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "stock_order_item",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "stock_order_item",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Actual",
                table: "stock_order_item",
                newName: "actual");

            migrationBuilder.RenameColumn(
                name: "StockOrderItemStatusId",
                table: "stock_order_item",
                newName: "stock_order_item_status_id");

            migrationBuilder.RenameColumn(
                name: "OrderAmount",
                table: "stock_order_item",
                newName: "order_amount");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "stock_order_item",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "stock_order_item",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "stock_order_item",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "stock_order_item",
                newName: "stock_id");

            migrationBuilder.RenameColumn(
                name: "StockOrderId",
                table: "stock_order_item",
                newName: "stock_order_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockOrderItem_StockOrderItemStatusId",
                table: "stock_order_item",
                newName: "ix_stock_order_item_stock_order_item_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockOrderItem_StockId",
                table: "stock_order_item",
                newName: "ix_stock_order_item_stock_id");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "stock_order",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "stock_order",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "stock_order",
                newName: "supplier_id");

            migrationBuilder.RenameColumn(
                name: "StockOrderStatusId",
                table: "stock_order",
                newName: "stock_order_status_id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "stock_order",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "OrderNumber",
                table: "stock_order",
                newName: "order_number");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "stock_order",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "stock_order",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "stock_order",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "stock_order",
                newName: "division_id");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "stock_order",
                newName: "created_by");

            migrationBuilder.RenameIndex(
                name: "IX_StockOrder_SupplierId",
                table: "stock_order",
                newName: "ix_stock_order_supplier_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockOrder_StockOrderStatusId",
                table: "stock_order",
                newName: "ix_stock_order_stock_order_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockOrder_DivisionId",
                table: "stock_order",
                newName: "ix_stock_order_division_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "stock_item_audit_type",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "stock_item_audit_type",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "stock_item_audit",
                newName: "updated");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "stock_item_audit",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "stock_item_audit",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ToActual",
                table: "stock_item_audit",
                newName: "to_actual");

            migrationBuilder.RenameColumn(
                name: "StockItemId",
                table: "stock_item_audit",
                newName: "stock_item_id");

            migrationBuilder.RenameColumn(
                name: "StockItemAuditTypeId",
                table: "stock_item_audit",
                newName: "stock_item_audit_type_id");

            migrationBuilder.RenameColumn(
                name: "OrderItemId",
                table: "stock_item_audit",
                newName: "order_item_id");

            migrationBuilder.RenameColumn(
                name: "FromActual",
                table: "stock_item_audit",
                newName: "from_actual");

            migrationBuilder.RenameIndex(
                name: "IX_StockItemAudit_StockItemAuditTypeId",
                table: "stock_item_audit",
                newName: "ix_stock_item_audit_stock_item_audit_type_id");

            migrationBuilder.RenameColumn(
                name: "Threshold",
                table: "stock_item",
                newName: "threshold");

            migrationBuilder.RenameColumn(
                name: "Actual",
                table: "stock_item",
                newName: "actual");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "stock_item",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "stock_item",
                newName: "stock_id");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "stock_item",
                newName: "division_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockItem_StockId",
                table: "stock_item",
                newName: "ix_stock_item_stock_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockItem_DivisionId",
                table: "stock_item",
                newName: "ix_stock_item_division_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "stock_category",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "stock_category",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "stock_allocate_status",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "stock_allocate_status",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "stock_allocate_status",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "stock_allocate_status",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "stock_allocate_status",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "stock_allocate_status",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "stock_allocate_item_status",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "stock_allocate_item_status",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "stock_allocate_item_status",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "stock_allocate_item_status",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "stock_allocate_item_status",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "stock_allocate_item_status",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "stock_allocate_item",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Completed",
                table: "stock_allocate_item",
                newName: "completed");

            migrationBuilder.RenameColumn(
                name: "Actual",
                table: "stock_allocate_item",
                newName: "actual");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "stock_allocate_item",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "stock_allocate_item",
                newName: "stock_id");

            migrationBuilder.RenameColumn(
                name: "StockAllocateItemStatusId",
                table: "stock_allocate_item",
                newName: "stock_allocate_item_status_id");

            migrationBuilder.RenameColumn(
                name: "StockAllocateId",
                table: "stock_allocate_item",
                newName: "stock_allocate_id");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "stock_allocate_item",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "stock_allocate_item",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "stock_allocate_item",
                newName: "division_id");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "stock_allocate_item",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "AllocateAmount",
                table: "stock_allocate_item",
                newName: "allocate_amount");

            migrationBuilder.RenameIndex(
                name: "IX_StockAllocateItem_StockId",
                table: "stock_allocate_item",
                newName: "ix_stock_allocate_item_stock_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockAllocateItem_StockAllocateItemStatusId",
                table: "stock_allocate_item",
                newName: "ix_stock_allocate_item_stock_allocate_item_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockAllocateItem_StockAllocateId",
                table: "stock_allocate_item",
                newName: "ix_stock_allocate_item_stock_allocate_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockAllocateItem_DivisionId",
                table: "stock_allocate_item",
                newName: "ix_stock_allocate_item_division_id");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "stock_allocate",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Completed",
                table: "stock_allocate",
                newName: "completed");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "stock_allocate",
                newName: "comment");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "stock_allocate",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ToDivisionId",
                table: "stock_allocate",
                newName: "to_division_id");

            migrationBuilder.RenameColumn(
                name: "StockAllocateStatusId",
                table: "stock_allocate",
                newName: "stock_allocate_status_id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "stock_allocate",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "FromUserUserId",
                table: "stock_allocate",
                newName: "from_user_user_id");

            migrationBuilder.RenameColumn(
                name: "FromUserId",
                table: "stock_allocate",
                newName: "from_user_id");

            migrationBuilder.RenameColumn(
                name: "FromDivisionId",
                table: "stock_allocate",
                newName: "from_division_id");

            migrationBuilder.RenameColumn(
                name: "AssignedUserUserId",
                table: "stock_allocate",
                newName: "assigned_user_user_id");

            migrationBuilder.RenameColumn(
                name: "AssignedUserId",
                table: "stock_allocate",
                newName: "assigned_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockAllocate_ToDivisionId",
                table: "stock_allocate",
                newName: "ix_stock_allocate_to_division_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockAllocate_StockAllocateStatusId",
                table: "stock_allocate",
                newName: "ix_stock_allocate_stock_allocate_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockAllocate_FromUserUserId",
                table: "stock_allocate",
                newName: "ix_stock_allocate_from_user_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockAllocate_FromDivisionId",
                table: "stock_allocate",
                newName: "ix_stock_allocate_from_division_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockAllocate_AssignedUserUserId",
                table: "stock_allocate",
                newName: "ix_stock_allocate_assigned_user_user_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "sales_period",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sales_period",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "sales_period",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "sales_period",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "sales_period",
                newName: "end_date");

            migrationBuilder.RenameIndex(
                name: "IX_SalesPeriod_OutletId",
                table: "sales_period",
                newName: "ix_sales_period_outlet_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "role_division",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "role_division",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "role_division",
                newName: "division_id");

            migrationBuilder.RenameIndex(
                name: "IX_RoleDivision_DivisionId",
                table: "role_division",
                newName: "ix_role_division_division_id");

            migrationBuilder.RenameColumn(
                name: "TipLevyPercentage",
                table: "payment_type",
                newName: "tip_levy_percentage");

            migrationBuilder.RenameColumn(
                name: "PaymentTypeName",
                table: "payment_type",
                newName: "payment_type_name");

            migrationBuilder.RenameColumn(
                name: "DiscountPercentage",
                table: "payment_type",
                newName: "discount_percentage");

            migrationBuilder.RenameColumn(
                name: "CanEdit",
                table: "payment_type",
                newName: "can_edit");

            migrationBuilder.RenameColumn(
                name: "PaymentTypeId",
                table: "payment_type",
                newName: "payment_type_id");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "outlet_payment_type",
                newName: "position");

            migrationBuilder.RenameColumn(
                name: "PaymentTypeId",
                table: "outlet_payment_type",
                newName: "payment_type_id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "outlet_payment_type",
                newName: "outlet_id");

            migrationBuilder.RenameIndex(
                name: "IX_OutletPaymentType_PaymentTypeId",
                table: "outlet_payment_type",
                newName: "ix_outlet_payment_type_payment_type_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "outlet_extra_group",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "outlet_extra_group",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "ExtraGroupId",
                table: "outlet_extra_group",
                newName: "extra_group_id");

            migrationBuilder.RenameIndex(
                name: "IX_OutletExtraGroup_OutletId",
                table: "outlet_extra_group",
                newName: "ix_outlet_extra_group_outlet_id");

            migrationBuilder.RenameIndex(
                name: "IX_OutletExtraGroup_ExtraGroupId",
                table: "outlet_extra_group",
                newName: "ix_outlet_extra_group_extra_group_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "order_item_status_audit",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "order_item_status_audit",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "StatusDate",
                table: "order_item_status_audit",
                newName: "status_date");

            migrationBuilder.RenameColumn(
                name: "OrderItemStatusId",
                table: "order_item_status_audit",
                newName: "order_item_status_id");

            migrationBuilder.RenameColumn(
                name: "OrderItemId",
                table: "order_item_status_audit",
                newName: "order_item_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "order_item_status",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "order_item_status",
                newName: "priority");

            migrationBuilder.RenameColumn(
                name: "Notify",
                table: "order_item_status",
                newName: "notify");

            migrationBuilder.RenameColumn(
                name: "isHistory",
                table: "order_item_status",
                newName: "is_history");

            migrationBuilder.RenameColumn(
                name: "isFrontLine",
                table: "order_item_status",
                newName: "is_front_line");

            migrationBuilder.RenameColumn(
                name: "isComplete",
                table: "order_item_status",
                newName: "is_complete");

            migrationBuilder.RenameColumn(
                name: "isCancelled",
                table: "order_item_status",
                newName: "is_cancelled");

            migrationBuilder.RenameColumn(
                name: "isBillable",
                table: "order_item_status",
                newName: "is_billable");

            migrationBuilder.RenameColumn(
                name: "isBackOffice",
                table: "order_item_status",
                newName: "is_back_office");

            migrationBuilder.RenameColumn(
                name: "assignGroup",
                table: "order_item_status",
                newName: "assign_group");

            migrationBuilder.RenameColumn(
                name: "OrderItemStatusId",
                table: "order_item_status",
                newName: "order_item_status_id");

            migrationBuilder.RenameColumn(
                name: "OrderItemId",
                table: "order_item_option",
                newName: "order_item_id");

            migrationBuilder.RenameColumn(
                name: "OptionId",
                table: "order_item_option",
                newName: "option_id");

            migrationBuilder.RenameColumn(
                name: "OrderItemOptionId",
                table: "order_item_option",
                newName: "order_item_option_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItemOption_OrderItemId",
                table: "order_item_option",
                newName: "ix_order_item_option_order_item_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItemOption_OptionId",
                table: "order_item_option",
                newName: "ix_order_item_option_option_id");

            migrationBuilder.RenameColumn(
                name: "OrderItemId",
                table: "order_item_extra",
                newName: "order_item_id");

            migrationBuilder.RenameColumn(
                name: "ExtraId",
                table: "order_item_extra",
                newName: "extra_id");

            migrationBuilder.RenameColumn(
                name: "OrderItemExtraId",
                table: "order_item_extra",
                newName: "order_item_extra_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItemExtra_OrderItemId",
                table: "order_item_extra",
                newName: "ix_order_item_extra_order_item_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItemExtra_ExtraId",
                table: "order_item_extra",
                newName: "ix_order_item_extra_extra_id");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "order_item",
                newName: "note");

            migrationBuilder.RenameColumn(
                name: "TableBookingId",
                table: "order_item",
                newName: "table_booking_id");

            migrationBuilder.RenameColumn(
                name: "OrderUpdated",
                table: "order_item",
                newName: "order_updated");

            migrationBuilder.RenameColumn(
                name: "OrderReceived",
                table: "order_item",
                newName: "order_received");

            migrationBuilder.RenameColumn(
                name: "OrderItemStatusId",
                table: "order_item",
                newName: "order_item_status_id");

            migrationBuilder.RenameColumn(
                name: "OrderGroupId",
                table: "order_item",
                newName: "order_group_id");

            migrationBuilder.RenameColumn(
                name: "OrderCompleted",
                table: "order_item",
                newName: "order_completed");

            migrationBuilder.RenameColumn(
                name: "MenuItemId",
                table: "order_item",
                newName: "menu_item_id");

            migrationBuilder.RenameColumn(
                name: "OrderItemId",
                table: "order_item",
                newName: "order_item_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_TableBookingId",
                table: "order_item",
                newName: "ix_order_item_table_booking_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderItemStatusId",
                table: "order_item",
                newName: "ix_order_item_order_item_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderGroupId",
                table: "order_item",
                newName: "ix_order_item_order_group_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_MenuItemId",
                table: "order_item",
                newName: "ix_order_item_menu_item_id");

            migrationBuilder.RenameColumn(
                name: "OrderGroupId",
                table: "order_group",
                newName: "order_group_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "option_stock",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "option_stock",
                newName: "unit_id");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "option_stock",
                newName: "stock_id");

            migrationBuilder.RenameColumn(
                name: "OptionId",
                table: "option_stock",
                newName: "option_id");

            migrationBuilder.RenameIndex(
                name: "IX_OptionStock_UnitId",
                table: "option_stock",
                newName: "ix_option_stock_unit_id");

            migrationBuilder.RenameIndex(
                name: "IX_OptionStock_StockId",
                table: "option_stock",
                newName: "ix_option_stock_stock_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "option_group",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "option_group",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "MinSelections",
                table: "option_group",
                newName: "min_selections");

            migrationBuilder.RenameColumn(
                name: "MaxSelections",
                table: "option_group",
                newName: "max_selections");

            migrationBuilder.RenameColumn(
                name: "OptionGroupId",
                table: "option_group",
                newName: "option_group_id");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "notification_user",
                newName: "token");

            migrationBuilder.RenameColumn(
                name: "DateInserted",
                table: "notification_user",
                newName: "date_inserted");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "notification_user",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "notification_log",
                newName: "token");

            migrationBuilder.RenameColumn(
                name: "Payload",
                table: "notification_log",
                newName: "payload");

            migrationBuilder.RenameColumn(
                name: "Error",
                table: "notification_log",
                newName: "error");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "notification_log",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "notification_log",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "IsSuccess",
                table: "notification_log",
                newName: "is_success");

            migrationBuilder.RenameColumn(
                name: "HttpStatusResponse",
                table: "notification_log",
                newName: "http_status_response");

            migrationBuilder.RenameColumn(
                name: "DateInserted",
                table: "notification_log",
                newName: "date_inserted");

            migrationBuilder.RenameColumn(
                name: "ChannelId",
                table: "notification_log",
                newName: "channel_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "menu_section",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "menu_section",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "menu_section",
                newName: "position_id");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "menu_section",
                newName: "parent_id");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "menu_section",
                newName: "menu_id");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "menu_section",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "menu_section",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "menu_section",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "MenuSectionId",
                table: "menu_section",
                newName: "menu_section_id");

            migrationBuilder.RenameIndex(
                name: "IX_MenuSection_ParentId",
                table: "menu_section",
                newName: "ix_menu_section_parent_id");

            migrationBuilder.RenameIndex(
                name: "IX_MenuSection_MenuId",
                table: "menu_section",
                newName: "ix_menu_section_menu_id");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "menu_item_stock",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "StockItemId",
                table: "menu_item_stock",
                newName: "stock_item_id");

            migrationBuilder.RenameColumn(
                name: "MenuItemId",
                table: "menu_item_stock",
                newName: "menu_item_id");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItemStock_StockItemId",
                table: "menu_item_stock",
                newName: "ix_menu_item_stock_stock_item_id");

            migrationBuilder.RenameColumn(
                name: "MenuItemId",
                table: "menu_item_option_group",
                newName: "menu_item_id");

            migrationBuilder.RenameColumn(
                name: "OptionGroupId",
                table: "menu_item_option_group",
                newName: "option_group_id");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItemOptionGroup_MenuItemId",
                table: "menu_item_option_group",
                newName: "ix_menu_item_option_group_menu_item_id");

            migrationBuilder.RenameColumn(
                name: "MenuItemId",
                table: "menu_item_extra_group",
                newName: "menu_item_id");

            migrationBuilder.RenameColumn(
                name: "ExtraGroupId",
                table: "menu_item_extra_group",
                newName: "extra_group_id");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItemExtraGroup_MenuItemId",
                table: "menu_item_extra_group",
                newName: "ix_menu_item_extra_group_menu_item_id");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "menu_item",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "menu_item",
                newName: "position");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "menu_item",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "menu_item",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "menu_item",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "StockPrice",
                table: "menu_item",
                newName: "stock_price");

            migrationBuilder.RenameColumn(
                name: "SearchVector",
                table: "menu_item",
                newName: "search_vector");

            migrationBuilder.RenameColumn(
                name: "MenuSectionId",
                table: "menu_item",
                newName: "menu_section_id");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "menu_item",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "menu_item",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "IsEnabled",
                table: "menu_item",
                newName: "is_enabled");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "menu_item",
                newName: "is_available");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "menu_item",
                newName: "division_id");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "menu_item",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "MenuItemId",
                table: "menu_item",
                newName: "menu_item_id");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItem_SearchVector",
                table: "menu_item",
                newName: "ix_menu_item_search_vector");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItem_MenuSectionId",
                table: "menu_item",
                newName: "ix_menu_item_menu_section_id");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItem_DivisionId",
                table: "menu_item",
                newName: "ix_menu_item_division_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "halo_reference",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "halo_reference",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TableBookingId",
                table: "halo_reference",
                newName: "table_booking_id");

            migrationBuilder.RenameColumn(
                name: "HaloRef",
                table: "halo_reference",
                newName: "halo_ref");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "halo_log",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Response",
                table: "halo_log",
                newName: "response");

            migrationBuilder.RenameColumn(
                name: "Request",
                table: "halo_log",
                newName: "request");

            migrationBuilder.RenameColumn(
                name: "Error",
                table: "halo_log",
                newName: "error");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "halo_log",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "halo_log",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StatusCode",
                table: "halo_log",
                newName: "status_code");

            migrationBuilder.RenameColumn(
                name: "RequestUrl",
                table: "halo_log",
                newName: "request_url");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "halo_log",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Iv",
                table: "halo_config",
                newName: "iv");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "halo_config",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "halo_config",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "XApiKey",
                table: "halo_config",
                newName: "x_api_key");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "halo_config",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "MerchantId",
                table: "halo_config",
                newName: "merchant_id");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "halo_config",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "halo_config",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "IsEnabled",
                table: "halo_config",
                newName: "is_enabled");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "halo_config",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "extra_stock",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "extra_stock",
                newName: "unit_id");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "extra_stock",
                newName: "stock_id");

            migrationBuilder.RenameColumn(
                name: "ExtraId",
                table: "extra_stock",
                newName: "extra_id");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraStock_UnitId",
                table: "extra_stock",
                newName: "ix_extra_stock_unit_id");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraStock_StockId",
                table: "extra_stock",
                newName: "ix_extra_stock_stock_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "extra_group",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "extra_group",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "ExtraGroupId",
                table: "extra_group",
                newName: "extra_group_id");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "email_log",
                newName: "subject");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "email_log",
                newName: "message");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "email_log",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "email_log",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "email_log",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IsSent",
                table: "email_log",
                newName: "is_sent");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "division_type",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DivisionName",
                table: "division_type",
                newName: "division_name");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "cash_up_user_item_type",
                newName: "position");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cash_up_user_item_type",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PaymentTypeId",
                table: "cash_up_user_item_type",
                newName: "payment_type_id");

            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "cash_up_user_item_type",
                newName: "item_type");

            migrationBuilder.RenameColumn(
                name: "IsAuto",
                table: "cash_up_user_item_type",
                newName: "is_auto");

            migrationBuilder.RenameColumn(
                name: "CashupConfigId",
                table: "cash_up_user_item_type",
                newName: "cashup_config_id");

            migrationBuilder.RenameColumn(
                name: "CashUpUserItemRule",
                table: "cash_up_user_item_type",
                newName: "cash_up_user_item_rule");

            migrationBuilder.RenameColumn(
                name: "AffectsGrossBalance",
                table: "cash_up_user_item_type",
                newName: "affects_gross_balance");

            migrationBuilder.RenameColumn(
                name: "AdjustmentTypeId",
                table: "cash_up_user_item_type",
                newName: "adjustment_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_CashUpUserItemType_PaymentTypeId",
                table: "cash_up_user_item_type",
                newName: "ix_cash_up_user_item_type_payment_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_CashUpUserItemType_CashupConfigId",
                table: "cash_up_user_item_type",
                newName: "ix_cash_up_user_item_type_cashup_config_id");

            migrationBuilder.RenameIndex(
                name: "IX_CashUpUserItemType_AdjustmentTypeId",
                table: "cash_up_user_item_type",
                newName: "ix_cash_up_user_item_type_adjustment_type_id");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "cash_up_user_item",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cash_up_user_item",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "cash_up_user_item",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "cash_up_user_item",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "CashUpUserItemTypeId",
                table: "cash_up_user_item",
                newName: "cash_up_user_item_type_id");

            migrationBuilder.RenameColumn(
                name: "CashUpUserId",
                table: "cash_up_user_item",
                newName: "cash_up_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_CashUpUserItem_CashUpUserItemTypeId",
                table: "cash_up_user_item",
                newName: "ix_cash_up_user_item_cash_up_user_item_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_CashUpUserItem_CashUpUserId",
                table: "cash_up_user_item",
                newName: "ix_cash_up_user_item_cash_up_user_id");

            migrationBuilder.RenameColumn(
                name: "Tips",
                table: "cash_up_user",
                newName: "tips");

            migrationBuilder.RenameColumn(
                name: "Sales",
                table: "cash_up_user",
                newName: "sales");

            migrationBuilder.RenameColumn(
                name: "Payments",
                table: "cash_up_user",
                newName: "payments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cash_up_user",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "cash_up_user",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "SalesPeriodId",
                table: "cash_up_user",
                newName: "sales_period_id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "cash_up_user",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "OpeningBalance",
                table: "cash_up_user",
                newName: "opening_balance");

            migrationBuilder.RenameColumn(
                name: "CompleterUserId",
                table: "cash_up_user",
                newName: "completer_user_id");

            migrationBuilder.RenameColumn(
                name: "ClosingBalance",
                table: "cash_up_user",
                newName: "closing_balance");

            migrationBuilder.RenameColumn(
                name: "CashUpDate",
                table: "cash_up_user",
                newName: "cash_up_date");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "cash_up_config",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "cash_up_config",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cash_up_config",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "cash_up_config",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cash_up",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "cash_up",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TableCount",
                table: "cash_up",
                newName: "table_count");

            migrationBuilder.RenameColumn(
                name: "SignOffUserId",
                table: "cash_up",
                newName: "sign_off_user_id");

            migrationBuilder.RenameColumn(
                name: "SignOffDate",
                table: "cash_up",
                newName: "sign_off_date");

            migrationBuilder.RenameColumn(
                name: "SalesPeriodId",
                table: "cash_up",
                newName: "sales_period_id");

            migrationBuilder.RenameColumn(
                name: "OpenTableCount",
                table: "cash_up",
                newName: "open_table_count");

            migrationBuilder.RenameColumn(
                name: "CashUpTotalPayments",
                table: "cash_up",
                newName: "cash_up_total_payments");

            migrationBuilder.RenameColumn(
                name: "CashUpTotal",
                table: "cash_up",
                newName: "cash_up_total");

            migrationBuilder.RenameColumn(
                name: "CashUpBalance",
                table: "cash_up",
                newName: "cash_up_balance");

            migrationBuilder.RenameIndex(
                name: "IX_CashUp_SalesPeriodId",
                table: "cash_up",
                newName: "ix_cash_up_sales_period_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "adjustment_type_outlet",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "adjustment_type_outlet",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "AdjustmentTypeId",
                table: "adjustment_type_outlet",
                newName: "adjustment_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_AdjustmentTypeOutlet_OutletId",
                table: "adjustment_type_outlet",
                newName: "ix_adjustment_type_outlet_outlet_id");

            migrationBuilder.RenameIndex(
                name: "IX_AdjustmentTypeOutlet_AdjustmentTypeId",
                table: "adjustment_type_outlet",
                newName: "ix_adjustment_type_outlet_adjustment_type_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "adjustment_type",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "adjustment_type",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "AdjustmentTypeId",
                table: "adjustment_type",
                newName: "adjustment_type_id");

            // migrationBuilder.AlterColumn<NpgsqlTsVector>(
            //     name: "search_vector",
            //     table: "menu_item",
            //     type: "tsvector",
            //     nullable: false,
            //     oldClrType: typeof(NpgsqlTsVector),
            //     oldType: "tsvector")
            //     .Annotation("Npgsql:TsVectorConfig", "english")
            //     .Annotation("Npgsql:TsVectorProperties", new[] { "name", "description" })
            //     .OldAnnotation("Npgsql:TsVectorConfig", "english")
            //     .OldAnnotation("Npgsql:TsVectorProperties", new[] { "Name", "Description" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_user",
                table: "user",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_unit",
                table: "unit",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tag",
                table: "tag",
                column: "tag_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_table",
                table: "table",
                column: "table_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_supplier",
                table: "supplier",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock",
                table: "stock",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_section",
                table: "section",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_role",
                table: "role",
                column: "role_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_printer",
                table: "printer",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_payment",
                table: "payment",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_outlet",
                table: "outlet",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_option",
                table: "option",
                column: "option_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu",
                table: "menu",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_extra",
                table: "extra",
                column: "extra_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_division",
                table: "division",
                column: "division_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_customer",
                table: "customer",
                column: "customer_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_clock",
                table: "clock",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_business",
                table: "business",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_adjustment",
                table: "adjustment",
                column: "adjustment_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_address",
                table: "address",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_role_outlet",
                table: "user_role_outlet",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_outlet",
                table: "user_outlet",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_table_booking",
                table: "table_booking",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock_order_status",
                table: "stock_order_status",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock_order_item_status",
                table: "stock_order_item_status",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock_order_item",
                table: "stock_order_item",
                columns: new[] { "stock_order_id", "stock_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock_order",
                table: "stock_order",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock_item_audit_type",
                table: "stock_item_audit_type",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock_item_audit",
                table: "stock_item_audit",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock_item",
                table: "stock_item",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock_category",
                table: "stock_category",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock_allocate_status",
                table: "stock_allocate_status",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock_allocate_item_status",
                table: "stock_allocate_item_status",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock_allocate_item",
                table: "stock_allocate_item",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stock_allocate",
                table: "stock_allocate",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sales_period",
                table: "sales_period",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_role_division",
                table: "role_division",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_payment_type",
                table: "payment_type",
                column: "payment_type_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_outlet_payment_type",
                table: "outlet_payment_type",
                columns: new[] { "outlet_id", "payment_type_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_outlet_extra_group",
                table: "outlet_extra_group",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_item_status_audit",
                table: "order_item_status_audit",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_item_status",
                table: "order_item_status",
                column: "order_item_status_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_item_option",
                table: "order_item_option",
                column: "order_item_option_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_item_extra",
                table: "order_item_extra",
                column: "order_item_extra_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_item",
                table: "order_item",
                column: "order_item_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_group",
                table: "order_group",
                column: "order_group_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_option_stock",
                table: "option_stock",
                columns: new[] { "option_id", "stock_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_option_group",
                table: "option_group",
                column: "option_group_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_notification_user",
                table: "notification_user",
                columns: new[] { "user_id", "token" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_notification_log",
                table: "notification_log",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu_section",
                table: "menu_section",
                column: "menu_section_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu_item_stock",
                table: "menu_item_stock",
                columns: new[] { "menu_item_id", "stock_item_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu_item_option_group",
                table: "menu_item_option_group",
                columns: new[] { "option_group_id", "menu_item_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu_item_extra_group",
                table: "menu_item_extra_group",
                columns: new[] { "extra_group_id", "menu_item_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu_item",
                table: "menu_item",
                column: "menu_item_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_halo_reference",
                table: "halo_reference",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_halo_log",
                table: "halo_log",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_halo_config",
                table: "halo_config",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_extra_stock",
                table: "extra_stock",
                columns: new[] { "extra_id", "stock_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_extra_group",
                table: "extra_group",
                column: "extra_group_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_email_log",
                table: "email_log",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_division_type",
                table: "division_type",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cash_up_user_item_type",
                table: "cash_up_user_item_type",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cash_up_user_item",
                table: "cash_up_user_item",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cash_up_user",
                table: "cash_up_user",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cash_up_config",
                table: "cash_up_config",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cash_up",
                table: "cash_up",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_adjustment_type_outlet",
                table: "adjustment_type_outlet",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_adjustment_type",
                table: "adjustment_type",
                column: "adjustment_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_adjustment_adjustment_type_adjustment_type_id",
                table: "adjustment",
                column: "adjustment_type_id",
                principalTable: "adjustment_type",
                principalColumn: "adjustment_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_adjustment_table_booking_table_booking_id",
                table: "adjustment",
                column: "table_booking_id",
                principalTable: "table_booking",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_adjustment_type_outlet_adjustment_type_adjustment_type_id",
                table: "adjustment_type_outlet",
                column: "adjustment_type_id",
                principalTable: "adjustment_type",
                principalColumn: "adjustment_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_adjustment_type_outlet_outlet_outlet_id",
                table: "adjustment_type_outlet",
                column: "outlet_id",
                principalTable: "outlet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cash_up_sales_period_sales_period_id",
                table: "cash_up",
                column: "sales_period_id",
                principalTable: "sales_period",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cash_up_user_item_cash_up_user_cash_up_user_id",
                table: "cash_up_user_item",
                column: "cash_up_user_id",
                principalTable: "cash_up_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cash_up_user_item_cash_up_user_item_type_cash_up_user_item_",
                table: "cash_up_user_item",
                column: "cash_up_user_item_type_id",
                principalTable: "cash_up_user_item_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cash_up_user_item_type_adjustment_type_adjustment_type_id",
                table: "cash_up_user_item_type",
                column: "adjustment_type_id",
                principalTable: "adjustment_type",
                principalColumn: "adjustment_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cash_up_user_item_type_cash_up_config_cashup_config_id",
                table: "cash_up_user_item_type",
                column: "cashup_config_id",
                principalTable: "cash_up_config",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cash_up_user_item_type_payment_type_payment_type_id",
                table: "cash_up_user_item_type",
                column: "payment_type_id",
                principalTable: "payment_type",
                principalColumn: "payment_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_clock_outlet_outlet_id",
                table: "clock",
                column: "outlet_id",
                principalTable: "outlet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_clock_user_user_id",
                table: "clock",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_customer_table_table_id",
                table: "customer",
                column: "table_id",
                principalTable: "table",
                principalColumn: "table_id");

            migrationBuilder.AddForeignKey(
                name: "fk_extra_extra_group_extra_group_id",
                table: "extra",
                column: "extra_group_id",
                principalTable: "extra_group",
                principalColumn: "extra_group_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_extra_stock_extra_extra_id",
                table: "extra_stock",
                column: "extra_id",
                principalTable: "extra",
                principalColumn: "extra_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_extra_stock_stock_stock_id",
                table: "extra_stock",
                column: "stock_id",
                principalTable: "stock",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_extra_stock_unit_unit_id",
                table: "extra_stock",
                column: "unit_id",
                principalTable: "unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_outlet_outlet_id",
                table: "menu",
                column: "outlet_id",
                principalTable: "outlet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_division_division_id",
                table: "menu_item",
                column: "division_id",
                principalTable: "division",
                principalColumn: "division_id");

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_menu_section_menu_section_id",
                table: "menu_item",
                column: "menu_section_id",
                principalTable: "menu_section",
                principalColumn: "menu_section_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_extra_group_extra_group_extra_group_id",
                table: "menu_item_extra_group",
                column: "extra_group_id",
                principalTable: "extra_group",
                principalColumn: "extra_group_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_extra_group_menu_item_menu_item_id",
                table: "menu_item_extra_group",
                column: "menu_item_id",
                principalTable: "menu_item",
                principalColumn: "menu_item_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_option_group_menu_item_menu_item_id",
                table: "menu_item_option_group",
                column: "menu_item_id",
                principalTable: "menu_item",
                principalColumn: "menu_item_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_option_group_option_group_option_group_id",
                table: "menu_item_option_group",
                column: "option_group_id",
                principalTable: "option_group",
                principalColumn: "option_group_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_stock_menu_item_menu_item_id",
                table: "menu_item_stock",
                column: "menu_item_id",
                principalTable: "menu_item",
                principalColumn: "menu_item_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_stock_stock_item_stock_item_id",
                table: "menu_item_stock",
                column: "stock_item_id",
                principalTable: "stock_item",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_section_menu_menu_id",
                table: "menu_section",
                column: "menu_id",
                principalTable: "menu",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_section_menu_section_parent_id",
                table: "menu_section",
                column: "parent_id",
                principalTable: "menu_section",
                principalColumn: "menu_section_id");

            migrationBuilder.AddForeignKey(
                name: "fk_option_option_group_option_group_id",
                table: "option",
                column: "option_group_id",
                principalTable: "option_group",
                principalColumn: "option_group_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_option_stock_option_option_id",
                table: "option_stock",
                column: "option_id",
                principalTable: "option",
                principalColumn: "option_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_option_stock_stock_stock_id",
                table: "option_stock",
                column: "stock_id",
                principalTable: "stock",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_option_stock_unit_unit_id",
                table: "option_stock",
                column: "unit_id",
                principalTable: "unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_menu_item_menu_item_id",
                table: "order_item",
                column: "menu_item_id",
                principalTable: "menu_item",
                principalColumn: "menu_item_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_order_group_order_group_id",
                table: "order_item",
                column: "order_group_id",
                principalTable: "order_group",
                principalColumn: "order_group_id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_order_item_status_order_item_status_id",
                table: "order_item",
                column: "order_item_status_id",
                principalTable: "order_item_status",
                principalColumn: "order_item_status_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_table_booking_table_booking_id",
                table: "order_item",
                column: "table_booking_id",
                principalTable: "table_booking",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_extra_extra_extra_id",
                table: "order_item_extra",
                column: "extra_id",
                principalTable: "extra",
                principalColumn: "extra_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_extra_order_item_order_item_id",
                table: "order_item_extra",
                column: "order_item_id",
                principalTable: "order_item",
                principalColumn: "order_item_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_option_option_option_id",
                table: "order_item_option",
                column: "option_id",
                principalTable: "option",
                principalColumn: "option_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_option_order_item_order_item_id",
                table: "order_item_option",
                column: "order_item_id",
                principalTable: "order_item",
                principalColumn: "order_item_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_outlet_business_business_id",
                table: "outlet",
                column: "business_id",
                principalTable: "business",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_outlet_extra_group_extra_group_extra_group_id",
                table: "outlet_extra_group",
                column: "extra_group_id",
                principalTable: "extra_group",
                principalColumn: "extra_group_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_outlet_extra_group_outlet_outlet_id",
                table: "outlet_extra_group",
                column: "outlet_id",
                principalTable: "outlet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_outlet_payment_type_outlet_outlet_id",
                table: "outlet_payment_type",
                column: "outlet_id",
                principalTable: "outlet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_outlet_payment_type_payment_type_payment_type_id",
                table: "outlet_payment_type",
                column: "payment_type_id",
                principalTable: "payment_type",
                principalColumn: "payment_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_payment_payment_type_payment_type_id",
                table: "payment",
                column: "payment_type_id",
                principalTable: "payment_type",
                principalColumn: "payment_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_table_booking_table_booking_id",
                table: "payment",
                column: "table_booking_id",
                principalTable: "table_booking",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_role_division_division_division_id",
                table: "role_division",
                column: "division_id",
                principalTable: "division",
                principalColumn: "division_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sales_period_outlet_outlet_id",
                table: "sales_period",
                column: "outlet_id",
                principalTable: "outlet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_section_outlet_outlet_id",
                table: "section",
                column: "outlet_id",
                principalTable: "outlet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_stock_category_stock_category_id",
                table: "stock",
                column: "stock_category_id",
                principalTable: "stock_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_unit_unit_id",
                table: "stock",
                column: "unit_id",
                principalTable: "unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_division_from_division_id",
                table: "stock_allocate",
                column: "from_division_id",
                principalTable: "division",
                principalColumn: "division_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_division_to_division_id",
                table: "stock_allocate",
                column: "to_division_id",
                principalTable: "division",
                principalColumn: "division_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_stock_allocate_status_stock_allocate_status_",
                table: "stock_allocate",
                column: "stock_allocate_status_id",
                principalTable: "stock_allocate_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_user_assigned_user_user_id",
                table: "stock_allocate",
                column: "assigned_user_user_id",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_user_from_user_user_id",
                table: "stock_allocate",
                column: "from_user_user_id",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_item_division_division_id",
                table: "stock_allocate_item",
                column: "division_id",
                principalTable: "division",
                principalColumn: "division_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_item_stock_allocate_item_status_stock_alloca",
                table: "stock_allocate_item",
                column: "stock_allocate_item_status_id",
                principalTable: "stock_allocate_item_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_item_stock_allocate_stock_allocate_id",
                table: "stock_allocate_item",
                column: "stock_allocate_id",
                principalTable: "stock_allocate",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_item_stock_stock_id",
                table: "stock_allocate_item",
                column: "stock_id",
                principalTable: "stock",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_item_division_division_id",
                table: "stock_item",
                column: "division_id",
                principalTable: "division",
                principalColumn: "division_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_item_stock_stock_id",
                table: "stock_item",
                column: "stock_id",
                principalTable: "stock",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_item_audit_stock_item_audit_type_stock_item_audit_typ",
                table: "stock_item_audit",
                column: "stock_item_audit_type_id",
                principalTable: "stock_item_audit_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_order_division_division_id",
                table: "stock_order",
                column: "division_id",
                principalTable: "division",
                principalColumn: "division_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_order_stock_order_status_stock_order_status_id",
                table: "stock_order",
                column: "stock_order_status_id",
                principalTable: "stock_order_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_order_supplier_supplier_id",
                table: "stock_order",
                column: "supplier_id",
                principalTable: "supplier",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_order_item_stock_order_item_status_stock_order_item_s",
                table: "stock_order_item",
                column: "stock_order_item_status_id",
                principalTable: "stock_order_item_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_order_item_stock_order_stock_order_id",
                table: "stock_order_item",
                column: "stock_order_id",
                principalTable: "stock_order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_order_item_stock_stock_id",
                table: "stock_order_item",
                column: "stock_id",
                principalTable: "stock",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_table_section_section_id",
                table: "table",
                column: "section_id",
                principalTable: "section",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_table_booking_cash_up_user_cash_up_user_id",
                table: "table_booking",
                column: "cash_up_user_id",
                principalTable: "cash_up_user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_table_booking_sales_period_sales_period_id",
                table: "table_booking",
                column: "sales_period_id",
                principalTable: "sales_period",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_table_booking_table_table_id",
                table: "table_booking",
                column: "table_id",
                principalTable: "table",
                principalColumn: "table_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_table_booking_user_user_id",
                table: "table_booking",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_tag_menu_item_menu_item_id",
                table: "tag",
                column: "menu_item_id",
                principalTable: "menu_item",
                principalColumn: "menu_item_id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_role_outlet_outlet_outlet_id",
                table: "user_role_outlet",
                column: "outlet_id",
                principalTable: "outlet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_role_outlet_role_role_id",
                table: "user_role_outlet",
                column: "role_id",
                principalTable: "role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_role_outlet_user_user_id",
                table: "user_role_outlet",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adjustment_adjustment_type_adjustment_type_id",
                table: "adjustment");

            migrationBuilder.DropForeignKey(
                name: "fk_adjustment_table_booking_table_booking_id",
                table: "adjustment");

            migrationBuilder.DropForeignKey(
                name: "fk_adjustment_type_outlet_adjustment_type_adjustment_type_id",
                table: "adjustment_type_outlet");

            migrationBuilder.DropForeignKey(
                name: "fk_adjustment_type_outlet_outlet_outlet_id",
                table: "adjustment_type_outlet");

            migrationBuilder.DropForeignKey(
                name: "fk_cash_up_sales_period_sales_period_id",
                table: "cash_up");

            migrationBuilder.DropForeignKey(
                name: "fk_cash_up_user_item_cash_up_user_cash_up_user_id",
                table: "cash_up_user_item");

            migrationBuilder.DropForeignKey(
                name: "fk_cash_up_user_item_cash_up_user_item_type_cash_up_user_item_",
                table: "cash_up_user_item");

            migrationBuilder.DropForeignKey(
                name: "fk_cash_up_user_item_type_adjustment_type_adjustment_type_id",
                table: "cash_up_user_item_type");

            migrationBuilder.DropForeignKey(
                name: "fk_cash_up_user_item_type_cash_up_config_cashup_config_id",
                table: "cash_up_user_item_type");

            migrationBuilder.DropForeignKey(
                name: "fk_cash_up_user_item_type_payment_type_payment_type_id",
                table: "cash_up_user_item_type");

            migrationBuilder.DropForeignKey(
                name: "fk_clock_outlet_outlet_id",
                table: "clock");

            migrationBuilder.DropForeignKey(
                name: "fk_clock_user_user_id",
                table: "clock");

            migrationBuilder.DropForeignKey(
                name: "fk_customer_table_table_id",
                table: "customer");

            migrationBuilder.DropForeignKey(
                name: "fk_extra_extra_group_extra_group_id",
                table: "extra");

            migrationBuilder.DropForeignKey(
                name: "fk_extra_stock_extra_extra_id",
                table: "extra_stock");

            migrationBuilder.DropForeignKey(
                name: "fk_extra_stock_stock_stock_id",
                table: "extra_stock");

            migrationBuilder.DropForeignKey(
                name: "fk_extra_stock_unit_unit_id",
                table: "extra_stock");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_outlet_outlet_id",
                table: "menu");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_division_division_id",
                table: "menu_item");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_menu_section_menu_section_id",
                table: "menu_item");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_extra_group_extra_group_extra_group_id",
                table: "menu_item_extra_group");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_extra_group_menu_item_menu_item_id",
                table: "menu_item_extra_group");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_option_group_menu_item_menu_item_id",
                table: "menu_item_option_group");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_option_group_option_group_option_group_id",
                table: "menu_item_option_group");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_stock_menu_item_menu_item_id",
                table: "menu_item_stock");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_stock_stock_item_stock_item_id",
                table: "menu_item_stock");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_section_menu_menu_id",
                table: "menu_section");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_section_menu_section_parent_id",
                table: "menu_section");

            migrationBuilder.DropForeignKey(
                name: "fk_option_option_group_option_group_id",
                table: "option");

            migrationBuilder.DropForeignKey(
                name: "fk_option_stock_option_option_id",
                table: "option_stock");

            migrationBuilder.DropForeignKey(
                name: "fk_option_stock_stock_stock_id",
                table: "option_stock");

            migrationBuilder.DropForeignKey(
                name: "fk_option_stock_unit_unit_id",
                table: "option_stock");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_menu_item_menu_item_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_order_group_order_group_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_order_item_status_order_item_status_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_table_booking_table_booking_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_extra_extra_extra_id",
                table: "order_item_extra");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_extra_order_item_order_item_id",
                table: "order_item_extra");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_option_option_option_id",
                table: "order_item_option");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_option_order_item_order_item_id",
                table: "order_item_option");

            migrationBuilder.DropForeignKey(
                name: "fk_outlet_business_business_id",
                table: "outlet");

            migrationBuilder.DropForeignKey(
                name: "fk_outlet_extra_group_extra_group_extra_group_id",
                table: "outlet_extra_group");

            migrationBuilder.DropForeignKey(
                name: "fk_outlet_extra_group_outlet_outlet_id",
                table: "outlet_extra_group");

            migrationBuilder.DropForeignKey(
                name: "fk_outlet_payment_type_outlet_outlet_id",
                table: "outlet_payment_type");

            migrationBuilder.DropForeignKey(
                name: "fk_outlet_payment_type_payment_type_payment_type_id",
                table: "outlet_payment_type");

            migrationBuilder.DropForeignKey(
                name: "fk_payment_payment_type_payment_type_id",
                table: "payment");

            migrationBuilder.DropForeignKey(
                name: "fk_payment_table_booking_table_booking_id",
                table: "payment");

            migrationBuilder.DropForeignKey(
                name: "fk_role_division_division_division_id",
                table: "role_division");

            migrationBuilder.DropForeignKey(
                name: "fk_sales_period_outlet_outlet_id",
                table: "sales_period");

            migrationBuilder.DropForeignKey(
                name: "fk_section_outlet_outlet_id",
                table: "section");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_stock_category_stock_category_id",
                table: "stock");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_unit_unit_id",
                table: "stock");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_division_from_division_id",
                table: "stock_allocate");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_division_to_division_id",
                table: "stock_allocate");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_stock_allocate_status_stock_allocate_status_",
                table: "stock_allocate");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_user_assigned_user_user_id",
                table: "stock_allocate");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_user_from_user_user_id",
                table: "stock_allocate");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_item_division_division_id",
                table: "stock_allocate_item");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_item_stock_allocate_item_status_stock_alloca",
                table: "stock_allocate_item");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_item_stock_allocate_stock_allocate_id",
                table: "stock_allocate_item");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_item_stock_stock_id",
                table: "stock_allocate_item");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_item_division_division_id",
                table: "stock_item");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_item_stock_stock_id",
                table: "stock_item");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_item_audit_stock_item_audit_type_stock_item_audit_typ",
                table: "stock_item_audit");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_order_division_division_id",
                table: "stock_order");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_order_stock_order_status_stock_order_status_id",
                table: "stock_order");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_order_supplier_supplier_id",
                table: "stock_order");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_order_item_stock_order_item_status_stock_order_item_s",
                table: "stock_order_item");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_order_item_stock_order_stock_order_id",
                table: "stock_order_item");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_order_item_stock_stock_id",
                table: "stock_order_item");

            migrationBuilder.DropForeignKey(
                name: "fk_table_section_section_id",
                table: "table");

            migrationBuilder.DropForeignKey(
                name: "fk_table_booking_cash_up_user_cash_up_user_id",
                table: "table_booking");

            migrationBuilder.DropForeignKey(
                name: "fk_table_booking_sales_period_sales_period_id",
                table: "table_booking");

            migrationBuilder.DropForeignKey(
                name: "fk_table_booking_table_table_id",
                table: "table_booking");

            migrationBuilder.DropForeignKey(
                name: "fk_table_booking_user_user_id",
                table: "table_booking");

            migrationBuilder.DropForeignKey(
                name: "fk_tag_menu_item_menu_item_id",
                table: "tag");

            migrationBuilder.DropForeignKey(
                name: "fk_user_role_outlet_outlet_outlet_id",
                table: "user_role_outlet");

            migrationBuilder.DropForeignKey(
                name: "fk_user_role_outlet_role_role_id",
                table: "user_role_outlet");

            migrationBuilder.DropForeignKey(
                name: "fk_user_role_outlet_user_user_id",
                table: "user_role_outlet");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "pk_unit",
                table: "unit");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tag",
                table: "tag");

            migrationBuilder.DropPrimaryKey(
                name: "pk_table",
                table: "table");

            migrationBuilder.DropPrimaryKey(
                name: "pk_supplier",
                table: "supplier");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock",
                table: "stock");

            migrationBuilder.DropPrimaryKey(
                name: "pk_section",
                table: "section");

            migrationBuilder.DropPrimaryKey(
                name: "pk_role",
                table: "role");

            migrationBuilder.DropPrimaryKey(
                name: "pk_printer",
                table: "printer");

            migrationBuilder.DropPrimaryKey(
                name: "pk_payment",
                table: "payment");

            migrationBuilder.DropPrimaryKey(
                name: "pk_outlet",
                table: "outlet");

            migrationBuilder.DropPrimaryKey(
                name: "pk_option",
                table: "option");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menu",
                table: "menu");

            migrationBuilder.DropPrimaryKey(
                name: "pk_extra",
                table: "extra");

            migrationBuilder.DropPrimaryKey(
                name: "pk_division",
                table: "division");

            migrationBuilder.DropPrimaryKey(
                name: "pk_customer",
                table: "customer");

            migrationBuilder.DropPrimaryKey(
                name: "pk_clock",
                table: "clock");

            migrationBuilder.DropPrimaryKey(
                name: "pk_business",
                table: "business");

            migrationBuilder.DropPrimaryKey(
                name: "pk_adjustment",
                table: "adjustment");

            migrationBuilder.DropPrimaryKey(
                name: "pk_address",
                table: "address");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_role_outlet",
                table: "user_role_outlet");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_outlet",
                table: "user_outlet");

            migrationBuilder.DropPrimaryKey(
                name: "pk_table_booking",
                table: "table_booking");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock_order_status",
                table: "stock_order_status");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock_order_item_status",
                table: "stock_order_item_status");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock_order_item",
                table: "stock_order_item");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock_order",
                table: "stock_order");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock_item_audit_type",
                table: "stock_item_audit_type");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock_item_audit",
                table: "stock_item_audit");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock_item",
                table: "stock_item");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock_category",
                table: "stock_category");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock_allocate_status",
                table: "stock_allocate_status");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock_allocate_item_status",
                table: "stock_allocate_item_status");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock_allocate_item",
                table: "stock_allocate_item");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stock_allocate",
                table: "stock_allocate");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sales_period",
                table: "sales_period");

            migrationBuilder.DropPrimaryKey(
                name: "pk_role_division",
                table: "role_division");

            migrationBuilder.DropPrimaryKey(
                name: "pk_payment_type",
                table: "payment_type");

            migrationBuilder.DropPrimaryKey(
                name: "pk_outlet_payment_type",
                table: "outlet_payment_type");

            migrationBuilder.DropPrimaryKey(
                name: "pk_outlet_extra_group",
                table: "outlet_extra_group");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_item_status_audit",
                table: "order_item_status_audit");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_item_status",
                table: "order_item_status");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_item_option",
                table: "order_item_option");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_item_extra",
                table: "order_item_extra");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_item",
                table: "order_item");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_group",
                table: "order_group");

            migrationBuilder.DropPrimaryKey(
                name: "pk_option_stock",
                table: "option_stock");

            migrationBuilder.DropPrimaryKey(
                name: "pk_option_group",
                table: "option_group");

            migrationBuilder.DropPrimaryKey(
                name: "pk_notification_user",
                table: "notification_user");

            migrationBuilder.DropPrimaryKey(
                name: "pk_notification_log",
                table: "notification_log");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menu_section",
                table: "menu_section");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menu_item_stock",
                table: "menu_item_stock");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menu_item_option_group",
                table: "menu_item_option_group");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menu_item_extra_group",
                table: "menu_item_extra_group");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menu_item",
                table: "menu_item");

            migrationBuilder.DropPrimaryKey(
                name: "pk_halo_reference",
                table: "halo_reference");

            migrationBuilder.DropPrimaryKey(
                name: "pk_halo_log",
                table: "halo_log");

            migrationBuilder.DropPrimaryKey(
                name: "pk_halo_config",
                table: "halo_config");

            migrationBuilder.DropPrimaryKey(
                name: "pk_extra_stock",
                table: "extra_stock");

            migrationBuilder.DropPrimaryKey(
                name: "pk_extra_group",
                table: "extra_group");

            migrationBuilder.DropPrimaryKey(
                name: "pk_email_log",
                table: "email_log");

            migrationBuilder.DropPrimaryKey(
                name: "pk_division_type",
                table: "division_type");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cash_up_user_item_type",
                table: "cash_up_user_item_type");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cash_up_user_item",
                table: "cash_up_user_item");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cash_up_user",
                table: "cash_up_user");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cash_up_config",
                table: "cash_up_config");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cash_up",
                table: "cash_up");

            migrationBuilder.DropPrimaryKey(
                name: "pk_adjustment_type_outlet",
                table: "adjustment_type_outlet");

            migrationBuilder.DropPrimaryKey(
                name: "pk_adjustment_type",
                table: "adjustment_type");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "unit",
                newName: "Unit");

            migrationBuilder.RenameTable(
                name: "tag",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "table",
                newName: "Table");

            migrationBuilder.RenameTable(
                name: "supplier",
                newName: "Supplier");

            migrationBuilder.RenameTable(
                name: "stock",
                newName: "Stock");

            migrationBuilder.RenameTable(
                name: "section",
                newName: "Section");

            migrationBuilder.RenameTable(
                name: "role",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "printer",
                newName: "Printer");

            migrationBuilder.RenameTable(
                name: "payment",
                newName: "Payment");

            migrationBuilder.RenameTable(
                name: "outlet",
                newName: "Outlet");

            migrationBuilder.RenameTable(
                name: "option",
                newName: "Option");

            migrationBuilder.RenameTable(
                name: "menu",
                newName: "Menu");

            migrationBuilder.RenameTable(
                name: "extra",
                newName: "Extra");

            migrationBuilder.RenameTable(
                name: "division",
                newName: "Division");

            migrationBuilder.RenameTable(
                name: "customer",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "clock",
                newName: "Clock");

            migrationBuilder.RenameTable(
                name: "business",
                newName: "Business");

            migrationBuilder.RenameTable(
                name: "adjustment",
                newName: "Adjustment");

            migrationBuilder.RenameTable(
                name: "address",
                newName: "Address");

            migrationBuilder.RenameTable(
                name: "user_role_outlet",
                newName: "UserRoleOutlet");

            migrationBuilder.RenameTable(
                name: "user_outlet",
                newName: "UserOutlet");

            migrationBuilder.RenameTable(
                name: "table_booking",
                newName: "TableBooking");

            migrationBuilder.RenameTable(
                name: "stock_order_status",
                newName: "StockOrderStatus");

            migrationBuilder.RenameTable(
                name: "stock_order_item_status",
                newName: "StockOrderItemStatus");

            migrationBuilder.RenameTable(
                name: "stock_order_item",
                newName: "StockOrderItem");

            migrationBuilder.RenameTable(
                name: "stock_order",
                newName: "StockOrder");

            migrationBuilder.RenameTable(
                name: "stock_item_audit_type",
                newName: "StockItemAuditType");

            migrationBuilder.RenameTable(
                name: "stock_item_audit",
                newName: "StockItemAudit");

            migrationBuilder.RenameTable(
                name: "stock_item",
                newName: "StockItem");

            migrationBuilder.RenameTable(
                name: "stock_category",
                newName: "StockCategory");

            migrationBuilder.RenameTable(
                name: "stock_allocate_status",
                newName: "StockAllocateStatus");

            migrationBuilder.RenameTable(
                name: "stock_allocate_item_status",
                newName: "StockAllocateItemStatus");

            migrationBuilder.RenameTable(
                name: "stock_allocate_item",
                newName: "StockAllocateItem");

            migrationBuilder.RenameTable(
                name: "stock_allocate",
                newName: "StockAllocate");

            migrationBuilder.RenameTable(
                name: "sales_period",
                newName: "SalesPeriod");

            migrationBuilder.RenameTable(
                name: "role_division",
                newName: "RoleDivision");

            migrationBuilder.RenameTable(
                name: "payment_type",
                newName: "PaymentType");

            migrationBuilder.RenameTable(
                name: "outlet_payment_type",
                newName: "OutletPaymentType");

            migrationBuilder.RenameTable(
                name: "outlet_extra_group",
                newName: "OutletExtraGroup");

            migrationBuilder.RenameTable(
                name: "order_item_status_audit",
                newName: "OrderItemStatusAudit");

            migrationBuilder.RenameTable(
                name: "order_item_status",
                newName: "OrderItemStatus");

            migrationBuilder.RenameTable(
                name: "order_item_option",
                newName: "OrderItemOption");

            migrationBuilder.RenameTable(
                name: "order_item_extra",
                newName: "OrderItemExtra");

            migrationBuilder.RenameTable(
                name: "order_item",
                newName: "OrderItem");

            migrationBuilder.RenameTable(
                name: "order_group",
                newName: "OrderGroup");

            migrationBuilder.RenameTable(
                name: "option_stock",
                newName: "OptionStock");

            migrationBuilder.RenameTable(
                name: "option_group",
                newName: "OptionGroup");

            migrationBuilder.RenameTable(
                name: "notification_user",
                newName: "NotificationUser");

            migrationBuilder.RenameTable(
                name: "notification_log",
                newName: "NotificationLog");

            migrationBuilder.RenameTable(
                name: "menu_section",
                newName: "MenuSection");

            migrationBuilder.RenameTable(
                name: "menu_item_stock",
                newName: "MenuItemStock");

            migrationBuilder.RenameTable(
                name: "menu_item_option_group",
                newName: "MenuItemOptionGroup");

            migrationBuilder.RenameTable(
                name: "menu_item_extra_group",
                newName: "MenuItemExtraGroup");

            migrationBuilder.RenameTable(
                name: "menu_item",
                newName: "MenuItem");

            migrationBuilder.RenameTable(
                name: "halo_reference",
                newName: "HaloReference");

            migrationBuilder.RenameTable(
                name: "halo_log",
                newName: "HaloLog");

            migrationBuilder.RenameTable(
                name: "halo_config",
                newName: "HaloConfig");

            migrationBuilder.RenameTable(
                name: "extra_stock",
                newName: "ExtraStock");

            migrationBuilder.RenameTable(
                name: "extra_group",
                newName: "ExtraGroup");

            migrationBuilder.RenameTable(
                name: "email_log",
                newName: "EmailLog");

            migrationBuilder.RenameTable(
                name: "division_type",
                newName: "DivisionType");

            migrationBuilder.RenameTable(
                name: "cash_up_user_item_type",
                newName: "CashUpUserItemType");

            migrationBuilder.RenameTable(
                name: "cash_up_user_item",
                newName: "CashUpUserItem");

            migrationBuilder.RenameTable(
                name: "cash_up_user",
                newName: "CashUpUser");

            migrationBuilder.RenameTable(
                name: "cash_up_config",
                newName: "CashUpConfig");

            migrationBuilder.RenameTable(
                name: "cash_up",
                newName: "CashUp");

            migrationBuilder.RenameTable(
                name: "adjustment_type_outlet",
                newName: "AdjustmentTypeOutlet");

            migrationBuilder.RenameTable(
                name: "adjustment_type",
                newName: "AdjustmentType");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "User",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "User",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "User",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "User",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "User",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "User",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "User",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "User",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Unit",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Unit",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Tag",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "menu_item_id",
                table: "Tag",
                newName: "MenuItemId");

            migrationBuilder.RenameColumn(
                name: "tag_id",
                table: "Tag",
                newName: "TagId");

            migrationBuilder.RenameIndex(
                name: "ix_tag_menu_item_id",
                table: "Tag",
                newName: "IX_Tag_MenuItemId");

            migrationBuilder.RenameColumn(
                name: "position",
                table: "Table",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Table",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "capacity",
                table: "Table",
                newName: "Capacity");

            migrationBuilder.RenameColumn(
                name: "section_id",
                table: "Table",
                newName: "SectionId");

            migrationBuilder.RenameColumn(
                name: "table_id",
                table: "Table",
                newName: "TableId");

            migrationBuilder.RenameIndex(
                name: "ix_table_section_id",
                table: "Table",
                newName: "IX_Table_SectionId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Supplier",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Supplier",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Supplier",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "Supplier",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "contact_number",
                table: "Supplier",
                newName: "ContactNumber");

            migrationBuilder.RenameColumn(
                name: "contact_name",
                table: "Supplier",
                newName: "ContactName");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Stock",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Stock",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Stock",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "unit_id",
                table: "Stock",
                newName: "UnitId");

            migrationBuilder.RenameColumn(
                name: "stock_category_id",
                table: "Stock",
                newName: "StockCategoryId");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "Stock",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "Stock",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "Stock",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "Stock",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "ix_stock_unit_id",
                table: "Stock",
                newName: "IX_Stock_UnitId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_stock_category_id",
                table: "Stock",
                newName: "IX_Stock_StockCategoryId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Section",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Section",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "Section",
                newName: "OutletId");

            migrationBuilder.RenameIndex(
                name: "ix_section_outlet_id",
                table: "Section",
                newName: "IX_Section_OutletId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Role",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Role",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "Role",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "is_front_line",
                table: "Role",
                newName: "isFrontLine");

            migrationBuilder.RenameColumn(
                name: "is_back_office",
                table: "Role",
                newName: "isBackOffice");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "Role",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "port",
                table: "Printer",
                newName: "Port");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Printer",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Printer",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "printer_name",
                table: "Printer",
                newName: "PrinterName");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "Printer",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "line_characters",
                table: "Printer",
                newName: "LineCharacters");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "Printer",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "Printer",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "is_enabled",
                table: "Printer",
                newName: "IsEnabled");

            migrationBuilder.RenameColumn(
                name: "ip_address",
                table: "Printer",
                newName: "IPAddress");

            migrationBuilder.RenameColumn(
                name: "device_id",
                table: "Printer",
                newName: "DeviceId");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "Printer",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Payment",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Payment",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Payment",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "table_booking_id",
                table: "Payment",
                newName: "TableBookingId");

            migrationBuilder.RenameColumn(
                name: "payment_type_id",
                table: "Payment",
                newName: "PaymentTypeId");

            migrationBuilder.RenameColumn(
                name: "payment_reference",
                table: "Payment",
                newName: "PaymentReference");

            migrationBuilder.RenameColumn(
                name: "date_received",
                table: "Payment",
                newName: "DateReceived");

            migrationBuilder.RenameIndex(
                name: "ix_payment_table_booking_id",
                table: "Payment",
                newName: "IX_Payment_TableBookingId");

            migrationBuilder.RenameIndex(
                name: "ix_payment_payment_type_id",
                table: "Payment",
                newName: "IX_Payment_PaymentTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_payment_payment_reference",
                table: "Payment",
                newName: "IX_Payment_PaymentReference");

            migrationBuilder.RenameColumn(
                name: "registration",
                table: "Outlet",
                newName: "Registration");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Outlet",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "logo",
                table: "Outlet",
                newName: "Logo");

            migrationBuilder.RenameColumn(
                name: "company",
                table: "Outlet",
                newName: "Company");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Outlet",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Outlet",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "vat_number",
                table: "Outlet",
                newName: "VATNumber");

            migrationBuilder.RenameColumn(
                name: "business_id",
                table: "Outlet",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "ix_outlet_business_id",
                table: "Outlet",
                newName: "IX_Outlet_BusinessId");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Option",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Option",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "position_id",
                table: "Option",
                newName: "PositionId");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "Option",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "option_group_id",
                table: "Option",
                newName: "OptionGroupId");

            migrationBuilder.RenameColumn(
                name: "option_id",
                table: "Option",
                newName: "OptionId");

            migrationBuilder.RenameIndex(
                name: "ix_option_option_group_id",
                table: "Option",
                newName: "IX_Option_OptionGroupId");

            migrationBuilder.RenameColumn(
                name: "position",
                table: "Menu",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Menu",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Menu",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Menu",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "Menu",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "Menu",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "Menu",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "Menu",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "ix_menu_outlet_id",
                table: "Menu",
                newName: "IX_Menu_OutletId");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Extra",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Extra",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "position_id",
                table: "Extra",
                newName: "PositionId");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "Extra",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "extra_group_id",
                table: "Extra",
                newName: "ExtraGroupId");

            migrationBuilder.RenameColumn(
                name: "extra_id",
                table: "Extra",
                newName: "ExtraId");

            migrationBuilder.RenameIndex(
                name: "ix_extra_extra_group_id",
                table: "Extra",
                newName: "IX_Extra_ExtraGroupId");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "Division",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "division_type_id",
                table: "Division",
                newName: "DivisionTypeId");

            migrationBuilder.RenameColumn(
                name: "division_name",
                table: "Division",
                newName: "DivisionName");

            migrationBuilder.RenameColumn(
                name: "division_id",
                table: "Division",
                newName: "DivisionId");

            migrationBuilder.RenameColumn(
                name: "orders",
                table: "Customer",
                newName: "Orders");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Customer",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "table_id",
                table: "Customer",
                newName: "TableId");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "Customer",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "ix_customer_table_id",
                table: "Customer",
                newName: "IX_Customer_TableId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Clock",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Clock",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "Clock",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "Clock",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "end_date",
                table: "Clock",
                newName: "EndDate");

            migrationBuilder.RenameIndex(
                name: "ix_clock_user_id",
                table: "Clock",
                newName: "IX_Clock_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_clock_outlet_id",
                table: "Clock",
                newName: "IX_Clock_OutletId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Business",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Business",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "note",
                table: "Adjustment",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Adjustment",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Adjustment",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Adjustment",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "table_booking_id",
                table: "Adjustment",
                newName: "TableBookingId");

            migrationBuilder.RenameColumn(
                name: "adjustment_type_id",
                table: "Adjustment",
                newName: "AdjustmentTypeId");

            migrationBuilder.RenameColumn(
                name: "adjustment_id",
                table: "Adjustment",
                newName: "AdjustmentId");

            migrationBuilder.RenameIndex(
                name: "ix_adjustment_table_booking_id",
                table: "Adjustment",
                newName: "IX_Adjustment_TableBookingId");

            migrationBuilder.RenameIndex(
                name: "ix_adjustment_adjustment_type_id",
                table: "Adjustment",
                newName: "IX_Adjustment_AdjustmentTypeId");

            migrationBuilder.RenameColumn(
                name: "suburb",
                table: "Address",
                newName: "Suburb");

            migrationBuilder.RenameColumn(
                name: "province",
                table: "Address",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Address",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Address",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "street_name",
                table: "Address",
                newName: "StreetName");

            migrationBuilder.RenameColumn(
                name: "postal_code",
                table: "Address",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "house_nr",
                table: "Address",
                newName: "HouseNr");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "UserRoleOutlet",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserRoleOutlet",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "UserRoleOutlet",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "UserRoleOutlet",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "UserRoleOutlet",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "UserRoleOutlet",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "UserRoleOutlet",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "UserRoleOutlet",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "ix_user_role_outlet_user_id",
                table: "UserRoleOutlet",
                newName: "IX_UserRoleOutlet_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_user_role_outlet_role_id",
                table: "UserRoleOutlet",
                newName: "IX_UserRoleOutlet_RoleId");

            migrationBuilder.RenameIndex(
                name: "ix_user_role_outlet_outlet_id",
                table: "UserRoleOutlet",
                newName: "IX_UserRoleOutlet_OutletId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserOutlet",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "UserOutlet",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "UserOutlet",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "is_current",
                table: "UserOutlet",
                newName: "IsCurrent");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "TableBooking",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TableBooking",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "TableBooking",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "total_tips",
                table: "TableBooking",
                newName: "TotalTips");

            migrationBuilder.RenameColumn(
                name: "total_payments",
                table: "TableBooking",
                newName: "TotalPayments");

            migrationBuilder.RenameColumn(
                name: "table_id",
                table: "TableBooking",
                newName: "TableId");

            migrationBuilder.RenameColumn(
                name: "sales_period_id",
                table: "TableBooking",
                newName: "SalesPeriodId");

            migrationBuilder.RenameColumn(
                name: "close_date",
                table: "TableBooking",
                newName: "CloseDate");

            migrationBuilder.RenameColumn(
                name: "cash_up_user_id",
                table: "TableBooking",
                newName: "CashUpUserId");

            migrationBuilder.RenameColumn(
                name: "booking_name",
                table: "TableBooking",
                newName: "BookingName");

            migrationBuilder.RenameColumn(
                name: "booking_date",
                table: "TableBooking",
                newName: "BookingDate");

            migrationBuilder.RenameIndex(
                name: "ix_table_booking_user_id",
                table: "TableBooking",
                newName: "IX_TableBooking_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_table_booking_table_id",
                table: "TableBooking",
                newName: "IX_TableBooking_TableId");

            migrationBuilder.RenameIndex(
                name: "ix_table_booking_sales_period_id",
                table: "TableBooking",
                newName: "IX_TableBooking_SalesPeriodId");

            migrationBuilder.RenameIndex(
                name: "ix_table_booking_cash_up_user_id",
                table: "TableBooking",
                newName: "IX_TableBooking_CashUpUserId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "StockOrderStatus",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "StockOrderStatus",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StockOrderStatus",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "StockOrderStatus",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "StockOrderStatus",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "StockOrderStatus",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "StockOrderItemStatus",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "StockOrderItemStatus",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StockOrderItemStatus",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "StockOrderItemStatus",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "StockOrderItemStatus",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "StockOrderItemStatus",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "StockOrderItem",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "StockOrderItem",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "actual",
                table: "StockOrderItem",
                newName: "Actual");

            migrationBuilder.RenameColumn(
                name: "stock_order_item_status_id",
                table: "StockOrderItem",
                newName: "StockOrderItemStatusId");

            migrationBuilder.RenameColumn(
                name: "order_amount",
                table: "StockOrderItem",
                newName: "OrderAmount");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "StockOrderItem",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "StockOrderItem",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "StockOrderItem",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "stock_id",
                table: "StockOrderItem",
                newName: "StockId");

            migrationBuilder.RenameColumn(
                name: "stock_order_id",
                table: "StockOrderItem",
                newName: "StockOrderId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_order_item_stock_order_item_status_id",
                table: "StockOrderItem",
                newName: "IX_StockOrderItem_StockOrderItemStatusId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_order_item_stock_id",
                table: "StockOrderItem",
                newName: "IX_StockOrderItem_StockId");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "StockOrder",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StockOrder",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "supplier_id",
                table: "StockOrder",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "stock_order_status_id",
                table: "StockOrder",
                newName: "StockOrderStatusId");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "StockOrder",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "order_number",
                table: "StockOrder",
                newName: "OrderNumber");

            migrationBuilder.RenameColumn(
                name: "order_date",
                table: "StockOrder",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "StockOrder",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "StockOrder",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "division_id",
                table: "StockOrder",
                newName: "DivisionId");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "StockOrder",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "ix_stock_order_supplier_id",
                table: "StockOrder",
                newName: "IX_StockOrder_SupplierId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_order_stock_order_status_id",
                table: "StockOrder",
                newName: "IX_StockOrder_StockOrderStatusId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_order_division_id",
                table: "StockOrder",
                newName: "IX_StockOrder_DivisionId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "StockItemAuditType",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StockItemAuditType",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated",
                table: "StockItemAudit",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StockItemAudit",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "StockItemAudit",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "to_actual",
                table: "StockItemAudit",
                newName: "ToActual");

            migrationBuilder.RenameColumn(
                name: "stock_item_id",
                table: "StockItemAudit",
                newName: "StockItemId");

            migrationBuilder.RenameColumn(
                name: "stock_item_audit_type_id",
                table: "StockItemAudit",
                newName: "StockItemAuditTypeId");

            migrationBuilder.RenameColumn(
                name: "order_item_id",
                table: "StockItemAudit",
                newName: "OrderItemId");

            migrationBuilder.RenameColumn(
                name: "from_actual",
                table: "StockItemAudit",
                newName: "FromActual");

            migrationBuilder.RenameIndex(
                name: "ix_stock_item_audit_stock_item_audit_type_id",
                table: "StockItemAudit",
                newName: "IX_StockItemAudit_StockItemAuditTypeId");

            migrationBuilder.RenameColumn(
                name: "threshold",
                table: "StockItem",
                newName: "Threshold");

            migrationBuilder.RenameColumn(
                name: "actual",
                table: "StockItem",
                newName: "Actual");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StockItem",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "stock_id",
                table: "StockItem",
                newName: "StockId");

            migrationBuilder.RenameColumn(
                name: "division_id",
                table: "StockItem",
                newName: "DivisionId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_item_stock_id",
                table: "StockItem",
                newName: "IX_StockItem_StockId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_item_division_id",
                table: "StockItem",
                newName: "IX_StockItem_DivisionId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "StockCategory",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StockCategory",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "StockAllocateStatus",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "StockAllocateStatus",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StockAllocateStatus",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "StockAllocateStatus",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "StockAllocateStatus",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "StockAllocateStatus",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "StockAllocateItemStatus",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "StockAllocateItemStatus",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StockAllocateItemStatus",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "StockAllocateItemStatus",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "StockAllocateItemStatus",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "StockAllocateItemStatus",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "StockAllocateItem",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "completed",
                table: "StockAllocateItem",
                newName: "Completed");

            migrationBuilder.RenameColumn(
                name: "actual",
                table: "StockAllocateItem",
                newName: "Actual");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StockAllocateItem",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "stock_id",
                table: "StockAllocateItem",
                newName: "StockId");

            migrationBuilder.RenameColumn(
                name: "stock_allocate_item_status_id",
                table: "StockAllocateItem",
                newName: "StockAllocateItemStatusId");

            migrationBuilder.RenameColumn(
                name: "stock_allocate_id",
                table: "StockAllocateItem",
                newName: "StockAllocateId");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "StockAllocateItem",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "StockAllocateItem",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "division_id",
                table: "StockAllocateItem",
                newName: "DivisionId");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "StockAllocateItem",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "allocate_amount",
                table: "StockAllocateItem",
                newName: "AllocateAmount");

            migrationBuilder.RenameIndex(
                name: "ix_stock_allocate_item_stock_id",
                table: "StockAllocateItem",
                newName: "IX_StockAllocateItem_StockId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_allocate_item_stock_allocate_item_status_id",
                table: "StockAllocateItem",
                newName: "IX_StockAllocateItem_StockAllocateItemStatusId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_allocate_item_stock_allocate_id",
                table: "StockAllocateItem",
                newName: "IX_StockAllocateItem_StockAllocateId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_allocate_item_division_id",
                table: "StockAllocateItem",
                newName: "IX_StockAllocateItem_DivisionId");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "StockAllocate",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "completed",
                table: "StockAllocate",
                newName: "Completed");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "StockAllocate",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StockAllocate",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "to_division_id",
                table: "StockAllocate",
                newName: "ToDivisionId");

            migrationBuilder.RenameColumn(
                name: "stock_allocate_status_id",
                table: "StockAllocate",
                newName: "StockAllocateStatusId");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "StockAllocate",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "from_user_user_id",
                table: "StockAllocate",
                newName: "FromUserUserId");

            migrationBuilder.RenameColumn(
                name: "from_user_id",
                table: "StockAllocate",
                newName: "FromUserId");

            migrationBuilder.RenameColumn(
                name: "from_division_id",
                table: "StockAllocate",
                newName: "FromDivisionId");

            migrationBuilder.RenameColumn(
                name: "assigned_user_user_id",
                table: "StockAllocate",
                newName: "AssignedUserUserId");

            migrationBuilder.RenameColumn(
                name: "assigned_user_id",
                table: "StockAllocate",
                newName: "AssignedUserId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_allocate_to_division_id",
                table: "StockAllocate",
                newName: "IX_StockAllocate_ToDivisionId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_allocate_stock_allocate_status_id",
                table: "StockAllocate",
                newName: "IX_StockAllocate_StockAllocateStatusId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_allocate_from_user_user_id",
                table: "StockAllocate",
                newName: "IX_StockAllocate_FromUserUserId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_allocate_from_division_id",
                table: "StockAllocate",
                newName: "IX_StockAllocate_FromDivisionId");

            migrationBuilder.RenameIndex(
                name: "ix_stock_allocate_assigned_user_user_id",
                table: "StockAllocate",
                newName: "IX_StockAllocate_AssignedUserUserId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "SalesPeriod",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SalesPeriod",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "SalesPeriod",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "SalesPeriod",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "end_date",
                table: "SalesPeriod",
                newName: "EndDate");

            migrationBuilder.RenameIndex(
                name: "ix_sales_period_outlet_id",
                table: "SalesPeriod",
                newName: "IX_SalesPeriod_OutletId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RoleDivision",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "RoleDivision",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "division_id",
                table: "RoleDivision",
                newName: "DivisionId");

            migrationBuilder.RenameIndex(
                name: "ix_role_division_division_id",
                table: "RoleDivision",
                newName: "IX_RoleDivision_DivisionId");

            migrationBuilder.RenameColumn(
                name: "tip_levy_percentage",
                table: "PaymentType",
                newName: "TipLevyPercentage");

            migrationBuilder.RenameColumn(
                name: "payment_type_name",
                table: "PaymentType",
                newName: "PaymentTypeName");

            migrationBuilder.RenameColumn(
                name: "discount_percentage",
                table: "PaymentType",
                newName: "DiscountPercentage");

            migrationBuilder.RenameColumn(
                name: "can_edit",
                table: "PaymentType",
                newName: "CanEdit");

            migrationBuilder.RenameColumn(
                name: "payment_type_id",
                table: "PaymentType",
                newName: "PaymentTypeId");

            migrationBuilder.RenameColumn(
                name: "position",
                table: "OutletPaymentType",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "payment_type_id",
                table: "OutletPaymentType",
                newName: "PaymentTypeId");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "OutletPaymentType",
                newName: "OutletId");

            migrationBuilder.RenameIndex(
                name: "ix_outlet_payment_type_payment_type_id",
                table: "OutletPaymentType",
                newName: "IX_OutletPaymentType_PaymentTypeId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OutletExtraGroup",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "OutletExtraGroup",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "extra_group_id",
                table: "OutletExtraGroup",
                newName: "ExtraGroupId");

            migrationBuilder.RenameIndex(
                name: "ix_outlet_extra_group_outlet_id",
                table: "OutletExtraGroup",
                newName: "IX_OutletExtraGroup_OutletId");

            migrationBuilder.RenameIndex(
                name: "ix_outlet_extra_group_extra_group_id",
                table: "OutletExtraGroup",
                newName: "IX_OutletExtraGroup_ExtraGroupId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OrderItemStatusAudit",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "OrderItemStatusAudit",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "status_date",
                table: "OrderItemStatusAudit",
                newName: "StatusDate");

            migrationBuilder.RenameColumn(
                name: "order_item_status_id",
                table: "OrderItemStatusAudit",
                newName: "OrderItemStatusId");

            migrationBuilder.RenameColumn(
                name: "order_item_id",
                table: "OrderItemStatusAudit",
                newName: "OrderItemId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "OrderItemStatus",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "priority",
                table: "OrderItemStatus",
                newName: "Priority");

            migrationBuilder.RenameColumn(
                name: "notify",
                table: "OrderItemStatus",
                newName: "Notify");

            migrationBuilder.RenameColumn(
                name: "is_history",
                table: "OrderItemStatus",
                newName: "isHistory");

            migrationBuilder.RenameColumn(
                name: "is_front_line",
                table: "OrderItemStatus",
                newName: "isFrontLine");

            migrationBuilder.RenameColumn(
                name: "is_complete",
                table: "OrderItemStatus",
                newName: "isComplete");

            migrationBuilder.RenameColumn(
                name: "is_cancelled",
                table: "OrderItemStatus",
                newName: "isCancelled");

            migrationBuilder.RenameColumn(
                name: "is_billable",
                table: "OrderItemStatus",
                newName: "isBillable");

            migrationBuilder.RenameColumn(
                name: "is_back_office",
                table: "OrderItemStatus",
                newName: "isBackOffice");

            migrationBuilder.RenameColumn(
                name: "assign_group",
                table: "OrderItemStatus",
                newName: "assignGroup");

            migrationBuilder.RenameColumn(
                name: "order_item_status_id",
                table: "OrderItemStatus",
                newName: "OrderItemStatusId");

            migrationBuilder.RenameColumn(
                name: "order_item_id",
                table: "OrderItemOption",
                newName: "OrderItemId");

            migrationBuilder.RenameColumn(
                name: "option_id",
                table: "OrderItemOption",
                newName: "OptionId");

            migrationBuilder.RenameColumn(
                name: "order_item_option_id",
                table: "OrderItemOption",
                newName: "OrderItemOptionId");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_option_order_item_id",
                table: "OrderItemOption",
                newName: "IX_OrderItemOption_OrderItemId");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_option_option_id",
                table: "OrderItemOption",
                newName: "IX_OrderItemOption_OptionId");

            migrationBuilder.RenameColumn(
                name: "order_item_id",
                table: "OrderItemExtra",
                newName: "OrderItemId");

            migrationBuilder.RenameColumn(
                name: "extra_id",
                table: "OrderItemExtra",
                newName: "ExtraId");

            migrationBuilder.RenameColumn(
                name: "order_item_extra_id",
                table: "OrderItemExtra",
                newName: "OrderItemExtraId");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_extra_order_item_id",
                table: "OrderItemExtra",
                newName: "IX_OrderItemExtra_OrderItemId");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_extra_extra_id",
                table: "OrderItemExtra",
                newName: "IX_OrderItemExtra_ExtraId");

            migrationBuilder.RenameColumn(
                name: "note",
                table: "OrderItem",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "table_booking_id",
                table: "OrderItem",
                newName: "TableBookingId");

            migrationBuilder.RenameColumn(
                name: "order_updated",
                table: "OrderItem",
                newName: "OrderUpdated");

            migrationBuilder.RenameColumn(
                name: "order_received",
                table: "OrderItem",
                newName: "OrderReceived");

            migrationBuilder.RenameColumn(
                name: "order_item_status_id",
                table: "OrderItem",
                newName: "OrderItemStatusId");

            migrationBuilder.RenameColumn(
                name: "order_group_id",
                table: "OrderItem",
                newName: "OrderGroupId");

            migrationBuilder.RenameColumn(
                name: "order_completed",
                table: "OrderItem",
                newName: "OrderCompleted");

            migrationBuilder.RenameColumn(
                name: "menu_item_id",
                table: "OrderItem",
                newName: "MenuItemId");

            migrationBuilder.RenameColumn(
                name: "order_item_id",
                table: "OrderItem",
                newName: "OrderItemId");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_table_booking_id",
                table: "OrderItem",
                newName: "IX_OrderItem_TableBookingId");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_order_item_status_id",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderItemStatusId");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_order_group_id",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderGroupId");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_menu_item_id",
                table: "OrderItem",
                newName: "IX_OrderItem_MenuItemId");

            migrationBuilder.RenameColumn(
                name: "order_group_id",
                table: "OrderGroup",
                newName: "OrderGroupId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "OptionStock",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "unit_id",
                table: "OptionStock",
                newName: "UnitId");

            migrationBuilder.RenameColumn(
                name: "stock_id",
                table: "OptionStock",
                newName: "StockId");

            migrationBuilder.RenameColumn(
                name: "option_id",
                table: "OptionStock",
                newName: "OptionId");

            migrationBuilder.RenameIndex(
                name: "ix_option_stock_unit_id",
                table: "OptionStock",
                newName: "IX_OptionStock_UnitId");

            migrationBuilder.RenameIndex(
                name: "ix_option_stock_stock_id",
                table: "OptionStock",
                newName: "IX_OptionStock_StockId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "OptionGroup",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "OptionGroup",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "min_selections",
                table: "OptionGroup",
                newName: "MinSelections");

            migrationBuilder.RenameColumn(
                name: "max_selections",
                table: "OptionGroup",
                newName: "MaxSelections");

            migrationBuilder.RenameColumn(
                name: "option_group_id",
                table: "OptionGroup",
                newName: "OptionGroupId");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "NotificationUser",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "date_inserted",
                table: "NotificationUser",
                newName: "DateInserted");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "NotificationUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "NotificationLog",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "payload",
                table: "NotificationLog",
                newName: "Payload");

            migrationBuilder.RenameColumn(
                name: "error",
                table: "NotificationLog",
                newName: "Error");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "NotificationLog",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "NotificationLog",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "is_success",
                table: "NotificationLog",
                newName: "IsSuccess");

            migrationBuilder.RenameColumn(
                name: "http_status_response",
                table: "NotificationLog",
                newName: "HttpStatusResponse");

            migrationBuilder.RenameColumn(
                name: "date_inserted",
                table: "NotificationLog",
                newName: "DateInserted");

            migrationBuilder.RenameColumn(
                name: "channel_id",
                table: "NotificationLog",
                newName: "ChannelId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "MenuSection",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "MenuSection",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "position_id",
                table: "MenuSection",
                newName: "PositionId");

            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "MenuSection",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "menu_id",
                table: "MenuSection",
                newName: "MenuId");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "MenuSection",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "MenuSection",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "MenuSection",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "menu_section_id",
                table: "MenuSection",
                newName: "MenuSectionId");

            migrationBuilder.RenameIndex(
                name: "ix_menu_section_parent_id",
                table: "MenuSection",
                newName: "IX_MenuSection_ParentId");

            migrationBuilder.RenameIndex(
                name: "ix_menu_section_menu_id",
                table: "MenuSection",
                newName: "IX_MenuSection_MenuId");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "MenuItemStock",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "stock_item_id",
                table: "MenuItemStock",
                newName: "StockItemId");

            migrationBuilder.RenameColumn(
                name: "menu_item_id",
                table: "MenuItemStock",
                newName: "MenuItemId");

            migrationBuilder.RenameIndex(
                name: "ix_menu_item_stock_stock_item_id",
                table: "MenuItemStock",
                newName: "IX_MenuItemStock_StockItemId");

            migrationBuilder.RenameColumn(
                name: "menu_item_id",
                table: "MenuItemOptionGroup",
                newName: "MenuItemId");

            migrationBuilder.RenameColumn(
                name: "option_group_id",
                table: "MenuItemOptionGroup",
                newName: "OptionGroupId");

            migrationBuilder.RenameIndex(
                name: "ix_menu_item_option_group_menu_item_id",
                table: "MenuItemOptionGroup",
                newName: "IX_MenuItemOptionGroup_MenuItemId");

            migrationBuilder.RenameColumn(
                name: "menu_item_id",
                table: "MenuItemExtraGroup",
                newName: "MenuItemId");

            migrationBuilder.RenameColumn(
                name: "extra_group_id",
                table: "MenuItemExtraGroup",
                newName: "ExtraGroupId");

            migrationBuilder.RenameIndex(
                name: "ix_menu_item_extra_group_menu_item_id",
                table: "MenuItemExtraGroup",
                newName: "IX_MenuItemExtraGroup_MenuItemId");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "MenuItem",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "position",
                table: "MenuItem",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "MenuItem",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "MenuItem",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "MenuItem",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "stock_price",
                table: "MenuItem",
                newName: "StockPrice");

            migrationBuilder.RenameColumn(
                name: "search_vector",
                table: "MenuItem",
                newName: "SearchVector");

            migrationBuilder.RenameColumn(
                name: "menu_section_id",
                table: "MenuItem",
                newName: "MenuSectionId");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "MenuItem",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "MenuItem",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "is_enabled",
                table: "MenuItem",
                newName: "IsEnabled");

            migrationBuilder.RenameColumn(
                name: "is_available",
                table: "MenuItem",
                newName: "IsAvailable");

            migrationBuilder.RenameColumn(
                name: "division_id",
                table: "MenuItem",
                newName: "DivisionId");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "MenuItem",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "menu_item_id",
                table: "MenuItem",
                newName: "MenuItemId");

            migrationBuilder.RenameIndex(
                name: "ix_menu_item_search_vector",
                table: "MenuItem",
                newName: "IX_MenuItem_SearchVector");

            migrationBuilder.RenameIndex(
                name: "ix_menu_item_menu_section_id",
                table: "MenuItem",
                newName: "IX_MenuItem_MenuSectionId");

            migrationBuilder.RenameIndex(
                name: "ix_menu_item_division_id",
                table: "MenuItem",
                newName: "IX_MenuItem_DivisionId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "HaloReference",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "HaloReference",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "table_booking_id",
                table: "HaloReference",
                newName: "TableBookingId");

            migrationBuilder.RenameColumn(
                name: "halo_ref",
                table: "HaloReference",
                newName: "HaloRef");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "HaloLog",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "response",
                table: "HaloLog",
                newName: "Response");

            migrationBuilder.RenameColumn(
                name: "request",
                table: "HaloLog",
                newName: "Request");

            migrationBuilder.RenameColumn(
                name: "error",
                table: "HaloLog",
                newName: "Error");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "HaloLog",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "HaloLog",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "status_code",
                table: "HaloLog",
                newName: "StatusCode");

            migrationBuilder.RenameColumn(
                name: "request_url",
                table: "HaloLog",
                newName: "RequestUrl");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "HaloLog",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "iv",
                table: "HaloConfig",
                newName: "Iv");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "HaloConfig",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "HaloConfig",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "x_api_key",
                table: "HaloConfig",
                newName: "XApiKey");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "HaloConfig",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "merchant_id",
                table: "HaloConfig",
                newName: "MerchantId");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "HaloConfig",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "HaloConfig",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "is_enabled",
                table: "HaloConfig",
                newName: "IsEnabled");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "HaloConfig",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "ExtraStock",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "unit_id",
                table: "ExtraStock",
                newName: "UnitId");

            migrationBuilder.RenameColumn(
                name: "stock_id",
                table: "ExtraStock",
                newName: "StockId");

            migrationBuilder.RenameColumn(
                name: "extra_id",
                table: "ExtraStock",
                newName: "ExtraId");

            migrationBuilder.RenameIndex(
                name: "ix_extra_stock_unit_id",
                table: "ExtraStock",
                newName: "IX_ExtraStock_UnitId");

            migrationBuilder.RenameIndex(
                name: "ix_extra_stock_stock_id",
                table: "ExtraStock",
                newName: "IX_ExtraStock_StockId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ExtraGroup",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "ExtraGroup",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "extra_group_id",
                table: "ExtraGroup",
                newName: "ExtraGroupId");

            migrationBuilder.RenameColumn(
                name: "subject",
                table: "EmailLog",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "message",
                table: "EmailLog",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "EmailLog",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "EmailLog",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "EmailLog",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "is_sent",
                table: "EmailLog",
                newName: "IsSent");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DivisionType",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "division_name",
                table: "DivisionType",
                newName: "DivisionName");

            migrationBuilder.RenameColumn(
                name: "position",
                table: "CashUpUserItemType",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CashUpUserItemType",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "payment_type_id",
                table: "CashUpUserItemType",
                newName: "PaymentTypeId");

            migrationBuilder.RenameColumn(
                name: "item_type",
                table: "CashUpUserItemType",
                newName: "ItemType");

            migrationBuilder.RenameColumn(
                name: "is_auto",
                table: "CashUpUserItemType",
                newName: "IsAuto");

            migrationBuilder.RenameColumn(
                name: "cashup_config_id",
                table: "CashUpUserItemType",
                newName: "CashupConfigId");

            migrationBuilder.RenameColumn(
                name: "cash_up_user_item_rule",
                table: "CashUpUserItemType",
                newName: "CashUpUserItemRule");

            migrationBuilder.RenameColumn(
                name: "affects_gross_balance",
                table: "CashUpUserItemType",
                newName: "AffectsGrossBalance");

            migrationBuilder.RenameColumn(
                name: "adjustment_type_id",
                table: "CashUpUserItemType",
                newName: "AdjustmentTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_cash_up_user_item_type_payment_type_id",
                table: "CashUpUserItemType",
                newName: "IX_CashUpUserItemType_PaymentTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_cash_up_user_item_type_cashup_config_id",
                table: "CashUpUserItemType",
                newName: "IX_CashUpUserItemType_CashupConfigId");

            migrationBuilder.RenameIndex(
                name: "ix_cash_up_user_item_type_adjustment_type_id",
                table: "CashUpUserItemType",
                newName: "IX_CashUpUserItemType_AdjustmentTypeId");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "CashUpUserItem",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CashUpUserItem",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "CashUpUserItem",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "CashUpUserItem",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "cash_up_user_item_type_id",
                table: "CashUpUserItem",
                newName: "CashUpUserItemTypeId");

            migrationBuilder.RenameColumn(
                name: "cash_up_user_id",
                table: "CashUpUserItem",
                newName: "CashUpUserId");

            migrationBuilder.RenameIndex(
                name: "ix_cash_up_user_item_cash_up_user_item_type_id",
                table: "CashUpUserItem",
                newName: "IX_CashUpUserItem_CashUpUserItemTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_cash_up_user_item_cash_up_user_id",
                table: "CashUpUserItem",
                newName: "IX_CashUpUserItem_CashUpUserId");

            migrationBuilder.RenameColumn(
                name: "tips",
                table: "CashUpUser",
                newName: "Tips");

            migrationBuilder.RenameColumn(
                name: "sales",
                table: "CashUpUser",
                newName: "Sales");

            migrationBuilder.RenameColumn(
                name: "payments",
                table: "CashUpUser",
                newName: "Payments");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CashUpUser",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "CashUpUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "sales_period_id",
                table: "CashUpUser",
                newName: "SalesPeriodId");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "CashUpUser",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "opening_balance",
                table: "CashUpUser",
                newName: "OpeningBalance");

            migrationBuilder.RenameColumn(
                name: "completer_user_id",
                table: "CashUpUser",
                newName: "CompleterUserId");

            migrationBuilder.RenameColumn(
                name: "closing_balance",
                table: "CashUpUser",
                newName: "ClosingBalance");

            migrationBuilder.RenameColumn(
                name: "cash_up_date",
                table: "CashUpUser",
                newName: "CashUpDate");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "CashUpConfig",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "CashUpConfig",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CashUpConfig",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "CashUpConfig",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CashUp",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "CashUp",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "table_count",
                table: "CashUp",
                newName: "TableCount");

            migrationBuilder.RenameColumn(
                name: "sign_off_user_id",
                table: "CashUp",
                newName: "SignOffUserId");

            migrationBuilder.RenameColumn(
                name: "sign_off_date",
                table: "CashUp",
                newName: "SignOffDate");

            migrationBuilder.RenameColumn(
                name: "sales_period_id",
                table: "CashUp",
                newName: "SalesPeriodId");

            migrationBuilder.RenameColumn(
                name: "open_table_count",
                table: "CashUp",
                newName: "OpenTableCount");

            migrationBuilder.RenameColumn(
                name: "cash_up_total_payments",
                table: "CashUp",
                newName: "CashUpTotalPayments");

            migrationBuilder.RenameColumn(
                name: "cash_up_total",
                table: "CashUp",
                newName: "CashUpTotal");

            migrationBuilder.RenameColumn(
                name: "cash_up_balance",
                table: "CashUp",
                newName: "CashUpBalance");

            migrationBuilder.RenameIndex(
                name: "ix_cash_up_sales_period_id",
                table: "CashUp",
                newName: "IX_CashUp_SalesPeriodId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AdjustmentTypeOutlet",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "AdjustmentTypeOutlet",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "adjustment_type_id",
                table: "AdjustmentTypeOutlet",
                newName: "AdjustmentTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_adjustment_type_outlet_outlet_id",
                table: "AdjustmentTypeOutlet",
                newName: "IX_AdjustmentTypeOutlet_OutletId");

            migrationBuilder.RenameIndex(
                name: "ix_adjustment_type_outlet_adjustment_type_id",
                table: "AdjustmentTypeOutlet",
                newName: "IX_AdjustmentTypeOutlet_AdjustmentTypeId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AdjustmentType",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "AdjustmentType",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "adjustment_type_id",
                table: "AdjustmentType",
                newName: "AdjustmentTypeId");

            // migrationBuilder.AlterColumn<NpgsqlTsVector>(
            //     name: "SearchVector",
            //     table: "MenuItem",
            //     type: "tsvector",
            //     nullable: false,
            //     oldClrType: typeof(NpgsqlTsVector),
            //     oldType: "tsvector")
            //     .Annotation("Npgsql:TsVectorConfig", "english")
            //     .Annotation("Npgsql:TsVectorProperties", new[] { "Name", "Description" })
            //     .OldAnnotation("Npgsql:TsVectorConfig", "english")
            //     .OldAnnotation("Npgsql:TsVectorProperties", new[] { "name", "description" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Table",
                table: "Table",
                column: "TableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Section",
                table: "Section",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Printer",
                table: "Printer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Outlet",
                table: "Outlet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Option",
                table: "Option",
                column: "OptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu",
                table: "Menu",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Extra",
                table: "Extra",
                column: "ExtraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Division",
                table: "Division",
                column: "DivisionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clock",
                table: "Clock",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Business",
                table: "Business",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adjustment",
                table: "Adjustment",
                column: "AdjustmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleOutlet",
                table: "UserRoleOutlet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOutlet",
                table: "UserOutlet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableBooking",
                table: "TableBooking",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockOrderStatus",
                table: "StockOrderStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockOrderItemStatus",
                table: "StockOrderItemStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockOrderItem",
                table: "StockOrderItem",
                columns: new[] { "StockOrderId", "StockId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockOrder",
                table: "StockOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockItemAuditType",
                table: "StockItemAuditType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockItemAudit",
                table: "StockItemAudit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockItem",
                table: "StockItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockCategory",
                table: "StockCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockAllocateStatus",
                table: "StockAllocateStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockAllocateItemStatus",
                table: "StockAllocateItemStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockAllocateItem",
                table: "StockAllocateItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockAllocate",
                table: "StockAllocate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesPeriod",
                table: "SalesPeriod",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleDivision",
                table: "RoleDivision",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentType",
                table: "PaymentType",
                column: "PaymentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutletPaymentType",
                table: "OutletPaymentType",
                columns: new[] { "OutletId", "PaymentTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutletExtraGroup",
                table: "OutletExtraGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItemStatusAudit",
                table: "OrderItemStatusAudit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItemStatus",
                table: "OrderItemStatus",
                column: "OrderItemStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItemOption",
                table: "OrderItemOption",
                column: "OrderItemOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItemExtra",
                table: "OrderItemExtra",
                column: "OrderItemExtraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem",
                column: "OrderItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderGroup",
                table: "OrderGroup",
                column: "OrderGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OptionStock",
                table: "OptionStock",
                columns: new[] { "OptionId", "StockId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OptionGroup",
                table: "OptionGroup",
                column: "OptionGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationUser",
                table: "NotificationUser",
                columns: new[] { "UserId", "Token" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationLog",
                table: "NotificationLog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuSection",
                table: "MenuSection",
                column: "MenuSectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemStock",
                table: "MenuItemStock",
                columns: new[] { "MenuItemId", "StockItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemOptionGroup",
                table: "MenuItemOptionGroup",
                columns: new[] { "OptionGroupId", "MenuItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemExtraGroup",
                table: "MenuItemExtraGroup",
                columns: new[] { "ExtraGroupId", "MenuItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItem",
                table: "MenuItem",
                column: "MenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HaloReference",
                table: "HaloReference",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HaloLog",
                table: "HaloLog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HaloConfig",
                table: "HaloConfig",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtraStock",
                table: "ExtraStock",
                columns: new[] { "ExtraId", "StockId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtraGroup",
                table: "ExtraGroup",
                column: "ExtraGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailLog",
                table: "EmailLog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DivisionType",
                table: "DivisionType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashUpUserItemType",
                table: "CashUpUserItemType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashUpUserItem",
                table: "CashUpUserItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashUpUser",
                table: "CashUpUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashUpConfig",
                table: "CashUpConfig",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashUp",
                table: "CashUp",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdjustmentTypeOutlet",
                table: "AdjustmentTypeOutlet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdjustmentType",
                table: "AdjustmentType",
                column: "AdjustmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adjustment_AdjustmentType_AdjustmentTypeId",
                table: "Adjustment",
                column: "AdjustmentTypeId",
                principalTable: "AdjustmentType",
                principalColumn: "AdjustmentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adjustment_TableBooking_TableBookingId",
                table: "Adjustment",
                column: "TableBookingId",
                principalTable: "TableBooking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdjustmentTypeOutlet_AdjustmentType_AdjustmentTypeId",
                table: "AdjustmentTypeOutlet",
                column: "AdjustmentTypeId",
                principalTable: "AdjustmentType",
                principalColumn: "AdjustmentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdjustmentTypeOutlet_Outlet_OutletId",
                table: "AdjustmentTypeOutlet",
                column: "OutletId",
                principalTable: "Outlet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashUp_SalesPeriod_SalesPeriodId",
                table: "CashUp",
                column: "SalesPeriodId",
                principalTable: "SalesPeriod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItem_CashUpUserItemType_CashUpUserItemTypeId",
                table: "CashUpUserItem",
                column: "CashUpUserItemTypeId",
                principalTable: "CashUpUserItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItem_CashUpUser_CashUpUserId",
                table: "CashUpUserItem",
                column: "CashUpUserId",
                principalTable: "CashUpUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItemType_AdjustmentType_AdjustmentTypeId",
                table: "CashUpUserItemType",
                column: "AdjustmentTypeId",
                principalTable: "AdjustmentType",
                principalColumn: "AdjustmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItemType_CashUpConfig_CashupConfigId",
                table: "CashUpUserItemType",
                column: "CashupConfigId",
                principalTable: "CashUpConfig",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItemType_PaymentType_PaymentTypeId",
                table: "CashUpUserItemType",
                column: "PaymentTypeId",
                principalTable: "PaymentType",
                principalColumn: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clock_Outlet_OutletId",
                table: "Clock",
                column: "OutletId",
                principalTable: "Outlet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clock_User_UserId",
                table: "Clock",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Table_TableId",
                table: "Customer",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extra_ExtraGroup_ExtraGroupId",
                table: "Extra",
                column: "ExtraGroupId",
                principalTable: "ExtraGroup",
                principalColumn: "ExtraGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraStock_Extra_ExtraId",
                table: "ExtraStock",
                column: "ExtraId",
                principalTable: "Extra",
                principalColumn: "ExtraId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraStock_Stock_StockId",
                table: "ExtraStock",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraStock_Unit_UnitId",
                table: "ExtraStock",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Outlet_OutletId",
                table: "Menu",
                column: "OutletId",
                principalTable: "Outlet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Division_DivisionId",
                table: "MenuItem",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_MenuSection_MenuSectionId",
                table: "MenuItem",
                column: "MenuSectionId",
                principalTable: "MenuSection",
                principalColumn: "MenuSectionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemExtraGroup_ExtraGroup_ExtraGroupId",
                table: "MenuItemExtraGroup",
                column: "ExtraGroupId",
                principalTable: "ExtraGroup",
                principalColumn: "ExtraGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemExtraGroup_MenuItem_MenuItemId",
                table: "MenuItemExtraGroup",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "MenuItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOptionGroup_MenuItem_MenuItemId",
                table: "MenuItemOptionGroup",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "MenuItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOptionGroup_OptionGroup_OptionGroupId",
                table: "MenuItemOptionGroup",
                column: "OptionGroupId",
                principalTable: "OptionGroup",
                principalColumn: "OptionGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemStock_MenuItem_MenuItemId",
                table: "MenuItemStock",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "MenuItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemStock_StockItem_StockItemId",
                table: "MenuItemStock",
                column: "StockItemId",
                principalTable: "StockItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuSection_MenuSection_ParentId",
                table: "MenuSection",
                column: "ParentId",
                principalTable: "MenuSection",
                principalColumn: "MenuSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuSection_Menu_MenuId",
                table: "MenuSection",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Option_OptionGroup_OptionGroupId",
                table: "Option",
                column: "OptionGroupId",
                principalTable: "OptionGroup",
                principalColumn: "OptionGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionStock_Option_OptionId",
                table: "OptionStock",
                column: "OptionId",
                principalTable: "Option",
                principalColumn: "OptionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionStock_Stock_StockId",
                table: "OptionStock",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionStock_Unit_UnitId",
                table: "OptionStock",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_MenuItem_MenuItemId",
                table: "OrderItem",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "MenuItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_OrderGroup_OrderGroupId",
                table: "OrderItem",
                column: "OrderGroupId",
                principalTable: "OrderGroup",
                principalColumn: "OrderGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_OrderItemStatus_OrderItemStatusId",
                table: "OrderItem",
                column: "OrderItemStatusId",
                principalTable: "OrderItemStatus",
                principalColumn: "OrderItemStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_TableBooking_TableBookingId",
                table: "OrderItem",
                column: "TableBookingId",
                principalTable: "TableBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemExtra_Extra_ExtraId",
                table: "OrderItemExtra",
                column: "ExtraId",
                principalTable: "Extra",
                principalColumn: "ExtraId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemExtra_OrderItem_OrderItemId",
                table: "OrderItemExtra",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "OrderItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemOption_Option_OptionId",
                table: "OrderItemOption",
                column: "OptionId",
                principalTable: "Option",
                principalColumn: "OptionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemOption_OrderItem_OrderItemId",
                table: "OrderItemOption",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "OrderItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Outlet_Business_BusinessId",
                table: "Outlet",
                column: "BusinessId",
                principalTable: "Business",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutletExtraGroup_ExtraGroup_ExtraGroupId",
                table: "OutletExtraGroup",
                column: "ExtraGroupId",
                principalTable: "ExtraGroup",
                principalColumn: "ExtraGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutletExtraGroup_Outlet_OutletId",
                table: "OutletExtraGroup",
                column: "OutletId",
                principalTable: "Outlet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutletPaymentType_Outlet_OutletId",
                table: "OutletPaymentType",
                column: "OutletId",
                principalTable: "Outlet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutletPaymentType_PaymentType_PaymentTypeId",
                table: "OutletPaymentType",
                column: "PaymentTypeId",
                principalTable: "PaymentType",
                principalColumn: "PaymentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_PaymentType_PaymentTypeId",
                table: "Payment",
                column: "PaymentTypeId",
                principalTable: "PaymentType",
                principalColumn: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_TableBooking_TableBookingId",
                table: "Payment",
                column: "TableBookingId",
                principalTable: "TableBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleDivision_Division_DivisionId",
                table: "RoleDivision",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPeriod_Outlet_OutletId",
                table: "SalesPeriod",
                column: "OutletId",
                principalTable: "Outlet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Outlet_OutletId",
                table: "Section",
                column: "OutletId",
                principalTable: "Outlet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_StockCategory_StockCategoryId",
                table: "Stock",
                column: "StockCategoryId",
                principalTable: "StockCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Unit_UnitId",
                table: "Stock",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockAllocate_Division_FromDivisionId",
                table: "StockAllocate",
                column: "FromDivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockAllocate_Division_ToDivisionId",
                table: "StockAllocate",
                column: "ToDivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockAllocate_StockAllocateStatus_StockAllocateStatusId",
                table: "StockAllocate",
                column: "StockAllocateStatusId",
                principalTable: "StockAllocateStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockAllocate_User_AssignedUserUserId",
                table: "StockAllocate",
                column: "AssignedUserUserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockAllocate_User_FromUserUserId",
                table: "StockAllocate",
                column: "FromUserUserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockAllocateItem_Division_DivisionId",
                table: "StockAllocateItem",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockAllocateItem_StockAllocateItemStatus_StockAllocateItem~",
                table: "StockAllocateItem",
                column: "StockAllocateItemStatusId",
                principalTable: "StockAllocateItemStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockAllocateItem_StockAllocate_StockAllocateId",
                table: "StockAllocateItem",
                column: "StockAllocateId",
                principalTable: "StockAllocate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockAllocateItem_Stock_StockId",
                table: "StockAllocateItem",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItem_Division_DivisionId",
                table: "StockItem",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItem_Stock_StockId",
                table: "StockItem",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItemAudit_StockItemAuditType_StockItemAuditTypeId",
                table: "StockItemAudit",
                column: "StockItemAuditTypeId",
                principalTable: "StockItemAuditType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrder_Division_DivisionId",
                table: "StockOrder",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrder_StockOrderStatus_StockOrderStatusId",
                table: "StockOrder",
                column: "StockOrderStatusId",
                principalTable: "StockOrderStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrder_Supplier_SupplierId",
                table: "StockOrder",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrderItem_StockOrderItemStatus_StockOrderItemStatusId",
                table: "StockOrderItem",
                column: "StockOrderItemStatusId",
                principalTable: "StockOrderItemStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrderItem_StockOrder_StockOrderId",
                table: "StockOrderItem",
                column: "StockOrderId",
                principalTable: "StockOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrderItem_Stock_StockId",
                table: "StockOrderItem",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Table_Section_SectionId",
                table: "Table",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableBooking_CashUpUser_CashUpUserId",
                table: "TableBooking",
                column: "CashUpUserId",
                principalTable: "CashUpUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableBooking_SalesPeriod_SalesPeriodId",
                table: "TableBooking",
                column: "SalesPeriodId",
                principalTable: "SalesPeriod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableBooking_Table_TableId",
                table: "TableBooking",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "TableId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableBooking_User_UserId",
                table: "TableBooking",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_MenuItem_MenuItemId",
                table: "Tag",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleOutlet_Outlet_OutletId",
                table: "UserRoleOutlet",
                column: "OutletId",
                principalTable: "Outlet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleOutlet_Role_RoleId",
                table: "UserRoleOutlet",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleOutlet_User_UserId",
                table: "UserRoleOutlet",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
