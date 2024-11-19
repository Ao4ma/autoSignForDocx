using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public class PersonInfo
{
    public string RoleName { get; set; }
    public string PersonName { get; set; }
    public string StampDate { get; set; }

    public PersonInfo(string roleName, string personName, string stampDate)
    {
        RoleName = roleName;
        PersonName = personName;
        StampDate = stampDate;
    }
}

public class PersonInfoManager
{
    public List<PersonInfo> PersonInfos { get; set; }

    public PersonInfoManager(string csvFilePath)
    {
        PersonInfos = new List<PersonInfo>();
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
                var personInfo = new PersonInfo(record.RoleName, record.PersonName, record.StampDate);
                PersonInfos.Add(personInfo);
            }
        }
    }

    public void WriteToCsv(string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.WriteRecords(PersonInfos);
        }
    }

    public List<dynamic> GetCustomAttributes()
    {
        // カスタム属性を取得する処理を実装
        return new List<dynamic>();
    }
}