using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public class DocumentSettings
{
    public string DocumentName { get; set; }
    public string RoleName { get; set; }
    public Dictionary<string, object> Font { get; set; }
    public string StampColor { get; set; }

    public DocumentSettings(string documentName, string roleName, Dictionary<string, object> font, string stampColor)
    {
        DocumentName = documentName;
        RoleName = roleName;
        Font = font;
        StampColor = stampColor;
    }
}

public class DocumentSettingsManager
{
    public List<DocumentSettings> DocumentSettings { get; set; }

    public DocumentSettingsManager(string csvFilePath)
    {
        DocumentSettings = new List<DocumentSettings>();
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
                var font = new Dictionary<string, object>
                {
                    { "name", record.FontName },
                    { "size", int.Parse(record.FontSize) }
                };
                var docSettings = new DocumentSettings(record.DocumentName, record.RoleName, font, record.StampColor);
                DocumentSettings.Add(docSettings);
            }
        }
    }

    public void WriteToCsv(string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.WriteRecords(DocumentSettings);
        }
    }
}