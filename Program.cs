
// dotnet add package Dapper
// dotnet add package DuckDB.NET.Data.Full
using Dapper;
using DuckDB.NET.Data;

// https://www.ibm.com/docs/en/SSS9AV2/attachments/Sample.Data.v3.zip

var locations = Query<Location>("./data/Location_v3.csv");
foreach (var l in locations)
    Console.WriteLine($"{l.Address1} {l.Address2} {l.City}");

Console.WriteLine("================================");

// info: Location_v5.csv was modified to test if all relevant data types can be mapped
var locationsTyped = Query<LocationTyped>("./data/Location_v5.csv");
foreach (var l in locationsTyped)
    Console.WriteLine($"{l.Address1} {l.Address2} {l.City} {l.Latitude} "
        + $"{l.Longitude} {l.PostalCode} {l.IncludeInCorrelation}");

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

public record LocationTyped
{
    // info: Dapper fits column names from CSV header.
    //       First attribute char can be upper or lower case.
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public int PostalCode { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IncludeInCorrelation { get; set; }
}
