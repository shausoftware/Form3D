using UnityEngine;
using UnityEditor;

public class CreatePrimitiveMacros
{
	[MenuItem("GameObject/Create Other/Adv. Capsule", false, 2700)]
	public static void CreateCapsule()
	{
		GameObject gameObject = new GameObject("Capsule");
		gameObject.AddComponent<CapsulePrimitive>();
		Selection.objects = new GameObject[1] { gameObject };
		EditorApplication.ExecuteMenuItem("GameObject/Move To View");
		RegisterUndo(gameObject, "Capsule");
	}

	[MenuItem("GameObject/Create Other/Adv. Cube", false, 2700)]
	public static void CreateCube()
	{
		GameObject gameObject = new GameObject("Cube");
		gameObject.AddComponent<CubePrimitive>();
		Selection.objects = new GameObject[1] { gameObject };
		EditorApplication.ExecuteMenuItem("GameObject/Move To View");
		RegisterUndo(gameObject, "Cube");
	}

	[MenuItem("GameObject/Create Other/Adv. Cylinder", false, 2700)]
	public static void CreateCylinder()
	{
		GameObject gameObject = new GameObject("Cylinder");
		gameObject.AddComponent<CylinderPrimitive>();
		Selection.objects = new GameObject[1] { gameObject };
		EditorApplication.ExecuteMenuItem("GameObject/Move To View");
		RegisterUndo(gameObject, "Cylinder");
	}

	[MenuItem("GameObject/Create Other/Adv. Disk", false, 2700)]
	public static void CreateDisk()
	{
		GameObject gameObject = new GameObject("Disk");
		gameObject.AddComponent<DiskPrimitive>();
		Selection.objects = new GameObject[1] { gameObject };
		EditorApplication.ExecuteMenuItem("GameObject/Move To View");
		RegisterUndo(gameObject, "Disk");
	}

	[MenuItem("GameObject/Create Other/Adv. Plane", false, 2700)]
	public static void CreatePlane()
	{
		GameObject gameObject = new GameObject("Plane");
		gameObject.AddComponent<PlanePrimitive>();
		Selection.objects = new GameObject[1] { gameObject };
		EditorApplication.ExecuteMenuItem("GameObject/Move To View");
		RegisterUndo(gameObject, "Plane");
	}

	[MenuItem("GameObject/Create Other/Adv. Pyramid", false, 2700)]
	public static void CreatePyramid()
	{
		GameObject gameObject = new GameObject("Pyramid");
		gameObject.AddComponent<PyramidPrimitive>();
		Selection.objects = new GameObject[1] { gameObject };
		EditorApplication.ExecuteMenuItem("GameObject/Move To View");
		RegisterUndo(gameObject, "Pyramid");
	}

	[MenuItem("GameObject/Create Other/Adv. Slope", false, 2700)]
	public static void CreateSlope()
	{
		GameObject gameObject = new GameObject("Slope");
		gameObject.AddComponent<SlopePrimitive>();
		Selection.objects = new GameObject[1] { gameObject };
		EditorApplication.ExecuteMenuItem("GameObject/Move To View");
		RegisterUndo(gameObject, "Slope");
	}

	[MenuItem("GameObject/Create Other/Adv. Sphere", false, 2700)]
	public static void CreateSphere()
	{
		GameObject gameObject = new GameObject("Sphere");
		gameObject.AddComponent<SpherePrimitive>();
		Selection.objects = new GameObject[1] { gameObject };
		EditorApplication.ExecuteMenuItem("GameObject/Move To View");
		RegisterUndo(gameObject, "Sphere");
	}

	[MenuItem("GameObject/Create Other/Adv. Torus", false, 2700)]
	public static void CreateTorus()
	{
		GameObject gameObject = new GameObject("Torus");
		gameObject.AddComponent<TorusPrimitive>();
		Selection.objects = new GameObject[1] { gameObject };
		EditorApplication.ExecuteMenuItem("GameObject/Move To View");
		RegisterUndo(gameObject, "Torus");
	}

	static void RegisterUndo(GameObject gameObject, string title)
	{
		#if UNITY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2
		#else
		Undo.RegisterCreatedObjectUndo(gameObject, "Created " + title);
		#endif
	}
}
