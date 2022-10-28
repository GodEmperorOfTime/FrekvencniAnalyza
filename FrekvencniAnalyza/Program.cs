using System.Security.Cryptography.X509Certificates;
using System.Text;



var frekvence = Analyzator.ZjistitFrekvence("aaa bbb cccCCCččččss", CharComparere.CurrentCultureIgnoreCaseIgnoreDiacritic);

foreach(var record in frekvence)
{
  Console.WriteLine(record);
}

public record FrekvencePismena(char Pismeno, double Frekvence)
{
  public override string ToString()
    => $"{Pismeno}: {Frekvence * 100.0:N1} %";
}

public static class Analyzator
{
  public static List<FrekvencePismena> ZjistitFrekvence(string s, IEqualityComparer<char> charComparer)
  {
    s = s.VyfiltrovatWhiteSpace();
    var charTotalAsDouble = (double)s.Length;
    FrekvencePismena vytvritFrekvenci(char key, IEnumerable<char> chary)
    {
      var frekvence = chary.Count() / charTotalAsDouble;
      return new (key.ToUpper(), frekvence);
    }
    return s
      .GroupBy(c => c, vytvritFrekvenci, charComparer)
      .OrderByDescending(r => r.Frekvence)
      .ToList();
  }

}

static class StringExtensions
{

  public static string VyfiltrovatWhiteSpace(this string s)
  {
    var b = new StringBuilder();
    foreach (var c in s)
    {
      string charJakoString = c.CharToString();
      if (!string.IsNullOrWhiteSpace(charJakoString))
      {
        b.Append(c);
      }
    }
    return b.ToString();
  }

  public static string CharToString(this char c) => new (new char[] { c });

  public static char ToUpper(this char c) => c.ToString().ToUpper()[0];

}
