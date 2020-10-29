using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace GeoSurveyForms
{
	public class DbTable : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;                               //[INotify] notifies subscribers whenever property is changed
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)         //[INotify]
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));          //[INotify]
		}


		private PropertyInfo[] _PropertyInfos = null;
		
		public string GetAllProperties()
		{
			if (_PropertyInfos == null)
				_PropertyInfos = this.GetType().GetProperties();

			var sb = new StringBuilder();

			foreach (var info in _PropertyInfos)
			{
				var value = info.GetValue(this, null) ?? "(null)";
				sb.Append(value.ToString() + ",");
			}
			sb.Length--;
			return sb.ToString();
		}

		public string ConvertColumnToHeader()
		{
			var sb = new StringBuilder();

			foreach (var info in _PropertyInfos)
			{
				var value = info.GetValue(this, null) ?? "(null)";
				sb.Append(value.ToString() + ",");
			}
			sb.Length--;
			return sb.ToString();
		}

		public string GetHeader()
		{
			if (_PropertyInfos == null)
				_PropertyInfos = this.GetType().GetProperties();

			var sb = new StringBuilder();

			foreach (var info in _PropertyInfos)
			{
				sb.Append(info.Name + ",");
			}
			sb.Length--;
			sb.AppendLine();
			return sb.ToString();
		}
	}
}
