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
  Launch,
  LaunchReturn
}

public enum SpecialStatus
{
  NotUsingSpecial,
  UsingSpecial
}

public class Character : MonoBehaviour {

  // constants
  public float moveAcceleration;
  public float moveMaxSpeed;
  public float moveResilience;
  public float turnSpeed;
  public float jumpHeight;
  public float decelSpeed;
  public Camera skyCameraPrefab;
  public float jumpCost;

  // state
  public float speed;
  protected JumpStatus jumpStatus = JumpStatus.Grounded; //0-ground 1-rising 2-hanging 3-falling
  public float maxEnergy = 1000;
  protected float energy = 1000;
  public float chargeRate = 1;
  protected bool canCollide = true;
  public Camera skyCamera;

  // references to children
  public Transform character;
  public ControlStatus controlStatus = ControlStatus.Normal; //0-normal 1-hitstun 2-launch
  public SpecialStatus specialStatus = SpecialStatus.NotUsingSpecial;

  public Vector3 standardPosition;

  protected void Awake() {
    this.skyCamera = GameObject.Instantiate(this.skyCameraPrefab.gameObject).GetComponent<Camera>();
  }

  // Use this for initialization
  protected void Start() {
    this.GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
    standardPosition = this.character.GetComponent<Transform>().localPosition;
    maxEnergy = energy = 1000;
  }

	// Update is called once per frame
	protected void Update () {
    if (this.speed < this.moveMaxSpeed && jumpStatus == JumpStatus.Grounded) {
      this.speed += this.moveAcceleration;
      if (this.specialStatus == SpecialStatus.NotUsingSpecial)
      {
        this.energy += this.chargeRate;
      }
    }
    if (this.speed > this.moveMaxSpeed) this.speed = this.moveMaxSpeed;
    if (this.energy > this.maxEnergy) this.energy = this.maxEnergy;
    if (this.jumpStatus == JumpStatus.Grounded) this.character.GetComponent<Transform>().localPosition = standardPosition;
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(speed, this.transform.right) * this.GetComponent<Rigidbody>().rotation;

    this.skyCamera.GetComponent<Transform>().localRotation = this.GetComponent<Transform>().localRotation;
  }

  public void TurnLeft() {
    float multiplier = jumpStatus == JumpStatus.Grounded ? 1F : 2F;
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(-1 * multiplier * this.turnSpeed, this.transform.up) * this.GetComponent<Rigidbody>().rotation;
  }

  public void TurnRight() {
    float multiplier = jumpStatus == JumpStatus.Grounded ? 1F : 2F;
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(multiplier * this.turnSpeed, this.transform.up) * this.GetComponent<Rigidbody>().rotation;
  }

  public void Jump()
  {
    if (jumpStatus == JumpStatus.Grounded && this.energy >= this.jumpCost)
    {
      this.energy -= this.jumpCost;
      speed -= decelSpeed;
      if (speed < 0)
      {
        speed = 0;
      }
      this.jumpStatus = JumpStatus.Rising;
      StartCoroutine("Rising");
    }
    //maybe do an initiate-jump animation here
    //camera control
  }

  public virtual void Special() { }

  public void OnTriggerEnter(Collider other) {
    //add a check later to see WHAT is being collided with
    if (!this.canCollide) return;
    Debug.Log("triggered");
    if (other.gameObject.tag == "enemy")
    {
      if (this.speed >= this.moveMaxSpeed && other.gameObject.GetComponentInParent<Character>().speed >= other.gameObject.GetComponentInParent<Character>().moveMaxSpeed)
      {
        //both at max speed

      } else if (this.speed >= this.moveMaxSpeed && other.gameObject.GetComponentInParent<Character>().speed < other.gameObject.GetComponentInParent<Character>().moveMaxSpeed)
      {
        //this guy at max speed
        Debug.Log("player 1 wins!");
        //what happens to the winner? nothing for now
      } else if (this.speed < this.moveMaxSpeed && other.gameObject.GetComponentInParent<Character>().speed >= other.gameObject.GetComponentInParent<Character>().moveMaxSpeed)
      {
        //that guy at max speed
        getDefeated();
      } else if (this.speed < this.moveMaxSpeed && other.gameObject.GetComponentInParent<Character>().speed < other.gameObject.GetComponentInParent<Character>().moveMaxSpeed)
      {
        //neither at max speed
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
    } else if (other.gameObject.tag == "weakObstacle")
    {
      if (this.speed < this.moveMaxSpeed)
      {
        speed -= decelSpeed * .5F;
        if (speed < 0)
        {
          speed = 0;
        }
      } else
      {
        speed -= decelSpeed * .7F;
        if (speed < 0)
        {
          speed = 0;
        }
      }
    } else if (other.gameObject.tag == "strongObstacle")
    {
      if (this.speed >= this.moveMaxSpeed)
      {
        Debug.Log("launch!");
        speed -= decelSpeed * 7F;
        if (speed < 0)
        {
          speed = 0;
        }
        //time for "character" collision
        this.GetComponentInParent<PlayerController>().GetComponentInChildren<CustomCamera>().Launch();
        StartCoroutine("LaunchStun");
      }
      else
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
  }

  public void getDefeated()
  {
    StartCoroutine("Defeated");
  }

  IEnumerator Defeated()
  {
    Debug.Log("Being Defeated!!");
    this.jumpStatus = JumpStatus.Hanging;
    this.controlStatus = ControlStatus.HitStun;
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(Random.Range(0, 360), this.transform.up) * this.GetComponent<Rigidbody>().rotation;
    for (int i = 0; i < 240; i++)
    {
      this.character.GetComponent<Transform>().localPosition += new Vector3(0, .1F, 0);
      this.character.GetComponent<Transform>().parent.GetComponentInChildren<CustomCamera>().RiseReact();
      yield return null;
    }
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
