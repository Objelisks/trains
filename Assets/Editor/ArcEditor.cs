using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Arc))]
class ArcEditor : Editor {

  void DrawAndUpdateVector(ref Vector3 vector, string label) {
    vector = Handles.PositionHandle(vector, Quaternion.identity);
    Handles.Label(vector, label);
  }

  public void OnSceneGUI() {
    var thing = (Arc)target;

    Handles.matrix = thing.transform.localToWorldMatrix;
    
    DrawAndUpdateVector(ref thing.start, "start");
    DrawAndUpdateVector(ref thing.finish, "finish");
    DrawAndUpdateVector(ref thing.startControl, "startControl");
    DrawAndUpdateVector(ref thing.finishControl, "finishControl");
    Handles.DrawLine(thing.start, thing.startControl);
    Handles.DrawLine(thing.finish, thing.finishControl);

    Vector3 last = thing.start;
    const int divisions = 10;
    for (var i = 0; i <= divisions; i++) {
      var current = thing.GetPointCubic((float)i/divisions);
      Handles.DrawLine(last, current);
      last = current;
    }
    
  }
}
