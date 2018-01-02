namespace HAPExplorer
{
    using System;
    using System.Diagnostics;
    using System.Windows;

    public class App : Application
    {
        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            base.StartupUri = new Uri("Window1.xaml", UriKind.Relative);
        }

        [DebuggerNonUserCode, STAThread]
        public static void Main()
        {
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}

