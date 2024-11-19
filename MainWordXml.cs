using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string docxFilePath = args.Length > 0 ? args[0] : "技100-999.docx";

        // スクリプトのディレクトリに移動
        string scriptDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        Directory.SetCurrentDirectory(scriptDir);

        if (!File.Exists(docxFilePath))
        {
            Console.Error.WriteLine($"The specified file path does not exist: {docxFilePath}");
            Environment.Exit(1);
        }

        // 使用例
        string personCsvFilePath = "personInfo.csv";
        string roleSettingsCsvFilePath = "roleSettings.csv";
        string documentSettingsCsvFilePath = "documentSettings.csv";
        string signatureTableCsvFilePath = "signatureTable.csv";

        // PersonInfoManagerのインスタンスを作成
        var personInfoManager = new PersonInfoManager(personCsvFilePath);
        var customAttributes = personInfoManager.GetCustomAttributes();

        // RoleSettingsManagerのインスタンスを作成
        var roleSettingsManager = new RoleSettingsManager(roleSettingsCsvFilePath);

        // DocumentSettingsManagerのインスタンスを作成
        var documentSettingsManager = new DocumentSettingsManager(documentSettingsCsvFilePath);

        // SignatureTableManagerのインスタンスを作成
        var signatureTableManager = new SignatureTableManager(signatureTableCsvFilePath);

        // HankoDocumentのインスタンスを作成
        var hankoDocument = new HankoDocument(docxFilePath);
        hankoDocument.LoadFromDocument();
        hankoDocument.ApplyCustomAttributes(customAttributes, roleSettingsManager, documentSettingsManager);

        // ここに他の処理を追加
    }
}