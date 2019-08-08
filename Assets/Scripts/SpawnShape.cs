using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdvancedSpawnerNamespace {

	[System.Serializable]
	public class SpawnShape {
		public ShapeType ShapeType = ShapeType.Rectangle;
		public float Dim1 = 10f;
		public float Dim2 = 10f;
		private float totalLength;

		public SpawnShape(ShapeType shapeType, float dim1, float dim2) {
			ShapeType = shapeType;
			Dim1 = dim1;
			Dim2 = dim2;
			computeTotalLength ();
		}

		private void computeTotalLength() {
			switch (ShapeType) {
			case ShapeType.Rectangle:
				totalLength = Dim1 * 2 + Dim2 * 2;
				break;
			case ShapeType.Circle:
				totalLength = 2 * Mathf.PI * Dim1;
				break;
			}
		}

		private Vector2 GetZeroPosition(Vector2 spawnCenter) {
			Vector2 result = new Vector2 (0, 0);
			switch (ShapeType) {
			case ShapeType.Rectangle:
				result = new Vector2 (spawnCenter.x - Dim1 / 2, spawnCenter.y + Dim2 / 2);
				break;
			case ShapeType.Circle:
				result = new Vector2 (spawnCenter.x, spawnCenter.y + Dim2 / 2);
				break;
			}
			return result;
		}

		public Vector2 GetPositionFromLinearPosition(Vector2 spawnCenter, float linearPos) {
			if (totalLength == 0) {
				computeTotalLength ();
			}

			Vector2 result = new Vector2(0, 0);
			Vector2 zeroPos = GetZeroPosition (spawnCenter);

			switch (ShapeType) {
			case ShapeType.Rectangle:
				if (linearPos <= Dim1 / totalLength) {
					result = new Vector2 (zeroPos.x + totalLength * linearPos, zeroPos.y);
				} else if (linearPos <= (Dim1 + Dim2) / totalLength) {
					result = new Vector2 (zeroPos.x + Dim1, zeroPos.y - totalLength * (linearPos - Dim1 / totalLength));
				} else if (linearPos <= (Dim1 + Dim2 + Dim1) / totalLength) {
					result = new Vector2 (zeroPos.x + Dim1 - totalLength * (linearPos - (Dim1 + Dim2) / totalLength), zeroPos.y - Dim2);
				} else if (linearPos <= (Dim1 + Dim2 + Dim1 + Dim2) / totalLength) {
					result = new Vector2 (zeroPos.x, zeroPos.y - Dim2 + totalLength * (linearPos - (Dim1 + Dim2 + Dim1) / totalLength));
				} else {
					result = zeroPos;
				}
				break;
			case ShapeType.Circle:
				float p = linearPos * totalLength;
				result = zeroPos +  new Vector2 (Dim1 * Mathf.Sin (p / Dim1), Dim1 * Mathf.Cos (p / Dim1));
				break;
			}

			return result;
		}
	}
}