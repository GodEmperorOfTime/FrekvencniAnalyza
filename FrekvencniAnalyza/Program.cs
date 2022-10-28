using System.Security.Cryptography.X509Certificates;
using System.Text;



var frekvence = Analyzator.ZjistitFrekvence("aaa bbb cccCCCččččss", CharComparere.CurrentCultureIgnoreCaseIgnoreDiacritic);

foreach(var record in frekvence)
{
  Console.WriteLine(record);
}



record FrekvencePismena(char Pismeno, double Frekvence)
{
  public override string ToString()
    => $"{Pismeno}: {Frekvence * 100.0:N1} %";
}

static class Analyzator
{
  public static List<FrekvencePismena> ZjistitFrekvence(string s, IEqualityComparer<char> charComparer)
  {
    s = s.VyfiltrovatWhiteSpace();
    var groupy = s
      .ToArray()
      .GroupBy(c => c, charComparer)
      .Select(g => (Pismeno: g.Key.ToUpper(), Pocet: g.Count()))
      ;
    var list = new List<FrekvencePismena>();
    foreach (var group in groupy)
    {
      var frekvence = group.Pocet / (double)s.Length;
      FrekvencePismena item = new(group.Pismeno, frekvence);
      list.Add(item);
    }
    return list;
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
