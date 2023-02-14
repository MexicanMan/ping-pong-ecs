using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BeresnevTest.Data;
using UnityEngine;

namespace BeresnevTest.Services
{
    public class FileSaveService : ISaveService
    {
        private const string DataFileName = "playerData";
        
        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();
        
        public void Save(PlayerData data)
        {
            using var file = File.Open(GetFilePath(), FileMode.OpenOrCreate);
            _binaryFormatter.Serialize(file, data);
        }

        public PlayerData Restore()
        {
            PlayerData result = null;
            
            if (!File.Exists(GetFilePath()))
                return null;

            using (var file = File.Open(GetFilePath(), FileMode.OpenOrCreate))
                result = (PlayerData) _binaryFormatter.Deserialize(file);

            return result;
        }
        
        private string GetFilePath()
        {
            return Path.Combine(Application.persistentDataPath, DataFileName);
        }
    }
}