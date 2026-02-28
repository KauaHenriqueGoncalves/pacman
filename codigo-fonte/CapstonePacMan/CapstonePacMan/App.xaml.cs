using LibVLCSharp.Shared;

namespace CapstonePacMan;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        Core.Initialize();
    }

    protected Window? MainWindow { get; private set; }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        MainWindow = new Window();

        var frame = new Frame();
        frame.Navigate(typeof(MainPage));

        MainWindow.Content = frame;
        MainWindow.Activate();
    }

}
