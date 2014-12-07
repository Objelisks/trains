using UnityEngine;
using System.Collections;

public class TrackNode : MonoBehaviour {

  public Transform start;
  public Transform finish;
  public Transform startControl;
  public Transform finishControl;
  public TrackNode[] connected;
  Vector3[] segments;

  Arc arc;

  // Use this for initialization
  void Start () {
    arc = new Arc(start, startControl, finishControl, finish);
  }

  // Update is called once per frame
  void Update () {
    
  }

  public TrackNode GetNext(float speed) {
    return connected[0];
  }

  public TrackNode GetPrevious(float speed) {
    return connected[0];
  }

  public Vector3 GetPositionAlong(float percent) {
    return new Vector3();
  }
}
