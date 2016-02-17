using UnityEngine;
using System.Collections;

public class FormBounds  {

	private Vector3 minBounds = new Vector3 (0, 0, 0);
	private Vector3 maxBounds = new Vector3 (0, 0, 0);

	public FormBounds(Vector3 firstPosition) {
		minBounds.x = firstPosition.x;
		minBounds.y = firstPosition.y;
		minBounds.z = firstPosition.z;
		maxBounds.x = firstPosition.x;
		maxBounds.y = firstPosition.y;
		maxBounds.z = firstPosition.z;
	}

	public void calculateNewBounds(Vector3 newPosition) {
		
		if (newPosition.x < minBounds.x) {
			minBounds.x = newPosition.x;
		}
		if (newPosition.y < minBounds.y) {
			minBounds.y = newPosition.y;
		}
		if (newPosition.z < minBounds.z) {
			minBounds.z = newPosition.z;
		}
		
		if (newPosition.x > maxBounds.x) {
			maxBounds.x = newPosition.x;
		}
		if (newPosition.y > maxBounds.y) {
			maxBounds.y = newPosition.y;
		}
		if (newPosition.z > maxBounds.z) {
			maxBounds.z = newPosition.z;
		}
	}

	public float getLargestBoundDistance() {
		
		float largestBoundDistance = maxBounds.x;
		
		if (maxBounds.y > largestBoundDistance) {
			largestBoundDistance = maxBounds.y;
		}
		if (maxBounds.z > largestBoundDistance) {
			largestBoundDistance = maxBounds.z;
		}
		
		if (Mathf.Abs (minBounds.x) > largestBoundDistance) {
			largestBoundDistance = Mathf.Abs (minBounds.x);
		}
		if (Mathf.Abs (minBounds.y) > largestBoundDistance) {
			largestBoundDistance = Mathf.Abs (minBounds.y);
		}
		if (Mathf.Abs (minBounds.z) > largestBoundDistance) {
			largestBoundDistance = Mathf.Abs (minBounds.z);
		}
		
		return largestBoundDistance;
	}
}
