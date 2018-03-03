using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EOTR.Prototype.Calibration {

  public class GUIPosition : MonoBehaviour {

    public string guiLabel = "label";
    private string label = "";
    [Range(0.0f, 1000.0f)]
    public float guiX = 0.0f;
    [Range(0.0f, 1000.0f)]
    public float guiY = 0.0f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
      label = guiLabel;

      var position = "position: x: " + transform.position.x + " y:" + transform.position.y + " z:" + transform.position.z;
      var rotation = "rotation: x: " + transform.rotation.x + " y:" + transform.rotation.y + " z:" + transform.rotation.z;

      label += "\n" + position;
      label += "\n" + rotation;
    }

    void OnGUI() {

      var mButtonRect = new Rect(guiX, guiY, 300, 250); 

      if (GUI.Button(mButtonRect, label)) {
        // do something on button click
      }
    }
  }
}
