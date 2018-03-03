using Unidux;
using UnityEngine;

namespace EOTR.Store {

  public static class UI {
    // specify the possible types of actions
    public enum ActionType {
      SoundToggle
    }

    // actions must have a type and may include a payload
    public class Action {
      public ActionType ActionType;
    }

    // ActionCreators creates actions and deliver payloads
    // in redux, you do not dispatch from the ActionCreator to allow for easy testability
    public static class ActionCreator {
      public static Action Create(ActionType type) {
        return new Action() { ActionType = type };
      }

      public static Action Toggle() {
        return new Action() { ActionType = ActionType.SoundToggle };
      }
    }

    // reducers handle state changes
    public class Reducer : ReducerBase<State, Action> {
      public override State Reduce(State state, Action action) {
        switch (action.ActionType) {
          case ActionType.SoundToggle:
            Debug.Log("sound is:" + state.UI.SoundActive);

            state.UI.SoundActive = !state.UI.SoundActive;
            break;
        }

        return state;
      }
    }
  }
}
