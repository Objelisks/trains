using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Arc))]
class ArcEditor : Editor {

  void DrawAndUpdateVector(ref Vector3 vector, Transform transform, string label) {
    var offsetVector = transform.TransformPoint(vector);
    vector = transform.InverseTransformPoint(Handles.PositionHandle(offsetVector, Quaternion.identity));
    Handles.Label(offsetVector, label);
  }

  public void OnSceneGUI() {
    var thing = (Arc)target;
    
    DrawAndUpdateVector(ref thing.start, thing.transform, "start");
    DrawAndUpdateVector(ref thing.finish, thing.transform, "finish");
    DrawAndUpdateVector(ref thing.startControl, thing.transform, "startControl");
    DrawAndUpdateVector(ref thing.finishControl, thing.transform, "finishControl");
    Handles.DrawLine(thing.start, thing.startControl);
    Handles.DrawLine(thing.finish, thing.finishControl);

    Vector3 last = thing.start;
    const int divisions = 10;
    for (var i = 0; i < divisions; i++) {
      Handles.DrawLine(last, thing.GetPointCubic(i/divisions));
    }
    
  }
}
