using UnityEngine;
using System.Collections;

public class IFormConfiguration  {

	public enum StackShape {Sphere, Box, Complex1, Complex2, SimpleTorus};

	private int index = 0;

	private FormProcessor formProcessor = new LoxodromeForm ();

	public FormProcessor getFormProcessor() {
		formProcessor.sanitiseConfiguration (this);
		return formProcessor;
	}
	public void setFormProcessor(FormProcessor formProcessor) {
		formProcessor.sanitiseConfiguration (this);
		this.formProcessor = formProcessor;
	}

	//trunk
	private Vector3 startPosition = new Vector3(0, 0, 0);
	private Vector3 startRotation = new Vector3(0, 0, 0);
	private Vector3 trunkPositionDelta = new Vector3(0, 0, 0);
	private Vector3 trunkRotationDelta = new Vector3 (0, 0, 0);
	private int trunkIterations = 6;
	private bool scaleTrunk = false;

	//branch
	private Vector3 branchPositionDelta = new Vector3(0, 0, 0);
	private Vector3 branchTwistDelta = new Vector3 (0, 0, 0);
	private int stackIterations = 30;
	private float startScale = 1f;
	private float scaleDelta = 0.9f;
	private bool scaleBranch = true;

	//stack
	private Vector3 stackStartTwist = new Vector3 (0, 0, 0);
	private Vector3 stackTwistDelta = new Vector3 (0, 0, 0);

	//shapes
	private StackShape stackShape = StackShape.Sphere;

	private float mutationStrength = 1.0f;

	//Constructors
	public IFormConfiguration() {
	}
	public IFormConfiguration(IFormConfiguration config) {
		this.index = config.getIndex ();
		this.startPosition = config.getStartPosition ();
		this.startRotation = config.getStartRotation ();
		this.trunkPositionDelta = config.getTrunkPositionDelta ();
		this.trunkRotationDelta = config.getTrunkRotationDelta ();
		this.trunkIterations = config.getTrunkIterations ();
		this.scaleTrunk = config.getScaleTrunk ();
		this.branchPositionDelta = config.getBranchPositionDelta ();
		this.branchTwistDelta = config.getBranchTwistDelta ();
		this.stackIterations = config.getStackIterations ();
		this.startScale = config.getStartScale ();
		this.scaleDelta = config.getScaleDelta ();
		this.scaleBranch = config.getScaleBranch ();
		this.stackStartTwist = config.getStackStartTwist ();
		this.stackTwistDelta = config.getStackTwistDelta ();
		this.stackShape = config.getStackShape ();
		this.mutationStrength = config.getMutationStrength ();
	}

	//overide methods
	public virtual void reset() {
	}

	public void randomiseValuesWithSymetry() {

		float randomNumber = Random.Range (-1.0f, 1.0f);

		branchPositionDelta.x += randomNumber * mutationStrength * 0.1f;
		branchPositionDelta.y += randomNumber *  mutationStrength * 0.1f;
		branchPositionDelta.z += randomNumber * mutationStrength * 0.1f;

		branchTwistDelta.x += randomNumber * 10 * mutationStrength;
		branchTwistDelta.y += randomNumber * 10 * mutationStrength;
		branchTwistDelta.z += randomNumber * 10 * mutationStrength;

		trunkPositionDelta.x += randomNumber * mutationStrength * 0.05f;
		trunkPositionDelta.y += randomNumber * mutationStrength * 0.05f;
		trunkPositionDelta.z += randomNumber * mutationStrength * 0.05f;

		trunkRotationDelta.x += randomNumber * 5 * mutationStrength;
		trunkRotationDelta.y += randomNumber * 5 * mutationStrength;
		trunkRotationDelta.z += randomNumber * 5 * mutationStrength;

		stackStartTwist.x += randomNumber * 60 * mutationStrength;
		stackStartTwist.y += randomNumber * 60 * mutationStrength;
		stackStartTwist.z += randomNumber * 60 * mutationStrength;

		stackTwistDelta.x += randomNumber * 30 * mutationStrength;
		stackTwistDelta.y += randomNumber * 30 * mutationStrength;
		stackStartTwist.y += randomNumber * 30 * mutationStrength;
	}

