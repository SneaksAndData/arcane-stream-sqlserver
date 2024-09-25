using System;
using Arcane.Framework.Services.Base;

namespace Arcane.Stream.SqlServer.Models;

public class SqlServerStreamContext : IStreamContext, IStreamContextWriter
{
    /// <summary>
    /// Sql Server connection string.
    /// </summary>
    public string ConnectionString { get; set; }
    
    /// <summary>
    /// Table schema.
    /// </summary>
    public string Schema { get; set; }
    
    /// <summary>
    /// Table name.
    /// </summary>
    public string Table { get; set; }
    
    /// <summary>
    /// Number of rows per parquet rowgroup.
    /// </summary>
    public int RowsPerGroup { get; set; }
    
    /// <summary>
    /// Max time to wait for rowsPerGroup to accumulate.
    /// </summary>
    public TimeSpan GroupingInterval { get; set; }
    
    /// <summary>
    /// Number of row groups per file.
    /// </summary>
    public int GroupsPerFile { get; set; }
    
    /// <summary>
    /// Data location for parquet files.
    /// </summary>
    public string SinkLocation { get; set; }

    /// <summary>
    /// Number of seconds to wait for result before sql commands should time out.
    /// </summary>
    public int CommandTimeout { get; set; }    
    
    /// <inheritdoc cref="IStreamContext.StreamId"/>
    public string StreamId { get; private set; }

    /// <inheritdoc cref="IStreamContext.IsBackfilling"/>
    public bool IsBackfilling { get; private set; }

    /// <inheritdoc cref="IStreamContext.StreamKind"/>
    public string StreamKind { get; private set; }

    /// <inheritdoc cref="IStreamContextWriter.SetStreamId"/>
    public void SetStreamId(string streamId)
    {
        this.StreamId = streamId;
    }

    /// <inheritdoc cref="IStreamContextWriter.SetBackfilling"/>
    public void SetBackfilling(bool isRunningInBackfillMode)
    {
        this.IsBackfilling = isRunningInBackfillMode;
    }

    /// <inheritdoc cref="IStreamContextWriter.SetStreamKind"/>
    public void SetStreamKind(string streamKind)
    {
        this.StreamKind = streamKind;
    }

    public void LoadSecretsFromEnvironment()
    {
        this.ConnectionString = this.GetSecretFromEnvironment("CONNECTIONSTRING");
    }
    
    private string GetSecretFromEnvironment(string secretName)
        => Environment.GetEnvironmentVariable($"{nameof(Arcane)}__{secretName}".ToUpperInvariant());
}
