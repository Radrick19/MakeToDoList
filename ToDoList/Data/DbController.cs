using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using ToDoList.Model;

namespace ToDoList.Data
{
    internal static class DbController
    {
        private static string dbName = "Data/TaskDB.db";
        private static SQLiteConnection sqlConnection;
        private static SQLiteCommand sqlCommand;

        public static List<ToDoModel> GetAllData()
        {
            sqlCommand.CommandText = "SELECT * FROM Tasks";
            return GetData();
        }

        public static List<ToDoModel> GetActiveData()
        {
            sqlCommand.CommandText = "SELECT * FROM Tasks WHERE isDone ='0'";
            return GetData();
        }

        public static List<ToDoModel> GetDoneData()
        {
            
            sqlCommand.CommandText = "SELECT * FROM Tasks WHERE isDone ='1'";
            return GetData();
        }

        public static void AddData(string task)
        {
            SQLiteTransaction transaction = sqlConnection.BeginTransaction();
            try
            {
                sqlCommand.CommandText = "INSERT INTO [Tasks] (id, task, isDone) VALUES (:id, :task, :isDone)";
                sqlCommand.Parameters.AddWithValue("id", null);
                sqlCommand.Parameters.AddWithValue("task", task);
                sqlCommand.Parameters.AddWithValue("isDone", 0);
                sqlCommand.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("Can't add data to DB : " + ex.Message);
            }
        }

        public static void AddData(ToDoModel model)
        {
            SQLiteTransaction transaction = sqlConnection.BeginTransaction();
            try
            {
                sqlCommand.CommandText = "INSERT INTO [Tasks] (id, task, isDone) VALUES (:id, :task, :isDone)";
                sqlCommand.Parameters.AddWithValue("id", model.Id);
                sqlCommand.Parameters.AddWithValue("task", model.Task);
                sqlCommand.Parameters.AddWithValue("isDone", Convert.ToInt32(model.IsDone));
                sqlCommand.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("Can't add data to DB : " + ex.Message);
            }
        }

        public static void RemoveItem(long id)
        {
            sqlCommand.CommandText = $"DELETE FROM [Tasks] WHERE [id]='{id}'";
            sqlCommand.ExecuteNonQuery();
        }

        public static void ChangeIsDone(ToDoModel model)
        {
            sqlCommand.CommandText = $"DELETE FROM [Tasks] WHERE [id]='{model.Id}'";
            sqlCommand.ExecuteNonQuery();
            AddData(model);
        }

        private static List<ToDoModel> GetData()
        {
            List<ToDoModel> returnModel = new List<ToDoModel>();
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlCommand);
            adapter.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                string task = item.Field<string>("task");
                long id = item.Field<long>("id");
                bool isDone = Convert.ToBoolean(item.Field<long>("isDone"));
                returnModel.Add(new ToDoModel(task, id, isDone));
            }
            return returnModel;
        }

        private static bool CreateDataBase()
        {
            try
            {
                Directory.CreateDirectory("Data");
                sqlConnection = new SQLiteConnection("Data Source=" + dbName + ";Version=3;FailIfMissing=False");
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS [Tasks] ([id] INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, [task] TEXT, [isDone] INTEGER)";
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Can't create data base : " + ex.Message);
                return false;
            }
        }

        private static bool isDbExists()
        {
            if (File.Exists("Data/TaskDB.db"))
            {
                sqlConnection = new SQLiteConnection("Data Source=" + dbName + ";Version=3;");
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                return true;
            }
            else
            {
                if (CreateDataBase())
                    return true;
                else
                    return false;
            }
        }

        static DbController()
        {
            sqlConnection = new SQLiteConnection();
            sqlCommand = new SQLiteCommand();
            isDbExists();
        }
    }
}
