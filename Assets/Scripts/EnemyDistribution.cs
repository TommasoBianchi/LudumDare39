using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdvancedSpawnerNamespace {

	[System.Serializable]
	public class EnemyDistribution {
		public float StartTime = 0;
		public float TimeBetweenUpdates = 0.5f;
		private float timeLastUpdate = 0;
		public bool Ended = false;

		public List<EnemyWithPos> EnemiesWithPos = new List<EnemyWithPos>();
		public DistributionType Distribution = DistributionType.Uniform;

		public EnemyDistribution(float startTime, float timeBetweenUpdates, DistributionType distribution) {
			StartTime = startTime;
			TimeBetweenUpdates = timeBetweenUpdates;
			Distribution = distribution;
		}

		public void AddEnemyWithPos(Enemy e, float linearPos) {
			EnemiesWithPos.Add (new EnemyWithPos (e, linearPos));
		}

		public void AddEnemiesToList(List<EnemyWithPos> list) {
			if (!Ended && Time.time > timeLastUpdate + TimeBetweenUpdates) {
				timeLastUpdate = Time.time;
				EnemyWithPos ewp = EnemiesWithPos[0];
				EnemiesWithPos.RemoveAt (0);
				list.Add (ewp);

				if (EnemiesWithPos.Count == 0) {
					Ended = true;
				}
			}
		}
	}

	[System.Serializable]
	public class EnemyWithPos {
		public Enemy Enemy;
		public float LinearPos;

		public EnemyWithPos(Enemy e, float l) {
			Enemy = e;
			LinearPos = l;
		}
	}
}