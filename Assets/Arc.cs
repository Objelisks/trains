using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Arc : MonoBehaviour {

  [System.Serializable]
  public class Exit {
    public Arc target;
    public bool isStartEnd;
    public bool switchOrientation;
  }

  public Vector3 start = new Vector3(0, 0, 0);
  public Vector3 finish = new Vector3(1, 0, 0);
  public Vector3 startControl = new Vector3(0, 0, 0);
  public Vector3 finishControl = new Vector3(1, 0, 0 );
  public Exit[] startExits;
  public Exit[] finishExits;
  public float totalLength;

  List<float> segmentLengths;
  const int segmentCount = 100;

  public void Start() {
    GenerateSegments();
    UpdateExits(startExits, true);
    UpdateExits(finishExits, false);
  }

  public void Update() {

  }

  // Get the sum length so far for each segment
  // The last number will be the total length of the arc
  private void GenerateSegments() {
    segmentLengths = new List<float>();
    var last = GetPointCubic(0);
    segmentLengths.Add(0);
    for(var i = 1; i < segmentCount; i++) {
      var current = GetPointCubic((float)i / segmentCount);
      segmentLengths.Add(segmentLengths[i-1] + Vector3.Distance(last, current));
      last = current;
    }
    totalLength = segmentLengths[segmentLengths.Count-1];
  }

  private void UpdateExits(Exit[] exits, bool isStart) {
    foreach(Exit exit in exits) {
      exit.switchOrientation = isStart ? exit.isStartEnd : !exit.isStartEnd;
    }
  }

  private Exit chooseExit(Exit[] exits, bool goLeft) {
    if(exits.Length > 1) {
      return goLeft ? exits[0] : exits[1];
    }
    return exits.Length > 0 ? exits[0] : null;
  }

  public Exit GetExitIfOffPath(float trackPos, bool goLeft) {
    Exit exit = null;
    if(trackPos > totalLength) {
      exit = chooseExit(finishExits, goLeft);
    } else if(trackPos < 0.0f) {
      exit = chooseExit(startExits, goLeft);
    }
    return exit;
  }

  // Get point uniformly along curve
  // targetLength: distance along curve (positive or negative)
  // return: vector on curve
  public Vector3 GetPositionAlongCurve(float targetLength) {
    var index = segmentLengths.BinarySearch(targetLength);
    if(index < 0) {
      index = (~index) - 1;
    }
    float fractional = index == -1 ? 0.0f : (targetLength - segmentLengths[index]) / (segmentLengths[index+1] - segmentLengths[index]);
    Vector3 pos = Vector3.Lerp(GetPointCubic((float)index/segmentCount), GetPointCubic((float)(index+1)/segmentCount), fractional);
    Debug.Log(fractional + " " + pos);
    return pos;
  }

  // t: percent along curve
  // return: vector on curve at t
  public Vector3 GetPointCubic(float t) {
    Vector3 p1 = Vector3.Lerp(start, startControl, t);
    Vector3 p2 = Vector3.Lerp(startControl, finishControl, t);
    Vector3 p3 = Vector3.Lerp(finishControl, finish, t);
    Vector3 p4 = Vector3.Lerp(p1, p2, t);
    Vector3 p5 = Vector3.Lerp(p2, p3, t);
    return Vector3.Lerp(p4, p5, t);
  }

  // gets the position along the path, traversing exits if needed and updating the track if
  // exits are used
  public static Vector3 MoveAlongTrack(ref Arc track, ref float trackPos, ref bool orientationSwitched, bool goLeft) {
    //Vector3 newPos = track.GetPositionAlongPath(trackPos, speed);
    //trackPos = track.GetPercentAlongPath(newPos);

    var targetPos = trackPos;

    Exit exit = track.GetExitIfOffPath(targetPos, goLeft);
    if(exit != null) {
      var oldTrackLength = track.totalLength;
      track = exit.target;
      // trackPos is >trackLength or <0
      // remainder is the absolute distance we're travelling on the new path
      float remainder = trackPos > oldTrackLength ? trackPos-oldTrackLength : -trackPos;
      if(exit.isStartEnd) {
        trackPos = remainder;
      } else {
        trackPos = track.totalLength - remainder;
      }
      // track direction switches if the tracks were connected start/start or finish/finish
      orientationSwitched = exit.switchOrientation ? !orientationSwitched : orientationSwitched;
    }

    // currently on final path with correct trackPos
    Vector3 finalPos = track.GetPositionAlongCurve(trackPos);
    return finalPos;
  }

}
