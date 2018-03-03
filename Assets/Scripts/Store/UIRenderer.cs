using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EOTR.Store {

  [RequireComponent(typeof(Text))]
  public class UIRenderer : MonoBehaviour {
    void OnEnable() {
      var text = this.GetComponent<Text>();

      Unidux.Subject
        .TakeUntilDisable(this)
        .StartWith(Unidux.State)
        .Subscribe(state => text.text = "sound active" + state.UI.SoundActive.ToString())
        .AddTo(this);
    }
  }
}
