using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Text.Json;
using ZXing.Common;
using ZXing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ChemicalLaboratory.Application.UseCases.Services
{
    public record QrReagentData(int Id, decimal Quantity);

    public class QrDecoderService
    {
        // public async Task<List<QrReagentData>> DecodeReagentQrCodesAsync(List<Stream> imageStreams)
        // {
        //     var resultList = new List<QrReagentData>();

        //     // var reader = new ZXing.ImageSharp.BarcodeReader<Rgba32>()
        //     // {
        //     //     AutoRotate = true,
        //     //     Options = new ZXing.Common.DecodingOptions
        //     //     {
        //     //         PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE },
        //     //         TryHarder = true
        //     //     }
        //     // };

        //     var reader = new ZXing.ImageSharp.BarcodeReader<Rgba32>(
        //         null, 
        //         null, 
        //         rgbs => new HybridBinarizer(rgbs) // КРИТИЧЕСКИ ВАЖНО: Используем адаптивный гибридный бинаризатор
        //     )
        //     {
        //         AutoRotate = true, // Помогает, если фото перевернуто
        //         Options = new DecodingOptions
        //         {
        //             PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE },
        //             TryHarder = true, // Максимально детальный анализ пикселей
        //             PureBarcode = false // Ставим false, чтобы движок искал маркеры QR в пространстве, даже если рамка обрезана
        //         }
        //     };

        //     foreach (var stream in imageStreams)
        //     {
        //         try
        //         {
        //             if (stream == null) continue;

        //             if (stream.CanSeek)
        //             {
        //                 stream.Position = 0; 
        //             }

        //             // Загружаем картинку через ImageSharp
        //             using var image = await Image.LoadAsync<Rgba32>(stream);

        //             // Пытаемся декодировать QR-код
        //             var result = reader.Decode(image);

        //             if (result != null && !string.IsNullOrEmpty(result.Text))
        //             {
        //                 // Десериализуем данные из текста QR-кода
        //                 var data = JsonSerializer.Deserialize<QrReagentData>(result.Text, new JsonSerializerOptions
        //                 {
        //                     PropertyNameCaseInsensitive = true
        //                 });

        //                 if (data != null)
        //                 {
        //                     resultList.Add(data);
        //                 }
        //             }
        //         }
        //         catch (Exception)
        //         {
        //         }
        //     }

        //     return resultList;
        // }

         public async Task<List<QrReagentData>> DecodeReagentQrCodesAsync(List<Stream> imageStreams)
        {
            var resultList = new List<QrReagentData>();
            
            // Архитектурное исправление: Используем напрямую низкоуровневый MultiFormatReader ядра ZXing.
            // У него нет дженериков, и метод decode() принимает исключительно BinaryBitmap.
            var reader = new MultiFormatReader();

            reader.Hints = new Dictionary<DecodeHintType, object>
            {
                { DecodeHintType.POSSIBLE_FORMATS, new List<BarcodeFormat> { BarcodeFormat.QR_CODE } },
                { DecodeHintType.TRY_HARDER, true }
            };

            foreach (var stream in imageStreams)
            {
                try
                {
                    if (stream.CanSeek) stream.Position = 0;

                    using var image = await Image.LoadAsync<Rgba32>(stream);
                    
                    byte[] luminanceMatrix = new byte[image.Width * image.Height];
                    int index = 0;

                    for (int y = 0; y < image.Height; y++)
                    {
                        for (int x = 0; x < image.Width; x++)
                        {
                            Rgba32 pixel = image[x, y];
                            luminanceMatrix[index++] = (byte)(0.299f * pixel.R + 0.587f * pixel.G + 0.114f * pixel.B);
                        }
                    }

                    var luminanceSource = new RGBLuminanceSource(
                        luminanceMatrix, 
                        image.Width, 
                        image.Height, 
                        RGBLuminanceSource.BitmapFormat.Gray8
                    );
                    
                    var binarizer = new HybridBinarizer(luminanceSource);
                    var binaryBitmap = new BinaryBitmap(binarizer);

                    var result = reader.decode(binaryBitmap);

                    if (result != null && !string.IsNullOrEmpty(result.Text))
                    {
                        var data = JsonSerializer.Deserialize<QrReagentData>(result.Text, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (data != null)
                        {
                            resultList.Add(data);
                        }
                    }
                }
                catch (ReaderException)
                {
                    // ZXing выбрасывает ReaderException, если QR-код на конкретном кадре просто не найден.
                    // Для пакетной загрузки это нормально (например, смазанное фото), просто идем дальше.
                    continue;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Ошибка обработки кадра: {ex.Message}");
                }
                finally
                {
                    // Сбрасываем внутреннее состояние ридера между кадрами для чистоты следующего поиска
                    reader.reset();
                }
            }

            return resultList; 
        }
    }
}
