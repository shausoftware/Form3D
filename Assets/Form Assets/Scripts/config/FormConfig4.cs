using UnityEngine;
using System.Collections;

/**
 * Config for demo mode 
 **/

public class FormConfig4 : IFormConfiguration {

	public FormConfig4() {
		reset ();
	}

	public override void reset() {
	
		//open starfish
		base.setIndex (3);

		base.setFormProcessor (new StarfishForm ());
		
		base.setStackShape (StackShape.Sphere);
		
		base.setStartPosition (new Vector3(0, 0, 0));
		base.setStartRotation (new Vector3(0, 0, 0));
		
		base.setTrunkPositionDelta (new Vector3(0.3f, 0, 0));
		base.setTrunkRotationDelta (new Vector3 (60, 0, 0));
		base.setTrunkIterations (24);
		
		base.setBranchPositionDelta (new Vector3(0, 1, 0));
		base.setBranchTwistDelta (new Vector3 (0, 0, 0));
		
		base.setStackIterations (30);
		base.setScaleDelta (0.9f);
		
		base.setStackStartTwist (new Vector3 (0, 0, 0));
		base.setStackTwistDelta (new Vector3 (0, 0, 0));
		
		base.setBranchTwistDelta (new Vector3 (0, 0, 7.5f));
	}
}
