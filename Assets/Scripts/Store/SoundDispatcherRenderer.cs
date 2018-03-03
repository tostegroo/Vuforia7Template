using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EOTR.Store {

  public class SoundDispatcherRenderer : MonoBehaviour {

    void Start() {
      this.GetComponent<Button>()
        .OnClickAsObservable()
        .Subscribe(state => Unidux.Store.Dispatch(UI.ActionCreator.Toggle()))
        .AddTo(this);
    }

    void OnEnable() {
      var text = GetComponentInChildren<Text>();

      Unidux.Subject
        .TakeUntilDisable(this)
        .StartWith(Unidux.State)
        .Subscribe(state => text.text = "Sound is: " + state.UI.SoundActive)
        .AddTo(this);
    }
  }
}
