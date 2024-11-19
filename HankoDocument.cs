using System;
using System.Collections.Generic;
using System.Linq;

public class HankoLayout
{
    public string RoleName { get; set; }
    public string PersonName { get; set; }
    public DateTime StampDate { get; set; }
    public Dictionary<string, object> Font { get; set; }
    public string StampColor { get; set; }
    public Dictionary<string, object> DefaultFont { get; set; }
}

public class HankoDocument
{
    public string FilePath { get; set; }
    public List<HankoLayout> HankoLayouts { get; set; }

    public HankoDocument(string filePath)
    {
        FilePath = filePath;
        HankoLayouts = new List<HankoLayout>();
    }

    public void LoadFromDocument()
    {
        // ドキュメントからハンコレイアウトを読み込む処理を実装
        // 例: HankoLayouts にハンコレイアウトを追加
    }

    public void ApplyCustomAttributes(List<dynamic> customAttributes, RoleSettingsManager roleSettingsManager, DocumentSettingsManager documentSettingsManager)
    {
        foreach (var hanko in HankoLayouts)
        {
            var personInfo = customAttributes.FirstOrDefault(attr => attr.RoleName == hanko.RoleName);
            if (personInfo != null)
            {
                hanko.PersonName = personInfo.PersonName;
                hanko.StampDate = personInfo.StampDate;

                var docSettings = documentSettingsManager.GetDocumentSettings(FilePath, hanko.RoleName);
                if (docSettings != null)
                {
                    hanko.Font = docSettings.Font;
                    hanko.StampColor = docSettings.StampColor;
                }
                else
                {
                    var roleSettings = roleSettingsManager.GetRoleSettings(hanko.RoleName);
                    if (roleSettings != null)
                    {
                        hanko.Font = roleSettings.Font;
                        hanko.StampColor = roleSettings.StampColor;
                    }
                    else
                    {
                        hanko.Font = hanko.DefaultFont;
                    }
                }
            }
        }
    }
}