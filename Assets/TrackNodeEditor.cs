using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TrackNode))]
public class TrackNodeEditor : Editor {

  public void OnSceneGui() {
    TrackNode thing = (TrackNode)target;
    Handles.color = Color.white;
    Debug.Log("node 1");
    foreach(TrackNode node in thing.forward) {
      Debug.Log("node 2");
      Vector3 p1 = thing.transform.position;
      Vector3 p2 = node.transform.position;
      Handles.DrawLine(p1, p2);
      Handles.ArrowCap(0, p2, Quaternion.identity, 1);
    }
  }
}
