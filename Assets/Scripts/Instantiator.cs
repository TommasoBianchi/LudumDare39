using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEditor;

public static class Instantiator {

	static List<string> names = new List<string> ();

	public static void Instantiate<T>(T obj, Vector3 pos, Quaternion rot) {
		Instantiate<T> (obj, pos, rot);
	}

	public static void InstantiateEnemyWithRandomName(Enemy e, Vector3 pos, Quaternion rot) {
		Enemy eo = GameObject.Instantiate (e, pos, rot);

		if (names.Count == 0) {
			Item item = JsonUtility.FromJson<Item> (System.IO.File.ReadAllText ("Assets/Resources/names.txt"));
			names = item.items;
		}
		
		int idx = Random.Range (0, names.Count);
		string name = names [idx];
		eo.GetComponentInChildren<TextMesh> ().text = name;
	}

	private class Item {
		public List<string> items;
	}
}