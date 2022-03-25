using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
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
using app.widgets;

namespace app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string BaseCurrency = "USD";
        
        public MainWindow()
        {
            InitializeComponent();
            
            ConvertCurrencies();
        }

        private void OnCurrencyClicked(object sender, RoutedEventArgs e)
        {
            MenuItem? obj =  sender as MenuItem;

            if (obj != null)
            {
                string? code = Convert.ToString(obj.Header);
                CurrencyMenu.Header = obj.Header;
                if (code != null)
                    BaseCurrency = code;
            }
        }

        public static Object? GetPropertyValue(object srcObj, string propertyInfo)
        {
            PropertyInfo propertyInfoObj = srcObj.GetType().GetProperty(propertyInfo);

            if (propertyInfoObj == null)
                return null;

            // Get the value from property.
            object srcValue = srcObj.GetType()
                .InvokeMember(propertyInfoObj.Name,
                    BindingFlags.GetProperty,
                    null,
                    srcObj,
                    null);

            return srcValue;
        }
        
        private void ConvertCurrencies()
        {
            API_Obj? o = Rates.Import(BaseCurrency);
            if (o != null)
            {
                Currencies.Children.Clear();
                
                Type myType = o.conversion_rates.GetType();

                PropertyInfo[] myProperties = myType.GetProperties();

                foreach (var property in myProperties)
                {
                    object _property = GetPropertyValue(o.conversion_rates, property.Name);
                    if (_property != null)
                    {
                        string res = Convert.ToString(_property);
                        Currencies.Children.Add(new CurrencyInfo(BaseCurrency, property.Name, Convert.ToDouble(res)));
                    }
                }
            }
            else
            {
                MessageBox.Show("Unknown Error! Check your internet!");
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ConvertCurrencies();
        }
    }
}