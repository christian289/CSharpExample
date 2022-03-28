using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using System.IO;
using System.Net.Http;
using SkiaSharp;
using System.Net.Sockets;

namespace ImageDownload
{
    class Program
    {
        const string basePath = @"D:\TempImage";
        const int breakTimeCount = 100;
        const int delay = 15 * 1000;
        const int semaphoreCount = 5;

        static async Task Main(string[] args)
        {
            string filePath = $"EcProduct1123.json";
            string textData = File.ReadAllText(filePath);
            List<Entity> jsonObject = JsonConvert.DeserializeObject<List<Entity>>(textData);
            List<Entity> filteredJsonObject = jsonObject.Where(x => x.Category is not null).ToList();
            IEnumerable<IGrouping<Entity._Category, Entity>> query = from json in filteredJsonObject
                                                                     group json by json.Category into temp
                                                                     select temp;
            HttpClient client = new();
            SemaphoreSlim semaphore = new(semaphoreCount);
            char[] invalidChars = Path.GetInvalidFileNameChars();
            int count100 = default;
            int count = default;
            List<Task> imageDownloadTasks = new();
            Console.WriteLine($"query Count: {query.Count()} 개");

            foreach (IGrouping<Entity._Category, Entity> category in query)
            {
                if (count <= 59208) // 중간에 끊긴 지점부터 시작
                {
                    count++;
                    continue;
                }

                if (++count100 > breakTimeCount)
                {
                    count100 = 1;
                    Console.WriteLine($"쉬는 시간 {delay / 1000}초! 현재시간: {DateTime.Now}");
                    await Task.Delay(delay);
                }

                Console.WriteLine($"현재 Count: {++count}개");
                DirectoryInfo di = new($@"{basePath}\{category.Key.CategoryName.Replace('/', '_')}");

                if (!di.Exists)
                {
                    di.Create();
                }

                imageDownloadTasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        await semaphore.WaitAsync();

                        foreach (Entity entity in category)
                        {
                            if (entity.Name.IndexOfAny(invalidChars) != -1) // Windows에서 파일명 또는 폴더명에 포함될 수 없는 문자열 변환.
                            {
                                foreach (char invalidChar in invalidChars)
                                {
                                    entity.Name = entity.Name.Replace(invalidChar, '_');
                                }
                            }

                            foreach (Entity._Image image in entity.Images.Where(image => image.Type == Type.BODY))
                            {
                                Uri uri = image.Url;

                                if (uri.Port != 80) continue; // HTTP 아닌 URI는 걸러냄.

                                string fileName = Path.GetFileName(uri.ToString());

                                if (fileName.IndexOfAny(invalidChars) != -1) // Windows에서 파일명 또는 폴더명에 포함될 수 없는 문자열 변환.
                                {
                                    foreach (char invalidChar in invalidChars)
                                    {
                                        fileName = fileName.Replace(invalidChar, '_');
                                    }
                                }

                                if (!(Path.GetExtension(fileName).ToLower() == ".png" || Path.GetExtension(fileName).ToLower() == ".jpg")) continue; // 이 두 타입 이외에는 딥러닝 학습 라벨링 이미지 모으는데 방해되는 확장자명

                                HttpResponseMessage check;

                                try
                                {
                                    check = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri));
                                }
                                catch (HttpRequestException ex) when (ex.InnerException is SocketException exception && exception.SocketErrorCode == SocketError.HostNotFound)
                                {
                                    // 알려진 호스트가 없습니다.
                                    Console.WriteLine($"HostNotFound: {uri}");

                                    continue;
                                }
                                catch (HttpRequestException ex) when (ex.InnerException is SocketException exception && exception.SocketErrorCode == SocketError.TimedOut)
                                {
                                    // 연결된 구성원으로부터 응답이 없어 연결하지 못했거나, 호스트로부터 응답이 없어 연결이 끊어졌습니다.
                                    Console.WriteLine($"TimedOut: {uri}");

                                    continue;
                                }
                                catch (HttpRequestException ex) when (ex.InnerException is SocketException exception && exception.SocketErrorCode == SocketError.NoData)
                                {
                                    Console.WriteLine($"NoData: {uri}");

                                    continue;
                                }

                                if (!check.IsSuccessStatusCode) continue;

                                byte[] downloadByte = await client.GetByteArrayAsync(uri);

                                if (downloadByte is null || downloadByte.Length <= 100000) continue; // 너무 짧은 이미지는 필터링하기 위함.

                                try
                                {
                                    //using MemoryStream ms = new(downloadByte);
                                    //using SKManagedStream skStream = new(ms, false);
                                    //using SKData data = SKData.Create(skStream);
                                    //using SKCodec codec = SKCodec.Create(skStream);
                                    //using SKBitmap bitmap = SKBitmap.Decode(codec);
                                    //using SKImage skImage = SKImage.FromBitmap(bitmap);
                                    using SKImage skImage = SKImage.FromEncodedData(downloadByte);

                                    if (skImage is null) continue; // 이상하게도 byte 값이 비어있지 않아도 null이 발생할 수 있음.

                                    using FileStream fs = File.OpenWrite($@"{di.FullName}\{entity.Name.Replace("/", "_")}_{fileName}");

                                    if (Path.GetExtension(fileName).ToLower() == ".png")
                                    {
                                        SKData data = skImage.Encode(SKEncodedImageFormat.Png, 100);

                                        if (data is null) continue;

                                        data.SaveTo(fs);
                                    }
                                    else if (Path.GetExtension(fileName).ToLower() == ".jpg")
                                    {
                                        SKData data = skImage.Encode(SKEncodedImageFormat.Jpeg, 100);

                                        if (data is null) continue;

                                        data.SaveTo(fs);
                                    }
                                }
                                catch (IOException ex)
                                {
                                    // 다른 프로세스에서 사용 중이 간혹 발생.
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));

                await Task.WhenAll(imageDownloadTasks);

                // 일반적으로 Task는 해제할 필요는 없다고 함.
                //foreach (Task task in imageDownloadTasks)
                //{
                //    task.Dispose();
                //}

                imageDownloadTasks.Clear();
            }
        }
    }
}
