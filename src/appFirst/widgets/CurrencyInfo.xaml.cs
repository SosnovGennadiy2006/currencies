using System;
using System.Windows.Controls;

namespace app.widgets;

public partial class CurrencyInfo : UserControl
{
    public CurrencyInfo(string codeFrom, string codeTo, double result)
    {
        InitializeComponent();

        CurrencyFromCode.Content = codeFrom;
        CurrencyToCode.Content = codeTo;
        CurrencyRes.Content = Convert.ToString(result);
    }
}