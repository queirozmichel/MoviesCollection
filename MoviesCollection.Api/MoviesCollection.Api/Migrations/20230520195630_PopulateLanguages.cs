using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesCollection.Api.Migrations
{
  /// <inheritdoc />
  public partial class PopulateLanguages : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("Insert into Languages(Description, LanguageCode) Values('Português (Brasil)','pt-BR')");
      migrationBuilder.Sql("Insert into Languages(Description, LanguageCode) Values('Inglês (Estados Unidos)','en-US')");
      migrationBuilder.Sql("Insert into Languages(Description, LanguageCode) Values('Espanhol (Espanha)','es-ES')");
      migrationBuilder.Sql("Insert into Languages(Description, LanguageCode) Values('Coreano','ko-KO')");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("Delete from Languages");
    }
  }
}
