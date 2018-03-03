using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScalableObject : MonoBehaviour {
	private Light[] lights;
	void Start () { }

	void Update () {
		if (transform.hasChanged) {
			lights = this.GetComponentsInChildren<Light> (true);
			Vector3 scale = transform.localScale;
			foreach (Light l in lights) {
				l.range = 5 * scale.x;
			}

			//print ("The transform has changed!");
			transform.hasChanged = false;
		}
	}
}