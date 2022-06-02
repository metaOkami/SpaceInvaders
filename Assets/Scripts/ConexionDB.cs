using System;
using System.Collections;
using System.Collections.Generic;
//Librerías que necesitamos
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class ConexionDB : MonoBehaviour
{
    //Variable para controlar la ruta de la base de datos, constructor de la ruta, y el nombre de la base de datos
    string rutaDB;
    string strConexion;
    string DBFileName = "PlayerStats.db";

    

    //Variable para trabajar con las conexiones
    IDbConnection dbConnection;
    //Para poder ejecutar comandos
    IDbCommand dbCommand;
    //Variable para leer
    IDataReader reader;

    // Start is called before the first frame update
    void Start()
    {
        AbrirDB();
    }

    //Método para abrir la base de datos
    void AbrirDB()
    {
        // Crear y abrir la conexión
        // Comprobar en que plataforma estamos
        // Si es PC mantenemos la ruta
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            rutaDB = Application.dataPath + "/StreamingAssets/" + DBFileName;
        }
        

        strConexion = "URI=file:" + rutaDB;
        dbConnection = new SqliteConnection(strConexion);
        dbConnection.Open();
        // Crear la consulta
        dbCommand = dbConnection.CreateCommand();
        string sqlQuery = "select * from PlayerStats";
        dbCommand.CommandText = sqlQuery;
        // Leer la base de datos
        reader = dbCommand.ExecuteReader();
        while (reader.Read())
        {
            
            int score = reader.GetInt32(0);
            
            float posPjX = reader.GetFloat(1);
            
            float posPjY = reader.GetFloat(2);
            
           
        }
        // Cerrar las conexiones
        reader.Close();
        reader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
}


