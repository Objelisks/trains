﻿using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TrackNode))]
public class TrackNodeEditor : Editor {

  public void OnSceneGUI() {
    TrackNode thing = (TrackNode)target;
    Handles.color = Color.white;
    foreach(TrackNode node in thing.connected) {
      Vector3 p1 = thing.transform.position;
      Vector3 p2 = node.transform.position;
      Handles.DrawLine(p1, p2);
    }
  }
}
