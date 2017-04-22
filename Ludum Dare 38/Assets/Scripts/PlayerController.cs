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
  public Transform obstacleIconPrefab;

  private Dictionary<GameObject, GameObject> icons = new Dictionary<GameObject, GameObject>();

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

      switch(this.player) {
        case Player.One:
          if (Input.GetKey(KeyCode.LeftArrow)) {
            this.GetComponent<Character>().TurnLeft();
          } else if (Input.GetKey(KeyCode.RightArrow)) {
            this.GetComponent<Character>().TurnRight();
          } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            this.GetComponent<Character>().Jump();
          }
          break;
        case Player.Two:
          if (Input.GetKey(KeyCode.A)) {
            this.GetComponent<Character>().TurnLeft();
          } else if (Input.GetKey(KeyCode.D)) {
            this.GetComponent<Character>().TurnRight();
          } else if (Input.GetKeyDown(KeyCode.W)) {
            this.GetComponent<Character>().Jump();
          }
          break;
      }

    }


    this.UpdateIcons();
	}

  void UpdateIcons () {
    // detect relevant objects within camera frustrum
    // for each, create an icon if it doesn't have one
    // place as overlay sprite on the screen
    // for position,
    //    take object coordinate,
    //    rotate to horizon based on current movement direction,
    //    then get camera coordinate

    foreach(Obstacle obstacle in GameObject.FindObjectsOfType<Obstacle>()) {
      Vector3 obstaclePosition = obstacle.GetComponent<Transform>().position;
      Vector3 obstacleViewportPosition = this.camera.WorldToViewportPoint(obstaclePosition);
      if(obstacleViewportPosition.x > 0
          && obstacleViewportPosition.x < 1
          && obstacleViewportPosition.y > 0
          && obstacleViewportPosition.y < 1
          && obstacleViewportPosition.z > 0) {

        if(this.icons.ContainsKey(obstacle.gameObject)) {
          Vector3 directionFromCamera = (obstaclePosition - this.camera.GetComponent<Transform>().position).normalized;
          this.icons[obstacle.gameObject].GetComponent<Transform>().position = obstaclePosition - directionFromCamera * obstacleViewportPosition.z * 0.7f;
        } else {
          GameObject newIcon = GameObject.Instantiate(this.obstacleIconPrefab.gameObject);

          Vector3 directionFromCamera = (obstaclePosition - this.camera.GetComponent<Transform>().position).normalized;
          newIcon.GetComponent<Transform>().position = obstaclePosition - directionFromCamera * obstacleViewportPosition.z * 0.7f;
          int playerLayer = LayerMask.NameToLayer("Player " + this.player.ToString() + " Only");
          newIcon.layer = playerLayer;
          foreach(Transform child in newIcon.GetComponent<Transform>()) {
            child.gameObject.layer = playerLayer;
          }
          newIcon.GetComponent<Billboard>().camera = this.camera;

          this.icons[obstacle.gameObject] = newIcon;
        }
      }
    }
  }
}
