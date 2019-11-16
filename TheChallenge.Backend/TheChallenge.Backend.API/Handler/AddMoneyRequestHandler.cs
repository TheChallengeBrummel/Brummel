using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TheChallenge.Backend.API.Tagging;
using TheChallenge.Backend.CognitiveServices;
using TheChallenge.Backend.Core;
using TheChallenge.Backend.Entities;
using TheChallenge.Backend.Requests;
using TheChallenge.Backend.Responses;
using TheChallenge.Backend.WebApi;

namespace TheChallenge.Backend.Handler
{
    public class AddMoneyRequestHandler : IRequestHandler<AddMoneyRequest, AddMoneyResponse>
    {
        private readonly ImageServiceSetting _imageServiceSetting;
        private readonly ILogger<AddMoneyRequestHandler> _logger;
        private readonly UserManager _userManager;

        public AddMoneyRequestHandler(
            ILogger<AddMoneyRequestHandler> logger,
            IOptions<ImageServiceSetting> imageServiceSetting,
            UserManager userManager
            )
        {
            if (imageServiceSetting is null)
            {
                throw new ArgumentNullException(nameof(imageServiceSetting));
            }

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _imageServiceSetting = imageServiceSetting.Value;
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<AddMoneyResponse> Handle(AddMoneyRequest request, CancellationToken cancellationToken)
        {
            string ImageResponse = null;

            var correlationId = Guid.NewGuid();
            if (request.ImageData != null && request.ImageData.Length > 0)
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Add(Const.ImageServiceHeaderKey, _imageServiceSetting.ServiceKey);

                using (var content = new ByteArrayContent(request.ImageData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(Const.ImageServiceMediaType);
                    HttpResponseMessage response = await client.PostAsync(_imageServiceSetting.ServiceUri, content);
                    ImageResponse = await response.Content.ReadAsStringAsync();
                }

                var imageWrapper = new ImageWrapper();
                imageWrapper.correlationId = correlationId;

                if (!string.IsNullOrEmpty(ImageResponse))
                {
                    imageWrapper.Image = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageRootobject>(ImageResponse);
                    var mappingResult = GetResponseFromImageWrapper(imageWrapper);
                    await _userManager.AddMoneyToUser(request.UserId, mappingResult.Amount, mappingResult.Description);
                    return mappingResult;
                }
                throw new Exception("Image response was empty");
            }
            throw new Exception("Image Data was empty.");
        }

        private AddMoneyResponse GetResponseFromImageWrapper(ImageWrapper imageWrapper)
        {
            var relevantPredications = imageWrapper.Image.GetSortedPredicationsHigherThan(0.8);

            var tagMap = MoneyTagMaps.GetMapByTag(relevantPredications.FirstOrDefault()?.tagName == null ? string.Empty : relevantPredications.FirstOrDefault()?.tagName);
            return new AddMoneyResponse(tagMap.Description, tagMap.Amount) { };
        }
    }
}
