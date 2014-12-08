using UnityEngine;
using System.Collections.Generic;

public class Arc : MonoBehaviour {

  public Vector3 start = new Vector3(0, 0, 0);
  public Vector3 finish = new Vector3(1, 0, 0);
  public Vector3 startControl = new Vector3(0, 0, 0);
  public Vector3 finishControl = new Vector3(1, 0, 0 );

  List<float> segmentLengths;
  const int segmentCount = 100;

  public void Start() {
    GenerateSegments();
  }

  public void Update() {

  }

  // Get the sum length so far for each segment
  // The last number will be the total length of the arc
  public void GenerateSegments() {
    segmentLengths = new List<float>();
    var last = GetPointCubic(0);
    segmentLengths.Add(0);
    for(var i = 1; i < segmentCount; i++) {
      var current = GetPointCubic(i / segmentCount);
      segmentLengths.Add(segmentLengths[i-1] + Vector3.Distance(last, current));
      last = current;
    }
  }

  // Get point uniformly along arclength
  public Vector3 GetPointAt(float t) {
    var targetLength = t * segmentLengths[segmentLengths.Count-1];
    var index = segmentLengths.BinarySearch(targetLength);
    if(index < 0) {
      index = (~index)-1;
    }
    var fractional = targetLength - segmentLengths[index];
    return Vector3.Lerp(GetPointCubic(index/segmentCount), GetPointCubic((index+1)/segmentCount), fractional);
  }

  public Vector3 GetPointCubic(float t) {
    Vector3 p1 = Vector3.Lerp(start, startControl, t);
    Vector3 p2 = Vector3.Lerp(startControl, finishControl, t);
    Vector3 p3 = Vector3.Lerp(finishControl, finish, t);
    Vector3 p4 = Vector3.Lerp(p1, p2, t);
    Vector3 p5 = Vector3.Lerp(p2, p3, t);
    return Vector3.Lerp(p4, p5, t);
  }

}
