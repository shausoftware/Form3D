using UnityEngine;
using System.Collections;

public class SimpleTorusStack : MonoBehaviour, IStack {

	private GameObject stack;
	private Rigidbody stackRigidBody;
	TorusPrimitive torus;

	private float currentRotationX;
	private float currentRotationY;
	private float currentRotationZ;

	public IStack initialise(Vector3 centroid, Vector3 stackTwist, float scale, Color stackColour) {

		stack = new GameObject("Torus");
		torus = stack.AddComponent<TorusPrimitive>();

		torus.OuterRadius = scale;
		torus.InnerRadius = 0.2f * scale;
		//torus.Sides = 32;
		//torus.Segments = 32;

		torus.UpdatePrimitive ();

		stackRigidBody = stack.AddComponent<Rigidbody>();
		stackRigidBody.useGravity = false;
		stackRigidBody.detectCollisions = false;
		
		stack.transform.position = centroid;
		
		currentRotationX = stackTwist.x;
		currentRotationY = stackTwist.y;
		currentRotationZ = stackTwist.z;
		stack.transform.Rotate (stackTwist);
		
		stack.transform.localScale = new Vector3(scale, scale, scale);
		
		stack.GetComponent<Renderer>().material.color = stackColour;
		return this;
	}
	
	public void mutateTo(Vector3 centroid, Vector3 stackTwist, float scale, Color stackColour) {
		
		if (stackRigidBody != null) {
			
			stack.GetComponent<Renderer>().material.color = stackColour;
			
			float deltaX = stackTwist.x - currentRotationX;
			float deltaY = stackTwist.y - currentRotationY;
			float deltaZ = stackTwist.z - currentRotationZ;
			
			if (deltaX != 0 || deltaY != 0 || deltaZ != 0) {
				stack.transform.Rotate(new Vector3(deltaX, deltaY, deltaZ));
				currentRotationX += deltaX;
				currentRotationY += deltaY;
				currentRotationZ += deltaZ;
			}
			
			stackRigidBody.transform.position = Vector3.Lerp(stackRigidBody.transform.position, 
			                                                 centroid, 
			                                                 (Time.deltaTime * 3) / Vector3.Distance(centroid, stackRigidBody.transform.position));
			stack.transform.localScale = new Vector3(scale, scale, scale);
		}
	}
	
	public void removeFromScene() {
		Destroy(stack);
	}
	
}
