using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class PeopleFilter
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        // XML verisini XDocument ile parse ediyoruz
        var doc = XDocument.Parse(xmlData);

        // "Person" elemanlarını alıp gerekli alanları seçiyoruz
        var query = doc.Descendants("Person")
            .Select(p => new
            {
                Name = (string)p.Element("Name"),                // İsim
                Age = (int)p.Element("Age"),                     // Yaş
                Department = (string)p.Element("Department"),    // Departman
                Salary = (int)p.Element("Salary"),               // Maaş
                HireDate = DateTime.Parse((string)p.Element("HireDate")) // İşe giriş tarihi
            })
            // Filtreleme koşulları:
            .Where(p => p.Age > 30                    // Yaşı 30'dan büyük
                     && p.Department == "IT"          // Departmanı IT olan
                     && p.Salary > 5000               // Maaşı 5000'den büyük
                     && p.HireDate < new DateTime(2019, 1, 1)) // 2019'dan önce işe girenler
            .ToList();

        // JSON'a dönüştürülecek sonuç nesnesini hazırlıyoruz
        var result = new
        {
            Names = query.Select(x => x.Name)   // Sadece isimleri al
                         .OrderBy(n => n)      // Alfabetik sırala
                         .ToList(),

            TotalSalary = query.Any() ? query.Sum(x => x.Salary) : 0, // Toplam maaş
            AverageSalary = query.Any() ? (int)query.Average(x => x.Salary) : 0, // Ortalama maaş
            MaxSalary = query.Any() ? query.Max(x => x.Salary) : 0,   // En yüksek maaş
            Count = query.Count                                       // Kişi sayısı
        };

        // Sonucu JSON formatına çevirip döndür
        return JsonSerializer.Serialize(result);
    }
}
