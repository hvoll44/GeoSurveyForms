using SQLite;

namespace GeoSurveyForms
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}

