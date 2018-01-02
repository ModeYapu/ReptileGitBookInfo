namespace HAPExplorer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Net;

    public class Window1 : Window, IComponentConnector
    {
        private bool _contentLoaded;
        private OpenFileDialog _fileDialog = new OpenFileDialog();
        private HtmlDocument _html = new HtmlDocument();
        internal Button btnParse;
        internal Button btnSearch;
        internal Button btnTestCode;
        internal CheckBox chkFromCurrent;
        internal CheckBox chkXPath;
        internal GridSplitter gridSplitter1;
        internal Window1 HAPExplorerWindow;
        internal NodeTreeView hapTree;
        internal HtmlAttributeViewer HtmlAttributeViewer1;
        internal HtmlNodeViewer HtmlNodeViewer1;
        internal ListBox listResults;
        internal MenuItem mnuExit;
        internal MenuItem mnuOpenFile;
        internal MenuItem mnuOpenUrl;
        internal TabControl tabControl1;
        internal TabItem tabNodeTree;
        internal TabItem tabSearchResults;
        internal TextBox txtHtml;
        internal TextBox txtSearchTag;

        public Window1()
        {
            this.InitializeComponent();
            try
            {
                this.txtHtml.Text = System.IO.File.ReadAllText("mshome.htm");
            }
            catch
            {
            }
            this.InitializeFileDialog();
        }

        [DebuggerNonUserCode]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void btnParse_Click(object sender, RoutedEventArgs e)
        {
            this.ParseHtml();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            HtmlNode documentNode = this._html.DocumentNode;
            if (((this.chkFromCurrent.IsChecked == true) && (this.hapTree.SelectedItem != null)) && ((this.hapTree.SelectedItem is TreeViewItem) && (((TreeViewItem) this.hapTree.SelectedItem).DataContext is HtmlNode)))
            {
                documentNode = ((TreeViewItem) this.hapTree.SelectedItem).DataContext as HtmlNode;
            }
            this.SearchFromNode(documentNode);
        }

        private void btnTestCode_Click(object sender, RoutedEventArgs e)
        {
            string html = this.GetHtml("http://htmlagilitypack.codeplex.com");
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            string requestUriString = (from x in document.DocumentNode.Descendants("a")
                where x.Id.ToLower().Contains("releasestab")
                select x).FirstOrDefault<HtmlNode>().Attributes["href"].Value;
            HtmlDocument document2 = new HtmlDocument();
            try
            {
                base.Cursor = Cursors.Wait;
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestUriString);
                using (Stream stream = request.GetResponse().GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        document2.LoadHtml(reader.ReadToEnd());
                        MessageBox.Show(int.Parse((from x in document2.DocumentNode.Descendants("span")
                            where x.Id.ToLower().Contains("releasedownloadsliteral")
                            select x).FirstOrDefault<HtmlNode>().InnerHtml.ToLower().Replace("downloads", string.Empty).Trim()).ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error loading file: " + exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
            }
        }

        private string GetHtml(string link)
        {
            string str;
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(link);
            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    str = reader.ReadToEnd();
                }
            }
            return str;
        }

        private void hapTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem)
            {
                TreeViewItem newValue = e.NewValue as TreeViewItem;
                if (newValue.DataContext is HtmlNode)
                {
                    this.HtmlAttributeViewer1.Visibility = Visibility.Hidden;
                    this.HtmlNodeViewer1.DataContext = null;
                    this.HtmlNodeViewer1.Visibility = Visibility.Visible;
                    this.HtmlNodeViewer1.DataContext = newValue.DataContext;
                }
                else if (newValue.DataContext is HtmlAttribute)
                {
                    this.HtmlNodeViewer1.Visibility = Visibility.Hidden;
                    this.HtmlAttributeViewer1.DataContext = null;
                    this.HtmlAttributeViewer1.Visibility = Visibility.Visible;
                    this.HtmlAttributeViewer1.DataContext = newValue.DataContext;
                }
            }
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/HAPExplorer;component/window1.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void InitializeFileDialog()
        {
            this._fileDialog.FileName = "Document";
            this._fileDialog.DefaultExt = ".html";
            this._fileDialog.Filter = "Text documents (.html,.htm,.aspx)|*.html;*.htm;*.aspx";
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            base.Close();
        }

        private void mnuOpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (this._fileDialog.ShowDialog() == true)
            {
                try
                {
                    base.Cursor = Cursors.Wait;
                    string fileName = this._fileDialog.FileName;
                    this.txtHtml.Text = System.IO.File.ReadAllText(fileName);
                }
                catch (FileNotFoundException exception)
                {
                    MessageBox.Show("Error loading file: " + exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
                }
                catch (FileLoadException exception2)
                {
                    MessageBox.Show("Error loading file: " + exception2.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
                }
                catch (Exception exception3)
                {
                    MessageBox.Show("Error loading file: " + exception3.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
                }
                finally
                {
                    base.Cursor = Cursors.Arrow;
                }
            }
        }

        private void mnuOpenUrl_Click(object sender, RoutedEventArgs e)
        {
            UrlDialog dialog = new UrlDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    base.Cursor = Cursors.Wait;
                    this._html = new HtmlWeb().Load(dialog.Url);
                    this.hapTree.BaseNode = this._html.DocumentNode;
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error loading file: " + exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
                }
                finally
                {
                    base.Cursor = Cursors.Arrow;
                }
            }
        }

        private void ParseHtml()
        {
            if (!this.txtHtml.Text.IsEmpty())
            {
                this._html = new HtmlDocument();
                this._html.LoadHtml(this.txtHtml.Text);
                this.hapTree.BaseNode = this._html.DocumentNode;
            }
        }

        private void SearchFromNode(HtmlNode baseNode)
        {
            IEnumerable<HtmlNode> enumerable = Enumerable.Empty<HtmlNode>();
            if (!this._html.DocumentNode.HasChildNodes)
            {
                this.ParseHtml();
            }
            if (this.chkXPath.IsChecked == true)
            {
                enumerable = baseNode.SelectNodes(this.txtSearchTag.Text);
            }
            else
            {
                enumerable = baseNode.Descendants(this.txtSearchTag.Text);
            }
            if (enumerable != null)
            {
                this.listResults.Items.Clear();
                foreach (HtmlNode node in enumerable)
                {
                    NodeTreeView element = new NodeTreeView {
                        BaseNode = node
                    };
                    ListBoxItem newItem = new ListBoxItem();
                    StackPanel panel = new StackPanel();
                    Label label = new Label {
                        Content = $"id:{node.Id} name:{node.Name} children{node.ChildNodes.Count}",
                        FontWeight = FontWeights.Bold
                    };
                    panel.Children.Add(label);
                    panel.Children.Add(element);
                    newItem.Content = panel;
                    this.listResults.Items.Add(newItem);
                }
                this.tabControl1.SelectedItem = this.tabSearchResults;
            }
        }

        [DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.HAPExplorerWindow = (Window1) target;
                    return;

                case 2:
                    this.mnuOpenFile = (MenuItem) target;
                    this.mnuOpenFile.Click += new RoutedEventHandler(this.mnuOpenFile_Click);
                    return;

                case 3:
                    this.mnuOpenUrl = (MenuItem) target;
                    this.mnuOpenUrl.Click += new RoutedEventHandler(this.mnuOpenUrl_Click);
                    return;

                case 4:
                    this.mnuExit = (MenuItem) target;
                    this.mnuExit.Click += new RoutedEventHandler(this.mnuExit_Click);
                    return;

                case 5:
                    this.txtHtml = (TextBox) target;
                    return;

                case 6:
                    this.btnParse = (Button) target;
                    this.btnParse.Click += new RoutedEventHandler(this.btnParse_Click);
                    return;

                case 7:
                    this.btnTestCode = (Button) target;
                    this.btnTestCode.Click += new RoutedEventHandler(this.btnTestCode_Click);
                    return;

                case 8:
                    this.txtSearchTag = (TextBox) target;
                    return;

                case 9:
                    this.btnSearch = (Button) target;
                    this.btnSearch.Click += new RoutedEventHandler(this.btnSearch_Click);
                    return;

                case 10:
                    this.chkFromCurrent = (CheckBox) target;
                    return;

                case 11:
                    this.chkXPath = (CheckBox) target;
                    return;

                case 12:
                    this.tabControl1 = (TabControl) target;
                    return;

                case 13:
                    this.tabNodeTree = (TabItem) target;
                    return;

                case 14:
                    this.HtmlNodeViewer1 = (HtmlNodeViewer) target;
                    return;

                case 15:
                    this.HtmlAttributeViewer1 = (HtmlAttributeViewer) target;
                    return;

                case 0x10:
                    this.gridSplitter1 = (GridSplitter) target;
                    return;

                case 0x11:
                    this.hapTree = (NodeTreeView) target;
                    return;

                case 0x12:
                    this.tabSearchResults = (TabItem) target;
                    return;

                case 0x13:
                    this.listResults = (ListBox) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

