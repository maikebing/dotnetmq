using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetMQ.Data.Migrations
{
    public partial class CreateIdentitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "DataStorage");

            migrationBuilder.DropTable(
                name: "DeviceIdentities");

            migrationBuilder.DropTable(
                name: "Relationship");

            migrationBuilder.DropTable(
                name: "RetainedMessage");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "AuthorizedKeys");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Tenant");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataStorage",
                columns: table => new
                {
                    Catalog = table.Column<int>(type: "integer", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    KeyName = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataSide = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Value_Binary = table.Column<byte[]>(type: "bytea", nullable: true),
                    Value_Boolean = table.Column<bool>(type: "boolean", nullable: false),
                    Value_DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Value_Double = table.Column<double>(type: "double precision", nullable: false),
                    Value_Json = table.Column<string>(type: "jsonb", nullable: true),
                    Value_Long = table.Column<long>(type: "bigint", nullable: false),
                    Value_String = table.Column<string>(type: "text", nullable: true),
                    Value_XML = table.Column<string>(type: "xml", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataStorage", x => new { x.Catalog, x.DeviceId, x.KeyName, x.DateTime });
                });

            migrationBuilder.CreateTable(
                name: "RetainedMessage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Payload = table.Column<byte[]>(type: "bytea", nullable: true),
                    QualityOfServiceLevel = table.Column<int>(type: "integer", nullable: false),
                    Retain = table.Column<bool>(type: "boolean", nullable: false),
                    Topic = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetainedMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    EMail = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Province = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Province = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ZipCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionData = table.Column<string>(type: "jsonb", nullable: true),
                    ActionName = table.Column<string>(type: "text", nullable: true),
                    ActionResult = table.Column<string>(type: "jsonb", nullable: true),
                    ActiveDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    ObjectID = table.Column<Guid>(type: "uuid", nullable: false),
                    ObjectName = table.Column<string>(type: "text", nullable: true),
                    ObjectType = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLog_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditLog_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizedKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthToken = table.Column<string>(type: "text", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizedKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorizedKeys_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthorizedKeys_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relationship",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    IdentityUserId = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relationship_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relationship_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relationship_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorizedKeyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeviceType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Device_AuthorizedKeys_AuthorizedKeyId",
                        column: x => x.AuthorizedKeyId,
                        principalTable: "AuthorizedKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Device_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Device_Device_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Device_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceIdentities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdentityId = table.Column<string>(type: "text", nullable: false),
                    IdentityType = table.Column<int>(type: "integer", nullable: false),
                    IdentityValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceIdentities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceIdentities_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_CustomerId",
                table: "AuditLog",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_TenantId",
                table: "AuditLog",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizedKeys_CustomerId",
                table: "AuthorizedKeys",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizedKeys_TenantId",
                table: "AuthorizedKeys",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_TenantId",
                table: "Customer",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_DataStorage_Catalog",
                table: "DataStorage",
                column: "Catalog");

            migrationBuilder.CreateIndex(
                name: "IX_DataStorage_Catalog_DeviceId",
                table: "DataStorage",
                columns: new[] { "Catalog", "DeviceId" });

            migrationBuilder.CreateIndex(
                name: "IX_DataStorage_Catalog_DeviceId_KeyName",
                table: "DataStorage",
                columns: new[] { "Catalog", "DeviceId", "KeyName" });

            migrationBuilder.CreateIndex(
                name: "IX_DataStorage_Catalog_DeviceId_KeyName_DateTime",
                table: "DataStorage",
                columns: new[] { "Catalog", "DeviceId", "KeyName", "DateTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Device_AuthorizedKeyId",
                table: "Device",
                column: "AuthorizedKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_CustomerId",
                table: "Device",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_OwnerId",
                table: "Device",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_TenantId",
                table: "Device",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceIdentities_DeviceId",
                table: "DeviceIdentities",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship_CustomerId",
                table: "Relationship",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship_IdentityUserId",
                table: "Relationship",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship_TenantId",
                table: "Relationship",
                column: "TenantId");
        }
    }
}
