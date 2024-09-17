
namespace MoMo4;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
        //MainPage = new TitlePage();
    }
}
