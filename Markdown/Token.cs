using System;

namespace Markdown
{
    internal class Token
    {
        public readonly string rawString;
        public int Pointer { get; private set; }
        public bool IsFinished { get; private set; }

        public Token(string rawString)
        {
            this.rawString = rawString;
            this.Pointer = -1;
        }

        public string TryGetCurrentSymbol() =>
            IsValidIndex(Pointer) ? null : rawString[Pointer].ToString();

        public string Next()
        {
            if (IsValidIndex(++Pointer))
                return rawString[Pointer].ToString();
            IsFinished = true;
            return null;
        }

        public void MovePointer(int count)
        {
            if (!IsValidIndex(Pointer + count))
                throw new IndexOutOfRangeException("Pointer moved out of string bounds");
            Pointer += count;
        }

        public string SeekNext() =>
            IsValidIndex(Pointer + 1) ? rawString[Pointer + 1].ToString() : null;

        public string SeekPrevious() =>
            IsValidIndex(Pointer - 1) ? rawString[Pointer - 1].ToString() : null;

        public string TryGetSymbol(int index) =>
            IsValidIndex(index) ? rawString[index].ToString() : null;

        public bool IsStartsFrom(int start, string substr)
        {
            return IsValidIndex(start) &&
                   IsValidIndex(start + substr.Length - 1)
                   && rawString.Substring(start).StartsWith(substr);
        }

        private bool IsValidIndex(int index) =>
            index >= 0 && index < rawString.Length;
    }
}