using UnityEngine;
using UnityEditor;

public class AdvPrimitiveToolBox : EditorWindow
{
	private static Texture iconCapsule  = (Texture)Resources.Load("icon-capsule");
	private static Texture iconCube     = (Texture)Resources.Load("icon-cube");
	private static Texture iconCylinder = (Texture)Resources.Load("icon-cylinder");
	private static Texture iconDisk     = (Texture)Resources.Load("icon-disk");
	private static Texture iconPlane    = (Texture)Resources.Load("icon-plane");
	private static Texture iconPyramid  = (Texture)Resources.Load("icon-pyramid");
	private static Texture iconSphere   = (Texture)Resources.Load("icon-sphere");
	private static Texture iconSlope    = (Texture)Resources.Load("icon-slope");
	private static Texture iconTorus    = (Texture)Resources.Load("icon-torus");
	private GUIStyle buttonStyle;
	private GUIStyle oldButtonStyle;

	[MenuItem("Window/Advanced Primitives")]
	public static void ShowWindow()
	{
		EditorWindow window = EditorWindow.GetWindow(typeof(AdvPrimitiveToolBox));
		window.title = "Primitives";
		window.minSize = new Vector2(470, 52);
		window.maxSize = new Vector2(472, 52);
	}

	private void OnGUI()
	{
		if (buttonStyle == null)
		{
			oldButtonStyle = GUI.skin.button;

			buttonStyle = new GUIStyle(GUI.skin.GetStyle("Button"));
			buttonStyle.contentOffset = new Vector2(0, 0);
			buttonStyle.padding = new RectOffset(0, 0, 0, 0);
			buttonStyle.overflow = new RectOffset(0, 0, 0, 0);
			buttonStyle.fixedWidth = 48;
			buttonStyle.fixedHeight = 48;
		}

		GUI.skin.button = buttonStyle;

		GUILayout.BeginHorizontal();
		if (GUILayout.Button(new GUIContent(iconSphere,   "Sphere")))   CreatePrimitiveMacros.CreateSphere();
		if (GUILayout.Button(new GUIContent(iconCube,     "Cube")))     CreatePrimitiveMacros.CreateCube();
		if (GUILayout.Button(new GUIContent(iconSlope,    "Slope")))    CreatePrimitiveMacros.CreateSlope();
		if (GUILayout.Button(new GUIContent(iconPyramid,  "Pyramid")))  CreatePrimitiveMacros.CreatePyramid();
		if (GUILayout.Button(new GUIContent(iconPlane,    "Plane")))    CreatePrimitiveMacros.CreatePlane();
		if (GUILayout.Button(new GUIContent(iconDisk,     "Disk")))     CreatePrimitiveMacros.CreateDisk();
		if (GUILayout.Button(new GUIContent(iconCylinder, "Cylinder"))) CreatePrimitiveMacros.CreateCylinder();
		if (GUILayout.Button(new GUIContent(iconCapsule,  "Capsule")))  CreatePrimitiveMacros.CreateCapsule();
		if (GUILayout.Button(new GUIContent(iconTorus,    "Torus")))    CreatePrimitiveMacros.CreateTorus();
		GUILayout.EndHorizontal();
		
		GUI.skin.button = oldButtonStyle;
	}
}
