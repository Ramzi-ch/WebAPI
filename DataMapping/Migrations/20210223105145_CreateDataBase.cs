using Microsoft.EntityFrameworkCore.Migrations;

namespace DataMapping.Migrations
{
    public partial class CreateDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    DateOfJoining = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoFileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "CountryId", "CountryName", "Population" },
                values: new object[,]
                {
                    { 1, "Sfax", 1000L },
                    { 2, "Sousse", 1000L },
                    { 3, "Gabes", 1200L }
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DepartmentId", "DepartmentName" },
                values: new object[,]
                {
                    { 7, "RH" },
                    { 5, "WordPress" },
                    { 4, "React" },
                    { 6, "Prestashop" },
                    { 2, "NodeJs" },
                    { 1, ".Net" },
                    { 3, "Flutter" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "CountryID", "DateOfJoining", "DepartmentId", "EmployeeName", "PhotoFileName" },
                values: new object[,]
                {
                    { 6, null, null, null, "Haitham", null },
                    { 1, null, null, null, "Sofien", null },
                    { 2, null, null, null, "Ahmed", null },
                    { 3, null, null, null, "Khadija", null },
                    { 4, null, null, null, "Mahmoud", null },
                    { 5, null, null, null, "Majdi", null },
                    { 7, null, null, null, "Maryam", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CountryID",
                table: "Employee",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
