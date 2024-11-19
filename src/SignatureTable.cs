using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public class SignatureTable
{
    public string DocumentFormat { get; set; }
    public List<string> Roles { get; set; }

    public SignatureTable(string documentFormat, List<string> roles)
    {
        DocumentFormat = documentFormat;
        Roles = roles;
    }
}

public class SignatureTableManager
{
    public List<SignatureTable> SignatureTables { get; set; }

    public SignatureTableManager(string csvFilePath)
    {
        SignatureTables = new List<SignatureTable>();
        ReadFromCsv(csvFilePath);
    }

    public void ReadFromCsv(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            var records = csv.GetRecords<dynamic>();
            foreach (var record in records)
            {
                var roles = new List<string>(record.Roles.Split(','));
                var signatureTable = new SignatureTable(record.DocumentFormat, roles);
                SignatureTables.Add(signatureTable);
            }
        }
    }

    public void WriteToCsv(string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            var csvData = new List<dynamic>();
            foreach (var table in SignatureTables)
            {
                csvData.Add(new
                {
                    DocumentFormat = table.DocumentFormat,
                    Roles = string.Join(",", table.Roles)
                });
            }
            csv.WriteRecords(csvData);
        }
    }
}