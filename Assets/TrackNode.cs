using UnityEngine;

public class TrackNode : MonoBehaviour {

  public TrackNode[] connected;

  Arc arc;
  public Arc Arc { get; set; }

  // Use this for initialization
  void Start () {
    arc = (Arc)GetComponent("Arc");
  }

  // Update is called once per frame
  void Update () {
    
  }

  public TrackNode GetNext(float speed) {
    return connected[0];
  }

  public TrackNode GetPrevious(float speed) {
    return connected[1];
  }

  public Vector3 GetPositionAlong(float percent) {
    return arc.GetPointAt(percent);
  }

}
