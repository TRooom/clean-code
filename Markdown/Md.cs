using System;
using NUnit.Framework;

namespace Markdown
{
    public class Md
    {
        public string RenderToHtml(string markdown)
        {
            return markdown; //TODO
        }

        private void ProcessText(Token markdown)
        {
            throw new NotImplementedException();
        }

        private void ProcessEscaping(Token markdown)
        {
            throw new NotImplementedException();
        }

        private void ProcessUnderscores(Token markdown)
        {
            throw new NotImplementedException();
        }
    }
}