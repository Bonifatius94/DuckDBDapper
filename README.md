
# Lightweight ORM For Loading CSV Files

## Technical Insight

DuckDB provides an IDbConnection to CSV files.
Therefore launch DuckDB in in-memory mode.

```cs
var connection = new DuckDBConnection("Data Source=:memory:");
```

Dapper maps the CSV data to the Location DTO.
Column names are adapted to the C# naming scheme.

```cs
public record Location
{
    public object Address1 { get; set; }
    public object Address2 { get; set; }
    public object City { get; set; }
}

var rows = connection.Query<Location>($"SELECT * FROM './data/Location_v3.csv'").AsList();
```


## Usage

Download CSV file from [here](https://www.ibm.com/docs/en/SSS9AV2/attachments/Sample.Data.v3.zip)
and put the file ``Location_v3.csv`` into a new folder ``data``.

```sh
dotnet run
```

```text
451 24th St N  Birmingham
2436 University Blvd  Tuscaloosa
5 Main St  Nashua
1500 PA-3  Philadelphia
341-349 Oakland St  Morgantown
2100 Willow Run  Wheaton
...
43 Côte de la Fabrique  Ville de Québec
1 Cambridge St  Boston
404 S Limestone  Lexington
260 North Ave  New Rochelle
402w Broadway  San Diego
100-198 N Main St  Bentonville
```
