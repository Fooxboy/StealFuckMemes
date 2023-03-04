using StealFuckMemes.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;


namespace StealFuckMemes
{
    public class StealMemesService
    {
        private const string _urlApi = "https://api.meme-arsenal.com/api/templates-share?";
        private const int _pages = 3;
        private int _currentPage;
        private const string _pathToDownload = "D:\\FuckMemes\\";

        private int _counter = 0;

        public async Task StartSteal()
        {
            Console.WriteLine("Загрузка json файла...");

            _currentPage = 1;

            while(_currentPage != _pages)
            {
                using (var mainClinet = new HttpClient())
                {
                    var json = await mainClinet.GetStringAsync(GetUrlAdress(_currentPage));

                    var memesTemplates = JsonSerializer.Deserialize<RootItem>(json);

                    Console.WriteLine("Json файл загружен.");

                    Parallel.ForEach(memesTemplates.Data, new ParallelOptions() { MaxDegreeOfParallelism = 5}, async (meme) =>
                    {
                        using (var client = new HttpClient())
                        {
                            try
                            {
                                var fileName = $"{Guid.NewGuid()}.jpg";

                                var response = await client.GetAsync(meme.Url);
                                byte[] fileData = await response.Content.ReadAsByteArrayAsync();

                                File.WriteAllBytes(_pathToDownload + fileName, fileData);

                                Console.WriteLine($"Загружен файл {fileName}");

                                _counter++;
                                Console.Title = $"Загружено мемов: {_counter}";
                            }catch(Exception ex)
                            {
                                Thread.Sleep(1000);
                                Console.ForegroundColor= ConsoleColor.Red;
                                Console.WriteLine($"Ошибка загрузки: {ex.Message}");
                                Console.ResetColor();
                            }
                           
                        }
                    });

                }

                _currentPage++;
            }
           
        }

        private string GetUrlAdress(int page) => _urlApi + $"page={page}&items_on_page=3000&sort=popular&type=all&lang=ru";
    }
}
