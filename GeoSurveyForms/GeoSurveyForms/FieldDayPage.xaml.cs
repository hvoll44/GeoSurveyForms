using System;
using SQLite;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoSurveyForms.Models;
using System.Collections.Generic;

namespace GeoSurveyForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FieldDayPage : ContentPage
    {
        public static string CurrentFieldDay { get; set; }

        private SQLiteAsyncConnection _connection;
        private ObservableCollection<string> _index;

        public FieldDayPage()
        {
            InitializeComponent();

            logo.Source = ImageSource.FromResource("GeoSurveyForms.Images.genericLogo.png");
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<TaskData>();

            //create observable collection
            var data = await _connection.Table<TaskData>().ToListAsync();

            List<string> days = new List<string>();
            foreach (var record in data)
                if (!days.Contains(record.FieldDayId))
                    days.Add(record.FieldDayId);

            _index = new ObservableCollection<string>(days);

            fieldDayListView.ItemsSource = _index;

            base.OnAppearing();
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var selectedDay = (String)menuItem.CommandParameter;
            
            var taskData = new TaskData();
            taskData.deleteData(selectedDay);

            _index.Remove(selectedDay);
        }

        async void fieldDayListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            fieldDayListView.SelectedItem = null;
        }

        async void fieldDayListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var fieldDay = e.Item;
            var fieldDayStr = (String)fieldDay;
            await Navigation.PushAsync(new DailyTasksPage(fieldDayStr));
        }

        async void NewDay_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new FieldDayCreatePage());
        }
    }
}