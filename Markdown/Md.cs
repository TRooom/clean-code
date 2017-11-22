using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Markdown
{
    public class Md
    {
        private readonly DomTree tree;

        public Md()
        {
            this.tree = new DomTree();
        }

        public string RenderToHtml(string markdown)
        {
            var token = new Token(markdown);
            ProcessText(token);
            return tree.ToHtml();
        }

        private void ProcessText(Token markdown)
        {
            do
            {
                var symbol = markdown.Next();
                if (symbol == "_")
                    ProcessUnderscores(markdown);
                else if (symbol == "\\")
                    ProcessEscaping(markdown);
                else if (symbol != null)
                    tree.AddText(symbol);
            } while (!markdown.IsFinished);
        }

        private void ProcessEscaping(Token markdown)
        {
            var escaped = @"_\";
            var next = markdown.Next();
            if (next == null)
            {
                tree.AddText(@"\");
                return;
            }
            tree.AddText(escaped.Contains(next) ? next : @"\");
            var nextNext = markdown.SeekNext();
            if (nextNext == "_") tree.AddText(markdown.Next());
        }

        private void ProcessUnderscores(Token markdown)
        {
            if (IsSelectionStart(markdown))
            {
                tree.AddNestedTag(DefineType(markdown));
                markdown.MovePointer(tree.GetCurrentOpenTag().MarkdownTag.Length - 1);
            }
            else if (IsSelectionEnd(markdown))
            {
                markdown.MovePointer(tree.GetCurrentOpenTag().MarkdownTag.Length-1);
                tree.CloseTag();
            }
            else
                tree.AddText("_");
        }

        private ITag DefineType(Token markdown)
        {
            if (markdown.SeekNext() != "_") return new Em();
            return new Strong();
        }

        private bool IsSelectionStart(Token markdown)
        {
            return string.IsNullOrWhiteSpace(markdown.SeekPrevious())
                   && !string.IsNullOrWhiteSpace(markdown.SeekNext());
        }

        private bool IsSelectionEnd(Token markdown)
        {
            var previous = markdown.SeekPrevious();
            var currentTag = tree.GetCurrentOpenTag();
            if (currentTag == null)
                return false;
            return !string.IsNullOrWhiteSpace(previous)
                   && markdown.IsSubstr(markdown.Pointer, currentTag.MarkdownTag)
                   && string.IsNullOrWhiteSpace(
                       markdown.TryGetSymbol(markdown.Pointer + currentTag.MarkdownTag.Length));
        }
    }
}