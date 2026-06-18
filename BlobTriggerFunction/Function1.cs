using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace BlobTriggerFunction
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function(nameof(Function1))]
        [BlobOutput("graubilder/grau-{name}", Connection = "AzureWebJobsStorage")]
        public byte[] Run([BlobTrigger("farbbilder/{name}", Connection = "AzureWebJobsStorage")] byte[] colorFileData, string name)
        {

            _logger.LogInformation($"Bild wird verarbeiten: {name}");
           
            using SKBitmap bitmap = SKBitmap.Decode(colorFileData);

            _logger.LogInformation($"Breite: {bitmap.Width}, Höhe: {bitmap.Height}");

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    SKColor color = bitmap.GetPixel(x, y);

                    byte gray = (byte)((color.Red + color.Green + color.Blue) / 3); 

                    SKColor grayColor = new SKColor(gray, gray, gray, color.Alpha);

                    bitmap.SetPixel(x, y, grayColor);
                }
            }

            using SKImage image = SKImage.FromBitmap(bitmap);

            using SKData data = image.Encode(SKEncodedImageFormat.Jpeg, 100);

            return data.ToArray();
        }
    }
}
