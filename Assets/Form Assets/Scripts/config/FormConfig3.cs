using UnityEngine;
using System.Collections;

/**
 * Config for demo mode 
 **/

public class FormConfig3 : IFormConfiguration {

	public FormConfig3() {
		reset ();
	}

	public override void reset() {

		//starfish thing
		base.setIndex (2);

		base.setFormProcessor (new StarfishForm ());

		base.setStackShape (StackShape.Sphere);

		base.setStartPosition (new Vector3(0, 0, 0));
		base.setStartRotation (new Vector3(0, 0, 0));
		
		base.setTrunkPositionDelta (new Vector3(0, 0, 0));
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
