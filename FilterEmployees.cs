using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class EmployeeAnalysis
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        // 1. Çalışanları filtreleme
        var filtered = employees
            .Where(e => e.Age >= 25 && e.Age <= 40)                  // Yaş aralığı: 25 - 40
            .Where(e => e.Department == "IT" || e.Department == "Finance") // Departman: IT veya Finance
            .Where(e => e.Salary >= 5000 && e.Salary <= 9000)        // Maaş aralığı: 5000 - 9000
            .Where(e => e.HireDate > new DateTime(2017, 1, 1))       // İşe giriş: 2017’den sonra
            .ToList();

        // 2. İsimleri uzunluklarına göre azalan, ardından alfabetik sırayla düzenleme
        var names = filtered
            .Select(e => e.Name)                     // Sadece isimleri al
            .OrderByDescending(n => n.Length)        // Önce uzunluklarına göre azalan sırala
            .ThenBy(n => n)                          // Sonra alfabetik sırala
            .ToList();

        // 3. Sonuç nesnesi oluşturma
        var result = new
        {
            Names = names,                                            // İsimler
            TotalSalary = filtered.Any() ? filtered.Sum(e => e.Salary) : 0, // Toplam maaş
            AverageSalary = filtered.Any() ? Math.Round(filtered.Average(e => e.Salary), 2) : 0, // Ortalama maaş (2 basamak)
            MinSalary = filtered.Any() ? filtered.Min(e => e.Salary) : 0,   // En düşük maaş
            MaxSalary = filtered.Any() ? filtered.Max(e => e.Salary) : 0,   // En yüksek maaş
            Count = filtered.Count                                     // Çalışan sayısı
        };

        // 4. Sonucu JSON formatında döndürme
        return JsonSerializer.Serialize(result);
    }
}
