using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

  public float moveAcceleration;
  public float moveResilience;
  public float turnSpeed;
  public float jumpHeight;
    public float speed;
  private int jumpStatus = 0; //0-ground 1-rising 2-hanging 3-falling
  public float maxSpeed;
  public Transform character;

  // Use this for initialization
  void Start()
  {
    this.GetComponent<Rigidbody>().centerOfMass = this.GetComponent<Transform>().position;
  }

	// Update is called once per frame
	protected void Update () {
    if (speed < maxSpeed)
    {
      speed += moveAcceleration;
    }
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(speed, this.transform.right) * this.GetComponent<Rigidbody>().rotation;
    if (jumpStatus == 1)
    {
      //character.GetComponent<Transform>().position += character.GetComponent<Transform>().up;
    } else if (jumpStatus == 2)
    {

    } else if (jumpStatus == 3)
    {
      //character.GetComponent<Transform>().position += character.GetComponent<Transform>().up;
    }
  }

  public void TurnLeft() {
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(-1 * turnSpeed, this.transform.up) * this.GetComponent<Rigidbody>().rotation;
  }

  public void TurnRight() {
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(1 * turnSpeed, this.transform.up) * this.GetComponent<Rigidbody>().rotation;
  }

  public void OnTriggerEnter(Collider other)
  {
    //add a check later to see WHAT is being collided with
    /*speed -= .5F;
    if(speed < 0)
    {
      speed = 0;
    }*/
    //time for "character" collision

  }

  public void Jump()
  {
    jumpStatus = 1;
    StartCoroutine("Rising");
    //maybe do an initiate-jump animation here
  }

  IEnumerator Rising()
  {
    for (int i = 0; i < 30; i++)
    {
      character.GetComponent<Transform>().localPosition += new Vector3(0, .1F, 0);
      yield return null;
    }
    jumpStatus = 2;
    StartCoroutine("Hanging");
  }

  IEnumerator Hanging()
  {
    for (int i = 0; i < 30; i++)
    {
      yield return null;
    }
    jumpStatus = 3;
    StartCoroutine("Falling");
  }

  IEnumerator Falling()
  {
    for (int i = 0; i < 30; i++)
    {
      character.GetComponent<Transform>().localPosition += new Vector3(0, -.1F, 0);
      yield return null;
    }
    jumpStatus = 0;
  }
}
