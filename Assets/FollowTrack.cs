using UnityEngine;

public enum TrainDirection {
  Forward,
  Backward
}

public class FollowTrack : MonoBehaviour {

  public TrackNode currentNode;
  public float speed;
  float nodePos = 0.5f;
  TrainDirection direction = TrainDirection.Forward;

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {
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
    transform.position = currentNode.GetPositionAlong(nodePos);
  }
}
