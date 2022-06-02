using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Usar StreamWriter y StreamReader
using System.Security.Cryptography; //Libería para encriptación y desencriptación de información

public class GuardadoJsonEncript : MonoBehaviour
{
    //Variables para el archivo de guardado
    public int score;
    public float playerPosX;
    public float playerPosY;
    
    
    //Variable que seguirá siendo pública y accesible pero no se guardará en nuestro archivo, al especificarle que no será serializable
    //[System.NonSerialized] public int damage = 10;



    public class SaveData
    {
        //Variables para serializar
        public int score;
        public float playerPosX;
        public float playerPosY;


        //Constructor de la clase
        public SaveData()
        {
            //Rellenamos las variables con las que le pasamos por parámetro
            score = ObserverPattern.score;
            playerPosX = GameObject.Find("Player").transform.position.x;
            playerPosY = GameObject.Find("Player").transform.position.y;


        }
        
    }

    
    public void LoadDataFunction()
    {
        //Ruta de donde queremos leer la información
        string saveFilePath = Application.persistentDataPath + "/jsonUtilityDemo.sav";

        //Muestra la ruta del archivo por consola
        Debug.Log("Loading from: " + saveFilePath);


        //SI QUISIESIMOS CARGAR LA INFORMACION Y VARIABLES NO ENCRIPTADAS
        //Creamos un StreamReader que nos permita leer la información del archivo de guardado
        //  StreamReader sr = new StreamReader(saveFilePath);
        //Creamos un string donde guardar la información que leemos
        //  string jsonString = sr.ReadToEnd();
        //Al acabar la lectura de datos cerramos el StreamReader
        //   sr.Close();


        //CARGAMOS LA INFORMACION ENCRIPTADA, DESENCIPTANDOLA 
        //Creamos un array con la información encriptada recibida
        byte[] decryptedSavegame = File.ReadAllBytes(saveFilePath);
        //Creamos un array donde guardar la información desencriptada recibida
        string jsonString = Decrypt(decryptedSavegame);

        //Instanciamos la clase anidada para cargar las variables de esta
        //La información recibida del archivo de guardado sobreescribirá los campos oportunos del jsonString
        SaveData sd = JsonUtility.FromJson<SaveData>(jsonString);

        //Realmente cargamos la información del archivo de guardado en las variables de Unity
        ObserverPattern.score = sd.score;
        GameObject.Find("Player").transform.position = new Vector2(sd.playerPosX,sd.playerPosY);
        
    }
    public void SaveDataFunction()
    {
        //Instanciamos la clase anidada pasándole por parámetro las variables que queremos guardar
        SaveData sd = new SaveData();

        //Guardamos en un string el contenido del script osea la instancia de este
        string jsonString = JsonUtility.ToJson(sd);

        //Ruta donde queremos guardar la información
        string saveFilePath = Application.persistentDataPath + "/jsonUtilityDemo.sav";

        //Creamos un StreamWriter para guardar la información en la ruta dada
        StreamWriter sw = new StreamWriter(saveFilePath);

        //Muestra la ruta del archivo por consola
        Debug.Log("Saving to: " + saveFilePath);

        //Escribimos la información que queremos en el archivo de guardado
        sw.WriteLine(jsonString);

        //Al acabar cerramos el StreamWriter
        sw.Close();


        //ENCRIPTAMOS LA INFORMACION DE NUESTRAS VARIABLES
        //Creamos un array de bytes para guardar el array que nos devuelve el método Encrypt para que pueda ser usado
        byte[] encryptSavegame = Encrypt(jsonString.ToString());
        //Escribimos esta información en el archivo de guardado, ya encriptada la información en su ruta 
        File.WriteAllBytes(saveFilePath, encryptSavegame);
        //Muestra la ruta del archivo por consola
        Debug.Log("Saving to: " + saveFilePath);
    }


    /*PARA ENCRIPTAR Y DESENCRIPTAR LA INFORMACIÓN DEL ARCHIVO DE GUARDADO
    */

    //Clave generada para la encriptación en formato bytes, 16 posiciones
    byte[] _key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
    //Vector de inicialización para la clave
    byte[] _initializationVector = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

    //Encriptamos los datos del archivo de guardado que le pasaremos en un string
    byte[] Encrypt(string message)
    {
        //Usamos esta librería que nos permitirá a través de una referencia crear un encriptador de la información
        AesManaged aes = new AesManaged();
        //Para usar este encriptador le pasamos tanto la clave como el vector de inicialización que hemos creado nosotros arriba
        ICryptoTransform encryptor = aes.CreateEncryptor(_key, _initializationVector);
        //Lugar en memoria donde guardamos la información encriptada
        MemoryStream memoryStream = new MemoryStream();
        //Con esta referencia podremos escribir en el MemoryStream de arriba la información ya encriptada usando el encriptador con sus claves que ya habíamos creado
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        //Con el StreamWriter podemos escribir en el archivo la información encriptada, que se habrá guardado en el MemoryStream
        StreamWriter streamWriter = new StreamWriter(cryptoStream);

        //Usando todo lo anterior, guardamos en el archivo de guardado el json que le pasamos por parámetro, haciendo el siguiente proceso: recibimos el string, lo encriptamos, queda guardado en la memoria reservada para la encriptación
        streamWriter.WriteLine(message);

        //Una vez hemos usado estas referencias las cerramos para evitar problemas de guardado o corrupción del archivo o de la propia encriptación
        streamWriter.Close();
        cryptoStream.Close();
        memoryStream.Close();

        //Por último el método devolverá esta información que reside en el hueco de memoria con la información encriptada, convertida esta información en array de bytes
        return memoryStream.ToArray();
    }

    //Generamos un método que nos devuelva la información del archivo de guardado desencriptada
    string Decrypt(byte[] message)
    {
        //Usamos esta librería que nos permitirá a través de una referencia crear un desencriptador de la información
        AesManaged aes = new AesManaged();
        //Para usar este desencriptador le pasamos tanto la clave como el vector de inicialización que hemos creado nosotros arriba
        ICryptoTransform decrypter = aes.CreateDecryptor(_key, _initializationVector);
        //Lugar en memoria donde guardamos la información desencriptada
        MemoryStream memoryStream = new MemoryStream(message);
        //Con esta referencia podremos escribir en el MemoryStream de arriba la información ya desencriptada usando el desencriptador con sus claves que ya habíamos creado
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decrypter, CryptoStreamMode.Read);
        //Con el StreamReader podemos leer del archivo la información desencriptada, que se habrá guardado en el MemoryStream
        StreamReader streamReader = new StreamReader(cryptoStream);

        //Usando todo lo anterior, cargamos del archivo de guardado el json que le pasamos por parámetro, haciendo el siguiente proceso: recibimos el string, lo desencriptamos, queda guardado en la memoria reservada para la desencriptación
        string decryptedMessage = streamReader.ReadToEnd();

        //Una vez hemos usado estas referencias las cerramos para evitar problemas de guardado o corrupción del archivo o de la propia encriptación
        streamReader.Close();
        cryptoStream.Close();
        memoryStream.Close();

        //Por último el método devolverá esta información que reside en el hueco de memoria con la información desencriptada, convertida esta en un string
        return decryptedMessage;
    }

    /*Ruta del archivo de guardado
        C:/Users/User/AppData/LocalLow/DefaultCompany/DataPersistenceProject/jsonUtilityDemo.sav
    */

}

