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

  private async void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
  {
    await ProcessDataAsync();
  }

  private async void CharComparerCheckedChanged(object sender, RoutedEventArgs e)
  {
    await ProcessDataAsync();
  }

  private async Task ProcessDataAsync()
  {
    string inputText = InputTextBox?.Text ?? string.Empty;
    var comparer = GetCharComparer();
    string vystup = await Task.Run(() => GetTextOutput(inputText, comparer));
    OutputTextBox.Text = vystup;
  }

  private string GetTextOutput(string inputText, IEqualityComparer<char> comparer)
  {
    var frekvence = Analyzator.ZjistitFrekvence(inputText, comparer);
    return FormatOutput(frekvence);    
  }

  private IEqualityComparer<char> GetCharComparer()
  {
    if (this.IgnoreCaseIgnoreDiacriticRadioButton?.IsChecked == true)
      return CharComparer.CurrentCultureIgnoreCaseIgnoreDiacritic;
    else if (this.IgnoreCaseRadioButton?.IsChecked == true)
      return CharComparer.CurrentCultureIgnoreCase;
    else
      return CharComparer.CurrentCultureIgnoreCase;
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
