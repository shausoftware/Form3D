using UnityEngine;
using System.Collections;

/**
 * Config for demo mode 
 **/

public class FormConfig7 : IFormConfiguration {
		
	public FormConfig7() {
		reset ();
	}
		
	public override void reset() {
			
		//twisted shell ring
		base.setIndex (6);

		base.setFormProcessor (new RingForm ());
		
		base.setStackShape (StackShape.Sphere);
		
		base.setStartPosition (new Vector3(0, 0, 0));
		
		//RING RADIUS
		base.setBranchPositionDelta (new Vector3(1, 0, 0));
		
		base.setTrunkPositionDelta (new Vector3(1, 0, 0));
		base.setTrunkRotationDelta (new Vector3 (0, 0, 15));
		base.setTrunkIterations (12);
		
		base.setBranchTwistDelta (new Vector3 (0, 0, 5));
		
		base.setStackIterations (55);
		base.setScaleDelta (1.0f);
		
		base.setStackStartTwist (new Vector3 (0, 0, 0));
		base.setStackTwistDelta (new Vector3 (10, 10, 10));
		
		base.setScaleDelta (0.95f);
	}
}
