
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
The CSV column types can be explicitly specified in the
[DuckDB query](https://duckdb.org/docs/data/csv/overview.html) if needed.

```cs
public record Location
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

var rows = connection.Query<Location>("SELECT * FROM './data/Location_v5.csv'").AsList();
```

## Usage

```sh
dotnet run
```

```text
451 24th St N  Birmingham 33,5206608 -86,80249 35203 True
2436 University Blvd  Tuscaloosa 33,2098407 -87,5691735 35401 True
5 Main St  Nashua 42,7653662 -71,467566 3064 True
1500 PA-3  Philadelphia 39,9525839 -75,1652215 19102 True
341-349 Oakland St  Morgantown 39,6478662 -79,966415 26505 True
2100 Willow Run  Wheaton 41,8461274 -88,1378685 60189 True
...
300-308 East St  Woodland 38,679113 -121,7659127 95776 True
3135-3145 Merrimac Ct  Southlake 32,9300384 -97,1997656 76092 True
25-99 Gano St  Cincinnati 39,1031182 -84,5120196 45202 True
2 Brook St  Waukesha 43,013853 -88,2341556 53188 True
207 N 5th St  Artesia 32,843623 -104,40286 88210 True
501-599 E Mills Ave  El Paso 31,7618778 -106,4850217 79901 True
```
