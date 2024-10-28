
# Lightweight ORM For Loading CSV Files

## Technical Insight

In this example, DuckDB doesn't need to persist any data.
It will just read some static CSV files from disk.
Therefore DuckDB is launched in memory mode without a database file.

```cs
var connection = new DuckDBConnection("Data Source=:memory:");
```

DuckDB provides direct access to CSV files via the SELECT query syntax
which is commonly used to mock fake databases with CSV files, e.g. for testing.
This can be leveraged as a .NET driver to load CSV files via an IDbConnection.
Given the connection and CSV headers, Dapper then maps the CSV rows to DTOs.
The CSV column types can be explicitly specified in the DuckDB query if needed.

```cs
public record Location
{
    public object Address1 { get; set; }
    public object Address2 { get; set; }
    public object City { get; set; }
}

var rows = connection.Query<Location>("SELECT * FROM './data/Location_v3.csv'").AsList();
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
