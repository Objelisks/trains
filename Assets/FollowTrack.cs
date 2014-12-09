using UnityEngine;

public class FollowTrack : MonoBehaviour {

  public Arc track;
  public float speed;
  bool orientationSwitched;
  public float trackPos;

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {
    if(track == null) return;

    trackPos += speed * (orientationSwitched ? 1.0f : -1.0f);
    Vector3 newPos = Arc.MoveAlongTrack(ref track, ref trackPos, ref orientationSwitched, true);
    transform.position = track.transform.TransformPoint(newPos);
    Debug.Log(transform.position);
  }
}
