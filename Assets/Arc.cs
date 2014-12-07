using UnityEngine;
using System.Collections;

public class Arc {

  public Transform start;
  public Transform finish;
  public Transform control1;
  public Transform control2;

  List<float> segmentLengths;
  int segmentCount = 100;

  public Arc(Transform start, Transform control1, Transform control2, Transform finish) {
    this.start = start;
    this.control1 = control1;
    this.control2 = control2;
    this.finish = finish;

    // generate segments
    GenerateSegments()
  }

  // Get the sum length so far for each segment
  // The last number will be the total length of the arc
  public void GenerateSegments() {
    segmentLengths = new List<float>();
    var last = GetPointCubic(0);
    segmentLengths.Add(0);
    for(var i = 1; i < segmentCount; i++) {
      var current = GetPointCubic(i / segmentCount);
      segmentLengths.Add(segmentLengths[i-1] + last.Distance(current));
      last = current;
    }
  }

  // Get point uniformly along arclength
  public Vector3 GetPointAt(t) {
    var targetLength = t * segmentLengths[segmentLengths.Length-1];
    var index = segmentLengths.BinarySearch(targetLength);
    if(index < 0) {
      index = (~index)-1;
    }
    var fractional = targetLength - segmentLengths[index];
    return Vector3.Lerp(GetPointCubic(index/segmentCount), GetPointCubic((index+1)/segmentCount), fractional);
  }

  public Vector3 GetPointCubic(t) {
    Vector3 p1 = Vector3.Lerp(this.start, this.control1, t);
    Vector3 p2 = Vector3.Lerp(this.control1, this.control2, t);
    Vector3 p3 = Vector3.Lerp(this.control2, this.finish, t);
    Vector3 p4 = Vector3.Lerp(p1, p2, t);
    Vector3 p5 = Vector3.Lerp(p2, p3, t);
    return Vector3.Lerp(p4, p5, t);
  }

  public void Draw() {
    Handles.DrawLine(this.start.position, this.control1.position);
    Handles.DrawLine(this.finish.position, this.control2.position);

    Vector3 last = null;
    foreach (Vector3 segment in segments) {
      if(last != null) {
        Handles.DrawLine(last, segment);
      }
      last = segment;
    }
    
  }

}
