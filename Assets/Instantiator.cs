using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public static class Instantiator {

	static List<string> names = new List<string> ();

	public static void Instantiate<T>(T obj, Vector3 pos, Quaternion rot) {
		Instantiate<T> (obj, pos, rot);
	}

	public static void InstantiateEnemyWithRandomName(Enemy e, Vector3 pos, Quaternion rot) {
		Enemy eo = GameObject.Instantiate (e, pos, rot);

		if (names == null)
			names = JsonUtility.FromJson<List<string>>(fileToString("filename"));
		
		int idx = Random.Range (0, names.Count);
		string name = names [idx];
		eo.name = name;
	}

	public static string fileToString(string fileName) {
		string result = "";

		try {
			string line;
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			using (theReader) {
				do {
					line = theReader.ReadLine();

					if (line != null) {
						result += line;
					}
				} while (line != null);

				theReader.Close();
				return result;
			}
		} catch (System.Exception e) {
			Debug.Log("{0}\n" + e.Message);
			return "";
		}
	}
}