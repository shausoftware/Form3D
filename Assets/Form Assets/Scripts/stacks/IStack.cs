using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IStack {

	IStack initialise(Vector3 centroid, Vector3 stackTwist, float scale, Color stackColour);

	void mutateTo(Vector3 centroid, Vector3 stackTwist, float scale, Color stackColour);

	void removeFromScene();

}
