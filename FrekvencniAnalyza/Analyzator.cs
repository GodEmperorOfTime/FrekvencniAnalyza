using System.Text;

namespace FrekvencniAnalyza;



public static class Analyzator
{

  /// <summary>
  /// Znaky, ktere bezne ignorovat. Tohle by elegantneji vyresil regex, ale co uz.
  /// </summary>
  public const string Interpunkce = ",.?!'\"„“/*-+<>-–:;@#$%^&*()[]{}§/|\\_…’`‚‘—";
  // ,.?!'"„“«»/*-+<>-–:;@#$%^&*()[]{}§/|\_…’`‚‘—

  public static List<FrekvencePismena> ZjistitFrekvence(
    string s, bool ignoreDiacritics, string charsToIgnore)
  {
    s = s.VyfiltrovatWhiteSpace().VyfiltrovatChary(charsToIgnore);
    if (ignoreDiacritics)
      s = s.RemoveDiacritics();
    var charTotalAsDouble = (double)s.Length;
    FrekvencePismena vytvritFrekvenci(char key, IEnumerable<char> chary)
    {
      var frekvence = chary.Count() / charTotalAsDouble;
      return new(key.ToUpper(), frekvence);
    }
    return s
      .GroupBy(c => c, vytvritFrekvenci, CharComparer.CurrentCultureIgnoreCase)
      .OrderByDescending(r => r.Frekvence)
      .ToList();
  }

}

public record FrekvencePismena(char Pismeno, double Frekvence)
{
  public override string ToString()
    => $"{Pismeno}: {Frekvence * 100.0:N1} %";
}

static class StringExtensions
{

  public static string VyfiltrovatWhiteSpace(this string s)
  {
    return s.Vyfiltrovat(jeWhiteSpace);
    static bool jeWhiteSpace(char c)
    {
      string charJakoString = c.CharToString();
      return string.IsNullOrWhiteSpace(charJakoString);
    }
  }

  public static string VyfiltrovatChary(this string s, string charsToIgnore)
  {
    var hashSet = charsToIgnore.ToHashSet();
    return s.Vyfiltrovat(hashSet.Contains);
  }

  static string Vyfiltrovat(this string s, Func<char, bool> filterPredicate)
  {
    var b = new StringBuilder();
    foreach (var c in s)
    {
      if (!filterPredicate.Invoke(c))
      {
        b.Append(c);
      }
    }
    return b.ToString();
  }

  public static string CharToString(this char c) => new(new char[] { c });

  public static char ToUpper(this char c) => c.ToString().ToUpper()[0];

  public static string RemoveDiacritics(this string s) => new(s.Select(RemoveDiacritics).ToArray());

  public static char RemoveDiacritics(this char c) => substituce.TryGetValue(c, out var r) ? r : c;


  static readonly IReadOnlyDictionary<char, char> substituce = CreateDiacriticsDict();

  static IReadOnlyDictionary<char, char> CreateDiacriticsDict()
  {
    var r =  new Dictionary<char, char>() {
      // Tohle bude fungovat jen pro cestinu
      // sem jen lowercasovany pismenka
        { 'ě', 'e' },
        { 'š', 's' },
        { 'č', 'c' },
        { 'ř', 'r' },
        { 'ž', 'z' },
        { 'ý', 'y' },
        { 'á', 'a' },
        { 'í', 'i' },
        { 'é', 'e' },
        { 'ú', 'u' },
        { 'ů', 'u' },
        { 'ó', 'o' },
        { 'ň', 'n' },
        { 'ü', 'u' },
        { 'ľ', 'l' },
        { 'ť', 't' },
        { 'ď', 'd' },
        { 'ö', 'o' },
        { 'ł', 'l' },
        { 'à', 'a' },
        { 'ä', 'a' },
        { 'è', 'e' },
      };
    r.ToList().ForEach(p => r.Add(p.Key.ToUpper(), p.Value.ToUpper()));
    return r;    
  }

}