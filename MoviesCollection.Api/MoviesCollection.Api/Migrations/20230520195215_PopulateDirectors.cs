using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesCollection.Api.Migrations
{
  /// <inheritdoc />
  public partial class PopulateDirectors : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("Insert into Directors(Name) Values('Peter Jackson')");
      migrationBuilder.Sql("Insert into Directors(Name) Values('Steven Spielberg')");
      migrationBuilder.Sql("Insert into Directors(Name) Values('Quentin Tarantino')");
      migrationBuilder.Sql("Insert into Directors(Name) Values('Alfred Hitchcock')");
      migrationBuilder.Sql("Insert into Directors(Name) Values('Tim Burton')");
      migrationBuilder.Sql("Insert into Directors(Name) Values('Francis Ford Coppola')");
      migrationBuilder.Sql("Insert into Directors(Name) Values('George Lucas')");
      migrationBuilder.Sql("Insert into Directors(Name) Values('James Cameron')");
      migrationBuilder.Sql("Insert into Directors(Name) Values('James Wan')");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("Delete from Directors");
    }
  }
}
