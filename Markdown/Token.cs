using System;

namespace Markdown
{
    internal class Token
    {
        public readonly string raw;
        public int pointer { get; private set; }

        public string TryGetPrevios(int index)
        {
            throw new NotImplementedException();
        }

        public string TryGetNext(int index)
        {
            throw new NotImplementedException();
        }
    }
}