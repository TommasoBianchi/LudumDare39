using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdvancedSpawnerNamespace;

namespace AdvancedSpawnerNamespace {
	
	public class AdvancedSpawner : MonoBehaviour {
		public SpawnShape SpawnShape;
		public List<EnemyDistribution> EnemyDistributions = new List<EnemyDistribution>();

		private EnemyManager enemyManager;

		[SerializeField]
		private int nextLevelToGenerate = 0;
		private int generatedLevels = 0;
		private float secondsForLevels = 20f;

		void Start() {
			enemyManager = GameObject.FindGameObjectWithTag ("SpawnerManager").GetComponent<EnemyManager> ();
			enemyManager.FillDict ();
		}

		void Update () {
			fillEnemyDistributions ();

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
				Vector2 enemyPosition = SpawnShape.GetPositionFromLinearPosition (spawnCenter, ewp.LinearPos);
				Instantiator.InstantiateEnemyWithRandomName (ewp.Enemy, new Vector3 (enemyPosition.x, enemyPosition.y, 0), Quaternion.identity);
			}
		}

		public void AddEnemyDistribution(EnemyDistribution ed) {
			EnemyDistributions.Add (ed);
		}

		private void fillEnemyDistributions() {
			while (Time.timeSinceLevelLoad > secondsForLevels * generatedLevels) {
				if (nextLevelToGenerate % 3 == 1) {
					/*
					 * For 20 seconds, generate a simple enemy every 7*e^(-currentLevel/15)
					 */
					float clock = 8f * Mathf.Exp (-nextLevelToGenerate / 15);
					int howMany = (int)Mathf.Floor (20f / clock);
					EnemyDistribution ed = new EnemyDistribution (secondsForLevels * generatedLevels, clock, DistributionType.Uniform);
					float linearPos = 0f;
					for (int i = 0; i < howMany; i++) {
						ed.AddEnemyWithPos(enemyManager.getEnemyFromName("SimpleEnemy"), linearPos);
						linearPos += 0.3f;
						if (linearPos >= 1) {
							linearPos -= 1;
						}
					}
					EnemyDistributions.Add (ed);
				} else if (nextLevelToGenerate % 3 == 2) {
					/*
					 * For 20 seconds, 2 MiniEnemy spawns every 10*e^(-currentLevel/15), from same sides
					 */ 
					float clock = 8f * Mathf.Exp (-nextLevelToGenerate / 15);
					int howMany = (int)Mathf.Floor (20f / clock);
					for (int k = 0; k < howMany; k++) {
						EnemyDistribution ed = new EnemyDistribution (secondsForLevels * generatedLevels + k * clock, 0, DistributionType.Uniform);
						float linearPos = Random.Range (0f, 1f);
						for (int i = 0; i < 2; i++) {
							ed.AddEnemyWithPos (enemyManager.getEnemyFromName ("EnemyMini"), linearPos);
						}
						EnemyDistributions.Add (ed);
					}
				} else {
					/*
					 * For 20 seconds, 2 MiniEnemy spawns every 10*e^(-currentLevel/15), from opposite sides
					 */ 
					float clock = 8f * Mathf.Exp (-nextLevelToGenerate / 15);
					int howMany = (int)Mathf.Floor (20f / clock);
					for (int k = 0; k < howMany; k++) {
						EnemyDistribution ed = new EnemyDistribution (secondsForLevels * generatedLevels + k * clock, 0, DistributionType.Uniform);
						float linearPos1 = Random.Range (0f, 1f);
						float linearPos2 = 1f - linearPos1;
						ed.AddEnemyWithPos (enemyManager.getEnemyFromName ("EnemyMini"), linearPos1);
						ed.AddEnemyWithPos (enemyManager.getEnemyFromName ("SimpleEnemy"), linearPos2);
						EnemyDistributions.Add (ed);
					}
				}

				Debug.Log ("Created level " + nextLevelToGenerate);
				generatedLevels++;
				nextLevelToGenerate++;
			}

			/*
			// At time 0, SimpleEnemy spawns every 7 secs, at most 8, random position
			EnemyDistribution ed = new EnemyDistribution (0, 7f, DistributionType.Uniform);
			float linearPos = 0f;
			for (int i = 0; i < 8; i++) {
				ed.AddEnemyWithPos(enemyManager.getEnemyFromName("SimpleEnemy"), linearPos);
				linearPos += 0.3f;
				if (linearPos >= 1) {
					linearPos -= 1;
				}
			}
			EnemyDistributions.Add (ed);

			// At time 30, 2 MiniEnemy spawns every 8 secs, at most 4 times, random position
			for (int k = 0; k < 4; k++) {
				ed = new EnemyDistribution (30f + k * 8f, 0, DistributionType.Uniform);
				linearPos = Random.Range (0f, 1f);
				for (int i = 0; i < 2; i++) {
					ed.AddEnemyWithPos (enemyManager.getEnemyFromName ("EnemyMini"), linearPos);
				}
				EnemyDistributions.Add (ed);
			}

			// At time 60, SimpleEnemy and MiniEnemy spawns at random every 4 seconds forever
			ed = new EnemyDistribution (60f, 4f, DistributionType.Uniform);
			for (int i = 0; i < 100; i++) {
				linearPos = Random.Range (0f, 1f);
				if (Random.Range(0,2) == 0)
					ed.AddEnemyWithPos(enemyManager.getEnemyFromName("SimpleEnemy"), linearPos);
				else 
					ed.AddEnemyWithPos(enemyManager.getEnemyFromName("EnemyMini"), linearPos);
			}
			EnemyDistributions.Add (ed);
			*/
		}


	}
}