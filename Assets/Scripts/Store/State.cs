using System;
using Unidux;

namespace EOTR.Store {

  [Serializable]
  public class State : StateBase {
    public GameState Game = new GameState();
    public UIState UI = new UIState();
  }
}
