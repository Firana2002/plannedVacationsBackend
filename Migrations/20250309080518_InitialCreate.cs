using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalVacationDays",
                columns: table => new
                {
                    AdditionalVacationDaysId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DaysCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Addition__609164D8D44CE74C", x => x.AdditionalVacationDaysId);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    HolidayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Holidays__2D35D57AEAB79E08", x => x.HolidayId);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Organiza__CADB0B12B6A1759C", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Position__60BB9A7994A6A7E7", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "Restrictions",
                columns: table => new
                {
                    RestrictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Restrict__529D86BA45717F5E", x => x.RestrictionId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "VacationStatuses",
                columns: table => new
                {
                    VacationStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationStatuses", x => x.VacationStatusId);
                });

            migrationBuilder.CreateTable(
                name: "VacationTypes",
                columns: table => new
                {
                    VacationTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Vacation__22E546763BF5943B", x => x.VacationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departme__B2079BEDA328D9C5", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK__Departmen__Organ__4BAC3F29",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId");
                });

            migrationBuilder.CreateTable(
                name: "DepartmentRestrictions",
                columns: table => new
                {
                    DepartmentRestrictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    RestrictionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departme__ABCAF3CDD5199EFE", x => x.DepartmentRestrictionId);
                    table.ForeignKey(
                        name: "FK__Departmen__Depar__6383C8BA",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId");
                    table.ForeignKey(
                        name: "FK__Departmen__Restr__6477ECF3",
                        column: x => x.RestrictionId,
                        principalTable: "Restrictions",
                        principalColumn: "RestrictionId");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HireDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsMultipleChildren = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    HasDisabledChild = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    IsVeteran = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    IsHonorDonor = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__7AD04F1107E6E4F2", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Roles",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK__Employees__Depar__5441852A",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId");
                    table.ForeignKey(
                        name: "FK__Employees__Posit__5535A963",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeVacationDays",
                columns: table => new
                {
                    EmployeeVacationDaysId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    VacationTypeId = table.Column<int>(type: "int", nullable: false),
                    DaysCount = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__2D6E62F473F37097", x => x.EmployeeVacationDaysId);
                    table.ForeignKey(
                        name: "FK__EmployeeV__Emplo__59FA5E80",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK__EmployeeV__Vacat__5AEE82B9",
                        column: x => x.VacationTypeId,
                        principalTable: "VacationTypes",
                        principalColumn: "VacationTypeId");
                });

            migrationBuilder.CreateTable(
                name: "PlannedVacations",
                columns: table => new
                {
                    PlannedVacationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    VacationTypeId = table.Column<int>(type: "int", nullable: false),
                    VacationStatusId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PlannedV__1C230134C01B9D4D", x => x.PlannedVacationId);
                    table.ForeignKey(
                        name: "FK_PlannedVacations_VacationStatuses_VacationStatusId",
                        column: x => x.VacationStatusId,
                        principalTable: "VacationStatuses",
                        principalColumn: "VacationStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__PlannedVa__Emplo__6754599E",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK__PlannedVa__Vacat__68487DD7",
                        column: x => x.VacationTypeId,
                        principalTable: "VacationTypes",
                        principalColumn: "VacationTypeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRestrictions_DepartmentId",
                table: "DepartmentRestrictions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRestrictions_RestrictionId",
                table: "DepartmentRestrictions",
                column: "RestrictionId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_OrganizationId",
                table: "Departments",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeVacationDays_EmployeeId",
                table: "EmployeeVacationDays",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeVacationDays_VacationTypeId",
                table: "EmployeeVacationDays",
                column: "VacationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedVacations_EmployeeId",
                table: "PlannedVacations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedVacations_VacationStatusId",
                table: "PlannedVacations",
                column: "VacationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedVacations_VacationTypeId",
                table: "PlannedVacations",
                column: "VacationTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalVacationDays");

            migrationBuilder.DropTable(
                name: "DepartmentRestrictions");

            migrationBuilder.DropTable(
                name: "EmployeeVacationDays");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "PlannedVacations");

            migrationBuilder.DropTable(
                name: "Restrictions");

            migrationBuilder.DropTable(
                name: "VacationStatuses");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "VacationTypes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
