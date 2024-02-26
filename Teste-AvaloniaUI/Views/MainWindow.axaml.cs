using Avalonia.Controls;

namespace Teste_AvaloniaUI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        this.Width = 600;
        this.Height = 400;
        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }
}
