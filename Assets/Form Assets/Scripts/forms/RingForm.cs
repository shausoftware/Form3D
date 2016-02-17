using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingForm : FormProcessor {

	IStack testStack;

	public void sanitiseConfiguration(IFormConfiguration formConfiguration) {
		
		//RADIUS
		if (formConfiguration.getBranchPositionDelta().x < 0.2f) {
			Vector3 v = formConfiguration.getBranchPositionDelta();
			v.x = 0.2f;
			formConfiguration.setBranchPositionDelta(v);
		}
		if (formConfiguration.getBranchPositionDelta().x > 1.0f) {
			Vector3 v = formConfiguration.getBranchPositionDelta();
			v.x = 1.0f;
			formConfiguration.setBranchPositionDelta(v);
		}
	}

	public void createTrunk(List<Branch> branches, 
	                        FormBounds formBounds, 
	                        IFormConfiguration formConfig, 
	                        ColourConfiguration colourConfig) {
		
		Vector3 branchStartPosition = formConfig.getStartPosition();

		Vector3 branchStartTwist = formConfig.getStartRotation();
		Vector3 branchStartOrigin = formConfig.getStartPosition();
		
		for (int i = 0; i < formConfig.getTrunkIterations(); i++) {
			
			formBounds.calculateNewBounds(branchStartPosition);
			
			Branch branch = new Branch (i,
			                            formConfig,
			                            colourConfig, 
			                            branchStartPosition,
			                            branchStartTwist,
			                            branchStartOrigin,
			                            formBounds);
			branches.Add(branch);

			branchStartPosition.x += formConfig.getTrunkPositionDelta().x;
			branchStartPosition.y += formConfig.getTrunkPositionDelta().y;
			branchStartPosition.z += formConfig.getTrunkPositionDelta().z;

			branchStartTwist.x += formConfig.getTrunkRotationDelta().x;
			branchStartTwist.y += formConfig.getTrunkRotationDelta().y;
			branchStartTwist.z += formConfig.getTrunkRotationDelta().z;
		}
	}
	
	public void mutateTrunk(List<Branch> branches, 
	                        FormBounds formBounds,
	                        IFormConfiguration formConfig, 
	                        ColourConfiguration colourConfig) {

		Vector3 branchStartPosition = formConfig.getStartPosition();
		
		Vector3 branchStartTwist = formConfig.getStartRotation();
		Vector3 branchStartOrigin = formConfig.getStartPosition();

		int i = 0;
		foreach (Branch branch in branches) {
			
			formBounds.calculateNewBounds(branchStartPosition);
			
			branch.mutate(i,
			              formConfig,  
			              colourConfig,
			              branchStartPosition,
			              branchStartTwist,
			              branchStartOrigin,
			              formBounds);
			
			branchStartPosition.x += formConfig.getTrunkPositionDelta().x;
			branchStartPosition.y += formConfig.getTrunkPositionDelta().y;
			branchStartPosition.z += formConfig.getTrunkPositionDelta().z;

			branchStartTwist.x += formConfig.getTrunkRotationDelta().x;
			branchStartTwist.y += formConfig.getTrunkRotationDelta().y;
			branchStartTwist.z += formConfig.getTrunkRotationDelta().z;
			i++;
		}
	}
	
	public void createBranch(List<IStack> stacks,
	                         int offset,
	                         IFormConfiguration formConfig, 
	                         ColourConfiguration colourConfig, 
	                         Vector3 branchStartPosition, 
	                         Vector3 branchStartTwist,
	                         Vector3 origin,
	                         FormBounds formBounds) {

		int iterations = formConfig.getStackIterations();

		float scale = formConfig.getStartScale();
		for (int o = 0; o < offset; o++) {
			scale *= formConfig.getScaleDelta();
		}

		//centre of ring
		Vector3 position = branchStartPosition;

		//add radius of ring
		float ringRadius = formConfig.getBranchPositionDelta().x * 10 * scale;

		//angle to calculate ring 
		float zAngle = branchStartTwist.z * Mathf.Deg2Rad;
		float yAngle = branchStartTwist.y * Mathf.Deg2Rad;
		float ringSegmentAngle = 360 / iterations * Mathf.Deg2Rad;

		Vector3 stackTwist = formConfig.getStackStartTwist();
		
		//colour stuff
		Color modelColour = new Color (colourConfig.getBaseRed(), 
		                               colourConfig.getBaseGreen(), 
		                               colourConfig.getBaseBlue(),
		                               1);
		//if cycling override base colour
		if (colourConfig.getCycle ()) {
			modelColour = colourConfig.getCycleColour();
		}

		for (int i = offset; i < offset + iterations; i++) {
			
			position.x = (ringRadius * Mathf.Cos(zAngle) * Mathf.Sin(yAngle)) + branchStartPosition.x;
			position.y = (ringRadius * Mathf.Sin(zAngle) * Mathf.Sin(yAngle)) + branchStartPosition.y;
			position.z = (ringRadius * Mathf.Cos(yAngle)) + branchStartPosition.z;

			formBounds.calculateNewBounds(position);
			
			//Stack Shape
			IStack stack = new SimpleStack(); //default
			if (formConfig.getStackShape() == IFormConfiguration.StackShape.Sphere) {
				//spehere
				stack = new SimpleStack();
			} else if (formConfig.getStackShape() == IFormConfiguration.StackShape.Box) {
				//box
				stack = new SimpleBoxStack();
			} else if (formConfig.getStackShape() == IFormConfiguration.StackShape.Complex1) {
				//complex 1
				stack = new ComplexStack();
			} else if (formConfig.getStackShape() == IFormConfiguration.StackShape.Complex2) {
				//complex 2
				stack = new ComplexStack2();
			} else if (formConfig.getStackShape() == IFormConfiguration.StackShape.SimpleTorus) {
				//simple Torus
				stack = new SimpleTorusStack();
			}
			
			//colour stuff order is important
			Color stackColour = modelColour;
			if (colourConfig.getFadeColour()) {
				//fade colour in 
				stackColour = ColourUtility.fadeModelColour(modelColour, iterations, i);
			}
			if (colourConfig.getPulse()) {
				//do pulse
				stackColour = colourConfig.getPulseColourForStack(stackColour, i);
			}
			
			stack.initialise(position, stackTwist, 0.5f * scale, stackColour);
			stacks.Add(stack);

			yAngle += ringSegmentAngle;
			stackTwist = GeometryUtility.addDeltaToRotation(stackTwist, formConfig.getStackTwistDelta());
		}
	}
	
	public void mutateBranch(List<IStack> stacks,
	                         int offset,
	                         IFormConfiguration formConfig, 
	                         ColourConfiguration colourConfig, 
	                         Vector3 branchStartPosition, 
	                         Vector3 branchStartTwist,
	                         Vector3 origin,
	                         FormBounds formBounds) {

		int iterations = formConfig.getStackIterations();

		float scale = formConfig.getStartScale();
		for (int o = 0; o < offset; o++) {
			scale *= formConfig.getScaleDelta();
		}

		//centre of ring
		Vector3 position = branchStartPosition;

		//add radius of ring
		float ringRadius = formConfig.getBranchPositionDelta().x * 10 * scale;
		
		//first element of ring on radius
		float zAngle = branchStartTwist.z * Mathf.Deg2Rad;
		float yAngle = branchStartTwist.y * Mathf.Deg2Rad;

		float ringSegmentAngle = 360 / iterations * Mathf.Deg2Rad;
		
		Vector3 stackTwist = formConfig.getStackStartTwist();
		
		//colour stuff
		Color modelColour = new Color (colourConfig.getBaseRed(), 
		                               colourConfig.getBaseGreen(), 
		                               colourConfig.getBaseBlue(),
		                               1);

		//if cycling override base colour
		if (colourConfig.getCycle ()) {
			modelColour = colourConfig.getCycleColour();
		}

		int i = 0;
		foreach (IStack stack in stacks) {
			
			position.x = (ringRadius * Mathf.Cos(zAngle) * Mathf.Sin(yAngle)) + branchStartPosition.x;
			position.y = (ringRadius * Mathf.Sin(zAngle) * Mathf.Sin(yAngle)) + branchStartPosition.y;
			position.z = (ringRadius * Mathf.Cos(yAngle)) + branchStartPosition.z;

			formBounds.calculateNewBounds(position);

			//colour stuff order is important
			Color stackColour = modelColour;
			if (colourConfig.getFadeColour()) {
				//fade colour in 
				stackColour = ColourUtility.fadeModelColour(modelColour, iterations, i);
			}
			if (colourConfig.getPulse()) {
				//do pulse
				stackColour = colourConfig.getPulseColourForStack(stackColour, i);
			}
			
			stack.mutateTo(position, stackTwist, 0.5f * scale, stackColour);

			yAngle += ringSegmentAngle;

			yAngle += ringSegmentAngle;
			stackTwist = GeometryUtility.addDeltaToRotation(stackTwist, formConfig.getStackTwistDelta());
			i++;
		}
	}
}
