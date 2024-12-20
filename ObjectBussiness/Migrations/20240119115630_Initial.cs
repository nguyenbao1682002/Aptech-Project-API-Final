using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ObjectBussiness.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elects",
                columns: table => new
                {
                    ElectID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elects", x => x.ElectID);
                });

            migrationBuilder.CreateTable(
                name: "ExamRegister",
                columns: table => new
                {
                    ExamRegisterID = table.Column<int>(type: "int", nullable: false),
                    CandidateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResidentialAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamRegister", x => x.ExamRegisterID);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    ExamID = table.Column<int>(type: "int", nullable: false),
                    ExamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreateTest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeBegin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeDelay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.ExamID);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    HistoryID = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.HistoryID);
                });

            migrationBuilder.CreateTable(
                name: "NewsCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    ExamID = table.Column<int>(type: "int", nullable: false),
                    ExamRegisterID = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Accounts_ExamRegister_ExamRegisterID",
                        column: x => x.ExamRegisterID,
                        principalTable: "ExamRegister",
                        principalColumn: "ExamRegisterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Exams_ExamID",
                        column: x => x.ExamID,
                        principalTable: "Exams",
                        principalColumn: "ExamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    RoundID = table.Column<int>(type: "int", nullable: false),
                    ExamID = table.Column<int>(type: "int", nullable: false),
                    RoundNumber = table.Column<int>(type: "int", nullable: false),
                    RoundName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.RoundID);
                    table.ForeignKey(
                        name: "FK_Rounds_Exams_ExamID",
                        column: x => x.ExamID,
                        principalTable: "Exams",
                        principalColumn: "ExamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultCandidates",
                columns: table => new
                {
                    ResultCandidateID = table.Column<int>(type: "int", nullable: false),
                    ExamID = table.Column<int>(type: "int", nullable: false),
                    ElectID = table.Column<int>(type: "int", nullable: false),
                    HistoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultCandidates", x => x.ResultCandidateID);
                    table.ForeignKey(
                        name: "FK_ResultCandidates_Elects_ElectID",
                        column: x => x.ElectID,
                        principalTable: "Elects",
                        principalColumn: "ElectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultCandidates_Exams_ExamID",
                        column: x => x.ExamID,
                        principalTable: "Exams",
                        principalColumn: "ExamID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultCandidates_Histories_HistoryID",
                        column: x => x.HistoryID,
                        principalTable: "Histories",
                        principalColumn: "HistoryID");
                });

            migrationBuilder.CreateTable(
                name: "Decentralizations",
                columns: table => new
                {
                    DecentralizationID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    RoleGrantDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decentralizations", x => x.DecentralizationID);
                    table.ForeignKey(
                        name: "FK_Decentralizations_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Decentralizations_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortContents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsID);
                    table.ForeignKey(
                        name: "FK_News_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_News_NewsCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "NewsCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    RoundID = table.Column<int>(type: "int", nullable: false),
                    QuestionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateMake = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Rounds_RoundID",
                        column: x => x.RoundID,
                        principalTable: "Rounds",
                        principalColumn: "RoundID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Elects",
                columns: new[] { "ElectID", "Status" },
                values: new object[,]
                {
                    { 1, true },
                    { 2, false }
                });

            migrationBuilder.InsertData(
                table: "ExamRegister",
                columns: new[] { "ExamRegisterID", "Age", "BirthDay", "CandidateName", "City", "Country", "Email", "Gender", "Phone", "PlaceOfBirth", "ResidentialAddress" },
                values: new object[] { 1, 17, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Đà Nẵng", "Việt Nam", "admin@gmail.com", true, "0911040107", "Đà Nẵng", "22 Nguyễn Thức Tự,Hoà Hải,Ngũ Hàng Sơn,Đà Nẵng" });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "ExamID", "DateCreateTest", "ExamName", "Status", "TimeBegin", "TimeDelay", "TimeEnd" },
                values: new object[] { 1, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin(Not select)", "Start", new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "NewsCategories",
                columns: new[] { "CategoryID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Gasoline Prices" },
                    { 2, "Recruitment Jobs" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Candidate" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountID", "ExamID", "ExamRegisterID", "Password" },
                values: new object[] { 1, 1, 1, "Admin@123.cntt" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ExamID",
                table: "Accounts",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ExamRegisterID",
                table: "Accounts",
                column: "ExamRegisterID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Decentralizations_AccountID",
                table: "Decentralizations",
                column: "AccountID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Decentralizations_RoleID",
                table: "Decentralizations",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_News_AccountID",
                table: "News",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_News_CategoryID",
                table: "News",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_RoundID",
                table: "Questions",
                column: "RoundID");

            migrationBuilder.CreateIndex(
                name: "IX_ResultCandidates_ElectID",
                table: "ResultCandidates",
                column: "ElectID");

            migrationBuilder.CreateIndex(
                name: "IX_ResultCandidates_ExamID",
                table: "ResultCandidates",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_ResultCandidates_HistoryID",
                table: "ResultCandidates",
                column: "HistoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_ExamID",
                table: "Rounds",
                column: "ExamID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Decentralizations");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "ResultCandidates");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "NewsCategories");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Elects");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "ExamRegister");

            migrationBuilder.DropTable(
                name: "Exams");
        }
    }
}
