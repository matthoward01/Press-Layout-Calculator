using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Press_Layout_Calculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void CalculateButton_Clicked(object sender, EventArgs e)
        {
            Models.Calculate calculate = new Models.Calculate();
            Models.Results results = new Models.Results();

            calculate.StockWidth = stockWidthEntry.Text;
            calculate.StockHeight = stockHeightEntry.Text;
            calculate.FileWidth = fileWidthEntry.Text;
            calculate.FileHeight = fileHeightEntry.Text;
            calculate.GuttersWidth = gutterWidthEntry.Text;
            calculate.GuttersHeight = gutterHeightEntry.Text;
            calculate.RotationLocked = lockRotationCheck.IsChecked;

            results = Calculate(calculate);

            DisplayAlert("Results", 
                "Number Up: " + results.NumberUp + "\r\n" + 
                "Horizontal: " + results.Horizontal + "\r\n" +
                "Vertical: " + results.Vertical + "\r\n" +
                "Rotation: " + results.Rotation + "\r\n" +
                "Sheet Count: " + results.SheetCount + "\r\n", "OK");
        }

        private Models.Results Calculate(Models.Calculate calculate)
        {
            Models.Results results = new Models.Results();
            Models.Reserved reserved = new Models.Reserved();
            reserved.Bottom = 0.0f;
            reserved.Right = 0.0f;
            reserved.Top = 0.0f;
            reserved.Left = 0.0f;

            int qty = 0;

            float reservedH = reserved.Left + reserved.Right;
            float reservedV = reserved.Top + reserved.Bottom;

            //No Rotation
            int resultsH1 = (int)Math.Floor((float.Parse(calculate.StockWidth) - reservedH) / (float.Parse(calculate.FileWidth) + float.Parse(calculate.GuttersWidth)));
            int resultsV1 = (int)Math.Floor((float.Parse(calculate.StockHeight) - reservedV) / (float.Parse(calculate.FileHeight) + float.Parse(calculate.GuttersHeight)));
            int nUp1 = resultsH1 * resultsV1;

            //Rotated
            int resultsH2 = (int)Math.Floor((float.Parse(calculate.StockWidth) - reservedH) / (float.Parse(calculate.FileHeight) + float.Parse(calculate.GuttersWidth)));
            int resultsV2 = (int)Math.Floor((float.Parse(calculate.StockHeight) - reservedV) / (float.Parse(calculate.FileWidth) + float.Parse(calculate.GuttersHeight)));
            int nUp2 = resultsH2 * resultsV2;

            if (calculate.RotationLocked)
            {
                results.NumberUp = nUp1;
                results.Horizontal = resultsH1;
                results.Vertical = resultsV1;
                results.Rotation = 0;
                if (qty == 0 || nUp1 == 0)
                {
                    results.SheetCount = 0;
                }
                else
                {
                    results.SheetCount = (int)Math.Ceiling((float)qty/(float)nUp1);
                }
            }
            else
            {
                if ((nUp1 >= nUp2))
                {
                    results.NumberUp = nUp1;
                    results.Horizontal = resultsH1;
                    results.Vertical = resultsV1;
                    results.Rotation = 0;
                    if (qty == 0 || nUp1 == 0)
                    {
                        results.SheetCount = 0;
                    }
                    else
                    {
                        results.SheetCount = (int)Math.Ceiling((float)qty / (float)nUp1);
                    }
                }
                else
                {
                    results.NumberUp = nUp2;
                    results.Horizontal = resultsH2;
                    results.Vertical = resultsV2;
                    results.Rotation = 90;
                    if (qty == 0 || nUp2 == 0)
                    {
                        results.SheetCount = 0;
                    }
                    else
                    {
                        results.SheetCount = (int)Math.Ceiling((float)qty / (float)nUp2);
                    }
                }
            }
            return results;
        }
    }
}
