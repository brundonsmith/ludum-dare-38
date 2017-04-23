using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour {

 // private Transform savedParent;
  public ControlStatus controlStatus = ControlStatus.Normal; //0-normal 1-hitstun 2-launch
  public Vector3 tempAbsoluteLocation;
  public Vector3 standardLocation;
  public Quaternion standardRotation;
  public Quaternion savedOrientation;
  Vector3 tempRotAxis;
  Vector3 tempForwardAxis;
  TransitionFloat transX;
  TransitionFloat transY;
  TransitionFloat transZ;
  TransitionFloat transRotX;
  TransitionFloat transRotY;
  TransitionFloat transRotZ;
  TransitionFloat transRotW;
  private bool lookingAt = false;

  // Use this for initialization
  void Start() {
    standardLocation = this.GetComponent<Transform>().localPosition;
    standardRotation = this.GetComponent<Transform>().localRotation;
    //savedParent = this.GetComponent<Transform>().parent;
  }

	// Update is called once per frame
	protected void Update () {
    if(controlStatus == ControlStatus.Launch)
    {
      this.GetComponent<Transform>().position = tempAbsoluteLocation;
      if (lookingAt)
      {
        this.GetComponent<Transform>().LookAt(this.GetComponent<Transform>().parent.FindChild("Character"));
      } else
      {
        this.GetComponent<Transform>().rotation = savedOrientation;
      }
    }
  }

  public void RiseReact()
  {
    var rot = Quaternion.AngleAxis(180, this.GetComponent<Transform>().right);
    this.GetComponent<Transform>().position += rot * (this.GetComponent<Transform>().forward * .1F);
  }

  public void FallReact()
  {
    var rot = Quaternion.AngleAxis(0, this.GetComponent<Transform>().right);
    this.GetComponent<Transform>().position += rot * (this.GetComponent<Transform>().forward * .1F);
  }

  public void Launch()
  {
    Debug.Log("trigger2");
    lookingAt = false;
    controlStatus = ControlStatus.Launch;
    tempRotAxis = this.GetComponent<Transform>().right;
    tempForwardAxis = this.GetComponent<Transform>().forward;
    tempAbsoluteLocation = this.GetComponent<Transform>().position;
    savedOrientation = this.GetComponent<Transform>().rotation;
    StartCoroutine("LaunchStun");
  }

  IEnumerator LaunchStun() {

    for (int i = 0; i < 60; i++) {
      var rot = Quaternion.AngleAxis(180, tempRotAxis);
      tempAbsoluteLocation += rot * (tempForwardAxis * .07F);
      if (i == 40) lookingAt = true;
      yield return null;
    }
    controlStatus = ControlStatus.LaunchReturn;
    StartCoroutine("LaunchReturn");
  }

  IEnumerator LaunchReturn()
  {
    transX = new TransitionFloat(this.GetComponent<Transform>().localPosition.x, .5F, TransitionFloat.EASE_IN_OUT, 0);
    transY = new TransitionFloat(this.GetComponent<Transform>().localPosition.y, .5F, TransitionFloat.EASE_IN_OUT, 0);
    transZ = new TransitionFloat(this.GetComponent<Transform>().localPosition.z, .5F, TransitionFloat.EASE_IN_OUT, 0);
    transRotX = new TransitionFloat(this.GetComponent<Transform>().localRotation.x, .5F, TransitionFloat.EASE_IN_OUT, 0);
    transRotY = new TransitionFloat(this.GetComponent<Transform>().localRotation.y, .5F, TransitionFloat.EASE_IN_OUT, 0);
    transRotZ = new TransitionFloat(this.GetComponent<Transform>().localRotation.z, .5F, TransitionFloat.EASE_IN_OUT, 0);
    transRotW = new TransitionFloat(this.GetComponent<Transform>().localRotation.w, .5F, TransitionFloat.EASE_IN_OUT, 0);
    transX.GoTo(this.standardLocation.x);
    transY.GoTo(this.standardLocation.y);
    transZ.GoTo(this.standardLocation.z);
    transRotX.GoTo(this.standardRotation.x);
    transRotY.GoTo(this.standardRotation.y);
    transRotZ.GoTo(this.standardRotation.z);
    transRotW.GoTo(this.standardRotation.w);
    while (!(transX.Finished || transY.Finished || transZ.Finished || transRotX.Finished || transRotY.Finished || transRotZ.Finished || transRotW.Finished))
    {
      transX.Update();
      transY.Update();
      transZ.Update();
      transRotX.Update();
      transRotY.Update();
      transRotZ.Update();
      transRotW.Update();
      this.GetComponent<Transform>().localPosition = new Vector3(transX.CurrentValue, transY.CurrentValue, transZ.CurrentValue);
      this.GetComponent<Transform>().localRotation = new Quaternion(transRotX.CurrentValue, transRotY.CurrentValue, transRotZ.CurrentValue, transRotW.CurrentValue);
      yield return null;
    }
    controlStatus = ControlStatus.Normal;
  }
}
