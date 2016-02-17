using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Branch {

	private List<IStack> stacks = new List<IStack>();

	public Branch(int offset,
	              IFormConfiguration formConfig, 
	              ColourConfiguration colourConfig, 
	              Vector3 branchStartPosition, 
	              Vector3 branchStartTwist,
	              Vector3 origin,
	              FormBounds formBounds) {

		FormProcessor formProcessor = formConfig.getFormProcessor ();
		formProcessor.createBranch(stacks,
		                           offset,
		                           formConfig, 
		                           colourConfig, 
		                           branchStartPosition, 
		                           branchStartTwist,
		                           origin,
		                           formBounds);
	}

	public void mutate(int offset,
					   IFormConfiguration formConfig, 
	                   ColourConfiguration colourConfig, 
	                   Vector3 branchStartPosition, 
	                   Vector3 branchStartTwist,
	                   Vector3 branchStartRotationOrigin,
	                   FormBounds formBounds) {

		FormProcessor formProcessor = formConfig.getFormProcessor ();
		formProcessor.mutateBranch(stacks,
		                           offset,
		                           formConfig, 
		                           colourConfig, 
		                           branchStartPosition, 
		                           branchStartTwist,
		                           branchStartRotationOrigin,
		                           formBounds);
	}

	public void removeFromScene() {

		foreach (IStack stack in stacks) {
			stack.removeFromScene();
		}

		stacks = new List<IStack> ();
	}
}
