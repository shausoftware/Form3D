using UnityEngine;
using System.Collections;

public class ComplexStack : MonoBehaviour, IStack {

	private GameObject centreStack;
	private GameObject orbitStack1;
	private GameObject orbitStack2;
	private GameObject orbitStack3;
	private GameObject orbitStack4;

	private Rigidbody centreStackRigidBody;
	private Rigidbody orbit1StackRigidBody;
	private Rigidbody orbit2StackRigidBody;
	private Rigidbody orbit3StackRigidBody;
	private Rigidbody orbit4StackRigidBody;

	private float currentRotationX;
	private float currentRotationY;
	private float currentRotationZ;

	private float stackScale;

	public IStack initialise(Vector3 centroid, Vector3 stackTwist, float scale, Color stackColour) {

		stackScale = scale;

		currentRotationX = stackTwist.x;
		currentRotationY = stackTwist.y;
		currentRotationZ = stackTwist.z;

		//create centre cube and rigid body
		centreStack = GameObject.CreatePrimitive (PrimitiveType.Cube);
		centreStackRigidBody = centreStack.AddComponent<Rigidbody>();
		centreStackRigidBody.useGravity = false;
		centreStackRigidBody.detectCollisions = false;

		//centre cube properties	
		centreStack.transform.position = centroid;
		centreStack.transform.Rotate (stackTwist);
		centreStack.transform.localScale = new Vector3(stackScale * 0.5f, stackScale * 0.5f, stackScale * 0.5f);
		centreStack.GetComponent<Renderer>().material.color = stackColour;

		//orbit spheres
		// 1 -x
		orbitStack1 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		orbit1StackRigidBody = orbitStack1.AddComponent<Rigidbody> ();
		orbit1StackRigidBody.useGravity = false;
		orbit1StackRigidBody.detectCollisions = false;

		orbitStack1.transform.position = new Vector3(centroid.x - (0.5f * stackScale), centroid.y, centroid.z);
		//rotate position about centre of stack
		GeometryUtility.rotateCoordinateAboutPoint (centroid, orbitStack1.transform.position, stackTwist);
		orbitStack1.transform.localScale = new Vector3(stackScale * 0.3f, stackScale * 0.3f, stackScale * 0.3f);
		orbitStack1.GetComponent<Renderer>().material.color = stackColour;

		// 2 +x
		orbitStack2 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		orbit2StackRigidBody = orbitStack2.AddComponent<Rigidbody> ();
		orbit2StackRigidBody.useGravity = false;
		orbit2StackRigidBody.detectCollisions = false;
		
		orbitStack2.transform.position = new Vector3(centroid.x + (0.5f * stackScale), centroid.y, centroid.z);
		//rotate position about centre of stack
		GeometryUtility.rotateCoordinateAboutPoint (centroid, orbitStack2.transform.position, stackTwist);
		orbitStack2.transform.localScale = new Vector3(stackScale * 0.3f, stackScale * 0.3f, stackScale * 0.3f);
		orbitStack2.GetComponent<Renderer>().material.color = stackColour;

		// 3 -z
		orbitStack3 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		orbit3StackRigidBody = orbitStack3.AddComponent<Rigidbody> ();
		orbit3StackRigidBody.useGravity = false;
		orbit3StackRigidBody.detectCollisions = false;
		
		orbitStack3.transform.position = new Vector3(centroid.x, centroid.y, centroid.z - (0.5f * stackScale));
		//rotate position about centre of stack
		GeometryUtility.rotateCoordinateAboutPoint (centroid, orbitStack3.transform.position, stackTwist);
		orbitStack3.transform.localScale = new Vector3(stackScale * 0.3f, stackScale * 0.3f, stackScale * 0.3f);
		orbitStack3.GetComponent<Renderer>().material.color = stackColour;

		// 4 +z
		orbitStack4 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		orbit4StackRigidBody = orbitStack4.AddComponent<Rigidbody> ();
		orbit4StackRigidBody.useGravity = false;
		orbit4StackRigidBody.detectCollisions = false;
		
		orbitStack4.transform.position = new Vector3(centroid.x, centroid.y, centroid.z + (0.5f * scale));
		//rotate position about centre of stack
		GeometryUtility.rotateCoordinateAboutPoint (centroid, orbitStack4.transform.position, stackTwist);
		orbitStack4.transform.localScale = new Vector3(stackScale * 0.3f, stackScale * 0.3f, stackScale * 0.3f);
		orbitStack4.GetComponent<Renderer>().material.color = stackColour;

		return this;
	}
	
