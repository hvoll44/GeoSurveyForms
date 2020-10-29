using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoSurveyForms.Models;
using SQLite;


namespace GeoSurveyForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdditionalTaskPage : ContentPage
    {
        private string _fieldDayId;
        public AdditionalTaskPage(string fieldDayId)
        {
            InitializeComponent();
            _fieldDayId = fieldDayId;
        }

        async void AddDatasetButton_Clicked(object sender, EventArgs e)
        {
            var taskData = new TaskData();
            taskData.createNewTask(_fieldDayId, "Dataset", "Dataset_Table");
            DisplayAlert("Task added", "New dataset added", "Ok");
            await Navigation.PopAsync();
        }

        async void AddQCButton_Clicked(object sender, EventArgs e)
        {
            var taskData = new TaskData();
            taskData.createNewTask(_fieldDayId, "QC", "Static_Repeatability_Test_Table");
            DisplayAlert("Task added", "New static test added", "Ok");
            await Navigation.PopAsync();
        }

        async void AddIVSButton_Clicked(object sender, EventArgs e)
        {
            var taskData = new TaskData();
            taskData.createNewTask(_fieldDayId, "IVS", "IVS_Daily_Results_Table");
            DisplayAlert("Task added", "New IVS test added", "Ok");
            await Navigation.PopAsync();
        }
    }
}