using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OpenAI.GPT3;
//using OpenAI_API;
//using OpenAI_API.Completions;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
//using TextCopy;

namespace OpenAIBlazor.Pages
{
    //OpenAI nuget package info:   https://www.nuget.org/packages/Betalgo.OpenAI.GPT3/

    public partial class ChatGPT : ComponentBase
    {
        private string? message { get; set; }
        private string generatedText { get; set; } = "";

        private string? _apiKey { get; set; }

        private string? SelectedModelType { get; set; }
        private string? SelectedModel { get; set; }

        private string messageRole { get; set; } = "";

        private string[]? lines { get; set; } = null;

        private readonly HttpClient _httpClient = new HttpClient();

        private string AnswerText { get; set; }

        private string ScreenText { get; set; } = "";

        private int sliderTempValue = (70);
        private int sliderTokensValue = (250);

        List<ChatMessage> MessageTrail = new List<ChatMessage>();

        [Inject]
        private IConfiguration Configuration { get; set; }


        [Inject]
        IJSRuntime? JSRuntime { get; set; }

        //  [Inject]
        //   IClipboard Clipboard { get; set; }


        private readonly IJSRuntime? _jsRuntime;

        protected override void OnInitialized() {
            // Call the method that handles the selection change event
            SelectedModel = "gpt-3.5-turbo";
            _apiKey = Configuration.GetValue<string>("openaiKey");
            MessageTrail.Add(ChatMessage.FromSystem("You are a helpful assistant."));
        }


        //private async Task CopyToClipboard()
        //{
        //    await Clipboard.SetTextAsync(AnswerText);
        //}

        public async Task SetItemAsync(string key, string value)
        {
            await _jsRuntime.InvokeAsync<object>("localStorage.setItem", key, value);
        }

        public async Task<string> GetItemAsync(string key)
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        }

        public async void clearInputBox()
        {
            await JSRuntime.InvokeAsync<object>("ClearInputText");
        }
       
        public async void CreateQuestionTrail()
        {
            //Add question to the DOM Object by calling this JavaScript function
            await JSRuntime.InvokeAsync<object>("CreateQuestion");

           //Get Answer from ChatGPT  
            await GetResponseFromGPT3();
        }
     
        private void UpdateSelectedValue(ChangeEventArgs e)
        {
            //currently only ChatGpt3_5Turbo and ChatGpt3_5Turbo0301 are supported!
            SelectedModel = e.Value.ToString();
        }


        private async Task GetResponseFromGPT3()
        {

            string answer = string.Empty;


            var _openAIService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = _apiKey
            });

            ScreenText = "Running request, please wait...";

            //Add new User Request to MessageTrail List Object
            MessageTrail.Add(ChatMessage.FromUser(message));



            var completionResult = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Temperature = sliderTempValue / 100,
                MaxTokens = sliderTokensValue,

                Messages = MessageTrail,

               // Currently only ChatGpt3_5Turbo and ChatGpt3_5Turbo0301 are supported!
               // To see all models in Windows 10, 11
               // Hold down the CTRL key and click on the Word Models below.

                Model = Models.ChatGpt3_5Turbo
            });
            if (completionResult.Successful)
            {

                messageRole = completionResult.Choices.First().Message.Role;

                generatedText = completionResult.Choices.First().Message.Content;

                if (generatedText.Length > 0)
                {
                    ScreenText = "";

                    //ScreenText = generatedText.Replace("\r", "<br>").Replace("\n", "<br>");
                    lines = generatedText.Replace("\r", "<br>").Replace("\n", "<br>").Split("<br>");

                    foreach (var line in lines)
                    {
                        if (line.Length > 0)
                        {
                            ScreenText = ScreenText + line + System.Environment.NewLine;

                        }
                    }
                    if (messageRole == "system")
                    {
                        MessageTrail.Add(ChatMessage.FromSystem(ScreenText));
                    } else if (messageRole == "assistant")
                    {
                        MessageTrail.Add(ChatMessage.FromAssistant(ScreenText));
                    }
                    else
                    {
                        MessageTrail.Add(ChatMessage.FromUser(ScreenText));
                    }
             
                  //      await JSRuntime.InvokeVoidAsync("typeOutWords", ScreenText);
                    await JSRuntime.InvokeVoidAsync("typeOutWords", ScreenText); // Call displayText() function with fetched text
                }
            }


            else
            {
                if (completionResult.Error == null)
                {
                    throw new Exception("Unknown Error");
                }

                Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
            }

        }
    }


    //private class ChatMessageTrail
    //{
    //    //
    //    // Summary:
    //    //     “system”, “user”, or “assistant”
    //    public string Role { get; set; }

    //    public string Content { get; set; }

    //    public ChatMessageTrail(string role, string content)
    //    {
    //        Role = role;
    //        Content = content;
    //    }

    //}
   
}


