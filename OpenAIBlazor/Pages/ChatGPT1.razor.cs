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

    public partial class ChatGPT1 : ComponentBase
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

        private readonly bool shouldRender = true;

        List<ChatMessage> MessageTrail = new List<ChatMessage>();

        [Inject]
        private IConfiguration Configuration { get; set; }


        [Inject]
        IJSRuntime JSRuntime { get; set; }

        //  [Inject]
        //   IClipboard Clipboard { get; set; }


        private readonly IJSRuntime _jsRuntime;

        protected override void OnInitialized() {
            // Call the method that handles the selection change event
           // SelectedModel = "gpt-3.5-turbo";
           // _apiKey = Configuration.GetValue<string>("openaiKey");
           // MessageTrail.Add(ChatMessage.FromSystem("You are a helpful assistant."));
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

        protected override bool ShouldRender() => shouldRender;
        

        private void UpdateSelectedValue(ChangeEventArgs e)
        {
            //currently only ChatGpt3_5Turbo and ChatGpt3_5Turbo0301 are supported!
            SelectedModel = e.Value.ToString();
        }


        private async Task GetResponseFromGPT3()
        {
            string answer = string.Empty;

            messageRole = "assistant";
            generatedText = @"Making French bread is a delicious and rewarding process. Here is a simple recipe to get you started:<br><br>Ingredients:<br><ul><br>3 cups bread flour<br><br>1 tsp salt<br><br>1 tsp sugar<br><br>1 tsp active dry yeast<br><br>1 cup warm water<br></ul><br>Instructions:<br><ol><br><br>In a large mixing bowl, combine the flour, salt, sugar, and yeast.<br><br><br><br>Gradually add in the warm water, stirring with a wooden spoon until the dough comes together and forms a shaggy ball.<br><br><br><br>Turn the dough out onto a lightly floured surface and knead it for about 10 minutes, until it becomes smooth and elastic.<br><br><br><br>Place the dough in a lightly oiled bowl, cover it with a damp towel, and let it rise in a warm, draft-free place for 1-2 hours, until it doubles in size.<br><br><br><br>Preheat your oven to 450°F (230°C). Place a shallow baking dish or metal pan on the bottom rack of the oven.<br><br><br><br>Punch down the dough to release the air, and then turn it out onto a floured surface.<br><br><br><br>Shape the dough into a long, narrow loaf and place it on a lightly greased or floured baking sheet. Alternatively, you can shape the dough into rounds or baguettes.<br><br><br><br>Use a sharp knife or razor blade to make several shallow slashes across the top of the loaf. This helps the bread expand as it bakes.<br><br><br><br>Fill a spray bottle with water and mist the bread lightly before placing it in the oven. This creates steam, which helps the bread develop a crispy crust.<br><br><br><br>Bake the bread for 20-25 minutes, or until it is golden brown and sounds hollow when tapped on the bottom.<br><br><br><br>Remove the bread from the oven and let it cool on a wire rack for at least 10-15 minutes before slicing and serving.<br><br></ol><br>Enjoy your freshly baked French bread!<br>";

            if (generatedText.Length > 0)
            {
                ScreenText = "";

                lines = generatedText.Replace("\r", "<br>").Replace("\n", "<br>").Split("<br>");

                foreach (var line in lines)
                {
                    if (line.Length > 0)
                    {
                        ScreenText = ScreenText + line + System.Environment.NewLine;

                    }
                }
                   
                await JSRuntime.InvokeVoidAsync("typeOutWords", ScreenText);
             // this.StateHasChanged();           
               
            }
            StateHasChanged();
        }
    }


    public class ChatMessageTrail
    {

        // Summary:   “system”, “user”, or “assistant”
        public string Role { get; set; }

        public string Content { get; set; }

    }

}


