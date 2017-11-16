using System;
using System.Collections.Generic;
using System.Text;

namespace Markdown
{
    internal class DomTree
    {
        private DomTree parent;

        private string text;

        private List<DomTree> childrens;

        private readonly DomNodeType type;

        public string ToHtml()
        {
            throw new NotImplementedException();
        }

        private string ToHtml(StringBuilder result)
        {
            throw new NotImplementedException();
        }
    }
}