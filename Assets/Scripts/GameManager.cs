using System.Collections;
using UnityEngine;
using Vuforia;

public class GameManager : MonoBehaviour {

  public static GameManager instance = null;

  void Awake() {

    if (instance == null) {
      instance = this;
    } else if (instance != this) {
      Destroy(gameObject);
    }

    DontDestroyOnLoad(transform.gameObject);

    var arCamera = GameObject.Find("ARCamera");

    // This is how we activate vuforia manually
    arCamera.GetComponent<VuforiaBehaviour>().enabled = true;
    arCamera.GetComponent<DefaultInitializationErrorHandler>().enabled = true;

    // Could dispatch an action to start things off
  }
}
