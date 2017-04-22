using System.Collections;
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
  Launch
}

public class Character : MonoBehaviour {

  // constants
  public float moveAcceleration;
  public float moveMaxSpeed;
  public float moveResilience;
  public float turnSpeed;
  public float jumpHeight;

  // state
  protected float speed;
  protected JumpStatus jumpStatus = JumpStatus.Grounded; //0-ground 1-rising 2-hanging 3-falling

  // references to children
  public Camera camera;
  public Transform character;
  public ControlStatus controlStatus = ControlStatus.Normal; //0-normal 1-hitstun 2-launch

  // Use this for initialization
  void Start() {
    this.GetComponent<Rigidbody>().centerOfMass = this.GetComponent<Transform>().position;
  }

	// Update is called once per frame
	protected void Update () {
    if (this.speed < this.moveMaxSpeed) {
      this.speed += this.moveAcceleration;
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
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(-1 * this.turnSpeed, this.transform.up) * this.GetComponent<Rigidbody>().rotation;
  }

  public void TurnRight() {
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(1 * this.turnSpeed, this.transform.up) * this.GetComponent<Rigidbody>().rotation;
  }

  public void OnTriggerEnter(Collider other) {
    //add a check later to see WHAT is being collided with
    /*speed -= .5F;
    if(speed < 0)
    {
      speed = 0;
    }*/
    //time for "character" collision
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(Random.Range(0, 360), this.transform.up) * this.GetComponent<Rigidbody>().rotation;
    this.controlStatus = ControlStatus.HitStun;
    StartCoroutine("LaunchStun");
  }

  public void Jump() {
    this.jumpStatus = JumpStatus.Rising;
    StartCoroutine("Rising");
    //maybe do an initiate-jump animation here
  }

  IEnumerator LaunchStun() {

    for (int i = 0; i < 45; i++) {
      yield return null;
    }

    this.controlStatus = ControlStatus.Normal;
  }

  IEnumerator Rising() {

    for (int i = 0; i < 30; i++) {
      this.character.GetComponent<Transform>().localPosition += new Vector3(0, .1F, 0);
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

    for (int i = 0; i < 30; i++) {
      this.character.GetComponent<Transform>().localPosition += new Vector3(0, -.1F, 0);
      yield return null;
    }

    this.jumpStatus = JumpStatus.Grounded;
  }
}
