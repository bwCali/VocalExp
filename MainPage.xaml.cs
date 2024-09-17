
using Microsoft.Maui.Graphics.Text;

using System.Collections.Generic;
using System.Globalization;

namespace MoMo4;



public partial class MainPage : ContentPage


{
    public string[] P3L1
{
    get { return p3l1; }
    set { p3l1 = value; }
}

public string Wordcolor { get; set; }


static private string[] p3l1 = { "Read", "This", "Out", "Loud" };

public ISpeechToText speechToText;
public CancellationTokenSource tokenSource = new CancellationTokenSource();
public Command ListenCommand { get; set; }
public string RecognitionText { get; set; }
public string Checkwords { get; set; }

public MainPage(ISpeechToText speechToText)
{
    InitializeComponent();

    this.speechToText = speechToText;

    ListenCommand = new Command(Listen);
    BindingContext = this;

}
private async void Listen()
{
    var isAuthorized = await speechToText.RequestPermissions();
    if (isAuthorized)
    {
        try
        {
            RecognitionText = await speechToText.Listen(CultureInfo.GetCultureInfo("en-us"),
                new Progress<string>(partialText =>
                {
                    if (DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        RecognitionText = partialText;
                    }
                    else
                    {
                        RecognitionText += partialText + " ";
                    }

                    OnPropertyChanged(nameof(RecognitionText));


                }), tokenSource.Token);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
    else
    {
        await DisplayAlert("Permission Error", "No microphone access", "OK");
    }

}
private void ListenCancel()
{
    tokenSource?.Cancel();
}



public MainPage()
{
    InitializeComponent();
    this.speechToText = MauiProgram.Services.GetService<ISpeechToText>();
    ListenCommand = new Command(Listen);
    BindingContext = this;

    InitializeComponent();
    if (RecognitionText is not null)
    {
        string[] Checkwords = RecognitionText.Split(' ');
        var matches = p3l1.Intersect(Checkwords);
        int Counter = 0;

        if (matches.Any())
        {
            foreach (var match in matches)
            {
                Wordcolor = "Yellow";
                Counter++;
      
            }


        }

    }
    else
    {
        string[] Checkwords = { "nothing", "here" };
        var matches = p3l1.Intersect(Checkwords);
    }


}

}