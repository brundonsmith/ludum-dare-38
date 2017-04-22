using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player {
  One,
  Two,
  Three,
  Four
}

public class PlayerController : MonoBehaviour {

  public Player player = Player.One;
  public Camera camera;

	// Use this for initialization
	void Start () {
    List<string> layersToHide = new List<string>();
    foreach(Player player in Enum.GetValues(typeof(Player))) {
      if(player != this.player) {
        layersToHide.Add("Player " + player.ToString() + " Only");
      }
    }
    this.camera.cullingMask = ~LayerMask.GetMask(layersToHide.ToArray());
	}

	// Update is called once per frame
	void Update () {
    if (this.GetComponent<Character>().controlStatus == ControlStatus.Normal) {

      if (Input.GetKey(KeyCode.LeftArrow)) {
        this.GetComponent<Character>().TurnLeft();
      } else if (Input.GetKey(KeyCode.RightArrow)) {
        this.GetComponent<Character>().TurnRight();
      } else if (Input.GetKeyDown(KeyCode.Space)) {
        this.GetComponent<Character>().Jump();
      }

    }
	}
}
