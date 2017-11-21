using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;

namespace Markdown
{
    internal class DomTree
    {
        private readonly DomTree parent;

        private DomTree working;

        private int currentChildren;

        private StringBuilder text;

        private readonly List<DomTree> childrens;

        private readonly ITag type;

        private bool isClosed;

        public DomTree()
        {
            childrens = new List<DomTree>();
            text = new StringBuilder();
            currentChildren = 0;
            working = this;
        }

        public DomTree(DomTree parent, ITag type = null)
        {
            childrens = new List<DomTree>();
            text = new StringBuilder();
            currentChildren = 0;
            this.type = type;
            this.parent = parent;
        }

        public void AddText(string text)
        {
            if (working.childrens.Count == working.currentChildren)
                working.childrens.Add(new DomTree(working));
            working.childrens[working.currentChildren].text.Append(text);
        }

        public void AddNestedTag(ITag type)
        {
            if (working.childrens.Count > working.currentChildren)
                working.currentChildren++;
            var nested = new DomTree(working, type);
            working.childrens.Add(nested);
            working = nested;
        }

        public void CloseTag()
        {
            working.isClosed = true;
            working = working.parent;
            working.currentChildren++;
        }

        public ITag GetCurrentOpenTag() => working.type;

        public string ToHtml()
        {
            var currentResult = new StringBuilder();
            if (childrens.Count == 0)
                return text.ToString();
            foreach (var children in childrens)
                currentResult.Append(children.ToHtml());
            if (type != null && isClosed)
                return $"<{type.HtmlTag}>{currentResult}</{type.HtmlTag}>";
            return type != null ? $"{type.MarkdownTag}{currentResult}" : currentResult.ToString();
        }

        public override string ToString() =>
            type?.HtmlTag.ToUpper() ?? text.ToString();
    }
}