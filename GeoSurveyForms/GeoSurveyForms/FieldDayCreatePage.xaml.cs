using System;
using System.Text.RegularExpressions;
using GeoSurveyForms.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoSurveyForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FieldDayCreatePage : ContentPage
    {
        private SQLiteAsyncConnection _connection;                                      //promote connection object from a local variable to a private field

        public FieldDayCreatePage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();			//gateway to database (contains methods for adding, updating, and deleting data from db)
        }

        protected override async void OnAppearing()                                     //used to house methods that are too expensive to keep in constructor
        {
            await _connection.CreateTableAsync<TaskData>();                           //create table if it doesn't exits
            base.OnAppearing();
        }

        async private void CreateNewDayButton_Clicked(object sender, EventArgs e)
        {
            string day;
            string teamId;
            string teamMembers;
            string equipmentId;
            string equipmentType;

            bool validForm = false;

            while (true)
            {
                if (EntryCellFieldDayTeam.Text == null || EntryCellTeamMembers.Text == null || EntryCellEquipmentType.Text == null || EntryCellEquipmentType.Text == null)
                {
                    DisplayAlert("Incomplete form", "Please enter values in all cells", "Ok");
                    break;
                }

                if (!Regex.IsMatch(EntryCellFieldDayTeam.Text, "[Gg][0-9][0-9]"))
                {
                    DisplayAlert("Invalid Team Id", "Team ID must be G##. Ex/ G03", "Ok");
                    break;
                }

                if (!Regex.IsMatch(EntryCellFieldDayEquipment.Text, "[AaEe][0-9][0-9]"))
                {
                    DisplayAlert("Invalid Equipment ID", "Equipment ID must be A## (for array) or E## (for Person portable)", "Ok");
                    break;
                }

                validForm = true;
                break;
            }

            if (validForm)
            {
                var dateId = DatePicker.Date.ToString("yyyyMMdd");
                string fieldDayId = dateId + EntryCellFieldDayTeam.Text + EntryCellFieldDayEquipment.Text;

                day = DatePicker.Date.ToString("MM/dd/yyyy");
                teamId = EntryCellFieldDayTeam.Text.ToUpper();
                teamMembers = EntryCellTeamMembers.Text;
                equipmentId = EntryCellFieldDayEquipment.Text.ToUpper();
                equipmentType = EntryCellEquipmentType.Text;

                var taskData = new TaskData();
                taskData.createNewDay(fieldDayId, day, teamId, teamMembers, equipmentId, equipmentType);
                await Navigation.PopAsync();
            }
        }

        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}