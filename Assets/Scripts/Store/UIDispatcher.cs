using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EOTR.Store {

  [RequireComponent(typeof(Button))]
  public class UIDispatcher : MonoBehaviour {
    public UI.Action Action = UI.ActionCreator.Toggle();

    void Start() {
      this.GetComponent<Button>()
        .OnClickAsObservable()
        .Subscribe(state => Unidux.Store.Dispatch(Action))
        .AddTo(this);
    }
  }
}
