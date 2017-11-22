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
            Pointer < 0 || Pointer >= rawString.Length ? null : rawString[Pointer].ToString();

        public string Next()
        {
            Pointer++;
            if (Pointer < rawString.Length) return rawString[Pointer].ToString();
            IsFinished = true;
            return null;
        }

        public void MovePointer(int count) => Pointer += count;

        public string SeekNext() =>
            Pointer + 1 >= rawString.Length ? null : rawString[Pointer + 1].ToString();

        public string SeekPrevious() =>
            Pointer - 1 < 0 ? null : rawString[Pointer - 1].ToString();

        public string TryGetSymbol(int index) => 
            index >= rawString.Length || index < 0 ? null : rawString[index].ToString();

        public bool IsSubstr(int start, string substr)
        {
                return start + substr.Length <= rawString.Length 
                && rawString.Substring(start, substr.Length) == substr;
        }
    }
}