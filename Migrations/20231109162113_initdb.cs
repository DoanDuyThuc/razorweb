using System;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;
using razoWeb.models;

#nullable disable

namespace ValidateAsp.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

                //insetData
                //fake data : Bogus

                Randomizer.Seed = new Random(8675309);

                var fakerArtical = new Faker<Article>();
                fakerArtical.RuleFor(a => a.Title, f => f.Lorem.Sentence(5,5));
                fakerArtical.RuleFor(a => a.Created, f => f.Date.Between(new DateTime(2022,1,1), new DateTime(2023,1,1)));
                fakerArtical.RuleFor(a => a.Content, f => f.Lorem.Paragraphs(1,4));


                for (int i = 0; i < 150; i++)
                {
                    Article article = fakerArtical.Generate();
                    migrationBuilder.InsertData(
                        table: "article",
                        columns: new[] {"Title", "Created", "Content"},
                        //chèn dữ liệu
                        values: new object[] {
                            article.Title,
                            article.Created,
                            article.Content
                        }
                );
                }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article");
        }
    }
}
