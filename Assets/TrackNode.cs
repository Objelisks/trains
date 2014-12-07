using UnityEngine;
using System.Collections;

public class TrackNode : MonoBehaviour {

  public TrackNode[] connected = new TrackNode[] {};
  Vector3[] path = {};

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {
    
  }

  public TrackNode GetNext(float speed) {
    return this;
  }

  public TrackNode GetPrevious(float speed) {
    return this;
  }

  public Vector3 GetPositionAlong(float percent) {
    Vector3 position = transform.position;
    return position;
  }
}
