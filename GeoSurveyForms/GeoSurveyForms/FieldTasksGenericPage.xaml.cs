using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;
using GeoSurveyForms.Models;


namespace GeoSurveyForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FieldTasksGenericPage : ContentPage
    {
        private string _taskId;
        private string _fieldDayId;
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<TaskData> _indexs;
        public bool entryCellsActivated = false;

        public FieldTasksGenericPage(string fieldDayId, string taskId)
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
            
            var indexLables = await _connection.Table<TaskData>().Where(b => b.TaskId.Equals(_taskId) && b.FieldDayId.Equals(_fieldDayId) && b.ReadOnly.Equals("Yes")).OrderBy(b => b.Sequence).ToListAsync();
            CollectionView.ItemsSource = indexLables;
            
            var indexs = await _connection.Table<TaskData>().Where(b => b.TaskId.Equals(_taskId) && b.FieldDayId.Equals(_fieldDayId) && b.ReadOnly.Equals("No")).OrderBy(b => b.Sequence).ToListAsync();
            _indexs = new ObservableCollection<TaskData>(indexs);
            EntryCellListView.ItemsSource = _indexs;

            base.OnAppearing();
        }

        private void OkButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void EntryCell_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var valueEntered = sender as EntryCell;
            var textEntered = valueEntered.Text;
            var entryLabel = valueEntered.Label;

            var targetRow = await _connection.Table<TaskData>().Where(b => b.TaskId.Equals(_taskId) && b.FieldDayId.Equals(_fieldDayId) && b.PropertyName.Equals(entryLabel)).ToListAsync();
            if (targetRow.Count > 0)
            {
                targetRow[0].PropertyValue = textEntered;
                await _connection.UpdateAsync(targetRow[0]);
            }
        }
    }
}