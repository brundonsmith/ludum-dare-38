using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  public int numPlayers = 2;

  public Character playerOneCharacter;
  public Character playerTwoCharacter;
  public Character playerThreeCharacter;
  public Character playerFourCharacter;

	// Use this for initialization
	void Start () {
    GameObject playerOne = GameObject.Instantiate(playerOneCharacter.gameObject);
    playerOne.GetComponent<Transform>().position = Vector3.zero;
    playerOne.GetComponent<PlayerController>().player = Player.One;

    playerOne.GetComponent<PlayerController>().camera.rect = new Rect(0, 0, 1, 1);

    if(numPlayers > 1) {
      GameObject playerTwo = GameObject.Instantiate(playerTwoCharacter.gameObject);
      playerTwo.GetComponent<Transform>().position = Vector3.zero;
      playerTwo.GetComponent<PlayerController>().player = Player.Two;

      playerOne.GetComponent<PlayerController>().camera.rect = new Rect(0, 0.5f, 1, 0.5f);
      playerTwo.GetComponent<PlayerController>().camera.rect = new Rect(0, 0f, 1, 0.5f);

      if(numPlayers > 2) {
        GameObject playerThree = GameObject.Instantiate(playerThreeCharacter.gameObject);
        playerThree.GetComponent<Transform>().position = Vector3.zero;
        playerThree.GetComponent<PlayerController>().player = Player.Three;

        playerOne.GetComponent<PlayerController>().camera.rect = new Rect(0, 0.5f, 1, 0.5f);
        playerTwo.GetComponent<PlayerController>().camera.rect = new Rect(0, 0f, 0.5f, 0.5f);
        playerThree.GetComponent<PlayerController>().camera.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);

        if(numPlayers > 3) {
          GameObject playerFour = GameObject.Instantiate(playerFourCharacter.gameObject);
          playerFour.GetComponent<Transform>().position = Vector3.zero;
          playerFour.GetComponent<PlayerController>().player = Player.Four;

          playerOne.GetComponent<PlayerController>().camera.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
          playerTwo.GetComponent<PlayerController>().camera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
          playerThree.GetComponent<PlayerController>().camera.rect = new Rect(0, 0f, 0.5f, 0.5f);
          playerFour.GetComponent<PlayerController>().camera.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
        }
      }
    }

	}

	// Update is called once per frame
	void Update () {

	}
}
