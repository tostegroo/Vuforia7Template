using System.Collections;
using UnityEngine; 
using Vuforia;

namespace EOTR.Prototype.Calibration {
  public class DetectImage : MonoBehaviour, ITrackableEventHandler { 
    private TrackableBehaviour mTrackableBehaviour; 
    private string label = "tracking...";
    private Rect mButtonRect = new Rect(50, 50, 300, 250); 

    public GameObject plane;

    void Start() {
      mTrackableBehaviour = GetComponent<TrackableBehaviour>();
      if (mTrackableBehaviour) {
        mTrackableBehaviour.RegisterTrackableEventHandler(this);
      }
    } 
    public void OnTrackableStateChanged(
      TrackableBehaviour.Status previousStatus,
      TrackableBehaviour.Status newStatus) {
      if (newStatus == TrackableBehaviour.Status.DETECTED ||
        newStatus == TrackableBehaviour.Status.TRACKED ||
        newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
        label = "found image";

        var position = "position: x: " + transform.position.x + " y:" + transform.position.y + " z:" + transform.position.z;
        var rotation = "rotation: x: " + transform.rotation.x + " y:" + transform.rotation.y + " z:" + transform.rotation.z;

        label += "\n" + position;
        label += "\n" + rotation;

        plane.transform.position = transform.position;
        plane.transform.rotation = Quaternion.identity;

      } else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
        newStatus == TrackableBehaviour.Status.NOT_FOUND) {
        // label = "lost image";
      } else {
        // label = "tracking again ...";
      }
    } 
    void OnGUI() {
      if (GUI.Button(mButtonRect, label)) { }
    }
  }

}
