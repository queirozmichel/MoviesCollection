using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesCollection.Api.Migrations
{
  /// <inheritdoc />
  public partial class PopulateParentalRatings : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql(
      "Insert into ParentalRatings(Description, Violence, SexAndNudity, Drugs)" +
      "Values('Livre','Arma sem violência; Morte sem Violência; Ossada ou esqueleto sem violência; Violência Fantasiosa Não Realista; Tentativa de Violência; Violência Física Leve; Agressão Verbal Leve.','Nudez não erótica.','Consumo moderado ou insinuado de droga lícita.')");
      migrationBuilder.Sql(
      "Insert into ParentalRatings(Description, Violence, SexAndNudity, Drugs)" +
      "Values('Não recomendado para menores de 10 anos','Angústia; Arma com violência; Ato criminoso sem violência; Linguagem depreciativa; Medo ou tensão; Ossada ou esqueleto com resquício de ato de violência; Violência Fantasiosa Realista; Nojo.','Conteúdo educativo sobre sexo.','Descrição do consumo de droga lícita; Discussão sobre o tema drogas; Uso medicinal de droga ilícita.')");
      migrationBuilder.Sql(
      "Insert into ParentalRatings(Description, Violence, SexAndNudity, Drugs)" +
      "Values('Não recomendado para menores de 12 anos','Agressão verbal; Assédio sexual; Ato violento; Ato violento contra animal; Bullying; Descrição de violência; Exposição ao perigo; Exposição de cadáver; Exposição de pessoa em situação constrangedora ou degradante; Lesão corporal; Morte derivada de ato heróico; Morte natural ou acidental com dor ou violência; Obscenidade; Presença de sangue; Sofrimento da vítima; Supervalorização da beleza física; Supervalorização do consumo; Violência psicológica.','Apelo sexual; Carícia sexual; Insinuação sexual; Linguagem chula; Linguagem de conteúdo sexual; Masturbação; Nudez velada; Simulação de sexo; Namoro ou Casamento com uma pessoa no mesmo momento que Flerte com outra pessoa.','Consumo de droga lícita; Consumo irregular de medicamento; Discussão sobre legalização de droga ilícita; Indução ao uso de droga lícita; Menção a droga ilícita.')");
      migrationBuilder.Sql(
      "Insert into ParentalRatings(Description, Violence, SexAndNudity, Drugs)" +
      "Values('Não recomendado para menores de 14 anos','Aborto; Estigma ou preconceito; Eutanásia; Exploração sexual; Morte intencional; Pena de morte.','Erotização; Nudez; Prostituição; Relação sexual; Vulgaridade.','Consumo insinuado de droga ilícita; Descrição do consumo ou tráfico de droga ilícita.')");
      migrationBuilder.Sql(
      "Insert into ParentalRatings(Description, Violence, SexAndNudity, Drugs)" +
      "Values('Não recomendado para menores de 16 anos','Ato de pedofilia; Crime de ódio; Estupro ou coação sexual; Mutilação; Suicídio; Tortura; Violência gratuita ou banalização da violência.','Relação sexual intensa; Poligamia.','Consumo de droga ilícita; Indução ao consumo de droga ilícita; Produção ou tráfico de droga ilícita.')");
      migrationBuilder.Sql(
      "Insert into ParentalRatings(Description, Violence, SexAndNudity, Drugs)" +
      "Values('Não recomendado para menores de 18 anos','Apologia à violência; Crueldade.','Sexo explícito; Situação sexual complexa ou de forte impacto; Incesto.','Apologia ao uso de droga ilícita.')");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("Delete from ParentalRatings");
    }
  }
}
