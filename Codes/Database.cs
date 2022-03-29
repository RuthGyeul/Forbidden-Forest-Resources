using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using UnityEngine.Networking;

public class Database : MonoBehaviour
{
    string dbName;

    void Start()
    {
        CreatePlayerTable("GameData");
    }

    private void CreatePlayerTable(string dbPath)
    {
        string conn = Application.dataPath + "/" + dbPath + ".db";
        if (!File.Exists(conn))
        {
            File.Copy(Application.streamingAssetsPath + "/Default_" + dbPath + ".db", conn);
        }
        IDbConnection dbconn;
        dbconn = new SqliteConnection("URI=file:" + conn);
        dbconn.Open();
        IDbCommand dbcmd;
        dbcmd = dbconn.CreateCommand();
        string tableQuery = "CREATE TABLE IF NOT EXISTS Data(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, name TEXT DEFAULT 'Player'  NOT NULL, score INTEGER DEFAULT '0' NOT NULL)";
        dbcmd.CommandText = tableQuery;
        dbcmd.ExecuteScalar();
        dbconn.Close();
    }

    public void WriteData(string dbPath, string dataSP, string dataColumn, string inputData)
    {
        string conn = Application.dataPath + "/" + dbPath + ".db";
        if (!File.Exists(conn))
        {
            File.Copy(Application.streamingAssetsPath + "/Default_" + dbPath + ".db", conn);
        }
        IDbConnection dbconn;
        dbconn = new SqliteConnection("URI=file:" + conn);
        dbconn.Open();
        IDbCommand dbcmdW = dbconn.CreateCommand();
        string insertQuery = "INSERT INTO " + dataSP + "(" + dataColumn + ") VALUES(" + inputData + ")";
        dbcmdW.CommandText = insertQuery;
        dbcmdW.ExecuteScalar();
        dbconn.Close();
    }

    public void UpdateData(string dbPath, string dataSP, int id, string columnType, string newData)
    {
        string conn = Application.dataPath + "/" + dbPath + ".db";
        if (!File.Exists(conn))
        {
            File.Copy(Application.streamingAssetsPath + "/Default_" + dbPath + ".db", conn);
        }
        IDbConnection dbconn;
        dbconn = new SqliteConnection("URI=file:" + conn);
        dbconn.Open();
        IDbCommand dbcmdU = dbconn.CreateCommand();
        string updateQuery = "UPDATE " + dataSP + " SET(" + columnType + ") = (" + newData + ") WHERE id = " + id;
        dbcmdU.CommandText = updateQuery;
        dbcmdU.ExecuteScalar();
        dbconn.Close();
    }

    public void DelateData(string dbPath, string dataSP, int id)
    {
        string conn = Application.dataPath + "/" + dbPath + ".db";
        if (!File.Exists(conn))
        {
            File.Copy(Application.streamingAssetsPath + "/Default_" + dbPath + ".db", conn);
        }
        IDbConnection dbconn;
        dbconn = new SqliteConnection("URI=file:" + conn);
        dbconn.Open();
        IDbCommand dbcmdD = dbconn.CreateCommand();
        string delateQuery = "DELETE FROM " + dataSP + " WHERE ID = " + id;
        dbcmdD.CommandText = delateQuery;
        dbcmdD.ExecuteScalar();
        dbconn.Close();
    }

    public int ReadDataI(string dbPath, string dataSP, string dataSelect, int rowID)
    {
        int resultD = 0;
        string conn = Application.dataPath + "/" + dbPath + ".db";
        if (!File.Exists(conn))
        {
            File.Copy(Application.streamingAssetsPath + "/Default_" + dbPath + ".db", conn);
        }
        IDbConnection dbconn;
        dbconn = new SqliteConnection("URI=file:" + conn);
        dbconn.Open();
        IDbCommand dbcmdI = dbconn.CreateCommand();
        string sqlQuery = "SELECT " + dataSelect + " FROM " + dataSP + " WHERE id=" + rowID;
        dbcmdI.CommandText = sqlQuery;
        IDataReader reader = dbcmdI.ExecuteReader();
        while (reader.Read())
        {
            int resultData = reader.GetInt32(0);
            resultD = resultData;
        }
        dbconn.Close();
        Debug.Log(resultD);
        return resultD;
    }

    public string ReadDataS(string dbPath, string dataSP, string dataSelect, int rowID)
    {
        string resultD = "";
        string conn = Application.dataPath + "/" + dbPath + ".db";
        if (!File.Exists(conn))
        {
            File.Copy(Application.streamingAssetsPath + "/Default_" + dbPath + ".db", conn);
        }
        IDbConnection dbconn;
        dbconn = new SqliteConnection("URI=file:" + conn);
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
