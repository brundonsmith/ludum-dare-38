﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JumpStatus {
  Grounded,
  Rising,
  Hanging,
  Falling
}

public enum ControlStatus {
  Normal,
  HitStun,
  Launch,
  LaunchReturn
}

public class Character : MonoBehaviour {

  // constants
  public float moveAcceleration;
  public float moveMaxSpeed;
  public float moveResilience;
  public float turnSpeed;
  public float jumpHeight;
  public float decelSpeed;

  // state
  protected float speed;
  protected JumpStatus jumpStatus = JumpStatus.Grounded; //0-ground 1-rising 2-hanging 3-falling

  // references to children
  public Transform character;
  public ControlStatus controlStatus = ControlStatus.Normal; //0-normal 1-hitstun 2-launch

  public Vector3 standardPosition;

  // Use this for initialization
  void Start() {
    standardPosition = this.character.GetComponent<Transform>().localPosition;
  }

	// Update is called once per frame
	protected void Update () {
    if (this.speed < this.moveMaxSpeed && jumpStatus == JumpStatus.Grounded) {
      this.speed += this.moveAcceleration;
     // if()
    }

    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(speed, this.transform.right) * this.GetComponent<Rigidbody>().rotation;

    if (this.jumpStatus == JumpStatus.Rising) {
      //character.GetComponent<Transform>().position += character.GetComponent<Transform>().up;
    } else if (this.jumpStatus == JumpStatus.Hanging) {

    } else if (this.jumpStatus == JumpStatus.Falling) {
      //character.GetComponent<Transform>().position += character.GetComponent<Transform>().up;
    }
  }

  public void TurnLeft() {
    float multiplier = jumpStatus == JumpStatus.Grounded ? 1F : 2F;
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(-1 * multiplier * this.turnSpeed, this.transform.up) * this.GetComponent<Rigidbody>().rotation;
  }

  public void TurnRight() {
    float multiplier = jumpStatus == JumpStatus.Grounded ? 1F : 2F;
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(multiplier * this.turnSpeed, this.transform.up) * this.GetComponent<Rigidbody>().rotation;
  }

  public void OnTriggerEnter(Collider other) {
    //add a check later to see WHAT is being collided with
    Debug.Log("triggered");
    if (other.gameObject.tag == "weakObstacle")
    {
      speed -= decelSpeed * .7F;
      if (speed < 0)
      {
        speed = 0;
      }
    } else if (other.gameObject.tag == "strongObstacle" || other.gameObject.tag == "enemy")
    {
      Debug.Log("launch!");
      speed -= decelSpeed;
      if (speed < 0)
      {
        speed = 0;
      }
      //time for "character" collision
      this.GetComponentInParent<PlayerController>().GetComponentInChildren<CustomCamera>().Launch();
      StartCoroutine("LaunchStun");
    }
  }

  public void Jump() {
    if (jumpStatus != JumpStatus.Grounded) return;
    speed -= decelSpeed;
    if (speed < 0)
    {
      speed = 0;
    }
    this.jumpStatus = JumpStatus.Rising;
    StartCoroutine("Rising");
    //maybe do an initiate-jump animation here
    //camera control
  }

  IEnumerator LaunchStun() {
    this.jumpStatus = JumpStatus.Hanging;
    this.controlStatus = ControlStatus.HitStun;
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(Random.Range(0, 360), this.transform.up) * this.GetComponent<Rigidbody>().rotation;
    //reduce speed specificall bc of launch
    for (int i = 0; i < 60; i++) {
      this.character.GetComponent<Transform>().localPosition = new Vector3(standardPosition.x, standardPosition.y + Mathf.Sin(Mathf.PI / 60 * i),
        standardPosition.z);
      yield return null;
    }

    this.controlStatus = ControlStatus.Normal;
    this.jumpStatus = JumpStatus.Grounded;
  }

  IEnumerator Rising() {

    for (int i = 0; i < 20; i++) {
      this.character.GetComponent<Transform>().localPosition += new Vector3(0, .05F, 0);
      this.character.GetComponent<Transform>().parent.GetComponentInChildren<CustomCamera>().RiseReact();
      yield return null;
    }

    this.jumpStatus = JumpStatus.Hanging;
    StartCoroutine("Hanging");
  }

  IEnumerator Hanging() {

    for (int i = 0; i < 30; i++) {
      yield return null;
    }

    this.jumpStatus = JumpStatus.Falling;
    StartCoroutine("Falling");
  }

  IEnumerator Falling() {

    for (int i = 0; i < 20; i++) {
      this.character.GetComponent<Transform>().localPosition += new Vector3(0, -.05F, 0);
      this.character.GetComponent<Transform>().parent.GetComponentInChildren<CustomCamera>().FallReact();
      yield return null;
    }

    this.jumpStatus = JumpStatus.Grounded;
  }
}
