using System;
using System.Collections.Generic;
using System.Text.Json;

public static class VowelSubsequenceFinder
{
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        // Eğer liste boş ya da null ise boş bir liste döndür
        if (words == null || words.Count == 0)
            return JsonSerializer.Serialize(new List<object>());

        // Sesli harfleri tanımlıyoruz (küçük harf olarak)
        HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

        // Her kelimenin sonucunu saklayacağımız liste
        var results = new List<object>();

        // Listedeki her kelime için işlemleri yap
        foreach (var word in words)
        {
            string longest = ""; // En uzun sesli alt diziyi (substring) saklayacak
            string current = ""; // O anki sesli harfleri geçici olarak saklayacak

            // Kelimenin her harfini küçük harfe çevirip kontrol et
            foreach (char c in word.ToLower())
            {
                if (vowels.Contains(c)) // Eğer sesli harfse
                {
                    current += c; // O anki dizinin sonuna ekle
                    if (current.Length > longest.Length) // Eğer mevcut daha uzunsa
                        longest = current; // En uzun olanı güncelle
                }
                else
                {
                    // Sessiz harf geldiğinde o anki sesli dizi sıfırlanır
                    current = "";
                }
            }

            // Her kelime için kelime + en uzun sesli alt dizi + uzunluğunu listeye ekle
            results.Add(new
            {
                word = word,
                sequence = longest,
                length = longest.Length
            });
        }

        // Bütün sonuçları JSON formatına çevirip döndür
        return JsonSerializer.Serialize(results);
    }
}
