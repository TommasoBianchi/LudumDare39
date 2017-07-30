using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdvancedSpawnerNamespace;

namespace AdvancedSpawnerNamespace {
	
	public class AdvancedSpawner : MonoBehaviour {
		public SpawnShape SpawnShape;
		public List<EnemyDistribution> EnemyDistributions = new List<EnemyDistribution>();

		private SpawnerManager spawnerManager;

		void Start() {
			spawnerManager = GameObject.FindGameObjectWithTag ("SpawnerManager").GetComponent<SpawnerManager> ();
			spawnerManager.FillDict ();

			fillEnemyDistributions ();
		}

		void Update () {
			Vector2 spawnCenter = new Vector2 (transform.position.x, transform.position.y);

			List<EnemyWithPos> enemyWithPosList = new List<EnemyWithPos> ();
			for (int i = 0; i < EnemyDistributions.Count; i++) {
				EnemyDistribution ed = EnemyDistributions [i];
				ed.AddEnemiesToList (enemyWithPosList);
				if (ed.Ended) {
					EnemyDistributions.Remove (ed);
				}
			}
			foreach (EnemyWithPos ewp in enemyWithPosList) {
				Debug.Log ("Spawn center: " + spawnCenter.x + " " + spawnCenter.y);
				Vector2 enemyPosition = SpawnShape.GetPositionFromLinearPosition (spawnCenter, ewp.LinearPos);
				Debug.Log ("enemy x: " + enemyPosition.x);
				GameObject.Instantiate (ewp.Enemy, new Vector3(enemyPosition.x, enemyPosition.y, 0), Quaternion.identity);
				Debug.Log ("Spawned enemy");
			}
		}

		public void AddEnemyDistribution(EnemyDistribution ed) {
			EnemyDistributions.Add (ed);
		}

		private void fillEnemyDistributions() {
			EnemyDistribution ed = new EnemyDistribution (0, 4f, DistributionType.Uniform);
			float linearPos = 0f;
			for (int i = 0; i < 100; i++) {
				ed.AddEnemyWithPos(spawnerManager.getEnemyFromName("Enemy"), linearPos);
				linearPos += 0.3f;
				if (linearPos >= 1) {
					linearPos -= 1;
				}
			}
			EnemyDistributions.Add (ed);
		}


	}
}