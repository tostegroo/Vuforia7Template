using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EOTR.Store {

  [RequireComponent(typeof(Text))]
  public class ChapterRenderer : MonoBehaviour {
    void OnEnable() {
      var text = this.GetComponent<Text>();

      Unidux.Subject
        .TakeUntilDisable(this)
        .StartWith(Unidux.State)
        .Subscribe(state => text.text = GameData.Chapters[state.Game.Chapter])
        .AddTo(this);
    }
  }
}
