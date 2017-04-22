using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyCharacter : Character {

	// Use this for initialization
	void Start () {
    speed = 0;
    moveAcceleration = .001F;
    maxSpeed = 2;
  }
  
  // Update is called once per frame
  void Update () {
    base.Update();
  }
}
