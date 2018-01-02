namespace HAPExplorer
{
    using HtmlAgilityPack;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Markup;

    public class NodeTreeView : TreeView, IComponentConnector
    {
        private bool _contentLoaded;
        private HtmlNode baseNode;

        public NodeTreeView()
        {
            this.InitializeComponent();
        }

        private TreeViewItem BuildTree(HtmlNode htmlNode)
        {
            TreeViewItem item = new TreeViewItem {
                DataContext = htmlNode
            };
            if ((htmlNode.NodeType == HtmlNodeType.Text) || (htmlNode.NodeType == HtmlNodeType.Comment))
            {
                item.Header = $"<{htmlNode.OriginalName}> = {htmlNode.InnerText.Trim()}";
            }
            else
            {
                item.Header = $"<{htmlNode.OriginalName}>";
            }
            this.PopulateItem(htmlNode, item);
            return item;
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/HAPExplorer;component/nodetreeview.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void PopulateItem(HtmlNode htmlNode, ItemsControl item)
        {
            TreeViewItem newItem = new TreeViewItem {
                Header = "Attributes"
            };
            foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) htmlNode.Attributes)
            {
                TreeViewItem item3 = new TreeViewItem {
                    Header = $"{attribute.OriginalName} = {attribute.Value}",
                    DataContext = attribute
                };
                newItem.Items.Add(item3);
            }
            if (newItem.Items.Count > 0)
            {
                item.Items.Add(newItem);
            }
            TreeViewItem item4 = new TreeViewItem {
                Header = "Elements",
                DataContext = htmlNode
            };
            foreach (HtmlNode node in (IEnumerable<HtmlNode>) htmlNode.ChildNodes)
            {
                if (newItem.Items.Count > 0)
                {
                    item4.Items.Add(this.BuildTree(node));
                }
                else
                {
                    item.Items.Add(this.BuildTree(node));
                }
            }
            if (item4.Items.Count > 0)
            {
                item.Items.Add(item4);
            }
        }

        public void PopulateTreeview()
        {
            base.Items.Clear();
            string str = (this.baseNode.NodeType == HtmlNodeType.Document) ? "DocumentElement" : this.baseNode.OriginalName;
            TreeViewItem newItem = new TreeViewItem {
                Header = str,
                DataContext = this.baseNode
            };
            base.Items.Add(newItem);
            this.PopulateItem(this.baseNode, newItem);
        }

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }

        public HtmlNode BaseNode
        {
            get => 
                this.baseNode;
            set
            {
                this.baseNode = value;
                this.PopulateTreeview();
            }
        }
    }
}

