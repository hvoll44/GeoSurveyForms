using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using GeoSurveyForms.Models;
using System.Collections.ObjectModel;
using System.Collections;

namespace GeoSurveyForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    [System.Runtime.InteropServices.Guid("B6BC09F7-3C7D-41ED-9955-8398D287BCE2")]       //not sure why this was auto-generated???
    public partial class DailyTasksPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;                                      //promote connection object from a local variable to a private field
        private ObservableCollection<Index> _index;                                     //observable collection needed for new/updated data to appear in listview
        
        private string _fieldDayId;

        public DailyTasksPage(string fieldDayId)
        {
            InitializeComponent();
            this.Title = "Daily Tasks:";
            logo.Source = ImageSource.FromResource("GeoSurveyForms.Images.genericLogo.png");

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();           //gateway to database (contains methods for adding, updating, and deleting data from db)
            
            _fieldDayId = fieldDayId;

            LabelFieldDay.Text = _fieldDayId;
        }
        
        private class Index : DbTable
        {
            private string _taskId;             //[INotify] backing field used to raise event when property changes (PropertyChangedEventHandler)
            [PrimaryKey, MaxLength(255)]
            public string TaskId
            {
                get { return _taskId; }
                set
                {
                    if (_taskId == value)
                        return;
                    _taskId = value;

                    OnPropertyChanged();
                }
            }

            private string _sequence;
            [MaxLength(255)]
            public string Sequence
            {
                get { return _sequence; }
                set
                {
                    if (_sequence == value)
                        return;
                    _sequence = value;

                    OnPropertyChanged();
                }
            }
        } 

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<TaskData>();

            //create observable collection
            var daysData = await _connection.Table<TaskData>().Where(b => b.FieldDayId.Equals(_fieldDayId)).ToListAsync();
            
            var daysTasks = new List<Index>();
            foreach (var record in daysData)
                daysTasks.Add(new Index { TaskId = record.TaskId, Sequence = record.Sequence });

            var daysTasksUniuqe = daysTasks.GroupBy(b => b.TaskId).Select(b => b.First());
            var daysTasksUniqueSorted = daysTasksUniuqe.OrderBy(b => b.Sequence);

            _index = new ObservableCollection<Index>(daysTasksUniqueSorted);

            dailyTasksListView.ItemsSource = _index;
            
            base.OnAppearing();
        }


        async void DailyTasksListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var fieldTask = e.SelectedItem as Index;
            var taskName = fieldTask.TaskId;

            await Navigation.PushAsync(new FieldTasksGenericPage(_fieldDayId, taskName));
        }

        private void ExportButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ExportPage(_fieldDayId));
        }

        private void AdditionalTaskButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdditionalTaskPage(_fieldDayId));
        }

        async void DeleteTask_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var taskId = menuItem.CommandParameter;
            var taskIdStr = (string)taskId;

            if (taskIdStr == "DailyLog" || taskIdStr == "QC1" || taskIdStr == "IVS1" || taskIdStr == "DatasetA")
                DisplayAlert("Alert!", "This is a required task and cannot be deleted. No action taken", "Ok");
            else
            {
                var taskData = new TaskData();
                taskData.deleteData(_fieldDayId, taskIdStr);

                var selectedTask = _index.Where(b => b.TaskId.Equals(taskIdStr)).ToList();
                var firstTask = selectedTask[0];
                _index.Remove(firstTask);
                DisplayAlert("Deleted", "Task Deleted", "Ok");
            }
        }
    }
}