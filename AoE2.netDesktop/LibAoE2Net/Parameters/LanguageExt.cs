#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace AoE2NetDesktop.LibAoE2Net.Parameters;

/// <summary>
/// Language parameter.
/// </summary>
public enum Language
{
    /// <summary>
    /// English
    /// </summary>
    en,

    /// <summary>
    /// German
    /// </summary>
    de,

    /// <summary>
    /// Greek
    /// </summary>
    el,

    /// <summary>
    /// Spanish
    /// </summary>
    es,

    /// <summary>
    /// Spanish (Mexico)
    /// </summary>
    es_MX,

    /// <summary>
    /// French
    /// </summary>
    fr,

    /// <summary>
    /// Hindi
    /// </summary>
    hi,

    /// <summary>
    /// Italian
    /// </summary>
    it,

    /// <summary>
    /// Japanese
    /// </summary>
    ja,

    /// <summary>
    /// Korean
    /// </summary>
    ko,

    /// <summary>
    /// Malay
    /// </summary>
    ms,

    /// <summary>
    /// Dutch; Flemish
    /// </summary>
    nl,

    /// <summary>
    /// Portuguese
    /// </summary>
    pt,

    /// <summary>
    /// Russian
    /// </summary>
    ru,

    /// <summary>
    /// Turkish
    /// </summary>
    tr,

    /// <summary>
    /// Vietnamese
    /// </summary>
    vi,

    /// <summary>
    /// Chinese
    /// </summary>
    zh,

    /// <summary>
    /// Chinese (Taiwan)
    /// </summary>
    zh_TW,
}
#pragma warning restore SA1300 // Element should begin with upper-case letter

/// <summary>
/// extention of Language enum.
/// </summary>
public static class LanguageExt
{
    /// <summary>
    /// Gets AoE2.net API parameter from enum Language.
    /// </summary>
    /// <param name="language">Language.</param>
    /// <returns>API parameter string.</returns>
    public static string ToApiString(this Language language)
        => language.ToString().Replace('_', '-');
}