	public void randomiseValuesWithoutSymetry() {
		
		float randomNumberX = Random.Range (-1.0f, 1.0f);
		float randomNumberY = Random.Range (-1.0f, 1.0f);
		float randomNumberZ = Random.Range (-1.0f, 1.0f);

		branchPositionDelta.x += randomNumberX * mutationStrength * 0.1f;
		branchPositionDelta.y += randomNumberY *  mutationStrength * 0.1f;
		branchPositionDelta.z += randomNumberZ * mutationStrength * 0.1f;
		
		branchTwistDelta.x += randomNumberX * 10 * mutationStrength;
		branchTwistDelta.y += randomNumberY * 10 * mutationStrength;
		branchTwistDelta.z += randomNumberZ * 10 * mutationStrength;
		
		trunkPositionDelta.x += randomNumberX * mutationStrength * 0.05f;
		trunkPositionDelta.y += randomNumberY * mutationStrength * 0.05f;
		trunkPositionDelta.z += randomNumberZ * mutationStrength * 0.05f;
		
		trunkRotationDelta.x += randomNumberX * 5 * mutationStrength;
		trunkRotationDelta.y += randomNumberY * 5 * mutationStrength;
		trunkRotationDelta.z += randomNumberZ * 5 * mutationStrength;
		
		stackStartTwist.x += randomNumberX * 60 * mutationStrength;
		stackStartTwist.y += randomNumberY * 60 * mutationStrength;
		stackStartTwist.z += randomNumberZ * 60 * mutationStrength;
		
		stackTwistDelta.x += randomNumberX * 30 * mutationStrength;
		stackTwistDelta.y += randomNumberY * 30 * mutationStrength;
		stackStartTwist.y += randomNumberZ * 30 * mutationStrength;
	}

