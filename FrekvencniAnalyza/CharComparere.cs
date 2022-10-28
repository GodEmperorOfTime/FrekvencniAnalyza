using System.Diagnostics.CodeAnalysis;

public static class CharComparere
{

  public static IEqualityComparer<char> CurrentCultureIgnoreCase { get; } 
    = StringComparer.CurrentCultureIgnoreCase.ToCharComparer();

  public static IEqualityComparer<char> CurrentCultureIgnoreCaseIgnoreDiacritic { get; } 
    = new CurrentCultureIgnoreCaseIgnoreDiacriticComparere();

  class CurrentCultureIgnoreCaseIgnoreDiacriticComparere : IEqualityComparer<char>
  {
    private static readonly IEqualityComparer<char> _icCharComparer
      = StringComparer.CurrentCultureIgnoreCase.ToCharComparer();

    static readonly IReadOnlyDictionary<char, char> substituce
      = new Dictionary<char, char>(_icCharComparer) {
      { 'ě', 'e' },
      { 'š', 's' },
      { 'č', 'c' },
      { 'ř', 'r' },
      { 'ž', 'z' },
      { 'ý', 'y' },
      { 'á', 'a' },
      { 'í', 'i' },
      { 'é', 'é' },
      { 'ú', 'u' },
      { 'ů', 'u' },
      { 'ó', 'o' },
      };

    static char BezDiakritiky(char c) => substituce.TryGetValue(c, out var r) ? r : c;

    public bool Equals(char x, char y)
      => _icCharComparer.Equals(BezDiakritiky(x), BezDiakritiky(y));

    public int GetHashCode([DisallowNull] char obj)
      => _icCharComparer.GetHashCode(BezDiakritiky(obj));
  }
  
  static IEqualityComparer<char> ToCharComparer(this IEqualityComparer<string> equalityComparer)
    => new CharEqualityComparereAdapter(equalityComparer);

  class CharEqualityComparereAdapter : IEqualityComparer<char>
  {

    readonly IEqualityComparer<string> stringComparer;

    public CharEqualityComparereAdapter(IEqualityComparer<string> stringComparer)
    {
      this.stringComparer = stringComparer ?? throw new ArgumentNullException(nameof(stringComparer));
    }

    public bool Equals(char x, char y)
      => stringComparer.Equals(x.CharToString(), y.CharToString());

    public int GetHashCode([DisallowNull] char obj)
      => stringComparer.GetHashCode(obj.CharToString());
  }

}





