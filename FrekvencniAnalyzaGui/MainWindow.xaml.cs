using FrekvencniAnalyza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrekvencniAnalyzaGui;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
  }

  private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
  {
    string inputText = InputTextBox.Text;
    var frekvence = Analyzator.ZjistitFrekvence(inputText, CharComparer.CurrentCultureIgnoreCase);
    OutputTextBox.Text = FormatOutput(frekvence);
  }

  private string FormatOutput(List<FrekvencePismena> frekvence)
  {
    var b = new StringBuilder();
    foreach (var rec in frekvence)
    {
      b.AppendLine($"{rec.Pismeno}\t{rec.Frekvence:N6}");
    }
    return b.ToString();
  }
}
