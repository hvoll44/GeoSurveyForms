using SQLite;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace GeoSurveyForms.Models
{
	public class TaskData : DbTable
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		private string _propertyName;		//[INotify] backing field used to raise event when property changes (PropertyChangedEventHandler)
		[MaxLength(255)]
		public string PropertyName
		{
			get { return _propertyName; }
			set
			{
				if (_propertyName == value)
					return;
				_propertyName = value;

				OnPropertyChanged();
			}
		}

		private string _propertyValue;
		[MaxLength(255)]
		public string PropertyValue
		{
			get { return _propertyValue; }
			set
			{
				if (_propertyValue == value)
					return;
				_propertyValue = value;

				OnPropertyChanged();
			}
		}

		private string _fieldDayId;
		[MaxLength(255)]
		public string FieldDayId
		{
			get { return _fieldDayId; }
			set
			{
				if (_fieldDayId == value)
					return;
				_fieldDayId = value;

				OnPropertyChanged();
			}
		}

		private string _taskId;
		[MaxLength(255)]
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

		private string _pageId;
		[MaxLength(255)]
		public string PageId
		{
			get { return _pageId; }
			set
			{
				if (_pageId == value)
					return;
				_pageId = value;

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

		private string _keyboard;
		[MaxLength(255)]
		public string Keyboard
		{
			get { return _keyboard; }
			set
			{
				if (_keyboard == value)
					return;
				_keyboard = value;

				OnPropertyChanged();
			}
		}

		private string _readOnly;
		[MaxLength(255)]
		public string ReadOnly
		{
			get { return _readOnly; }
			set
			{
				if (_readOnly == value)
					return;
				_readOnly = value;

				OnPropertyChanged();
			}
		}

		private string _exportTable;
		[MaxLength(255)]
		public string ExportTable
		{
			get { return _exportTable; }
			set
			{
				if (_exportTable == value)
					return;
				_exportTable = value;

				OnPropertyChanged();
			}
		}

		private SQLiteAsyncConnection _connection;

		public TaskData()
		{
			_connection = DependencyService.Get<ISQLiteDb>().GetConnection();
		}

		public void CreateDailyLogTemplate(string fieldDayId, string task, string date, string teamId, string teamMembers, string equipment, string equipmentType)
		{
			_connection.CreateTableAsync<TaskData>();
			_connection.InsertAsync(new TaskData { PropertyName = "Project_ID",					PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Aa", PageId = "DailyLogPage", Keyboard = "Default", ReadOnly = "Yes", ExportTable = "Daily_Log_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Date",						PropertyValue = date,		FieldDayId = fieldDayId, TaskId = task, Sequence = "Ab", PageId = "DailyLogPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Daily_Log_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Team_ID",					PropertyValue = teamId,		FieldDayId = fieldDayId, TaskId = task, Sequence = "Ac", PageId = "DailyLogPage", Keyboard = "Default", ReadOnly = "Yes", ExportTable = "Daily_Log_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Team_Members",				PropertyValue = teamMembers,FieldDayId = fieldDayId, TaskId = task, Sequence = "Ad", PageId = "DailyLogPage", Keyboard = "Default", ReadOnly = "Yes", ExportTable = "Daily_Log_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Completed_Datasets",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ae", PageId = "DailyLogPage", Keyboard = "Default", ReadOnly = "No", ExportTable = "Daily_Log_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Partial_Datasets",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Af", PageId = "DailyLogPage", Keyboard = "Default", ReadOnly = "No", ExportTable = "Daily_Log_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Team_Hours",					PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ag", PageId = "DailyLogPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Daily_Log_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Standby_Time",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ah", PageId = "DailyLogPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Daily_Log_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Comments",					PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ai", PageId = "DailyLogPage", Keyboard = "Default", ReadOnly = "No", ExportTable = "Daily_Log_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Lessons_Learned",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Aj", PageId = "DailyLogPage", Keyboard = "Default", ReadOnly = "No", ExportTable = "Daily_Log_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Grids_Reacquired",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ak", PageId = "DailyLogPage", Keyboard = "Default", ReadOnly = "No", ExportTable = "Daily_Log_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Number_Targets_Reacquired",	PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Al", PageId = "DailyLogPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Daily_Log_Table" });
		}

		public void CreatePPQCTemplate(string fieldDayId, string task, string date, string teamId, string teamMembers, string equipment, string equipmentType)
		{
			string sequence;
			sequence = (task == "QC1") ? "B" : "F";
			
			string amPm;
			amPm = (task == "QC1") ? "AM" : "PM";

			string qcId = fieldDayId + task;

			_connection.CreateTableAsync<TaskData>();
			_connection.InsertAsync(new TaskData { PropertyName = "Project_ID",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task,	Sequence = sequence + "a", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Repeatability_ID", PropertyValue = qcId,		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "b", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "AM_PM",					PropertyValue = amPm,		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "c", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Collection_Date",		PropertyValue = date,		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "d", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Team_ID",				PropertyValue = teamId,		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "e", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Sensor_ID",				PropertyValue = equipment,	FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "f", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Location",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task,	Sequence = sequence + "g", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Geophysical_Sensor",		PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "h", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Item",		PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "i", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Item_Height",PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "j", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_PreBkg_CH1",	PropertyValue = "0",		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "k", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_PreBkg_CH2",	PropertyValue = "0",		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "l", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_PreBkg_CH3",	PropertyValue = "0",		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "m", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_PreBkg_CH4",	PropertyValue = "0",		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "n", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_Spike_CH1",		PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "o", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_Spike_CH2",		PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "p", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_Spike_CH3",		PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "q", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_Spike_CH4",		PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "r", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_PostBkg_CH1",	PropertyValue = "0",		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "s", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_PostBkg_CH2",	PropertyValue = "0",		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "t", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_PostBkg_CH3",	PropertyValue = "0",		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "u", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_PostBkg_CH4",	PropertyValue = "0",		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "v", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Field_Comments",	PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "w", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Files",		PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "x", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Repeatability_Test_Table" });
		}

		public void CreateArrayQCTemplate(string fieldDayId, string task, string date, string teamId, string teamMembers, string equipment, string equipmentType)
		{
			string sequence;
			sequence = (task == "QC1") ? "B" : "F";

			string amPm;
			amPm = (task == "QC1") ? "AM" : "PM";

			string qcId = fieldDayId + task;
			string files = qcId + ".xyz";

			_connection.CreateTableAsync<TaskData>();
			_connection.InsertAsync(new TaskData { PropertyName = "Project_ID",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "a", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Repeatability_ID",		PropertyValue = qcId,		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "b", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "AM_PM",							PropertyValue = amPm,		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "c", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Collection_Date",				PropertyValue = date,		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "d", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Team_ID",						PropertyValue = teamId,		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "e", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Sensor_ID",						PropertyValue = equipment,	FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "f", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Files",				PropertyValue = files,		FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "g", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Location",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "h", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Geophysical_Sensor",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "i", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_Spike_Coil1_CH2",		PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "j", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_Spike_Coil2_CH2",		PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "k", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_Spike_Coil3_CH2",		PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "l", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_Spike_Coil4_CH2",		PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "m", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Observed_Spike_Coil5_CH2",		PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "n", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Item_Coil1",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "o", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Item_Coil2",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "p", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Item_Coil3",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "q", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Item_Coil4",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "r", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Item_Coil5",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "s", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Item_Coil1_Height",	PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "t", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Item_Coil2_Height",	PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "u", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Item_Coil3_Height",	PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "v", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Item_Coil4_Height",	PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "w", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Test_Item_Coil5_Height",	PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "x", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Static_Field_Comments",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = sequence + "y", PageId = "QC1Page", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Static_Array_Table" });
		}

		public void CreateIVSTemplate(string fieldDayId, string task, string date, string teamId, string teamMembers, string equipment, string equipmentType)
		{
			string sequence;
			sequence = (task == "IVS1") ? "C" : "G";

			string amPm;
			amPm = (task == "QC1") ? "AM" : "PM";

			string ivsTestId = fieldDayId + task;

			_connection.CreateTableAsync<TaskData>();
			_connection.InsertAsync(new TaskData { PropertyName = "Project_ID",				PropertyValue = "",				FieldDayId = fieldDayId, TaskId = task,	Sequence = sequence, PageId = "IVSPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "IVS_Daily_Results_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "IVS_Test_ID",			PropertyValue = ivsTestId,		FieldDayId = fieldDayId, TaskId = task,	Sequence = sequence, PageId = "IVSPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "IVS_Daily_Results_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Geophysical_Sensor",		PropertyValue = equipmentType,	FieldDayId = fieldDayId, TaskId = task,	Sequence = sequence, PageId = "IVSPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "IVS_Daily_Results_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Sensor_ID",				PropertyValue = equipment,		FieldDayId = fieldDayId, TaskId = task,	Sequence = sequence, PageId = "IVSPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "IVS_Daily_Results_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Team_ID",				PropertyValue = teamId,			FieldDayId = fieldDayId, TaskId = task,	Sequence = sequence, PageId = "IVSPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "IVS_Daily_Results_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Date",					PropertyValue = date,			FieldDayId = fieldDayId, TaskId = task,	Sequence = sequence, PageId = "IVSPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "IVS_Daily_Results_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "AM_PM",					PropertyValue = amPm,			FieldDayId = fieldDayId, TaskId = task,	Sequence = sequence, PageId = "IVSPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "IVS_Daily_Results_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Location",				PropertyValue = "IVS",			FieldDayId = fieldDayId, TaskId = task,	Sequence = sequence, PageId = "IVSPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "IVS_Daily_Results_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Test_Item_ID",			PropertyValue = "",				FieldDayId = fieldDayId, TaskId = task,	Sequence = sequence, PageId = "IVSPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "IVS_Daily_Results_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Field_Comments",			PropertyValue = "",				FieldDayId = fieldDayId, TaskId = task,	Sequence = sequence, PageId = "IVSPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "IVS_Daily_Results_Table" });
		}
																																																							 
		public void CreateGPSQCTemplate(string fieldDayId, string task, string date, string teamId, string teamMembers, string equipment, string equipmentType)
		{
			string geodedicId = fieldDayId + "GPSQC";

			_connection.CreateTableAsync<TaskData>();
			_connection.InsertAsync(new TaskData { PropertyName = "Project_ID",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task,	Sequence = "Da", PageId = "GPSQCPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Geodetic_Functionality_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Team_ID",						PropertyValue = teamId,		FieldDayId = fieldDayId, TaskId = task,	Sequence = "Db", PageId = "GPSQCPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Geodetic_Functionality_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Date",							PropertyValue = date,		FieldDayId = fieldDayId, TaskId = task,	Sequence = "Dc", PageId = "GPSQCPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Geodetic_Functionality_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Geodetic_Functionality_ID",		PropertyValue = geodedicId,	FieldDayId = fieldDayId, TaskId = task,	Sequence = "Dd", PageId = "GPSQCPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Geodetic_Functionality_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Geodetic_Sensor",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task,	Sequence = "Df", PageId = "GPSQCPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Geodetic_Functionality_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Control_Point_ID",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task,	Sequence = "De", PageId = "GPSQCPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Geodetic_Functionality_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Measured_X",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task,	Sequence = "Dg", PageId = "GPSQCPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Geodetic_Functionality_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Measured_Y",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task,	Sequence = "Dh", PageId = "GPSQCPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Geodetic_Functionality_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Comments",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task,	Sequence = "Di", PageId = "GPSQCPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Geodetic_Functionality_Table" });
		}

		public void CreateDatasetTemplate(string fieldDayId, string task, string date, string teamId, string teamMembers, string equipment, string equipmentType)
		{
			var datasetId = fieldDayId + task.Substring(7, 1);
			var xzyFilename = datasetId + ".xyz";

			_connection.CreateTableAsync<TaskData>();
			_connection.InsertAsync(new TaskData { PropertyName = "Project_ID",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ea", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Dataset_ID",						PropertyValue = datasetId,	FieldDayId = fieldDayId, TaskId = task, Sequence = "Eb", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Collection_Date",				PropertyValue = date,		FieldDayId = fieldDayId, TaskId = task, Sequence = "Ec", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Team_ID",						PropertyValue = teamId,		FieldDayId = fieldDayId, TaskId = task, Sequence = "Ed", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Team_Members",					PropertyValue = teamMembers,FieldDayId = fieldDayId, TaskId = task, Sequence = "Ee", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Geophysical_Sensor",				PropertyValue = equipment,	FieldDayId = fieldDayId, TaskId = task, Sequence = "Ef", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Location",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Eg", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "Yes", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Start_Time",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Eh", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Battery_Start_Voltage",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ei", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Line_Direction",					PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ej", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Field_Notes",					PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ek", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Battery_End_Voltage",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "El", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "End_Time",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Em", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Raw_Filenames",					PropertyValue = datasetId,	FieldDayId = fieldDayId, TaskId = task, Sequence = "En", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "XYZ_Filenames",					PropertyValue = xzyFilename,FieldDayId = fieldDayId, TaskId = task, Sequence = "Eo", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Completed",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ep", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Comments",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Eq", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Culture",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Er", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Terrain",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Es", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Vegetation",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Et", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Weather",						PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Eu", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Interference_Sources",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ev", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Geodetic_Sensor",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ew", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Positioning_Type",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ex", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Geophysical_Sensor_Description",	PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ey", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Geophysical_System",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ez", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Base_Station_File",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ez", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Lot_ID",							PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ez", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Offset_Parameter_X",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ez", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Offset_Parameter_Y",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ez", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Positional_Offset",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ez", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Positioning_System_Description",	PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ez", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Raw_Coordinate_System",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ez", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Raw_Coordinate_Units",			PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ez", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Repeatability_ID",				PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ez", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
			_connection.InsertAsync(new TaskData { PropertyName = "Repeat_Lines",					PropertyValue = "",			FieldDayId = fieldDayId, TaskId = task, Sequence = "Ez", PageId = "DatasetPage", Keyboard = "Numeric", ReadOnly = "No", ExportTable = "Dataset_Table" });
		}

		public void createNewDay(string FieldDayId, string Date, string TeamId, string TeamMembers, string EquipmentId, string EquipmentType)
		{
			var platformType = EquipmentId[0];
			
			CreateDailyLogTemplate(FieldDayId, "DailyLog", Date, TeamId, TeamMembers, EquipmentId, EquipmentType);
			
			if (platformType == 'A')
			{
				CreateArrayQCTemplate(FieldDayId, "QC1", Date, TeamId, TeamMembers, EquipmentId, EquipmentType);
				CreateArrayQCTemplate(FieldDayId, "QC2", Date, TeamId, TeamMembers, EquipmentId, EquipmentType);
			}
			else
			{
				CreatePPQCTemplate(FieldDayId, "QC1", Date, TeamId, TeamMembers, EquipmentId, EquipmentType);
				CreatePPQCTemplate(FieldDayId, "QC2", Date, TeamId, TeamMembers, EquipmentId, EquipmentType);
			}
			
			CreateIVSTemplate(FieldDayId, "IVS1", Date, TeamId, TeamMembers, EquipmentId, EquipmentType);
			CreateIVSTemplate(FieldDayId, "IVS2", Date, TeamId, TeamMembers, EquipmentId, EquipmentType);
			CreateGPSQCTemplate(FieldDayId, "GPSQC", Date, TeamId, TeamMembers, EquipmentId, EquipmentType);
			CreateDatasetTemplate(FieldDayId, "DatasetA", Date, TeamId, TeamMembers, EquipmentId, EquipmentType);
		}

		async public void createNewTask(string FieldDayId, string TaskType, string ExportTable)
		{
			var previousTask = await _connection.Table<TaskData>()
															.Where(b => b.FieldDayId.Equals(FieldDayId) && b.ExportTable.Equals(ExportTable))
															.OrderByDescending(b => b.TaskId)
															.FirstAsync();
			var previousTaskCharacter = previousTask.TaskId.Last();
			var nextTaskCharacter = (char)((int)previousTaskCharacter + 1);
			var taskId = TaskType + nextTaskCharacter;

			var dayInfo = await _connection.Table<TaskData>()
														.Where(b => b.FieldDayId.Equals(FieldDayId) && b.TaskId.Equals("DatasetA")).ToListAsync();

			//var date = dayInfo.Where(b => b.PropertyName.Equals("Collection_Date"));
			var date = dayInfo.Where(b => b.PropertyName.Equals("Collection_Date")).Select(b => b.PropertyValue).FirstOrDefault();
			var teamId = dayInfo.Where(b => b.PropertyName.Equals("Team_ID")).Select(b => b.PropertyValue).FirstOrDefault();
			string teamMembers = dayInfo.Where(b => b.PropertyName.Equals("Team_Members")).Select(b => b.PropertyValue).FirstOrDefault();
			string equipmentId = dayInfo.Where(b => b.PropertyName.Equals("Geophysical_Sensor")).Select(b => b.PropertyValue).FirstOrDefault();
			string equipmentType = dayInfo.Where(b => b.PropertyName.Equals("")).Select(b => b.PropertyValue).FirstOrDefault();

			var platformType = equipmentId[0];

			switch (TaskType)
			{
				case "Dataset":
					CreateDatasetTemplate(FieldDayId, taskId, date, teamId, teamMembers, equipmentId, equipmentType);
					break;
				
				case "QC":
					if (platformType == 'A')
						CreateArrayQCTemplate(FieldDayId, taskId, date, teamId, teamMembers, equipmentId, equipmentType);
					else
						CreatePPQCTemplate(FieldDayId, taskId, date, teamId, teamMembers, equipmentId, equipmentType);
					break;
				
				case "IVS":
					CreateIVSTemplate(FieldDayId, taskId, date, teamId, teamMembers, equipmentId, equipmentType);
					break;
			}
		}

		async public void deleteData(string FieldDayId)
		{
			var filter = await _connection.Table<TaskData>().Where(b => b.FieldDayId.Equals(FieldDayId)).ToListAsync();
			foreach (var record in filter)
				await _connection.DeleteAsync(record);
		}

		async public void deleteData(string FieldDayId, string TaskId)
		{
			var filter = await _connection.Table<TaskData>().Where(b => b.FieldDayId.Equals(FieldDayId) && b.TaskId.Equals(TaskId)).ToListAsync();
			foreach (var record in filter)
				await _connection.DeleteAsync(record);
		}

		async public void exportData(string FieldDayId)
		{
			string DirectoryName = FieldDayId;
			string DownloadsPath = "/storage/emulated/0/Download/";
			Directory.CreateDirectory(DownloadsPath + DirectoryName);

			var daysData = await _connection.Table<TaskData>().Where(b => b.FieldDayId.Equals(FieldDayId)).ToListAsync();

			var exportTables = new List<string>();
			foreach (var str in daysData)
				if (!exportTables.Contains(str.ExportTable))
					exportTables.Add(str.ExportTable);

			foreach (var exportTable in exportTables)
			{
				var filePath = Path.Combine((DownloadsPath + DirectoryName), exportTable + ".txt");

				var tableData = await _connection.Table<TaskData>().Where(b => b.FieldDayId.Equals(FieldDayId) && b.ExportTable.Equals(exportTable)).ToListAsync();

				var tableHeader = new List<string>();
				foreach (var record in tableData)
					if (!tableHeader.Contains(record.PropertyName))
						tableHeader.Add(record.PropertyName);

				var sbHeader = new StringBuilder();
				foreach (var str in tableHeader)
					sbHeader.Append(str + ",");
				sbHeader.Length--;
				var header = sbHeader.ToString();
				File.WriteAllText(filePath, header + (Environment.NewLine));

				var tableTasks = new List<string>();
				foreach (var record in tableData)
					if (!tableTasks.Contains(record.TaskId))
						tableTasks.Add(record.TaskId);


				foreach (var task in tableTasks)
				{
					var taskData = await _connection.Table<TaskData>().Where(b => b.FieldDayId.Equals(FieldDayId) && b.ExportTable.Equals(exportTable) && b.TaskId.Equals(task)).ToListAsync();

					var sbTaskValues = new StringBuilder();
					foreach (var record in taskData)
						sbTaskValues.Append(record.PropertyValue + ",");
					sbTaskValues.Length--;
					var taskValues = sbTaskValues.ToString();
					File.AppendAllText(filePath, (taskValues + Environment.NewLine));
				}
			}
		}
	}
}