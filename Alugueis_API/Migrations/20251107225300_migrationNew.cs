using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alugueis_API.Migrations
{
    /// <inheritdoc />
    public partial class migrationNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "VrlTotalDespesa",
                table: "Despesas",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "VlrRateio",
                table: "DespesaRateios",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "VrlTotalDespesa",
                table: "Despesas",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "VlrRateio",
                table: "DespesaRateios",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