	public void mutateTo(Vector3 centroid, Vector3 stackTwist, float scale, Color stackColour) {
		
		float deltaX = stackTwist.x - currentRotationX;
		float deltaY = stackTwist.y - currentRotationY;
		float deltaZ = stackTwist.z - currentRotationZ;
		if (deltaX != 0 || deltaY != 0 || deltaZ != 0) {
			currentRotationX += deltaX;
			currentRotationY += deltaY;
			currentRotationZ += deltaZ;
		}

		if (centreStackRigidBody != null) {
			
			centreStack.GetComponent<Renderer>().material.color = stackColour;

			if (deltaX != 0 || deltaY != 0 || deltaZ != 0) {
				centreStack.transform.Rotate(new Vector3(deltaX, deltaY, deltaZ));
			}
			centreStackRigidBody.transform.position = Vector3.Lerp(centreStackRigidBody.transform.position, 
			                                                 centroid, 
			                                                 (Time.deltaTime * 3) / Vector3.Distance(centroid, 
				                                       		 centreStackRigidBody.transform.position));

			centreStack.transform.localScale = Vector3.Lerp(centreStack.transform.localScale,
			                                                new Vector3(scale * 0.5f, scale * 0.5f, scale * 0.5f),
			            									Time.deltaTime);
		}

		if (orbit1StackRigidBody != null) {

			Vector3 newPosition = new Vector3(centroid.x - (0.5f * scale), centroid.y, centroid.z);
			if (deltaX != 0 || deltaY != 0 || deltaZ != 0) {
				newPosition = GeometryUtility.rotateCoordinateAboutPoint(centroid, newPosition, new Vector3(deltaX, deltaY, deltaZ));
			}
			orbit1StackRigidBody.transform.position = Vector3.Lerp(orbit1StackRigidBody.transform.position, 
			                                                       newPosition, 
			                                                       (Time.deltaTime * 3) / Vector3.Distance(newPosition, 
			                                        				orbit1StackRigidBody.transform.position));

			orbitStack1.transform.localScale = Vector3.Lerp(orbitStack1.transform.localScale,
			                                                new Vector3(scale * 0.3f, scale * 0.3f, scale * 0.3f),
			                                                Time.deltaTime);
		}

		if (orbit2StackRigidBody != null) {
			
			Vector3 newPosition = new Vector3(centroid.x + (0.5f * scale), centroid.y, centroid.z);
			if (deltaX != 0 || deltaY != 0 || deltaZ != 0) {
				newPosition = GeometryUtility.rotateCoordinateAboutPoint(centroid, newPosition, new Vector3(deltaX, deltaY, deltaZ));
			}
			orbit2StackRigidBody.transform.position = Vector3.Lerp(orbit2StackRigidBody.transform.position, 
			                                                       newPosition, 
			                                                       (Time.deltaTime * 3) / Vector3.Distance(newPosition, 
			                                        				orbit2StackRigidBody.transform.position));

			orbitStack2.transform.localScale = Vector3.Lerp(orbitStack2.transform.localScale,
			                                                new Vector3(scale * 0.3f, scale * 0.3f, scale * 0.3f),
			                                                Time.deltaTime);
		}

		if (orbit3StackRigidBody != null) {
			
			Vector3 newPosition = new Vector3(centroid.x, centroid.y, centroid.z - (0.5f * scale));
			if (deltaX != 0 || deltaY != 0 || deltaZ != 0) {
				newPosition = GeometryUtility.rotateCoordinateAboutPoint(centroid, newPosition, new Vector3(deltaX, deltaY, deltaZ));
			}
			orbit3StackRigidBody.transform.position = Vector3.Lerp(orbit3StackRigidBody.transform.position, 
			                                                       newPosition, 
			                                                       (Time.deltaTime * 3) / Vector3.Distance(newPosition, 

			                                        orbit3StackRigidBody.transform.position));

			orbitStack3.transform.localScale = Vector3.Lerp(orbitStack3.transform.localScale,
			                                                new Vector3(scale * 0.3f, scale * 0.3f, scale * 0.3f),
			                                                Time.deltaTime);
		}

		if (orbit4StackRigidBody != null) {
			
			Vector3 newPosition = new Vector3(centroid.x, centroid.y, centroid.z + (0.5f * scale));
			if (deltaX != 0 || deltaY != 0 || deltaZ != 0) {
				newPosition = GeometryUtility.rotateCoordinateAboutPoint(centroid, newPosition, new Vector3(deltaX, deltaY, deltaZ));
			}
			orbit4StackRigidBody.transform.position = Vector3.Lerp(orbit4StackRigidBody.transform.position, 
			                                                       newPosition, 
			                                                       (Time.deltaTime * 3) / Vector3.Distance(newPosition, 
			                                        				orbit4StackRigidBody.transform.position));

			orbitStack4.transform.localScale = Vector3.Lerp(orbitStack4.transform.localScale,
			                                                new Vector3(scale * 0.3f, scale * 0.3f, scale * 0.3f),
			                                                Time.deltaTime);
		}
	}
	
	public void removeFromScene() {
		Destroy(centreStack);
		Destroy(orbitStack1);
		Destroy(orbitStack2);
		Destroy(orbitStack3);
		Destroy(orbitStack4);
	}
}
