using UnityEngine;
using System.Collections;

/**
 * Config for demo mode 
 **/

public class FormConfig2 : IFormConfiguration {

	public FormConfig2() {
		reset ();
	}

	public override void reset() {

		//open loxodrome
		base.setIndex (1);

		base.setFormProcessor (new LoxodromeForm ());
		
		base.setStackShape (StackShape.Sphere);
		
		base.setStartPosition (new Vector3(0, 0, 0));
		
		//RHUMB ANGLE
		base.setStartRotation (new Vector3(0.3f, 0, 0));
		
		//RING RADIUS
		base.setBranchPositionDelta (new Vector3(1, 0, 0));
		
		base.setTrunkPositionDelta (new Vector3(0, 0, 0));
		base.setTrunkRotationDelta (new Vector3 (60, 0, 0));
		base.setTrunkIterations (12);
		
		base.setBranchTwistDelta (new Vector3 (0, 0, 5));
		
		base.setStackIterations (55);
		base.setScaleDelta (1.0f);
		
		base.setStackStartTwist (new Vector3 (0, 0, 0));
		base.setStackTwistDelta (new Vector3 (10, 10, 10));
	}
}
