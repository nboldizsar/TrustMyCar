using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrustMyCar.Migrations
{
    public partial class Images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceBill");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "ServiceEvent",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "ServiceEvent",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "ServiceEvent");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "ServiceEvent");

            migrationBuilder.CreateTable(
                name: "ServiceBill",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhotoData = table.Column<byte[]>(nullable: true),
                    ServiceEventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceBill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceBill_ServiceEvent_ServiceEventId",
                        column: x => x.ServiceEventId,
                        principalTable: "ServiceEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBill_ServiceEventId",
                table: "ServiceBill",
                column: "ServiceEventId");
        }
    }
}
