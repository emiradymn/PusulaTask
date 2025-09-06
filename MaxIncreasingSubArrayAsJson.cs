using System;
using System.Collections.Generic;
using System.Text.Json;

public static class SubarrayFinder
{
    public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
    {
        // Eğer liste boş ya da null ise, direkt boş bir JSON [] döndürüyorum
        if (numbers == null || numbers.Count == 0)
            return JsonSerializer.Serialize(new List<int>());

        // İlk elemanı başlangıç olarak hem current (geçici) hem de max (en iyi) alt diziye ekliyorum
        List<int> currentSubarray = new List<int> { numbers[0] };
        List<int> maxSubarray = new List<int>(currentSubarray);

        // Toplamları takip etmek için değişkenler
        int currentSum = numbers[0]; // Şu anki alt dizinin toplamı
        int maxSum = numbers[0];     // Şimdiye kadarki en yüksek toplam

        // Listedeki diğer elemanlara sırayla bakıyorum
        for (int i = 1; i < numbers.Count; i++)
        {
            // Eğer önceki sayıdan büyükse artış devam ediyor demektir
            if (numbers[i] > numbers[i - 1])
            {
                // Alt diziye ekliyorum ve toplamı güncelliyorum
                currentSubarray.Add(numbers[i]);
                currentSum += numbers[i];
            }
            else
            {
                // Artış bozulduğunda: önce mevcut alt dizinin toplamını kontrol ediyorum
                if (currentSum > maxSum)
                {
                    // Eğer mevcut toplam daha büyükse max alt dizi olarak güncelliyorum
                    maxSum = currentSum;
                    maxSubarray = new List<int>(currentSubarray);
                }
                // Yeni bir alt dizi başlatıyorum (sadece o elemanla)
                currentSubarray = new List<int> { numbers[i] };
                currentSum = numbers[i];
            }
        }

        // Döngü bitince son alt diziyi de kontrol etmem gerekiyor
        if (currentSum > maxSum)
        {
            maxSubarray = new List<int>(currentSubarray);
        }

        // En büyük toplamı veren alt diziyi JSON formatında string olarak döndürüyorum
        return JsonSerializer.Serialize(maxSubarray);
    }
}
