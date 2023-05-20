using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesCollection.Api.Migrations
{
  /// <inheritdoc />
  public partial class PopulateMovies : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql(
      "Insert into Movies(Title, OriginalTitle, GenreId, Runtime, DirectorId, LanguageId, CountryId, ReleaseDate, ParentalRatingId, Evaluation, Synopsis) " +
      "Values('O Senhor dos Anéis: A Sociedade do Anel','The Lord of the Rings: The Fellowship of the Ring', 8, 178, 1, 2, 7,'19-12-2001', 3, 5,'Numa terra fantástica e única, chamada Terra-Média, um hobbit (seres de estatura entre 80 cm e 1,20 m, com pés peludos e bochechas um pouco avermelhadas) recebe de presente de seu tio o Um Anel, um anel mágico e maligno que precisa ser destruído antes que caia nas mãos do mal. Para isso o hobbit Frodo (Elijah Woods) terá um caminho árduo pela frente, onde encontrará perigo, medo e personagens bizarros. Ao seu lado para o cumprimento desta jornada aos poucos ele poderá contar com outros hobbits, um elfo, um anão, dois humanos e um mago, totalizando 9 pessoas que formarão a Sociedade do Anel.')");
      migrationBuilder.Sql(
      "Insert into Movies(Title, OriginalTitle, GenreId, Runtime, DirectorId, LanguageId, CountryId, ReleaseDate, ParentalRatingId, Evaluation, Synopsis) " +
      "Values('Invocação do Mal','The Conjuring', 14, 112, 9, 2, 2,'13-09-2003', 4, 5,'Baseado em uma história real, o longa se passa em uma casa mal-assombrada, para onde uma família liderada por Lili Taylor e Ron Livingston se muda. Quando fica claro que uma entidade obscura os está perseguindo, eles chamam os investigadores paranormais Ed e Lorraine (Patrick Wilson e Vera Farmiga) para ajudar.')");
      migrationBuilder.Sql(
      "Insert into Movies(Title, OriginalTitle, GenreId, Runtime, DirectorId, LanguageId, CountryId, ReleaseDate, ParentalRatingId, Evaluation, Synopsis) " +
      "Values('Jurassic Park: O Parque dos Dinossauros','Jurassic Park', 2, 126, 2, 2, 2,'13-06-1993', 1, 5,'O Dr. John Hammond (Richard Attenborough) possui uma equipe de cientistas para descobrir um método cientifico de regenerar dinossauros através do DNA que eles encontram em âmbares preservados. Com isso ele cria um parque temático chamado Jurassic Park, onde todos são bem vindos para ver o passado retornar a vida. Dr. Hammond convida seus netos, Dr. Alan Grant (Sam Neil), Dra. Ellie Sattler (Laura Dern) e Ian Malcolm (Jeff Goldblum) para serem os primeiros à testemunhar a sua criação. As coisas começam a ir mal quando um empregado, Nedry (Wayne Knight), desativa o alarme de segurança e o mecanismo de defesa na tentativa de roubar espécies para revendê-las. Os dinossauros estão livres e as pessoas precisam lutar por suas vidas ou virar refeição para os ferozes Tiranossauros.')");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("Delete from Movies");
    }
  }
}
