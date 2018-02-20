using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainApp : MonoBehaviour {

	//public string objFile = "None";

	// Use this for initialization
	void Start () {
		Sample ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup > 3.0) {
			//Sample ();
		}
	}

	void Sample()
	{
		var args = Environment.GetCommandLineArgs();

		if (args.Length != 2) {
			Debug.LogError ("Specify *.obj file!");
			//Application.Quit ();
		}

		string objFile = args[1];
		//string objFile = "/Volumes/MacintoshHD3/3DObjects/DrinkBottle/Model.obj";
		// MARK : Check the included Shaders!!!
		//var obj = OBJLoader.LoadOBJFile (objFile);
		PLYLoader.ExportObj2Ply (objFile);
		Application.Quit ();
	}
}
