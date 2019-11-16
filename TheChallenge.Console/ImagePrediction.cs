using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace YbHackaton.Infront
{
    static class ImagePrediction
    {
        private static string imageFileUrl = "https://custsom-vision.cognitiveservices.azure.com/customvision/v3.0/Prediction/ceb86cbd-7809-464e-b7d5-3faf0238ed75/classify/iterations/Iteration1/image";

        static byte[] GetImageAsByteArray(string imageFilePath){ 
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        public static async Task MakePredictionRequestImage(string imageFilePath){ 
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Prediction-Key", "5c3b887657f145e89baf941aa160add8");

            HttpResponseMessage response;

            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using(var content = new ByteArrayContent(byteData)){ 
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(imageFileUrl, content);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
