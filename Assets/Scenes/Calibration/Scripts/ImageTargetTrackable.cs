using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

namespace EOTR.Prototype.Calibration {

  public class ImageTargetTrackable : MonoBehaviour, ITrackableEventHandler {

    protected TrackableBehaviour mTrackableBehaviour;
    public GameObject anchoreState; //it is object Mid Air Stage or Ground Plane Stage
    //public UnityEngine.UI.Image imageIndicator; //it is my indicator for detection image target

    protected virtual void Start() {
      anchoreState.SetActive(false);
      mTrackableBehaviour = GetComponent<TrackableBehaviour>();
      if (mTrackableBehaviour)
        mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(
      TrackableBehaviour.Status previousStatus,
      TrackableBehaviour.Status newStatus) {
      if (newStatus == TrackableBehaviour.Status.DETECTED ||
        newStatus == TrackableBehaviour.Status.TRACKED ||
        newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        OnTrackingFound();
      } else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
        newStatus == TrackableBehaviour.Status.NOT_FOUND) {
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        OnTrackingLost();
      } else {
        OnTrackingLost();
      }
    }

    protected virtual void OnTrackingFound() {
      anchoreState.transform.position = this.transform.position;
      anchoreState.transform.localPosition = Vector3.zero;
      anchoreState.transform.rotation = Quaternion.identity;
      anchoreState.SetActive(true);

      Debug.Log("------");
      Debug.Log("position");
      Debug.Log(anchoreState.transform.position.x + " " + anchoreState.transform.position.y + " " + anchoreState.transform.position.z);

      //imageIndicator.color = Color.green;
    }

    protected virtual void OnTrackingLost() {
      //imageIndicator.color = Color.red;
    }

    public void Reset() {
      anchoreState.SetActive(false);
    }
  }
}
