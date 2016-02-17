using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarfishForm : FormProcessor {

	public void sanitiseConfiguration(IFormConfiguration formConfiguration) {

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
			
			Branch branch = new Branch (0,
			                            formConfig, 
			                            colourConfig, 
			                            branchStartPosition,
			                            branchStartTwist,
			                            branchStartOrigin,
			                            formBounds); //magic 0 no offset
			branches.Add(branch);
			
			//set origin to previous start position
			branchStartOrigin = branchStartPosition;
			
			branchStartTwist = GeometryUtility.addDeltaToRotation(branchStartTwist, 
			                                                      formConfig.getTrunkRotationDelta());
			branchStartPosition = GeometryUtility.addDeltaToPosition(branchStartPosition, 
			                                                         formConfig.getTrunkPositionDelta(), 1);
		}
	}

	public void mutateTrunk(List<Branch> branches, 
	                 FormBounds formBounds,
	                 IFormConfiguration formConfig, 
	                 ColourConfiguration colourConfig) {

		Vector3 branchStartPosition = formConfig.getStartPosition();

		Vector3 branchStartTwist = formConfig.getStartRotation();
		Vector3 origin = formConfig.getStartPosition();
		
		foreach (Branch branch in branches) {
			
			formBounds.calculateNewBounds(branchStartPosition);
			
			branch.mutate(0,
			              formConfig,  
			              colourConfig,
			              branchStartPosition,
			              branchStartTwist,
			              origin,
			              formBounds);
			
			//set origin to previous start position
			origin = branchStartPosition;
			
			branchStartTwist = GeometryUtility.addDeltaToRotation(branchStartTwist, 
			                                                      formConfig.getTrunkRotationDelta());
			branchStartPosition = GeometryUtility.addDeltaToPosition(branchStartPosition, 
			                                                         formConfig.getTrunkPositionDelta(), 1);
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

		float scale = formConfig.getStartScale();
		Vector3 stackTwist = formConfig.getStackStartTwist();
		Vector3 position = branchStartPosition;
		Vector3 twist = branchStartTwist;
		//rotate position araound previous branch start position
		position = GeometryUtility.rotateCoordinateAboutPoint(origin, 
		                                                      position, 
		                                                      branchStartTwist);
		
		//colour stuff
		int iterations = formConfig.getStackIterations();
		Color modelColour = new Color (colourConfig.getBaseRed(), 
		                               colourConfig.getBaseGreen(), 
		                               colourConfig.getBaseBlue(),
		                               1);
		//if cycling override base colour
		if (colourConfig.getCycle ()) {
			modelColour = colourConfig.getCycleColour();
		}
		
		for (int i = 0; i < iterations; i++) {
			
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
			
			stack.initialise(position, stackTwist, scale, stackColour);
			stacks.Add(stack);
			
			scale = scale * formConfig.getScaleDelta();
			Vector3 newPosition = GeometryUtility.addDeltaToPosition(position, formConfig.getBranchPositionDelta(), scale);
			position = GeometryUtility.rotateCoordinateAboutPoint(position, newPosition, twist);
			twist = GeometryUtility.addDeltaToRotation(twist, formConfig.getBranchTwistDelta());
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

		float scale = formConfig.getStartScale();
		Vector3 stackTwist = formConfig.getStackStartTwist();
		Vector3 position = branchStartPosition;
		Vector3 twist = branchStartTwist;
		//rotate position araound previous branch start position
		position = GeometryUtility.rotateCoordinateAboutPoint(origin, 
		                                                      position, 
		                                                      branchStartTwist);
		
		//colour stuff
		int iterations = formConfig.getStackIterations();
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
			
			stack.mutateTo(position, stackTwist, scale, stackColour);
			
			scale = scale * formConfig.getScaleDelta();

			float positionScale = scale;
			if (!formConfig.getScaleBranch()) {
				//ignore scale
				positionScale = 1;
			}
			Vector3 newPosition = GeometryUtility.addDeltaToPosition(position, 
			                                                         formConfig.getBranchPositionDelta(), 
			                                                         positionScale);
			position = GeometryUtility.rotateCoordinateAboutPoint(position, newPosition, twist);
			twist = GeometryUtility.addDeltaToRotation(twist, formConfig.getBranchTwistDelta());
			stackTwist = GeometryUtility.addDeltaToRotation(stackTwist, formConfig.getStackTwistDelta());
			i++;
		}
	}
}
