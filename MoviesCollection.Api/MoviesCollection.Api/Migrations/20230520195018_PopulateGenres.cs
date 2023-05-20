using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesCollection.Api.Migrations
{
  /// <inheritdoc />
  public partial class PopulateGenres : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("Insert into Genres(Description) Values('Ação')");
      migrationBuilder.Sql("Insert into Genres(Description) Values('Aventura')");
      migrationBuilder.Sql("Insert into Genres(Description) Values('Comédia')");
      migrationBuilder.Sql("Insert into Genres(Description) Values('Documentário')");
      migrationBuilder.Sql("Insert into Genres(Description) Values('Drama')");
      migrationBuilder.Sql("Insert into Genres(Description) Values('Espionagem')");
      migrationBuilder.Sql("Insert into Genres(Description) Values('Faroeste')");
      migrationBuilder.Sql("Insert into Genres(Description) Values('Fantasia')");
      migrationBuilder.Sql("Insert into Genres(Description) Values('Ficção Científica')");
      migrationBuilder.Sql("Insert into Genres(Description) Values('Guerra')");
      migrationBuilder.Sql("Insert into Genres(Description) Values('Musical')");
      migrationBuilder.Sql("Insert into Genres(Description) Values('Romance')");
      migrationBuilder.Sql("Insert into Genres(Description) Values('Suspense')");
      migrationBuilder.Sql("Insert into Genres(Description) Values('Terror')");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("Delete from Genres");
    }
  }
}
