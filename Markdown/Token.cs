using System;

namespace Markdown
{
    internal class Token
    {
        public readonly string raw;
        public int Pointer { get; private set; }
        public bool IsFinished { get; private set; }

        public Token(string raw)
        {
            this.raw = raw;
            this.Pointer = -1;
        }

        public string TryGetCurrentSymbol() =>
            Pointer < 0 || Pointer >= raw.Length ? null : raw[Pointer].ToString();

        public string Next()
        {
            Pointer++;
            if (Pointer < raw.Length) return raw[Pointer].ToString();
            IsFinished = true;
            return null;
        }

        public void MovePointer(int count) => Pointer += count;

        public string SeekNext() =>
            Pointer + 1 >= raw.Length ? null : raw[Pointer + 1].ToString();

        public string SeekPrevious() =>
            Pointer - 1 < 0 ? null : raw[Pointer - 1].ToString();

        public string TryGetSymbol(int index) => 
            index >= raw.Length || index < 0 ? null : raw[index].ToString();

        public bool IsSubstr(int start, string substr)
        {
                return start + substr.Length <= raw.Length 
                && raw.Substring(start, substr.Length) == substr;
        }
    }
}