using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface FormProcessor {

	void createTrunk(List<Branch> branches, 
	                 FormBounds formBounds, 
	                 IFormConfiguration formConfig, 
	                 ColourConfiguration colourConfig);
	
 	void mutateTrunk(List<Branch> branches, 
	                 FormBounds formBounds,
	                 IFormConfiguration formConfig, 
	                 ColourConfiguration colourConfig);

	void createBranch(List<IStack> stacks,
	                    int offset,
	                    IFormConfiguration formConfig, 
	              		ColourConfiguration colourConfig, 
	              		Vector3 branchStartPosition, 
	              		Vector3 branchStartTwist,
	              		Vector3 origin,
	                  	FormBounds formBounds);

	void mutateBranch(List<IStack> stacks,
	                    int offset,
	                  	IFormConfiguration formConfig, 
	                   	ColourConfiguration colourConfig, 
	                   	Vector3 branchStartPosition, 
	                   	Vector3 branchStartTwist,
	                   	Vector3 origin,
	                   	FormBounds formBounds);

	void sanitiseConfiguration(IFormConfiguration formConfig);
}
