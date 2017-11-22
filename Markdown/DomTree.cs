using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;

namespace Markdown
{
    internal class DomTree
    {
        private readonly DomTree parent;

        private DomTree workingTag;

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
            workingTag = this;
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
            if (workingTag.childrens.Count == workingTag.currentChildren)
                workingTag.childrens.Add(new DomTree(workingTag));
            workingTag.childrens[workingTag.currentChildren].text.Append(text);
        }

        public void AddNestedTag(ITag type)
        {
            if (workingTag.childrens.Count > workingTag.currentChildren)
                workingTag.currentChildren++;
            var nested = new DomTree(workingTag, type);
            workingTag.childrens.Add(nested);
            workingTag = nested;
        }

        public void CloseTag()
        {
            workingTag.isClosed = true;
            workingTag = workingTag.parent;
            workingTag.currentChildren++;
        }

        public ITag GetCurrentOpenTag() => workingTag.type;

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
    }
}