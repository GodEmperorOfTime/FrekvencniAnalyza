using FrekvencniAnalyza;
using System.Security.Cryptography.X509Certificates;
using System.Text;

var s = await Console.In.ReadToEndAsync();

var frekvence = Analyzator.ZjistitFrekvence(s, true, Analyzator.Interpunkce);

foreach (var record in frekvence)
{
  Console.WriteLine(record);
}

