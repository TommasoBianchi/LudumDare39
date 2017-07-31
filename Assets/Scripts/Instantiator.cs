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

		// Choose name
		string name = GetRandomName();
		eo.GetComponentInChildren<TextMesh> ().text = name;

		// Choose scale
		float localScale = Random.Range(0.6f, 2.5f);
		eo.transform.localScale = new Vector3(localScale, localScale, 1);
	}

	private class Item {
		public List<string> items;
	}

	public static string GetRandomName() {
		if (names.Count == 0) {
			int n = Random.Range (1, 5);
			Item item = JsonUtility.FromJson<Item> (System.IO.File.ReadAllText ("Assets/Resources/names" + n + ".json"));
			if (!names.Contains (item.items [0])) {
				names.AddRange (item.items);
			}
		}
		int idx = Random.Range (0, names.Count);
		return names [idx];
	}
}