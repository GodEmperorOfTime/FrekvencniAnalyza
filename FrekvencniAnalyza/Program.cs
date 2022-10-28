using FrekvencniAnalyza;
using System.Security.Cryptography.X509Certificates;
using System.Text;



var frekvence = Analyzator.ZjistitFrekvence(
  "aaa bbb cccCCCččččss", CharComparer.CurrentCultureIgnoreCaseIgnoreDiacritic, string.Empty);

foreach(var record in frekvence)
{
  Console.WriteLine(record);
}

