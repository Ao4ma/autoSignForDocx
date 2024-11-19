using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public class RoleSettings
{
    public string RoleName { get; set; }
    public Dictionary<string, object> Font { get; set; }
    public string StampColor { get; set; }

    public RoleSettings(string roleName, Dictionary<string, object> font, string stampColor)
    {
        RoleName = roleName;
        Font = font;
        StampColor = stampColor;
    }
}

public class RoleSettingsManager
{
    public List<RoleSettings> RoleSettings { get; set; }

    public RoleSettingsManager(string csvFilePath)
    {
        RoleSettings = new List<RoleSettings>();
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
                var roleSettings = new RoleSettings(record.RoleName, font, record.StampColor);
                RoleSettings.Add(roleSettings);
            }
        }
    }

    public void WriteToCsv(string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.WriteRecords(RoleSettings);
        }
    }

    public RoleSettings GetRoleSettings(string roleName)
    {
        // RoleSettingsを取得する処理を実装
        return RoleSettings.Find(role => role.RoleName == roleName);
    }
}