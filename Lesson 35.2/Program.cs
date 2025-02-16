using System.Formats.Asn1;
using System.Globalization;
using System;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;

StreamReader reader = new StreamReader("Movies.csv");
CsvReader csvReader = new CsvReader(reader,
    new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        Delimiter = ",",
        HasHeaderRecord = true,
        HeaderValidated = null,
         BadDataFound = null
    });
List<Movie> records = csvReader.GetRecords<Movie>().ToList();
reader.Close();
var sortedMovies = records.OrderByDescending(f => f.Year).ThenBy(f => f.Name);

foreach (Movie record in sortedMovies)
{
    Console.WriteLine("Номер: " + record.Id + " Название: " + record.Name + " Дата: " + record.Year + " Режиссер: " + record.Director + " Жанр: " + record.Genre + " Рейтинг: " + record.Graduate);
}
class Movie
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public int Year { get; set; }
    public string? Director { get; set; }
    public string? Genre { get; set; }
    public double Graduate { get; set; }
}