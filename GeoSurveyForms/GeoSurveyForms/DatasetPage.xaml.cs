using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;
using GeoSurveyForms.Models;

namespace GeoSurveyForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatasetPage : ContentPage
    {
        private string _taskId;
        private string _fieldDayId;
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<TaskData> _indexs;
        public bool entryCellsActivated = false;

        public DatasetPage(string fieldDayId, string taskId)
        {
            InitializeComponent();
            _taskId = taskId;
            _fieldDayId = fieldDayId;
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            this.Title = _taskId;
        }

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<TaskData>();

            var indexs = await _connection.Table<TaskData>().Where(b => b.FieldDayId.Equals(_fieldDayId) && b.TaskId.Equals(_taskId)).ToListAsync();
            _indexs = new ObservableCollection<TaskData>(indexs);

            string projectId = Project_ID.Text;
            string date = Date.Text;
            string team = Team_ID.Text;
            //string TeamMembers = TeamMembers.Text;
            string completed = Completed_Datasets.Text;
            string partial = Partial_Datasets.Text;
            string teamHours = Team_Hours.Text;
            string standbyTime = Standby_Time.Text;
            string comments = Comments.Text;
            string lessonsLearned = Lessons_Learned.Text;
            string grids = Grids_Reacquired.Text;
            string numberTargetsReaquired = Number_Targets_Reacquired.Text;

            base.OnAppearing();
        }

        private void OkButton_Clicked(object sender, EventArgs e)
        {
            //await _connection.UpdateAsync(_indexs);
            Navigation.PopAsync();
        }

        async void EntryCell_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (entryCellsActivated)
            {
                var valueEntered = sender as EntryCell;                 //convert object so the entered value can be retrieved
                var textEntered = valueEntered.Text;                    //get entered value
                var entryLabel = valueEntered.Label;                    //get label of value that was entered

                var targetRow = await _connection.Table<TaskData>().Where(b => b.TaskId.Equals(_taskId) && b.FieldDayId.Equals(_fieldDayId) && b.PropertyName.Equals(entryLabel)).ToListAsync();

                targetRow[0].PropertyValue = textEntered;               //set db property equal to entered value
                await _connection.UpdateAsync(targetRow[0]);            //persist changes in db (todo use updateall to pass a list of objects)
            }
        }

        private void EntryCell_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("test", "tapperd", "ok");
        }




    }
}