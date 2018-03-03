using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EOTR.Store {

  [RequireComponent(typeof(Button))]
  public class ChapterDispatcher : MonoBehaviour {

    // -1 or 1
    public int direction;

    void Start() {
      this.GetComponent<Button>()
        .OnClickAsObservable()
        .Subscribe(state => Unidux.Store.Dispatch(Game.ActionCreator.ChangeChapter(direction)))
        .AddTo(this);
    }
  }
}
