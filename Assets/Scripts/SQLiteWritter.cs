using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class SQLiteWritter : MonoBehaviour
{
    //Variable donde guardar la dirección de la Base de Datos
    string rutaDB;
    string strConexion;
    //Nombre de la base de datos con la que vamos a trabajar
    string DBFileName = "PlayerStats.db";
    //Variable texto UI
    //public Text myText;

    //Referencia que necesitamos para poder crear una conexión 
    IDbConnection dbConnection;
    //Referencia que necesitamos para poder ejecutar comandos
    IDbCommand dbCommand;
    //Referencia que necesitamos para leer datos
    IDataReader reader;
    // Start is called before the first frame update
    public GameObject Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AbrirDB()
    {
        // Crear y abrir la conexión
        //Compuebo en que plataforma estamos
        //Si estamos en PC mantenemos la ruta
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            rutaDB = Application.dataPath + "/StreamingAssets/" + DBFileName;
        }
        //Si estamos en Android
        else if (Application.platform == RuntimePlatform.Android)
        {
            rutaDB = Application.persistentDataPath + "/" + DBFileName;
            //Comprobar si el archivo se encuentra almacenado en persistant data
            if (!File.Exists(rutaDB))
            {
                //Almaceno el archivo en load db
                //Copio el archivo a persistant data
                WWW loadDB = new WWW("jar;file://" + Application.dataPath + DBFileName);
                while (!loadDB.isDone)
                {

                }
                File.WriteAllBytes(rutaDB, loadDB.bytes);
            }
        }

        strConexion = "URI=file:" + rutaDB;
        dbConnection = new SqliteConnection(strConexion);
        dbConnection.Open();
    }
    void CerrarDB()
    {
        // Cerrar las conexiones
        
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }

    public void GuardarSQLite()
    {
        INSERT(ObserverPattern.score, Player.transform.position.x, Player.transform.position.y);
    }
    public void INSERT(int dato,float PlayerPosX,float PlayerPosY)
    {
        AbrirDB();
        dbCommand = dbConnection.CreateCommand();
        string sqlQuery = string.Format("UPDATE PlayerStats SET PlayerScore = \"{0}\",PlayerPositionX = \"{1}\", PlayerPositionY = \"{2}\"",dato,PlayerPosX,PlayerPosY);
        dbCommand.CommandText = sqlQuery;
        //Para poder contar datos de las filas tenemos que poner
        dbCommand.ExecuteScalar();
        //ExecuteScalar me devuelve un objeto 
        //Cerramos la DB
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
}
