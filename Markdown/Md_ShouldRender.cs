using NUnit.Framework;
using FluentAssertions;

namespace Markdown
{
    [TestFixture]
    public class Md_ShouldRender
    {
        private Md render;
        [SetUp]
        public void SetUp()
        {
            render = new Md();
        }

        [TestCase("","",TestName = "Render_EmptyString")]
        [TestCase("text", "text", TestName = "Render_SimpleText")]
        [TestCase(@"\_text\_", "_text_", TestName = "Render_EscapeingUnderscore")]
        [TestCase(@"\__text\__", "__text__", TestName = "Render_EscapeingDoubleUnderscore")]
        [TestCase(@"\\text", @"\text", TestName = "Render_DoubleEscapeing")]
        [TestCase(@"\", @"\", TestName = "Render_SingleSlash")]
        [TestCase(@"\\ ", @"\ ", TestName = "Render_SlashBeforeWhitespace")]
        [TestCase(@"\\text", @"\text", TestName = "Render_SlashBeforeText")]
        [TestCase(@"_text_", @"<em>text</em>", TestName = "Render_EmSelection")]
        [TestCase(@"_text_ text", @"<em>text</em> text", TestName = "Render_EmSelectionWithText")]
        [TestCase(@"__text__", @"<strong>text</strong>", TestName = "Render_StrongSelection")]
        [TestCase(@"t_ex_t", @"t_ex_t", TestName = "Render_UnderscoresInText")]
        [TestCase(@"t__ex__t", @"t__ex__t", TestName = "Render_DoubleUnderscoresInText")]
        [TestCase(@"_ text_", @"_ text_", TestName = "Render_NotSelectionsUnderscoresInSrart")]
        [TestCase(@"_text _", @"_text _", TestName = "Render_NotSelectionsUnderscoresInEnd")]
        [TestCase(@"_text __text__ text_", @"<em>text <strong>text</strong> text</em>", TestName = "Render_StongInEm")]
        [TestCase(@"__text _text_ text__", @"<strong>text <em>text</em> text</strong>", TestName = "Render_EmInStrong")]
        [TestCase(@"_text", @"_text", TestName = "Render_NotClosedEmSelection")]
        [TestCase(@"__text", @"__text", TestName = "Render_NotClosedStrongSelection")]
        public void Render(string markdown, string expected)
        {
            var html = render.RenderToHtml(markdown);
            html.Should().Be(expected);
        }

    }
}