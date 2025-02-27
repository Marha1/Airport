using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Airport.Notification
{
    public class Notificat
    {
        private readonly string apiUrl = "https://ttsmp3.com/makemp3_ai.php";
        private readonly string speaker = "Alloy(Female)"; // Указание спикера
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1); // Механизм синхронизации
        private IWavePlayer waveOut;
        private AudioFileReader audioFile;

        // Метод для преобразования текста в речь
        private async Task<string> Speak(string textToConvert)
        {
            using (HttpClient client = new HttpClient())
            {
                for (int attempt = 0; attempt < 3; attempt++) // Три попытки
                {
                    try
                    {
                        var postData = new FormUrlEncodedContent(new[]
                        {
                    new KeyValuePair<string, string>("msg", textToConvert),
                    new KeyValuePair<string, string>("speed", "1"),
                    new KeyValuePair<string, string>("Speaker", speaker)
                });

                        HttpResponseMessage response = await client.PostAsync(apiUrl, postData);
                        response.EnsureSuccessStatusCode();

                        string responseContent = await response.Content.ReadAsStringAsync();
                        var json = JsonDocument.Parse(responseContent);
                        string mp3Url = json.RootElement.GetProperty("URL").GetString();

                        if (!string.IsNullOrEmpty(mp3Url))
                        {
                            HttpResponseMessage mp3Response = await client.GetAsync(mp3Url);
                            mp3Response.EnsureSuccessStatusCode();
                            string savePath = "output.mp3";

                            await using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                            {
                                await mp3Response.Content.CopyToAsync(fs);
                            }

                            return savePath;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Попытка {attempt + 1} не удалась: {ex.Message}");
                    }
                }
            }
            return null;
        }



        // Метод для воспроизведения музыки
        public async Task PlayMusic()
        {
            await semaphore.WaitAsync(); // Блокируем выполнение других задач
            try
            {
                // Освобождаем старые ресурсы, если они есть
                waveOut?.Dispose();
                audioFile?.Dispose();

                waveOut = new WaveOutEvent();
                audioFile = new AudioFileReader("zvuk1.mp3");
                waveOut.Init(audioFile);
                waveOut.Play();

                // Ожидаем окончания воспроизведения
                while (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    await Task.Delay(100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при воспроизведении музыки: {ex.Message}");
            }
            finally
            {
                semaphore.Release(); // Разблокируем выполнение
            }
        }

        // Метод для воспроизведения объявления о посадке
        public async Task OnBoarding(string number)
        {
            await semaphore.WaitAsync(); // Блокируем выполнение других задач
            try
            {
                // Освобождаем старые ресурсы, если они есть
                waveOut?.Dispose();
                audioFile?.Dispose();

                string filePath = await Speak($"Объявляется посадка на рейс номер {number}");
                if (!string.IsNullOrEmpty(filePath))
                {
                    waveOut = new WaveOutEvent();
                    audioFile = new AudioFileReader(filePath);
                    waveOut.Init(audioFile);
                    waveOut.Play();

                    // Ожидаем окончания воспроизведения
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        await Task.Delay(100);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при воспроизведении объявления о посадке: {ex.Message}");
            }
            finally
            {
                semaphore.Release(); // Разблокируем выполнение
            }
        }

        // Метод для воспроизведения сообщения о переносе рейса
        public async Task Perenos(string number, string time)
        {
            await semaphore.WaitAsync(); // Блокируем выполнение других задач
            try
            {
                // Освобождаем старые ресурсы, если они есть
                waveOut?.Dispose();
                audioFile?.Dispose();

                string filePath = await Speak($"Рейс номер {number} задержан на {time} часа");
                if (!string.IsNullOrEmpty(filePath))
                {
                    waveOut = new WaveOutEvent();
                    audioFile = new AudioFileReader(filePath);
                    waveOut.Init(audioFile);
                    waveOut.Play();

                    // Ожидаем окончания воспроизведения
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        await Task.Delay(100);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при воспроизведении сообщения о переносе: {ex.Message}");
            }
            finally
            {
                semaphore.Release(); // Разблокируем выполнение
            }
        }
        public async Task Zader(string text)
        {
            await semaphore.WaitAsync(); // Блокируем выполнение других задач
            try
            {
                // Освобождаем старые ресурсы, если они есть
                waveOut?.Dispose();
                audioFile?.Dispose();

                string filePath = await Speak($"{text}");
                if (!string.IsNullOrEmpty(filePath))
                {
                    waveOut = new WaveOutEvent();
                    audioFile = new AudioFileReader(filePath);
                    waveOut.Init(audioFile);
                    waveOut.Play();

                    // Ожидаем окончания воспроизведения
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        await Task.Delay(100);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при воспроизведении объявления о посадке: {ex.Message}");
            }
            finally
            {
                semaphore.Release(); // Разблокируем выполнение
            }
        }
    }
}
