using UnityEngine;
using System.Collections;

public class GeometryUtility {

	public static Vector3 rotateCoordinateAboutPoint(Vector3 origin, Vector3 coordinate, Vector3 rotation) {

		Vector3 dir = coordinate - origin; // get point direction relative to pivot
		dir = Quaternion.Euler(rotation) * dir; // rotate it
		Vector3 point = dir + origin; // calculate rotated point
		return point; // return it
	}

	public static Vector3 addDeltaToPosition(Vector3 position, Vector3 positionDelta, float scale) {

		Vector3 result = new Vector3 (position.x + (scale * positionDelta.x),
		                              position.y + (scale * positionDelta.y),
		                              position.z + (scale * positionDelta.z));

		return result;
	}

	public static Vector3 addDeltaToRotation(Vector3 rotation, Vector3 rotationDelta) {

		Vector3 result = new Vector3 (rotation.x, rotation.y, rotation.z);

		result.x += rotationDelta.x;
		result.y += rotationDelta.y;
		result.z += rotationDelta.z;

		if (result.x > 360) {
			result.x = result.x - 360;
		}
		if (result.y > 360) {
			result.y = result.y - 360;
		}
		if (result.z > 360) {
			result.z = result.z - 360;
		}

		return result;
	}
}
