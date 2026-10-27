using Bibliotheca.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Bibliotheca.Data;

public sealed class SqlLiteraryWorkRepository : ILiteraryWorkRepository
{
    private readonly string _connectionString;

    public SqlLiteraryWorkRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("LiteratureConnection")
            ?? throw new InvalidOperationException("The 'LiteratureConnection' connection string is missing from configuration.");
    }

    public List<LiteraryWork> ListAllWorks()
    {
        var works = new List<LiteraryWork>();

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = new SqlCommand("SELECT * FROM dbo.books ORDER BY Id DESC", connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            works.Add(MapReaderToWork(reader));
        }

        return works;
    }

    public LiteraryWork? FindWorkById(int workId)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = new SqlCommand("SELECT * FROM dbo.books WHERE Id = @Id", connection);
        command.Parameters.Add(new SqlParameter("@Id", workId));

        using var reader = command.ExecuteReader();
        return reader.Read() ? MapReaderToWork(reader) : null;
    }

    public int CreateWork(LiteraryWork work)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string insertSql = @"
            INSERT INTO dbo.books
            (title, author, description, publisher, status, category)
            VALUES
            (@title, @author, @description, @publisher, @status, @category)";

        using var command = new SqlCommand(insertSql, connection);
        command.Parameters.Add(new SqlParameter("@title", (object?)work.WorkTitle ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@author", (object?)work.Writer ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@description", (object?)work.Synopsis ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@publisher", (object?)work.PublishingHouse ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@status", (object?)work.AvailabilityState ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@category", (object?)work.Genre ?? DBNull.Value));

        return command.ExecuteNonQuery();
    }

    public int UpdateWork(LiteraryWork work)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string updateSql = @"
            UPDATE dbo.books SET
                title = @title,
                author = @author,
                description = @description,
                publisher = @publisher,
                status = @status,
                category = @category
            WHERE Id = @Id";

        using var command = new SqlCommand(updateSql, connection);
        command.Parameters.Add(new SqlParameter("@Id", work.RecordId));
        command.Parameters.Add(new SqlParameter("@title", (object?)work.WorkTitle ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@author", (object?)work.Writer ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@description", (object?)work.Synopsis ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@publisher", (object?)work.PublishingHouse ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@status", (object?)work.AvailabilityState ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@category", (object?)work.Genre ?? DBNull.Value));

        return command.ExecuteNonQuery();
    }

    public int DeleteWork(int workId)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = new SqlCommand("DELETE FROM dbo.books WHERE Id = @Id", connection);
        command.Parameters.Add(new SqlParameter("@Id", workId));

        return command.ExecuteNonQuery();
    }

    private static LiteraryWork MapReaderToWork(SqlDataReader reader)
    {
        return new LiteraryWork
        {
            RecordId = reader.GetInt32(reader.GetOrdinal("Id")),
            WorkTitle = IsDbNull(reader, "title") ? null : reader.GetString(reader.GetOrdinal("title")),
            Writer = IsDbNull(reader, "author") ? null : reader.GetString(reader.GetOrdinal("author")),
            Synopsis = IsDbNull(reader, "description") ? null : reader.GetString(reader.GetOrdinal("description")),
            PublishingHouse = IsDbNull(reader, "publisher") ? null : reader.GetString(reader.GetOrdinal("publisher")),
            AvailabilityState = IsDbNull(reader, "status") ? null : reader.GetString(reader.GetOrdinal("status")),
            Genre = IsDbNull(reader, "category") ? null : reader.GetString(reader.GetOrdinal("category")),
        };
    }

    private static bool IsDbNull(SqlDataReader reader, string columnName)
    {
        return reader.IsDBNull(reader.GetOrdinal(columnName));
    }
}
