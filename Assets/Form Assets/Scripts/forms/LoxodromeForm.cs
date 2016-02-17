using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LoxodromeForm : FormProcessor {

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
		
		//RHUMB ANGLE
		if (formConfiguration.getStartRotation().x < 0) {
			Vector3 v = formConfiguration.getStartRotation();
			v.x = 0;
			formConfiguration.setStartRotation(v);
		}
		if (formConfiguration.getStartRotation().x > 2f) {
			Vector3 v = formConfiguration.getStartRotation();
			v.x = 2.0f;
			formConfiguration.setStartRotation(v);
		}
	}

	public void createTrunk(List<Branch> branches, 
	                 FormBounds formBounds, 
	                 IFormConfiguration formConfig, 
	                 ColourConfiguration colourConfig) {

		int offset = (formConfig.getTrunkIterations() * formConfig.getStackIterations() / 2) * -1;
		
		Vector3 branchStartPosition = formConfig.getStartPosition();
		branchStartPosition.y = branchStartPosition.y + 10;

		Vector3 branchStartTwist = formConfig.getStartRotation();
		Vector3 branchStartOrigin = formConfig.getStartPosition();
		
		for (int i = 0; i < formConfig.getTrunkIterations(); i++) {
			
			formBounds.calculateNewBounds(branchStartPosition);
			
			Branch branch = new Branch (offset,
			                            formConfig,
			                            colourConfig, 
			                            branchStartPosition,
			                            branchStartTwist,
			                            branchStartOrigin,
			                            formBounds);
			branches.Add(branch);

			offset += formConfig.getStackIterations();
		}
	}
	
	public void mutateTrunk(List<Branch> branches, 
	                 FormBounds formBounds,
	                 IFormConfiguration formConfig, 
	                 ColourConfiguration colourConfig) {

		int offset = (formConfig.getTrunkIterations() * formConfig.getStackIterations() / 2) * -1;
		
		Vector3 branchStartPosition = formConfig.getStartPosition();
		branchStartPosition.y = branchStartPosition.y + 10;
		
		Vector3 branchStartTwist = formConfig.getStartRotation();
		Vector3 branchStartOrigin = formConfig.getStartPosition();
		
		foreach (Branch branch in branches) {
			
			formBounds.calculateNewBounds(branchStartPosition);
			
			branch.mutate(offset,
			              formConfig,  
			              colourConfig,
			              branchStartPosition,
			              branchStartTwist,
			              branchStartOrigin,
			              formBounds);
			
			offset += formConfig.getStackIterations();
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

		Vector3 position = origin;
		Vector3 stackTwist = formConfig.getStackStartTwist();

		float radius = formConfig.getBranchPositionDelta ().x * 20;
		float rhumbAngle = formConfig.getStartRotation().x * 0.032f;

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

		for (int i = offset; i < offset + iterations; i++) {
			
			position.x = formConfig.getStartPosition().x + (radius * (float) Math.Cos(i) / (float) Math.Cosh (Mathf.Rad2Deg * (Mathf.Deg2Rad * i * rhumbAngle)));
			position.y = formConfig.getStartPosition().y + (radius * (float) Math.Sin(i) / (float) Math.Cosh (Mathf.Rad2Deg * (Mathf.Deg2Rad * i * rhumbAngle)));
			position.z = formConfig.getStartPosition().z + (radius * (float) Math.Tanh(Mathf.Rad2Deg * (Mathf.Deg2Rad * i * rhumbAngle)));
			
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
			
			stack.initialise(position, stackTwist, 0.5f, stackColour);
			stacks.Add(stack);

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

		Vector3 position = origin;
		Vector3 stackTwist = formConfig.getStackStartTwist();

		float radius = formConfig.getBranchPositionDelta ().x * 20;
		float rhumbAngle = formConfig.getStartRotation().x * 0.032f;

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

		int i = offset;
		foreach (IStack stack in stacks) {

			position.x = formConfig.getStartPosition().x + (radius * (float) Math.Cos(i) / (float) Math.Cosh (Mathf.Rad2Deg * (Mathf.Deg2Rad * i * rhumbAngle)));
			position.y = formConfig.getStartPosition().y + (radius * (float) Math.Sin(i) / (float) Math.Cosh (Mathf.Rad2Deg * (Mathf.Deg2Rad * i * rhumbAngle)));
			position.z = formConfig.getStartPosition().z + (radius * (float) Math.Tanh(Mathf.Rad2Deg * (Mathf.Deg2Rad * i * rhumbAngle)));
			
			formBounds.calculateNewBounds(position);
			
			//colour stuff order is important
			Color stackColour = modelColour;
			if (colourConfig.getFadeColour()) {
				//fade colour in 
				stackColour = ColourUtility.fadeModelColour(modelColour, iterations, i - offset);
			}
			if (colourConfig.getPulse()) {
				//do pulse
				stackColour = colourConfig.getPulseColourForStack(stackColour, i - offset);
			}
			
			stack.mutateTo(position, stackTwist, 0.5f, stackColour);

			stackTwist = GeometryUtility.addDeltaToRotation(stackTwist, formConfig.getStackTwistDelta());

			i++;
		}
	}
}
