namespace HAPExplorer
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class UrlDialog : Window, IComponentConnector
    {
        private bool _contentLoaded;
        internal Button btnCancel;
        internal Button btnOpen;
        internal Label label1;
        internal TextBox textBox1;

        public UrlDialog()
        {
            this.InitializeComponent();
            this.textBox1.Focus();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            base.DialogResult = false;
            base.Close();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            base.DialogResult = new bool?(!this.textBox1.Text.IsEmpty());
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/HAPExplorer;component/urldialog.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.textBox1 = (TextBox) target;
                    return;

                case 2:
                    this.label1 = (Label) target;
                    return;

                case 3:
                    this.btnOpen = (Button) target;
                    this.btnOpen.Click += new RoutedEventHandler(this.btnOpen_Click);
                    return;

                case 4:
                    this.btnCancel = (Button) target;
                    this.btnCancel.Click += new RoutedEventHandler(this.btnCancel_Click);
                    return;
            }
            this._contentLoaded = true;
        }

        public string Url =>
            this.textBox1.Text.Trim();
    }
}

