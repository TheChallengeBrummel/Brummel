using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microcharts;
using Mobile.Services;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BIView : ContentPage
    {
        private static readonly ColorProvider colorProvider = new ColorProvider();

        public BIView(TransactionsProvider transactionsProvider)
        {
            InitializeComponent();
            var transactions = transactionsProvider.GetTransactions();
            PrepareCashflowChart(transactions);
            PrepareExpensesChart(transactions);
        }

        private void PrepareCashflowChart(List<Transaction> transactions)
        {
            var entries = transactions
                .GroupBy(t => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(t.CreatedOn.Month))
                .Select(month => new KeyValuePair<string, float>(month.Key, Convert.ToSingle(month.Sum(t => t.Amount))))
                .Select(summarizedMonth => new Microcharts.Entry(summarizedMonth.Value)
                {
                    Label = summarizedMonth.Key,
                    ValueLabel = string.Format("{0:0.00}", summarizedMonth.Value),
                    Color = SKColor.Parse("#266489")
                });
            balanceChart.Chart = new LineChart()
            {
                Entries = entries,
                LabelTextSize = 32
            };
        }

        private void PrepareExpensesChart(List<Transaction> transactions)
        {
            var entries = transactions
                .GroupBy(t => t.Description)
                .Select(description => new KeyValuePair<string, float>(description.Key, Convert.ToSingle(description.Sum(transaction => transaction.Amount))))
                .Where(summary => summary.Value < 0)
                .Select(summary => new Microcharts.Entry(summary.Value)
                {
                    Label = summary.Key,
                    ValueLabel = summary.Value.ToString(),
                    Color = colorProvider.GetNextColor()
                });

            expensesChart.Chart = new DonutChart()
            {
                Entries = entries,
                LabelTextSize = 32
            };
        }
    }
}