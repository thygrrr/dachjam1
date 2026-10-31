using System.Diagnostics;
using Godot;
using twodog.Testing;
using twodog.Testing.Xunit;

namespace dachjam.Tests;

[Collection<HeadlessCollection>]
public sealed class LocalizationTests(HeadlessFixture godot)
{
    [Theory]
    [InlineData("de")]
    [InlineData("en")]
    [InlineData("fr")]
    public void Catalogs_CoverEveryMessageAndHaveTheRequiredGlyphs(string locale)
    {
        using var english = GD.Load<Translation>("res://localization/en.po");
        using var catalog = GD.Load<Translation>($"res://localization/{locale}.po");
        Assert.Equal(english.GetMessageList().Order(), catalog.GetMessageList().Order());
        Assert.Contains(locale, TranslationServer.GetLoadedLocales());

        using var interludeFont = GD.Load<FontFile>("res://fonts/Terminal_Square/Bold/terminal-square-bold.ttf");
        using var graffitiFont = GD.Load<FontFile>("res://fonts/Bytesized-Regular.ttf");
        foreach (var key in catalog.GetMessageList())
        {
            var text = catalog.GetMessage(key).ToString();
            Assert.False(string.IsNullOrWhiteSpace(text), $"{locale}: {key} is empty.");
            var font = key.StartsWith("GRAFFITI_", StringComparison.Ordinal) ? graffitiFont : interludeFont;
            foreach (var character in text.Where(c => !char.IsWhiteSpace(c)).Distinct())
                Assert.True(font.HasChar(character), $"{locale}: {key} lacks glyph {character}.");
        }
    }

    [Theory]
    [InlineData("de_DE", "Ich schritt\ndurch den \nNebel...")]
    [InlineData("en_US", "I walked\nthrough the\nfog...")]
    [InlineData("fr_CA", "J'avançais\ndans le\nbrouillard...")]
    [InlineData("es_ES", "I walked\nthrough the\nfog...")]
    public void Locale_UsesRegionalMatchesAndEnglishFallback(string locale, string expected)
    {
        var previous = TranslationServer.GetLocale();
        try
        {
            TranslationServer.SetLocale(locale);
            Assert.Equal(expected, TranslationServer.Translate("LEVEL_0_PRETEXT").ToString());
        }
        finally { TranslationServer.SetLocale(previous); }
    }

    [Theory]
    [InlineData("de")]
    [InlineData("en")]
    [InlineData("fr")]
    public void Interlude_TypesTheWholeTranslation(string locale)
    {
        var previous = TranslationServer.GetLocale();
        Control? interlude = null;
        try
        {
            TranslationServer.SetLocale(locale);
            interlude = GD.Load<PackedScene>("res://interludes/pretext-1.tscn").Instantiate<Control>();
            interlude.Set("typing", new[] { 0.001f });
            interlude.Set("hold", 60);
            var quote = interlude.GetNode<Label>("Quote");
            godot.Tree.Root.AddChild(interlude);
            var translated = quote.Tr(quote.Text).ToString();
            Assert.NotEqual(quote.Text, translated);
            Assert.Equal(translated.Length, quote.GetTotalCharacterCount());

            var clock = Stopwatch.StartNew();
            while (quote.VisibleCharacters != translated.Length && clock.Elapsed < TimeSpan.FromSeconds(5))
                godot.Engine.Update();
            Assert.Equal(translated.Length, quote.VisibleCharacters);
        }
        finally
        {
            interlude?.Free();
            TranslationServer.SetLocale(previous);
        }
    }
}
