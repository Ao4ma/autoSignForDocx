using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public class HankoLayout
{
    public string RoleName { get; set; }
    public string PersonName { get; set; }
    public string StampDate { get; set; }
    public Dictionary<string, object> Position { get; set; }
    public Dictionary<string, object> Size { get; set; }
    public Dictionary<string, object> Font { get; set; }
    public bool WrapText { get; set; }
    public string StampColor { get; set; }
    public Dictionary<string, object> RoleFont { get; set; }
    public string RolePosition { get; set; }
    public string StampType { get; set; }
    public string ImageUrl { get; set; }
    public string Layout { get; set; }
    public Dictionary<string, object> DefaultFont { get; set; }

    public HankoLayout(string roleName, Dictionary<string, object> position, Dictionary<string, object> size, Dictionary<string, object> roleFont, string rolePosition, string layout, Dictionary<string, object> defaultFont)
    {
        RoleName = roleName;
        Position = position;
        Size = size;
        RoleFont = roleFont;
        RolePosition = rolePosition;
        Layout = layout;
        DefaultFont = defaultFont;
        PersonName = "";
        StampDate = "";
        Font = new Dictionary<string, object> { { "name", "" }, { "size", 0 } };
        WrapText = false;
        StampColor = "";
        StampType = "";
        ImageUrl = "";
    }

    public void WriteToCsv(string filePath)
    {
        var csvData = new List<dynamic>
        {
            new
            {
                RoleName,
                PersonName,
                StampDate,
                Position,
                Size,
                Font,
                WrapText,
                StampColor,
                RoleFont,
                RolePosition,
                StampType,
                ImageUrl,
                Layout,
                DefaultFont
            }
        };

        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.WriteRecords(csvData);
        }
    }
}