using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesCollection.Api.Migrations
{
  /// <inheritdoc />
  public partial class PopulateCountries : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("Insert into Countries(Name) Values('Brasil')");
      migrationBuilder.Sql("Insert into Countries(Name) Values('Estados Unidos')");
      migrationBuilder.Sql("Insert into Countries(Name) Values('Espanha')");
      migrationBuilder.Sql("Insert into Countries(Name) Values('Japão')");
      migrationBuilder.Sql("Insert into Countries(Name) Values('Inglaterra')");
      migrationBuilder.Sql("Insert into Countries(Name) Values('Coreia do Sul')");
      migrationBuilder.Sql("Insert into Countries(Name) Values('Nova Zelândia')");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("Delete from Countries");
    }
  }
}
