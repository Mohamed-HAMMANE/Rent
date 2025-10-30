using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
var cs = builder.Configuration.GetConnectionString("DefaultConnection")!;
var app = builder.Build();
app.UseCors();

app.UseHttpsRedirection();

app.MapGet("/bachelorpad", async () =>
{
    var list = new List<BachelorPadDto>();
    await using var con = new SqlConnection(cs);
    await con.OpenAsync();

    const string sql = @"
        SELECT 
            Id,
            Link,
            Status,
            Address,
            Price,
            Quality,
            Location,
            Aesthetics,
            Furniture,
            Phone,
            Observation,
            ChatLink,
            Mark
        FROM BachelorPadView ORDER BY Mark DESC;";

    await using var cmd = new SqlCommand(sql, con);
    await using var rd = await cmd.ExecuteReaderAsync();
    while (await rd.ReadAsync())
    {
        var id = rd.GetInt32(0);
        list.Add(new BachelorPadDto(
            Id: id,
            ImageUrl: $"/bachelorpad/{id}/image",
            Link: rd.GetString(1),
            Status: rd.GetString(2),
            Address: rd.IsDBNull(3) ? null : rd.GetString(3),
            Price: rd.GetInt32(4),
            Quality: rd.GetInt32(5),
            Location: rd.GetInt32(6),
            Aesthetics: rd.GetInt32(7),
            Furniture: rd.GetInt32(8),
            Phone: rd.IsDBNull(9) ? null : rd.GetString(9),
            Observation: rd.IsDBNull(10) ? null : rd.GetString(10),
            ChatLink: rd.IsDBNull(11) ? null : rd.GetString(11),
            Mark: Convert.ToDecimal(rd.GetValue(12))
        ));
    }

    return Results.Ok(list);
});

app.MapGet("/bachelorpad/{id:int}/image", async (int id) =>
{
    await using var con = new SqlConnection(cs);
    await con.OpenAsync();

    await using var cmd = new SqlCommand("SELECT Image FROM BachelorPad WHERE Id = @id;", con);
    cmd.Parameters.AddWithValue("@id", id);

    if (await cmd.ExecuteScalarAsync() is not byte[] bytes || bytes.Length == 0) return Results.NotFound();

    return Results.File(bytes, "image/jpeg");
});

app.MapPut("/bachelorpad/{id:int}", async (int id, BachelorPadUpdateDto dto) =>
{
    // Minimal sanity checks (personal use)
    if (dto.Price < 0 || dto.Quality < 0 || dto.Location < 0 || dto.Aesthetics < 0 || dto.Furniture < 0)
        return Results.BadRequest("Numeric fields must be non-negative.");

    await using var con = new SqlConnection(cs);
    await con.OpenAsync();

    const string sql = @"
        UPDATE BachelorPad
        SET
            Price = @Price,
            Quality = @Quality,
            Location = @Location,
            Aesthetics = @Aesthetics,
            Furniture = @Furniture,
            Phone = @Phone,
            Observation = @Observation
        WHERE Id = @Id;";

    await using var cmd = new SqlCommand(sql, con);
    cmd.Parameters.AddWithValue("@Id", id);
    cmd.Parameters.AddWithValue("@Price", dto.Price);
    cmd.Parameters.AddWithValue("@Quality", dto.Quality);
    cmd.Parameters.AddWithValue("@Location", dto.Location);
    cmd.Parameters.AddWithValue("@Aesthetics", dto.Aesthetics);
    cmd.Parameters.AddWithValue("@Furniture", dto.Furniture);

    // Handle nulls explicitly
    var pPhone = cmd.Parameters.Add("@Phone", System.Data.SqlDbType.NVarChar, 50);
    pPhone.Value = (object?)dto.Phone ?? DBNull.Value;

    var pObs = cmd.Parameters.Add("@Observation", System.Data.SqlDbType.NVarChar, -1); // -1 = NVARCHAR(MAX)
    pObs.Value = (object?)dto.Observation ?? DBNull.Value;

    var affected = await cmd.ExecuteNonQueryAsync();
    return affected == 1 ? Results.NoContent() : Results.NotFound();
});

app.MapPatch("/bachelorpad/{id:int}/status", async (int id, StatusChangeDto dto) =>
{
    var allowedStatuses = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Available","Reserved","NoResponse","NoShow","Unreliable","NotInterested","Closed" };
    if (string.IsNullOrWhiteSpace(dto.Status) || !allowedStatuses.Contains(dto.Status))
        return Results.BadRequest("Invalid status.");

    await using var con = new SqlConnection(cs);
    await con.OpenAsync();

    const string sql = @"
        UPDATE BachelorPad
        SET Status = @Status,
            Observation = @Observation
        WHERE Id = @Id;";

    await using var cmd = new SqlCommand(sql, con);
    cmd.Parameters.AddWithValue("@Id", id);
    cmd.Parameters.AddWithValue("@Status", dto.Status);

    var pObs = cmd.Parameters.Add("@Observation", System.Data.SqlDbType.NVarChar, -1);
    pObs.Value = (object?)dto.Observation ?? DBNull.Value;

    var affected = await cmd.ExecuteNonQueryAsync();
    return affected == 1 ? Results.NoContent() : Results.NotFound();
});



app.Run();


internal sealed record StatusChangeDto(string Status, string? Observation);

internal record BachelorPadDto(
    int Id,
    string ImageUrl,
    string Link,
    string? Address,
    string Status,
    int Price,
    int Quality,
    int Location,
    int Aesthetics,
    int Furniture,
    string? Phone,
    string? Observation,
    string? ChatLink,
    decimal Mark
);

internal sealed record BachelorPadUpdateDto(
    int Price,
    string Status,
    int Quality,
    int Location,
    int Aesthetics,
    int Furniture,
    string? Phone,
    string? Observation
);
