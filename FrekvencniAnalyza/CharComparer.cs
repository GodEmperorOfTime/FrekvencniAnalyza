using System.Diagnostics.CodeAnalysis;

namespace FrekvencniAnalyza;

public static class CharComparer
{

  public static IEqualityComparer<char> CurrentCultureIgnoreCase { get; }
    = StringComparer.CurrentCultureIgnoreCase.ToCharComparer();

  public static IEqualityComparer<char> CurrentCultureIgnoreCaseIgnoreDiacritic { get; }
    = new CurrentCultureIgnoreCaseIgnoreDiacriticComparere();

  class CurrentCultureIgnoreCaseIgnoreDiacriticComparere : IEqualityComparer<char>
  {
    private static readonly IEqualityComparer<char> _icCharComparer
      = StringComparer.CurrentCultureIgnoreCase.ToCharComparer();
    

    public bool Equals(char x, char y)
      => _icCharComparer.Equals(x.RemoveDiacritics(), y.RemoveDiacritics());

    public int GetHashCode([DisallowNull] char obj)
      => _icCharComparer.GetHashCode(obj.RemoveDiacritics());
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