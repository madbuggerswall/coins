using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class FilePath {
	public static readonly string stats = "/stats.dat";
	public static readonly string achievements = "/achievements.dat";
}

public class SaveManager {
	public static void save<T>(T serializable, string filePath) {
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + filePath, FileMode.OpenOrCreate);
		binaryFormatter.Serialize(file, serializable);
		file.Close();
	}

	public static T load<T>(string filePath) {
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + filePath, FileMode.Open);
		T deserializedObject = (T) binaryFormatter.Deserialize(file);
		file.Close();
		return deserializedObject;
	}

	public static bool exists(string filePath) {
		return File.Exists(Application.persistentDataPath + filePath);
	}
}