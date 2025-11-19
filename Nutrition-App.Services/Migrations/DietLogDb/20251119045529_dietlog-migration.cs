using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_App.Services.Migrations.DietLogDb
{
    /// <inheritdoc />
    public partial class dietlogmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodAttributeType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodAttributeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasureUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Abbreviation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Height = table.Column<double>(type: "REAL", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    Sex = table.Column<string>(type: "TEXT", nullable: false),
                    BMI = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WweiaFoodCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WweiaFoodCategoryDescription = table.Column<string>(type: "TEXT", nullable: false),
                    WweiaFoodCategoryCode = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WweiaFoodCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FoodClass = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    FoodCode = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<string>(type: "TEXT", nullable: false),
                    EndDate = table.Column<string>(type: "TEXT", nullable: false),
                    WweiaFoodCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataType = table.Column<string>(type: "TEXT", nullable: false),
                    FdcId = table.Column<int>(type: "INTEGER", nullable: false),
                    PublicationDate = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Food_WweiaFoodCategory_WweiaFoodCategoryId",
                        column: x => x.WweiaFoodCategoryId,
                        principalTable: "WweiaFoodCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DietLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FoodId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateEaten = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WeightEaten = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietLogs_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietLogs_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    FoodAttributeTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    FoodId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodAttribute_FoodAttributeType_FoodAttributeTypeId",
                        column: x => x.FoodAttributeTypeId,
                        principalTable: "FoodAttributeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodAttribute_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoodPortion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MeasureUnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    Modifier = table.Column<string>(type: "TEXT", nullable: false),
                    GramWeight = table.Column<double>(type: "REAL", nullable: false),
                    PortionDescription = table.Column<string>(type: "TEXT", nullable: false),
                    SequenceNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodPortion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodPortion_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FoodPortion_MeasureUnit_MeasureUnitId",
                        column: x => x.MeasureUnitId,
                        principalTable: "MeasureUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InputFood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Unit = table.Column<string>(type: "TEXT", nullable: false),
                    PortionDescription = table.Column<string>(type: "TEXT", nullable: false),
                    PortionCode = table.Column<string>(type: "TEXT", nullable: false),
                    FoodDescription = table.Column<string>(type: "TEXT", nullable: false),
                    RetentionCode = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientWeight = table.Column<double>(type: "REAL", nullable: false),
                    IngredientCode = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientDescription = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    SequenceNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputFood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InputFood_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Nutrient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Rank = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitName = table.Column<string>(type: "TEXT", nullable: false),
                    DietLogId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nutrient_DietLogs_DietLogId",
                        column: x => x.DietLogId,
                        principalTable: "DietLogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoodNutrient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    NutrientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    FoodId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodNutrient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodNutrient_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FoodNutrient_Nutrient_NutrientId",
                        column: x => x.NutrientId,
                        principalTable: "Nutrient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietLogs_FoodId",
                table: "DietLogs",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DietLogs_UserId",
                table: "DietLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_WweiaFoodCategoryId",
                table: "Food",
                column: "WweiaFoodCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodAttribute_FoodAttributeTypeId",
                table: "FoodAttribute",
                column: "FoodAttributeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodAttribute_FoodId",
                table: "FoodAttribute",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodNutrient_FoodId",
                table: "FoodNutrient",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodNutrient_NutrientId",
                table: "FoodNutrient",
                column: "NutrientId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodPortion_FoodId",
                table: "FoodPortion",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodPortion_MeasureUnitId",
                table: "FoodPortion",
                column: "MeasureUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_InputFood_FoodId",
                table: "InputFood",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrient_DietLogId",
                table: "Nutrient",
                column: "DietLogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodAttribute");

            migrationBuilder.DropTable(
                name: "FoodNutrient");

            migrationBuilder.DropTable(
                name: "FoodPortion");

            migrationBuilder.DropTable(
                name: "InputFood");

            migrationBuilder.DropTable(
                name: "FoodAttributeType");

            migrationBuilder.DropTable(
                name: "Nutrient");

            migrationBuilder.DropTable(
                name: "MeasureUnit");

            migrationBuilder.DropTable(
                name: "DietLogs");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "WweiaFoodCategory");
        }
    }
}
