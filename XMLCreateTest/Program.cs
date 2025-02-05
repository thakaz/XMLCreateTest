using System;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

string fileName = "complex_output.xml";

XmlWriterSettings settings = new XmlWriterSettings();
settings.Indent = true;

using (var context = new CompanyContext())
{
    var employees = context.Employees
        .Include(e => e.住所)
        .Include(e => e.プロジェクト)
        .ToList();

    using (XmlWriter xmlWriter = XmlWriter.Create(fileName, settings))
    {
        xmlWriter.WriteStartDocument();
        xmlWriter.WriteStartElement("会社");

        foreach (var employee in employees)
        {
            xmlWriter.WriteStartElement("従業員");
            xmlWriter.WriteElementString("ID", employee.ID.ToString());
            xmlWriter.WriteElementString("名前", employee.名前);
            xmlWriter.WriteElementString("年齢", employee.年齢.ToString());
            xmlWriter.WriteStartElement("住所");
            xmlWriter.WriteElementString("通り", employee.住所.通り);
            xmlWriter.WriteElementString("市", employee.住所.市);
            xmlWriter.WriteElementString("州", employee.住所.州);
            xmlWriter.WriteElementString("郵便番号", employee.住所.郵便番号);
            xmlWriter.WriteEndElement(); // 住所
            xmlWriter.WriteStartElement("プロジェクト");
            foreach (var project in employee.プロジェクト)
            {
                xmlWriter.WriteStartElement("プロジェクト詳細");
                xmlWriter.WriteElementString("名前", project.名前);
                xmlWriter.WriteElementString("期間", project.期間);
                xmlWriter.WriteEndElement(); // プロジェクト詳細
            }
            xmlWriter.WriteEndElement(); // プロジェクト
            xmlWriter.WriteEndElement(); // 従業員
        }

        xmlWriter.WriteEndElement(); // 会社
        xmlWriter.WriteEndDocument();
    }
}

// OSの標準的な機能でXMLファイルを開く
Process.Start(new ProcessStartInfo
{
    FileName = fileName,
    UseShellExecute = true
});
