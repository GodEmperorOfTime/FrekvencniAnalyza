using System.Diagnostics.CodeAnalysis;
using System.Text;

var clen = int.Parse(args[0]);
var suma = Fibbonaci.SumaPrevracenych(clen);
Console.WriteLine(suma);


static class Fibbonaci
{
  public static double SumaPrevracenych(int clen)
  {
    if (clen <= 0)
      throw new ArgumentOutOfRangeException("n");
    else if (clen == 1)
      return 1.0;
    else if (clen == 2)
      return 0.0;
    else
    {
      var a = 1;
      var b = 1;
      double sum = 0.0;
      bool jePlus = true;
      for (int n = 3; n <= clen; n++)
      {
        var c = a + b;
        var prevracenaC = 1.0 / c;
        sum = sum + (jePlus ? prevracenaC : -prevracenaC);

        a = b;
        b = c;
        jePlus = !jePlus;
      }
      return sum;
    }
  }
}
