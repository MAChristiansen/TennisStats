
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace TennisStats
{

[Activity(Label = "ActivityUserProfil")]
    public class ActivityUserProfil : Activity
    {
        Button btOverall, btLastYear, btLastMonth, btMatch;
        PlotView plotViewModel;
        public PlotModel myModel { get; set; }

        private int[] modelAllocValues = new int[] { 12, 5, 2, 40, 40, 1 };
        private String[] modelAllocations = new string[] { "slice1", "slice 2", "slice 3", "slice 4", "slice 5", "slice 6" };
        private String[] colors = new string[] { "#7DA137", "#6EA6F3", "#999999", "#3B8DA5", "#F0BA22", "#EC8542" };
        int total = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //finding layouts
            SetContentView(Resource.Layout.UserProfil);
            plotViewModel = FindViewById<PlotView>(Resource.Id.plotViewModel);

            //Model Allocation Pie Chart
            var plotModel2 = new PlotModel();
            var pieSeries2 = new PieSeries();
            pieSeries2.InsideLabelPosition = 0.0;
            pieSeries2.InsideLabelFormat = null;

            for (int i = 0; i < modelAllocations.Length && i < modelAllocValues.Length; i++) {

                pieSeries2.Slices.Add(new PieSlice(modelAllocations[i], modelAllocValues[i]) {Fill = OxyColor.Parse(colors[i]) });
                pieSeries2.OutsideLabelFormat = "NameOfStat";

                double mValue = modelAllocValues[i];
                double percentValue = (mValue / total) * 100;
                String percent = percentValue.ToString("#.##");
            }
            //Plot the graph
            plotModel2.Series.Add(pieSeries2);
            myModel = plotModel2;
            plotViewModel.Model = myModel;
        }
    }
}
