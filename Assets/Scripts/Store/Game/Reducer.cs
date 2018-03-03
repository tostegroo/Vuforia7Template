using Unidux;
using UnityEngine;

namespace EOTR.Store {

  public static class Game {
    // specify the possible types of actions
    public enum ActionType {
      ChangeChapter,
      SceneLoaded
    }

    // actions must have a type and may include a payload
    public class Action {
      public ActionType ActionType;
      public int Direction;
      public string Scene;
    }

    // ActionCreators creates actions and deliver payloads
    // in redux, you do not dispatch from the ActionCreator to allow for easy testability
    public static class ActionCreator {
      public static Action Create(ActionType type) {
        return new Action() { ActionType = type };
      }

      public static Action ChangeChapter(int direction) {
        return new Action() {
          ActionType = ActionType.ChangeChapter,
            Direction = direction
        };
      }

      public static Action SceneLoaded(string scene) {
        return new Action() {
          ActionType = ActionType.SceneLoaded,
            Scene = scene
        };
      }
    }

    // reducers handle state changes
    public class Reducer : ReducerBase<State, Action> {
      public override State Reduce(State state, Action action) {
        switch (action.ActionType) {
          case ActionType.ChangeChapter:
            var indexChapter = state.Game.Chapter + action.Direction;
            indexChapter = Mathf.Clamp(indexChapter, 0, GameData.Chapters.Length - 1);

            // Set new chapter index
            state.Game.Chapter = indexChapter;

            // Set state to loading
            state.Game.State = StateConstants.Loading;
            break;
          case ActionType.SceneLoaded:
            state.Game.Scene = action.Scene;

            if (action.Scene != "Preload") {
              state.Game.State = StateConstants.Loaded;
            }

            break;
        }
        return state;
      }
    }
  }
}