	//used when loading config from file
	public void validateAndFix() {

		//mutation strength
		if (mutationStrength > 3.5) {
			mutationStrength = 3.5f;
		}
		if (mutationStrength < 0.5f) {
			mutationStrength = 0.5f;
		}

		//start position
		if (startPosition.x > 5) {
			startPosition.x = 5;
		}
		if (startPosition.x < -5) {
			startPosition.x = -5;
		}
		if (startPosition.y > 5) {
			startPosition.y = 5;
		}
		if (startPosition.y < -5) {
			startPosition.y = -5;
		}
		if (startPosition.z > 5) {
			startPosition.z = 5;
		}
		if (startPosition.z < -5) {
			startPosition.z = -5;
		}

		//start rotation
		if (startRotation.x > 360) {
			startRotation.x = 360;
		}
		if (startRotation.x < 0) {
			startRotation.x = 0;
		}
		if (startRotation.y > 360) {
			startRotation.y = 360;
		}
		if (startRotation.y < 0) {
			startRotation.y = 0;
		}
		if (startRotation.z > 360) {
			startRotation.z = 360;
		}
		if (startRotation.z < 0) {
			startRotation.z = 0;
		}

		//branch position delta
		if (branchPositionDelta.x > 1) {
			branchPositionDelta.x = 1;
		}
		if (branchPositionDelta.x < -1) {
			branchPositionDelta.x = -1;
		}
		if (branchPositionDelta.y > 1) {
			branchPositionDelta.y = 1;
		}
		if (branchPositionDelta.y < -1) {
			branchPositionDelta.y = -1;
		}
		if (branchPositionDelta.z > 1) {
			branchPositionDelta.z = 1;
		}
		if (branchPositionDelta.z < -1) {
			branchPositionDelta.z = -1;
		}

		//branch twist delta
		if (branchTwistDelta.x > 30) {
			branchTwistDelta.x = 30;
		}
		if (branchTwistDelta.x < -30) {
			branchTwistDelta.x = -30;
		}
		if (branchTwistDelta.y > 30) {
			branchTwistDelta.y = 30;
		}
		if (branchTwistDelta.y < -30) {
			branchTwistDelta.y = -30;
		}
		if (branchTwistDelta.z > 30) {
			branchTwistDelta.z = 30;
		}
		if (branchTwistDelta.z < -30) {
			branchTwistDelta.z = -30;
		}

		//stack iterations
		if (stackIterations > 80) {
			stackIterations = 80;
		}
		if (stackIterations < 1) {
			stackIterations = 1;
		}

		//start scale
		if (startScale < 2) {
			startScale = 2;
		}
		if (startScale < 0.5f) {
			startScale = 0.5f;
		}

		//scale delta
		if (scaleDelta > 1.1f) {
			scaleDelta = 1.1f;
		}
		if (scaleDelta < 0.9f) {
			scaleDelta = 0.9f;
		}

		//trunk position delta
		if (trunkPositionDelta.x > 1f) {
			trunkPositionDelta.x = 1f;
		}
		if (trunkPositionDelta.x < -1f) {
			trunkPositionDelta.x = -1f;
		}
		if (trunkPositionDelta.y > 1f) {
			trunkPositionDelta.y = 1f;
		}
		if (trunkPositionDelta.y < -1f) {
			trunkPositionDelta.y = -1f;
		}
		if (trunkPositionDelta.z > 1f) {
			trunkPositionDelta.z = 1f;
		}
		if (trunkPositionDelta.z < -1f) {
			trunkPositionDelta.z = -1f;
		}

		//trunk rotation delta
		if (trunkRotationDelta.x > 60f) {
			trunkRotationDelta.x = 60f;
		}
		if (trunkRotationDelta.x < -60f) {
			trunkRotationDelta.x = -60f;
		}
		if (trunkRotationDelta.y > 60f) {
			trunkRotationDelta.y = 60f;
		}
		if (trunkRotationDelta.y < -60f) {
			trunkRotationDelta.y = -60f;
		}
		if (trunkRotationDelta.z > 60f) {
			trunkRotationDelta.z = 60f;
		}
		if (trunkRotationDelta.z < -60f) {
			trunkRotationDelta.z = -60f;
		}

		//trunk iterations
		if (trunkIterations > 40) {
			trunkIterations = 40;
		}
		if (trunkIterations < 1) {
			trunkIterations = 1;
		}

		//stack start twist
		if (stackStartTwist.x > 360f) {
			stackStartTwist.x = 360f;
		}
		if (stackStartTwist.x < 0f) {
			stackStartTwist.x = 0f;
		}
		if (stackStartTwist.y > 360f) {
			stackStartTwist.y = 360f;
		}
		if (stackStartTwist.y < 0f) {
			stackStartTwist.y = 0f;
		}
		if (stackStartTwist.z > 360f) {
			stackStartTwist.z = 360f;
		}
		if (stackStartTwist.z < 0f) {
			stackStartTwist.z = 0f;
		}

		//stack twist delta
		if (stackTwistDelta.x > 30f) {
			stackTwistDelta.x = 30f;
		}
		if (stackTwistDelta.x < -30f) {
			stackTwistDelta.x = -30f;
		}
		if (stackTwistDelta.y > 30f) {
			stackTwistDelta.y = 30f;
		}
		if (stackTwistDelta.y < -30f) {
			stackTwistDelta.y = -30f;
		}
		if (stackTwistDelta.z > 30f) {
			stackTwistDelta.z = 30f;
		}
		if (stackTwistDelta.z < -30f) {
			stackTwistDelta.z = -30f;
		}
	}

	public void setIndex(int index) {
		this.index = index;	
	}
	public int getIndex() {
		return index;
	}

	public void setMutationStrength(float mutationStrength) {
		this.mutationStrength = mutationStrength;
	}
	public float getMutationStrength() {
		return mutationStrength;
	}

