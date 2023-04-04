using Microsoft.AspNetCore.Components;

namespace OpenAIBlazor.Pages
{
    public partial class Test : ComponentBase
    {
        //  public string? message { get; set; }
        public string GeneratedText { get; set; } = "Answer will be displayed here.";

        public string ScreenText { get; set; } = "";

        public string[]? lines { get; set; } 
        public string ResultText { get; set; } = "";

        private readonly HttpClient _httpClient = new HttpClient();

        private async Task GetResponseFromDalle2()
        {

            await Task.Run(() =>
                    {
                        GeneratedText = "Retrieving Answer, please wait...";

                        ResultText = "\n\n# Create Form\n$form = New-Object System.Windows.Forms.Form\n$form.Text = 'My Form'\n$form.Width = 300\n$form.Height = 200\n\n# Create RadioButtons\n$radioButton1 = New-Object System.Windows.Forms.RadioButton\n$radioButton1.Text = 'Radio Button 1'\n$ra...";

                        if (ResultText.Length > 0)
                        {
                            lines = ResultText.Replace("\r\n", "\n").Replace("\n", "\n").Split("\n");

                            foreach (var line in lines)
                            {
                                if (line.Length > 0)
                                {
                                    ScreenText = ScreenText + line + Environment.NewLine;

                                }
                            }
                        }
                     });
        }
    }



    public static class GPT3Response
    {
        public static string? Response { get; set; }
    }
}
