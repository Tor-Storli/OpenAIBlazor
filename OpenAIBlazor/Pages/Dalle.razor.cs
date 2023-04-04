using Microsoft.AspNetCore.Components;
//using Nest;
using Microsoft.JSInterop;
using OpenAI.GPT3;
//using OpenAI_API;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace OpenAIBlazor.Pages
{

    /*
     prompt 1:
        ---------
        A cat with green eyes and a Doberman dog eating ice cream together.

     prompt 2:
        ---------
        Photo of a 50-year old white man, smiling, good teeth, silver hair, neat beard, wearing a blue sports jaket, no glasses

     prompt 3:
        ---------
        woman in cybernetic armor, asian ethnicity, studio, half portrait 

     prompt 4:
        ---------
        man in samurai armor, Japanese ethnicity, studio, half portrait 
     
    prompt 5:
        ---------
        Dark huge thunderstorm approaching a house on the prairie, cinematic theme, wide shot, outdoors, late afternoon, very detailed, landscape, sharp
     
     */
    public partial class Dalle: ComponentBase
    {
        //OpenAI nuget package info:   https://www.nuget.org/packages/Betalgo.OpenAI.GPT3/
            private string? Prompt { get; set; }
            private string generatedText { get; set; } = "";

            private string? _apiKey { get; set; }

            private string? SelectedModelType { get; set; }
            private string? ImageSize { get; set; }

            private string ScreenText { get; set; } = "";
       
            
            private string response = "";

            private string ImageResponse = "";

           // private readonly HttpClient _httpClient = new HttpClient();

            private int sliderImgCountValue = (1);
           // private int sliderTokensValue = (250);

           [Inject]
           private IConfiguration Configuration { get; set; }


           [Inject]
           IJSRuntime JSRuntime { get; set; }

           [Inject]
           NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
            {
                ImageSize = "256x256";
            }

            // Call the method that handles the selection change event
            private void UpdateSelectedValue(ChangeEventArgs e)
            {
              ImageSize = e.Value.ToString();
            }

            public async void RemoveImages()
            {
                await JSRuntime.InvokeAsync<object>("removeImages");
                ReloadPage();
            }


        private void ReloadPage()
        {
            NavigationManager.NavigateTo(NavigationManager.Uri, true);
        }


        private async Task GetImageFromGPT3()
            {
                _apiKey = Configuration.GetValue<string>("openaiKey");

            var _openAIService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = _apiKey
            });

            var imageResult = await _openAIService.Image.CreateImage(new ImageCreateRequest
            {
                    Prompt = Prompt,
                    N = sliderImgCountValue,
                    Size = ImageSize,
                    ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                    User = "Tor"
            });
            
            if (imageResult.Successful)
            {
                response = "";
                foreach (var image in imageResult.Results)
                {
         
                    ImageResponse += $@"<img src=""{image.Url}"" />&nbsp;&nbsp;";
                }
            }
            else
            {
                if (imageResult.Error == null)
                {
                    response = "Unknown Error";
                }
                response =
                $"{imageResult.Error?.Code}: {imageResult.Error?.Message}";
            }

            }

        }
}



