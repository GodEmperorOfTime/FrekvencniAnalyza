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
    this.CharsToIgnoreTextBox.Text = ",.?!'\"„“/*-+<>-–:;@#$%^&*()[]{}§/|\\_…’`‚‘—";
    // ,.?!'"„“«»/*-+<>-–:;@#$%^&*()[]{}§/|\_…’`‚‘—
  }

  private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
  {
    await ProcessDataAsync();
  }

  private async void IgnoreDiacriticsCheckedChanged(object sender, RoutedEventArgs e)
  {
    await ProcessDataAsync();
  }

  private async Task ProcessDataAsync()
  {
    string inputText = InputTextBox?.Text ?? string.Empty;
    var charsToIgnore = CharsToIgnoreTextBox?.Text ?? string.Empty;
    var ignoreDiacritics = IgnoreDiacriticsCheckBox.IsChecked == true;
    string vystup = await Task.Run(() => GetTextOutput(inputText, ignoreDiacritics, charsToIgnore));
    OutputTextBox.Text = vystup;
  }

  private string GetTextOutput(string inputText, bool ignoreDiacritics, string charsToIgnore)
  {
    var frekvence = Analyzator.ZjistitFrekvence(inputText, ignoreDiacritics, charsToIgnore);
    return FormatOutput(frekvence);    
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
