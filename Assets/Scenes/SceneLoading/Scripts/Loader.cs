using AssetBundles;
using EOTR.Store;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

namespace EOTR.Store {

  // Define class for props
  class LoaderProps {
    public int chapter;
    public string state;
    public string scene;

    public void Set(int chapter, string state, string scene) {
      this.chapter = chapter;
      this.state = state;
      this.scene = scene;
    }

    // Clean way to check if the props have changed
    public bool Equals(LoaderProps newProps) {
      return this.chapter == newProps.chapter && this.state == newProps.state && this.scene == newProps.scene;
    }
  }

  public class Loader : MonoBehaviour {

    // Initialise props
    private LoaderProps props = new LoaderProps();

    // Use this for initialization
    IEnumerator Start() {
      // Setup the AssetBundleManager
      yield return StartCoroutine(Initialize());

      // Subscribe to the game state
      Unidux.Subject
        .TakeUntilDisable(this)
        .StartWith(Unidux.State)
        .Subscribe(state => {
          var newProps = new LoaderProps();
          newProps.Set(state.Game.Chapter, state.Game.State, state.Game.Scene);
          this.onStateChanged(newProps);
        })
        .AddTo(this);
    }

    IEnumerator Initialize() {
      // Initialize the downloading url and AssetBundleManifest object.
      // With this code, when in-editor or using a development builds: Always use the AssetBundle Server
      // (This is very dependent on the production workflow of the project.
      // 	Another approach would be to make this configurable in the standalone player.)
#if DEVELOPMENT_BUILD || UNITY_EDITOR
      AssetBundleManager.SetDevelopmentAssetBundleServer();
#else
      var bundlePath = Application.dataPath + "/";

      // Use the following code if AssetBundles are embedded in the project for example via StreamingAssets folder etc:
      AssetBundleManager.SetSourceAssetBundleURL(bundlePath);
      // Or customize the URL based on your deployment or configuration
      //AssetBundleManager.SetSourceAssetBundleURL("http://www.MyWebsite/MyAssetBundles");
#endif
      // Initialize AssetBundleManifest which loads the AssetBundleManifest object.
      var request = AssetBundleManager.Initialize();

      if (request != null)
        yield return StartCoroutine(request);
    }

    void onStateChanged(LoaderProps newProps) {
      // Only update from the state if the props we need change
      if (!newProps.Equals(this.props)) {
        this.props.Set(newProps.chapter, newProps.state, newProps.scene);
        this.stateChanged();
      }
    }

    void stateChanged() {
      Debug.Log("----------------------------");
      var chapter = this.props.chapter;
      var state = this.props.state;
      var scene = this.props.scene;
      Debug.Log("onStateChanged: " + chapter + " State: " + state + " Scene: " + scene);

      switch (state) {
        case StateConstants.Loading:
          switch (scene) {
            case "Preload":
              // If the current active scene is Preload, we load the next chapter
              StartCoroutine(LoadChapterSceneAsync(chapter));
              break;
            default:
              StartCoroutine(LoadPreloadSceneAsync());
              break;
          }
          break;
        default:
          break;
      }
    }

    IEnumerator LoadPreloadSceneAsync() {
      var scene = "Assets/Scenes/SceneLoading/Preload.unity";

      // Load preload scene in the background
      AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

      // Wait until the last operation fully loads to return anything
      while (!asyncLoad.isDone) {
        yield return null;
        // Notify store when scene has loaded
        Unidux.Store.Dispatch(Game.ActionCreator.SceneLoaded("Preload"));
      }
    }

    IEnumerator LoadChapterSceneAsync(int chapter) {
      // This is simply to get the elapsed time for this phase of AssetLoading
      float startTime = Time.realtimeSinceStartup;

      // Load level from assetBundle
      var sceneAssetBundle = "chapter" + chapter;
      var levelName = "Chapter" + chapter;

      // Simulate the preload for a minimim of x seconds
      yield return new WaitForSeconds(3);

      AssetBundleLoadOperation request = AssetBundleManager.LoadLevelAsync(sceneAssetBundle, levelName, false);
      if (request == null)
        yield break;
      yield return StartCoroutine(request);

      // Calculate and display the elapsed time.
      float elapsedTime = Time.realtimeSinceStartup - startTime;
      Debug.Log("Finished loading Chapter scene " + levelName + " in: " + elapsedTime + " seconds");

      // Notify store when scene has loaded
      Unidux.Store.Dispatch(Game.ActionCreator.SceneLoaded(levelName));
    }
  }
}
