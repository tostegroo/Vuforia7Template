using System;
using Unidux;

namespace EOTR.Store {

  public class StateConstants {
    public const string Loading = "state/Loading";
    public const string Loaded = "state/Loaded";
  }

  [Serializable]
  public class GameState : StateElement {
    public int Chapter = 0;
    public string State = StateConstants.Loaded;
    public string Scene = "";
  }
}
