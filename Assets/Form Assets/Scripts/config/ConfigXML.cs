using UnityEngine;
using System.Collections;
using System.Xml;

public class ConfigXML  {

	public static void saveConfiguration(IFormConfiguration formConfiguration) {

		string fileSavePath = Application.persistentDataPath + "/FormConfigSlot" + (formConfiguration.getIndex() + 1) + ".xml";

		Debug.Log ("Save:" + fileSavePath);

		XmlDocument xmlDoc = new XmlDocument();
		XmlElement root = xmlDoc.CreateElement("", "form", "");

		//save data
		root.SetAttribute("branchPositionDeltaX", formConfiguration.getBranchPositionDelta().x.ToString());
		root.SetAttribute("branchPositionDeltaY", formConfiguration.getBranchPositionDelta().y.ToString());
		root.SetAttribute("branchPositionDeltaZ", formConfiguration.getBranchPositionDelta().z.ToString());

		root.SetAttribute("branchTwistDeltaX", formConfiguration.getBranchTwistDelta().x.ToString());
		root.SetAttribute("branchTwistDeltaY", formConfiguration.getBranchTwistDelta().y.ToString());
		root.SetAttribute("branchTwistDeltaZ", formConfiguration.getBranchTwistDelta().z.ToString());

		root.SetAttribute("index", formConfiguration.getIndex().ToString());
		root.SetAttribute("mutationStrength", formConfiguration.getMutationStrength().ToString());
		root.SetAttribute("scaleBranch", formConfiguration.getScaleBranch().ToString());
		root.SetAttribute("scaleDelta", formConfiguration.getScaleDelta().ToString());
		root.SetAttribute("scaleTrunk", formConfiguration.getScaleTrunk().ToString());
		root.SetAttribute("stackIterations", formConfiguration.getStackIterations().ToString());
		root.SetAttribute("stackShapeIndex", formConfiguration.getStackShapeIndex().ToString());

		root.SetAttribute("stackStartTwistX", formConfiguration.getStackStartTwist().x.ToString());
		root.SetAttribute("stackStartTwistY", formConfiguration.getStackStartTwist().y.ToString());
		root.SetAttribute("stackStartTwistZ", formConfiguration.getStackStartTwist().z.ToString());

		root.SetAttribute("stackTwistDeltaX", formConfiguration.getStackTwistDelta().x.ToString());
		root.SetAttribute("stackTwistDeltaY", formConfiguration.getStackTwistDelta().y.ToString());
		root.SetAttribute("stackTwistDeltaZ", formConfiguration.getStackTwistDelta().z.ToString());

		root.SetAttribute("startPositionX", formConfiguration.getStartPosition().x.ToString());
		root.SetAttribute("startPositionY", formConfiguration.getStartPosition().y.ToString());
		root.SetAttribute("startPositionZ", formConfiguration.getStartPosition().z.ToString());

		root.SetAttribute("startRotationX", formConfiguration.getStartRotation().x.ToString());
		root.SetAttribute("startRotationY", formConfiguration.getStartRotation().y.ToString());
		root.SetAttribute("startRotationZ", formConfiguration.getStartRotation().z.ToString());

		root.SetAttribute("startScale", formConfiguration.getStartScale().ToString());
		root.SetAttribute("trunkIterations", formConfiguration.getTrunkIterations().ToString());

		root.SetAttribute("trunkPositionDeltaX", formConfiguration.getTrunkPositionDelta().x.ToString());
		root.SetAttribute("trunkPositionDeltaY", formConfiguration.getTrunkPositionDelta().y.ToString());
		root.SetAttribute("trunkPositionDeltaZ", formConfiguration.getTrunkPositionDelta().z.ToString());

		root.SetAttribute("trunklRotationDeltaX", formConfiguration.getTrunkRotationDelta().x.ToString());
		root.SetAttribute("trunklRotationDeltaY", formConfiguration.getTrunkRotationDelta().y.ToString());
		root.SetAttribute("trunklRotationDeltaZ", formConfiguration.getTrunkRotationDelta().z.ToString());

		xmlDoc.AppendChild(root);
		//save
		xmlDoc.Save(fileSavePath);
	}