	//TODO - torus coming soon
	public void setStackShape(int stackIndex) {
		if (stackIndex == 0) {
			stackShape = StackShape.Sphere;
		} else if (stackIndex == 1) {
			stackShape = StackShape.Box;
		} else if (stackIndex == 2) {
			stackShape = StackShape.Complex1;
		} else if (stackIndex == 3) {
			stackShape = StackShape.Complex2;
//		} else if (stackIndex == 4) {
//			stackShape = StackShape.SimpleTorus;
		} else {
			//index not recognised revert to default
			stackIndex = 0;
			stackShape = StackShape.Sphere;
		}
	}
	public int getStackShapeIndex() {

		int stackIndex = 0; //sphere

		if (stackShape == StackShape.Box) {
			stackIndex = 1;
		} else if (stackShape == StackShape.Complex1) {
			stackIndex = 2;
		} else if (stackShape == StackShape.Complex2) {
			stackIndex = 3;
//		} else if (stackShape == StackShape.SimpleTorus) {
//			stackIndex = 4;
		}

		return stackIndex;
	}

	public void setStackShape(StackShape stackShape) {
		this.stackShape = stackShape;
	}
	public StackShape getStackShape() {
		return stackShape;
	}

	public void setScaleTrunk(bool scaleTrunk) {
		this.scaleTrunk = scaleTrunk;
	}
	public bool getScaleTrunk() {
		return scaleTrunk;
	}

	public void setScaleBranch(bool scaleBranch) {
		this.scaleBranch = scaleBranch;
	}
	public bool getScaleBranch() {
		return scaleBranch;
	}

	public Vector3 getStartPosition() {
		return startPosition;
	}
	public void setStartPosition(Vector3 startPosition) {
		this.startPosition = startPosition;
	}

	public Vector3 getStartRotation() {
		return startRotation;
	}
	public void setStartRotation(Vector3 startRotation) {
		this.startRotation = startRotation;
	}

	public Vector3 getBranchPositionDelta() {
		return branchPositionDelta;
	}
	public void setBranchPositionDelta(Vector3 branchPositionDelta) {
		this.branchPositionDelta = branchPositionDelta;
	}

	public Vector3 getBranchTwistDelta() {
		return branchTwistDelta;
	}
	public void setBranchTwistDelta(Vector3 branchTwistDelta) {
		this.branchTwistDelta = branchTwistDelta;
	}

	public int getStackIterations() {
		return stackIterations;
	}
	public void setStackIterations(int stackIterations) {
		this.stackIterations = stackIterations;
	}

	public float getStartScale() {
		return startScale;
	}
	public void setStartScale(float startScale) {
		this.startScale = startScale;
	}

	public float getScaleDelta() {
		return scaleDelta;
	}
	public void setScaleDelta(float scaleDelta) {
		this.scaleDelta = scaleDelta;
	}

	public Vector3 getTrunkPositionDelta() {
		return trunkPositionDelta;
	}
	public void setTrunkPositionDelta(Vector3 trunkPositionDelta) {
		this.trunkPositionDelta = trunkPositionDelta;
	}

	public Vector3 getTrunkRotationDelta() {
		return trunkRotationDelta;
	}
	public void setTrunkRotationDelta(Vector3 trunkRotationDelta) {
		this.trunkRotationDelta = trunkRotationDelta;
	}

	public int getTrunkIterations() {
		return trunkIterations;
	}
	public void setTrunkIterations(int trunkIterations) {
		this.trunkIterations = trunkIterations;
	}
	
	public void setStackStartTwist(Vector2 stackStartTwist) {
		this.stackStartTwist = stackStartTwist;
	}
	public Vector3 getStackStartTwist() {
		return stackStartTwist;
	}

	public void setStackTwistDelta(Vector3 stackTwistDelta) {
		this.stackTwistDelta = stackTwistDelta;
	}
	public Vector3 getStackTwistDelta() {
		return stackTwistDelta;
	}
}
