using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using GeoSurveyForms.Models;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace GeoSurveyForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExportPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;                                      //promote connection object from a local variable to a private field
        private string _fieldDayId;
        public string FieldDayTableList { get; set; }

        public ExportPage(string fieldDayId)
        {
            InitializeComponent();
            _fieldDayId = fieldDayId;
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();           //gateway to database (contains methods for adding, updating, and deleting data from db)
        }

        private async void ExecuteExportButton_Clicked(object sender, EventArgs e)
        {
            var taskData = new TaskData();
            taskData.exportData(_fieldDayId);

            //ZipFile.CreateFromDirectory((downloadsPath + directoryName), (downloadsPath + directoryName + ".zip"));
        }
    }
}