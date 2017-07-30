using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	
	private Dictionary<string, Enemy> EnemiesDictionary = new Dictionary<string, Enemy>();
	public List<Enemy> Enemies;

	public void FillDict() {
		foreach (Enemy e in Enemies) {
			EnemiesDictionary.Add (e.name, e);
		}
	}

	public Enemy getEnemyFromName(string enemyName) {
		return EnemiesDictionary [enemyName];
	}
}
