using System;
using UnityEngine;
using Vuforia;

public class DeployStageOnce : MonoBehaviour {

	public GameObject AnchorStage;
	private PositionalDeviceTracker _deviceTracker;
	private GameObject _previousAnchor;

	public void Start() {
		if (AnchorStage == null) {
			Debug.Log("AnchorStage must be specified");
			return;
		}

		Debug.Log("HIDING ANCHOR");

		// AnchorStage.SetActive(false);
	}

	public void Awake() {
		Debug.Log("Awake");
		VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
	}

	public void OnDestroy() {
		Debug.Log("OnDestroy");
		VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);
	}

	private void OnVuforiaStarted() {
		Debug.Log("OnVuforiaStarted");
		_deviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
	}

	public void OnInteractiveHitTest(HitTestResult result) {
		Debug.Log("OnInteractiveHitTest");
		if (result == null || AnchorStage == null) {
			Debug.LogWarning("Hit test is invalid or AnchorStage not set");
			return;
		}

		var anchor = _deviceTracker.CreatePlaneAnchor(Guid.NewGuid().ToString(), result);

		Debug.Log(anchor);

		if (anchor != null) {
			Debug.Log("setting values");
			AnchorStage.transform.parent = anchor.transform;
			AnchorStage.transform.localPosition = Vector3.zero;
			AnchorStage.transform.localRotation = Quaternion.identity;
			AnchorStage.SetActive(true);
		}

		if (_previousAnchor != null) {
			Destroy(_previousAnchor);
		}

		_previousAnchor = anchor;
	}
}