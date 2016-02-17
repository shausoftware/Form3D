using UnityEngine;
using System.Collections;

public class HelpPalette : MonoBehaviour {

	public void displayHelp() {

		// Make a background box
		GUI.Box(new Rect(10, 10, 800, 650), "Form3D Help - BETA v1.2.1");

		string helpText = "Form3D generates and mutates 3 dimensional geometries using simple mathematical relationships. "
						+ "The program is loosely based on ‘Form’, an old DOS program written by Andrew Rowbottom and runs on the Unity engine. ";
		GUI.TextArea (new Rect (20, 40, 780, 40), helpText);

		helpText = "There are 9 configuration slots that can be used to mutate the geometry into a Form. "
						+ "Pressing keys 1 - 9 or selecting slots in Form Builder mutates the current geometries towards the Form. "
						+ "The configurations can be accessed and edited via the Form Builder Palette (see below).\n" 
						+ "F - toggles form builder palette on and off.\n"
						+ "Try creating forms and altering the parameters to see their affect. Please be patient when creating new Forms as "
						+ "the camera can take a while to move its initial position. "
				+ "The Load and Save buttons save the current configuration slot settings to and from the local file system. See the Unity documents for location of 'Application.persistentDataPath'.";
		GUI.TextArea (new Rect (20, 85, 780, 115), helpText);

		helpText = "Now to add some colour.\n"
						+ "T - toggles the Colour Palette on and off.\n"
						+ "Try changing the parameters here to their affect on the Form and the background.\n"
						+ "Note engaging Colour Cycling overrides the base palette colour.";
		GUI.TextArea (new Rect (20, 205, 780, 85), helpText);

		helpText = "H - toggles the Help screen on and off. You're here now :)\n"
					+ "Left Cursor - left rotates the camera around the scene centre.\n"
					+ "Right Cursor - right rotates the camera around the scene centre.\n"
					+ "Up Cursor - up rotates the camera around the scene centre (x axis).\n"
					+ "Down Cursor - down rotates the camera around the scene centre (x axis).\n"
					+ "Q - up rotates the camera around the scene centre (z axis).\n"
					+ "W - down rotates the  camera around the scene centre (z axis).\n"
					+ ", - roll the camera left.\n"
					+ ". - roll the camera right.\n"
					+ "Z - camera zoom in.\n"
					+ "X - camera zoom out.\n"
					+ "A - auto orbit (changes every 30 seconds or so).\n"
					+ "1 to 9 - mutate once to Form configuration.\n"
					+ "V - toggle scaling of branch stacks :).\n"
					+ "R - mutate once with symmetry in the current configuration slot.\n"
					+ "T - mutate once without symmetry in the current configuration slot.\n"
					+ "J - auto change between configurations every 10 seconds or so.\n"
					+ "K - auto mutate (symmetrical) every 10 seconds or so in the current configuration slot.\n"
					+ "L - auto mutate (non-symmetrical) every 10 seconds or so in the current configuration slot.\n"
					+ "N - new Form using the current configuration slot.\n"
					+ "B - clear the current Form.\n"
					+ "S - save the current Form to the scene. Note control of Form is lost as new Form receives focus.\n"
					+ "C - clear all of scene.";
		GUI.TextArea (new Rect (20, 295, 780, 355), helpText);

	}

}
