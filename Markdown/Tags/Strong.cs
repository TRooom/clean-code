namespace Markdown
{
    internal class Strong : ITag
    {
        public string HtmlTag => "strong";
        public string MarkdownTag => "__";
    }
}