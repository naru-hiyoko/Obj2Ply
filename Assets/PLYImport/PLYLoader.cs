using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLYLoader {

	/**
	<summary>
		<params> 
			<modelFile : path of *.obj />
			<colour : whether export rgb? />
			<alpha : whether export alpha? />
			<colorScale : check the *.ply format. (http://paulbourke.net/dataformats/ply/) />
		</params>
	</summary>
	**/

	public static void ExportObj2Ply(string modelFile, bool colour = true, bool alpha = false, float colorScale = 255.0f)
	{
		string model = "";
		model += "ply\n";
		model += "format ascii 1.0\n";

		var msg = String.Format("Loading Rig File \"{0}\" ", modelFile);
		Debug.Log(msg);

		var obj = OBJLoader.LoadOBJFile(modelFile);

		/**
			3D models scaned with 'Structure Sensor (iOS)' works fine.
		**/
		var child = obj.transform.GetChild (0);
		var meshFilter = child.GetComponent<MeshFilter> ();
		var meshRenderer = child.GetComponent<MeshRenderer> ();
		var mesh = meshFilter.mesh;
		var mat = meshRenderer.material;
		var tex = mat.mainTexture as Texture2D;
		var tex_width = (float) tex.width;
		var tex_height = (float) tex.height;

		Debug.Log(String.Format("texture size : {0} x {1}", tex_width, tex_height));

		model += String.Format("element vertex {0}\n", mesh.vertexCount);
		model += "comment vertices\n";
		model += "property float x\n";
		model += "property float y\n";
		model += "property float z\n";
		if (colour) {
			model += "property uchar red\n";
			model += "property uchar green\n";
			model += "property uchar blue\n";
			if (alpha)
				model += "property float alpha\n";
		}
		model += "end_header\n";

		for (int i = 0; i < mesh.vertexCount; i++)
		{
			Vector3 v = mesh.vertices [i];
			Vector2 u = mesh.uv [i];
			int x = (int) (tex_width * u.x);
			int y = (int) (tex_height * u.y);
			Color c = tex.GetPixel(x, y);

			float scale = colorScale;

			if (colour) 
			{
				if (alpha) {
					model += String.Format("{0} {1} {2} {3} {4} {5} {6}\n", v.x, v.y, v.z, c.r * scale, c.g * scale , c.b * scale, c.a);
				} else {
					model += String.Format("{0} {1} {2} {3} {4} {5}\n", v.x, v.y, v.z, c.r * scale, c.g * scale, c.b * scale);
				}

			} else {
				model += String.Format ("{0} {1} {2}\n", v.x, v.y, v.z);
			}
		}
			
		var savedModelPath = Path.ChangeExtension (modelFile, ".ply");
		File.WriteAllText (savedModelPath, model, encoding: Encoding.ASCII);
		Debug.Log (String.Format ("Wrote : {0}", savedModelPath));

		//GameObject.Destroy (obj);

	}

	private static GameObject AddPclObject(List<Vector3> vertices, List<Color> colors)
	{
		//var obj = GameObject.CreatePrimitive(PrimitiveType.
		var obj = new GameObject("Model");

		var mesh = new Mesh ();
		mesh.name = "NoName";
		mesh.SetVertices (vertices);
		mesh.SetColors (colors);
		Debug.Log (mesh.colors.Length);

		MeshFilter meshFilter = obj.AddComponent<MeshFilter> ();
		meshFilter.mesh = mesh;

		MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer> ();
		var shader = new Shader ();

		var mat = new Material(shader);
		mat.name = "material0";
		meshRenderer.material = mat;

		return obj;
	}

}
