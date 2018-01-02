namespace HAPExplorer
{
    using HtmlAgilityPack;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Markup;

    public class HtmlNodeViewer : UserControl, IComponentConnector
    {
        private bool _contentLoaded;
        internal Button btnCheckXpath;
        internal ColumnDefinition Headers;
        internal Label lblTag;
        internal Label lblTagHeader;
        internal Label lblType;
        internal Label lblTypeHeader;
        internal TextBox lblValue;
        internal Label lblValueHeader;
        internal TextBox lblXpath;
        internal Label lblXpathHeader;
        internal RowDefinition Tag;
        internal RowDefinition Type;
        internal RowDefinition Value;
        internal ColumnDefinition Values;
        internal RowDefinition XpathRow;

        public HtmlNodeViewer()
        {
            this.InitializeComponent();
        }

        private void btnCheckXpath_Click(object sender, RoutedEventArgs e)
        {
            HtmlNode dataContext = base.DataContext as HtmlNode;
            if (dataContext != null)
            {
                if (dataContext.OwnerDocument.DocumentNode.SelectSingleNode(dataContext.XPath) == dataContext)
                {
                    MessageBox.Show("Pass");
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/HAPExplorer;component/htmlnodeviewer.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.Headers = (ColumnDefinition) target;
                    return;

                case 2:
                    this.Values = (ColumnDefinition) target;
                    return;

                case 3:
                    this.Tag = (RowDefinition) target;
                    return;

                case 4:
                    this.Type = (RowDefinition) target;
                    return;

                case 5:
                    this.XpathRow = (RowDefinition) target;
                    return;

                case 6:
                    this.Value = (RowDefinition) target;
                    return;

                case 7:
                    this.lblTagHeader = (Label) target;
                    return;

                case 8:
                    this.lblTypeHeader = (Label) target;
                    return;

                case 9:
                    this.lblXpathHeader = (Label) target;
                    return;

                case 10:
                    this.lblValueHeader = (Label) target;
                    return;

                case 11:
                    this.lblTag = (Label) target;
                    return;

                case 12:
                    this.lblType = (Label) target;
                    return;

                case 13:
                    this.btnCheckXpath = (Button) target;
                    this.btnCheckXpath.Click += new RoutedEventHandler(this.btnCheckXpath_Click);
                    return;

                case 14:
                    this.lblXpath = (TextBox) target;
                    return;

                case 15:
                    this.lblValue = (TextBox) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

