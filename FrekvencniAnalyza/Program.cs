using System.Diagnostics.CodeAnalysis;
using System.Text;

//var clen = int.Parse(args[0]);
//var suma = Fibbonaci.Suma(clen);
//Console.WriteLine(suma);

var frekvence = Analyzator.ZjistitFrekvence("aaa bbb cccCCC");

//// frekvence.ForEach(Console.WriteLine);

foreach(var record in frekvence)
{
  Console.WriteLine(record);
}


//static class Fibbonaci
//{
//  public static double Suma(int clen)
//  {
//    if (clen <= 0)
//      throw new ArgumentOutOfRangeException("n");
//    else if (clen == 1)
//      return 1.0;
//    else if (clen == 2)
//      return 0.0;
//    else
//    {
//      var a = 1;
//      var b = 1;
//      double sum = 0.0;
//      bool jePlus = true;
//      for (int n = 3; n <= clen; n++)
//      {
//        var c = a + b;
//        a = b;
//        b = c;
//        var prevracena = 1.0 / c;
//        sum += jePlus ? prevracena : -prevracena;
//        jePlus = !jePlus;
//      }
//      return sum;
//    }
      
//  }
//}


record FrekvencePismena(char Pismeno, double Frekvence)
{
  public override string ToString()
    => $"{Pismeno}: {Frekvence * 100.0:N1} %";
}

static class Analyzator
{
  public static List<FrekvencePismena> ZjistitFrekvence(string s)
  {
    s = s.VyfiltrovatWhiteSpace();
    var groupy = s
      .ToArray()
      .GroupBy(CharToString, StringComparer.CurrentCultureIgnoreCase)
      .Select(g => (Pismeno: g.Key.ToUpper()[0], Pocet: g.Count()))
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

  private static string CharToString(this char c)
  {
    return new string(new char[] { c });
  }

  static string VyfiltrovatWhiteSpace(this string s)
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

}

class DiakritikaComparet : IEqualityComparer<string>
{

  static readonly Dictionary<char, char> substituce = new Dictionary<char, char>() { };
  
  static string BezDiakritiky(string s)
  {
    var b = new StringBuilder();
    foreach (var c in s)
    {
      var subChar = substituce.TryGetValue(c, out var r)
        ? r : c;
      b.Append(subChar);      
    }
    return b.ToString();
  }
  
  public bool Equals(string? x, string? y)
  {
    throw new NotImplementedException();
  }

  public int GetHashCode([DisallowNull] string obj)
  {
    throw new NotImplementedException();
  }
}

