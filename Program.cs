
// dotnet add package Dapper
// dotnet add package DuckDB.NET.Data.Full
using Dapper;
using DuckDB.NET.Data;

// https://www.ibm.com/docs/en/SSS9AV2/attachments/Sample.Data.v3.zip
var locations = Query<Location>("./data/Location_v3.csv");
foreach (var l in locations)
    Console.WriteLine($"{l.Address1} {l.Address2} {l.City}");

static List<T> Query<T>(string csvFile)
    => new DuckDBConnection("Data Source=:memory:")
        .Query<T>($"SELECT * FROM '{csvFile}'").AsList();

public record Location
{
    // info: Dapper fits column names from CSV header.
    //       First attribute char can be upper or lower case.
    public object Address1 { get; set; }
    public object Address2 { get; set; }
    public object City { get; set; }
}