	public static void loadConfiguration(IFormConfiguration formConfiguration) {

		string fileSavePath = Application.persistentDataPath + "/FormConfigSlot" + (formConfiguration.getIndex() + 1) + ".xml";

		Debug.Log ("Load:" + fileSavePath);

		XmlDocument xmlDoc = new XmlDocument();

		if (System.IO.File.Exists (fileSavePath)) {

			xmlDoc.Load(new XmlTextReader(fileSavePath));
			XmlElement root = xmlDoc.DocumentElement;
			
			float x = float.Parse(root.GetAttribute("branchPositionDeltaX"));
			float y = float.Parse(root.GetAttribute("branchPositionDeltaY"));
			float z = float.Parse(root.GetAttribute("branchPositionDeltaZ"));
			formConfiguration.setBranchPositionDelta(new Vector3(x, y, z));
			
			x = float.Parse(root.GetAttribute("branchTwistDeltaX"));
			y = float.Parse(root.GetAttribute("branchTwistDeltaY"));
			z = float.Parse(root.GetAttribute("branchTwistDeltaZ"));
			formConfiguration.setBranchTwistDelta(new Vector3(x, y, z));
			
			formConfiguration.setIndex(int.Parse(root.GetAttribute("index")));
			formConfiguration.setMutationStrength(float.Parse(root.GetAttribute("mutationStrength")));
			
			formConfiguration.setScaleBranch(bool.Parse(root.GetAttribute("scaleBranch")));
			formConfiguration.setScaleDelta(float.Parse(root.GetAttribute("scaleDelta")));
			formConfiguration.setScaleTrunk(bool.Parse(root.GetAttribute("scaleTrunk")));
			formConfiguration.setStackIterations(int.Parse(root.GetAttribute("stackIterations")));
			formConfiguration.setStackShape(int.Parse(root.GetAttribute("stackShapeIndex")));
			
			x = float.Parse(root.GetAttribute("stackStartTwistX"));
			y = float.Parse(root.GetAttribute("stackStartTwistY"));
			z = float.Parse(root.GetAttribute("stackStartTwistZ"));
			formConfiguration.setStackStartTwist(new Vector3(x, y, z));
			
			x = float.Parse(root.GetAttribute("stackTwistDeltaX"));
			y = float.Parse(root.GetAttribute("stackTwistDeltaY"));
			z = float.Parse(root.GetAttribute("stackTwistDeltaZ"));
			formConfiguration.setStackTwistDelta(new Vector3(x, y, z));
			
			x = float.Parse(root.GetAttribute("startPositionX"));
			y = float.Parse(root.GetAttribute("startPositionY"));
			z = float.Parse(root.GetAttribute("startPositionZ"));
			formConfiguration.setStartPosition(new Vector3(x, y, z));
			
			x = float.Parse(root.GetAttribute("startRotationX"));
			y = float.Parse(root.GetAttribute("startRotationY"));
			z = float.Parse(root.GetAttribute("startRotationZ"));
			formConfiguration.setStartRotation(new Vector3(x, y, z));
			
			formConfiguration.setStartScale(float.Parse(root.GetAttribute("startScale")));
			formConfiguration.setTrunkIterations(int.Parse(root.GetAttribute("trunkIterations")));
			
			x = float.Parse(root.GetAttribute("trunkPositionDeltaX"));
			y = float.Parse(root.GetAttribute("trunkPositionDeltaY"));
			z = float.Parse(root.GetAttribute("trunkPositionDeltaZ"));
			formConfiguration.setTrunkPositionDelta(new Vector3(x, y, z));
			
			x = float.Parse(root.GetAttribute("trunklRotationDeltaX"));
			y = float.Parse(root.GetAttribute("trunklRotationDeltaY"));
			z = float.Parse(root.GetAttribute("trunklRotationDeltaZ"));
			formConfiguration.setTrunkRotationDelta(new Vector3(x, y, z));

			//perfom bounds checks and fix
			formConfiguration.validateAndFix();
		
		} else {
			Debug.Log("File Not Found:" + fileSavePath);
		}
	}
}
