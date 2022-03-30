using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public class Database : MonoBehaviour
{
    void Start()
    {
        CheckFile("GameData"); //check if file exist
    }

    private void CheckFile(string dbPath)
    {
        string path = Application.dataPath + "/" + dbPath + ".db"; //original file path
        if (!File.Exists(path)) //if file doesn't exist on file path
        {
            File.Copy(Application.streamingAssetsPath + "/Default_" + dbPath + ".db", path); //copy original file to file path
        }
    }

    public void WriteData(string dbPath, string dataSP, string dataColumn, string inputData) //write data
    {
        string path = Application.dataPath + "/" + dbPath + ".db"; //file path
        CheckFile(dbPath); //check if file exist
        IDbConnection dbconn; //connect file
        dbconn = new SqliteConnection("URI=file:" + path); //connect to sql db
        dbconn.Open(); //open file
        IDbCommand dbcmdW = dbconn.CreateCommand(); //creat command
        string insertQuery = "INSERT INTO " + dataSP + "(" + dataColumn + ") VALUES(" + inputData + ")"; //input command
        dbcmdW.CommandText = insertQuery; //process command
        dbcmdW.ExecuteScalar(); //done
        dbconn.Close(); //close it
    }

    public void UpdateData(string dbPath, string dataSP, int id, string columnType, string newData) //update data
    {
        string path = Application.dataPath + "/" + dbPath + ".db";
        CheckFile(dbPath);
        IDbConnection dbconn;
        dbconn = new SqliteConnection("URI=file:" + path);
        dbconn.Open();
        IDbCommand dbcmdU = dbconn.CreateCommand();
        string updateQuery = "UPDATE " + dataSP + " SET(" + columnType + ") = (" + newData + ") WHERE id = " + id;
        dbcmdU.CommandText = updateQuery;
        dbcmdU.ExecuteScalar();
        dbconn.Close();
    }

    public void DelateData(string dbPath, string dataSP, int id) //delate data
    {
        string path = Application.dataPath + "/" + dbPath + ".db";
        CheckFile(dbPath);
        IDbConnection dbconn;
        dbconn = new SqliteConnection("URI=file:" + path);
        dbconn.Open();
        IDbCommand dbcmdD = dbconn.CreateCommand();
        string delateQuery = "DELETE FROM " + dataSP + " WHERE ID = " + id;
        dbcmdD.CommandText = delateQuery;
        dbcmdD.ExecuteScalar();
        dbconn.Close();
    }

    public int ReadDataI(string dbPath, string dataSP, string dataSelect, int rowID) //read int data
    {
        int resultD = 0;
        string path = Application.dataPath + "/" + dbPath + ".db";
        CheckFile(dbPath);
        IDbConnection dbconn;
        dbconn = new SqliteConnection("URI=file:" + path);
        dbconn.Open();
        IDbCommand dbcmdI = dbconn.CreateCommand();
        string intReadQuery = "SELECT " + dataSelect + " FROM " + dataSP + " WHERE id=" + rowID;
        dbcmdI.CommandText = intReadQuery;
        IDataReader reader = dbcmdI.ExecuteReader();
        while (reader.Read())
        {
            int resultData = reader.GetInt32(0);
            resultD = resultData;
        }
        dbconn.Close();
        Debug.Log(resultD);
        return resultD; //return output
    }

    public string ReadDataS(string dbPath, string dataSP, string dataSelect, int rowID) //read string data
    {
        string resultD = "";
        string path = Application.dataPath + "/" + dbPath + ".db";
        CheckFile(dbPath);
        IDbConnection dbconn;
        dbconn = new SqliteConnection("URI=file:" + path);
        dbconn.Open();
        IDbCommand dbcmdS = dbconn.CreateCommand();
        string sqlQuery = "SELECT " + dataSelect + " FROM " + dataSP + " WHERE id=" + rowID;
        dbcmdS.CommandText = sqlQuery;
        IDataReader reader = dbcmdS.ExecuteReader();
        while (reader.Read())
        {
            string resultData = reader.GetString(0);
            resultD += resultData;
        }
        dbconn.Close();
        Debug.Log(resultD);
        return resultD;
    }
}
