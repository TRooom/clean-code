namespace Markdown
{
    interface ITag
    {
        string HtmlTag { get; }
        string MarkdownTag { get; }
    }
}