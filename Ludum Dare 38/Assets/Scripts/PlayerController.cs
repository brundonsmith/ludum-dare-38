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
  public Transform enemyIconPrefab;

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
          } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            this.GetComponent<Character>().Special();
          }
          break;
        case Player.Two:
          if (Input.GetKey(KeyCode.A)) {
            this.GetComponent<Character>().TurnLeft();
          } else if (Input.GetKey(KeyCode.D)) {
            this.GetComponent<Character>().TurnRight();
          } else if (Input.GetKeyDown(KeyCode.W)) {
            this.GetComponent<Character>().Jump();
          } else if (Input.GetKeyDown(KeyCode.S))
          {
            this.GetComponent<Character>().Special();
          }
          break;
      }

    }

    if(this.GetComponentInChildren<TextMesh>() != null) {
      this.GetComponentInChildren<TextMesh>().text = this.GetComponent<Character>().speed + "/" + this.GetComponent<Character>().moveMaxSpeed;
    }

    this.UpdateIcons();
	}

  void UpdateIcons () {
    // detect relevant objects
    // for each, create an icon if it doesn't have one
    // place as overlay sprite on the screen
    // for position,
    //    take object coordinate,
    //    rotate to horizon based on current movement direction,
    //    then get camera coordinate

    // Obstacles
    foreach(Obstacle obstacle in GameObject.FindObjectsOfType<Obstacle>()) {
      Vector3 obstaclePosition = obstacle.GetComponent<Transform>().position;
      Vector3 obstacleViewportPosition = this.camera.WorldToViewportPoint(obstaclePosition);

      if(obstacleViewportPosition.x > 0
          && obstacleViewportPosition.x < 1
          && obstacleViewportPosition.y > 0
          && obstacleViewportPosition.y < 1
          && obstacleViewportPosition.z > 0
          && Vector3.Project(obstaclePosition, this.GetComponent<Transform>().Find("Character").forward).magnitude > 1 ) {

        if(this.icons.ContainsKey(obstacle.gameObject)) {
          this.icons[obstacle.gameObject].SetActive(true);
          this.PositionIcon(obstacle.gameObject, this.icons[obstacle.gameObject]);
        } else {
          this.icons[obstacle.gameObject] = this.CreateIcon(obstacle.gameObject, this.obstacleIconPrefab);
        }
      } else {
        if(this.icons.ContainsKey(obstacle.gameObject)) {
          this.icons[obstacle.gameObject].SetActive(false);
        }
      }
    }

    // Enemies
    foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("enemy")) {
      if(enemy != this.gameObject) {
        Vector3 enemyPosition = enemy.GetComponent<Transform>().position;
        Vector3 enemyViewportPosition = this.camera.WorldToViewportPoint(enemyPosition);

        if(enemyViewportPosition.x > 0
            && enemyViewportPosition.x < 1
            && enemyViewportPosition.y > 0
            && enemyViewportPosition.y < 1
            && enemyViewportPosition.z > 0
            && Vector3.Project(enemyPosition, this.GetComponent<Transform>().Find("Character").forward).magnitude > 0.5 ) {

          if(this.icons.ContainsKey(enemy)) {
            this.icons[enemy].SetActive(true);
            this.PositionIcon(enemy, this.icons[enemy]);
          } else {
            this.icons[enemy] = this.CreateIcon(enemy, this.enemyIconPrefab);
          }
        } else {
          if(this.icons.ContainsKey(enemy)) {
            this.icons[enemy].SetActive(false);
          }
        }
      }
    }
  }

  void PositionIcon (GameObject subject, GameObject icon) {
    Vector3 subjectPosition = subject.GetComponent<Transform>().position;
    Vector3 subjectViewportPosition = this.camera.WorldToViewportPoint(subjectPosition);
    Vector3 directionFromCamera = (subjectPosition - this.camera.GetComponent<Transform>().position).normalized;

    icon.GetComponent<Transform>().position = subjectPosition - directionFromCamera * subjectViewportPosition.z * 0.7f;
  }

  GameObject CreateIcon (GameObject subject, Transform iconPrefab) {
    GameObject newIcon = GameObject.Instantiate(iconPrefab.gameObject);

    this.PositionIcon(subject, newIcon);

    int playerLayer = LayerMask.NameToLayer("Player " + this.player.ToString() + " Only");
    newIcon.layer = playerLayer;
    foreach(Transform child in newIcon.GetComponent<Transform>()) {
      child.gameObject.layer = playerLayer;
    }
    newIcon.GetComponent<Billboard>().camera = this.camera;

    return newIcon;
  }
}
