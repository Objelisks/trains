﻿using UnityEngine;
using System.Collections;

public enum TrainDirection {
  Forward,
  Backward
}

public class FollowTrack : MonoBehaviour {

  public TrackNode currentNode = null;
  public float speed = 0.0f;
  float nodePos = 0.5f;
  TrainDirection direction = TrainDirection.Forward;

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {
    Debug.Log("update");
    if(currentNode == null) return;

    nodePos += speed;
    if(nodePos > 1.0f) {
      currentNode = currentNode.GetNext(speed);
      nodePos -= 1.0f;
    }
    if(nodePos < 0.0f) {
      currentNode = currentNode.GetPrevious(speed);
      nodePos += 1.0f;
    }
    Debug.Log("moove");
    transform.position = currentNode.GetPositionAlong(nodePos);
  }
}