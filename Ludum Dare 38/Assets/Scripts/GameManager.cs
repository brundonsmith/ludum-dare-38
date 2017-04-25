using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  public int numPlayers = 2;
  public bool AIEnabled = false;

  public Character playerOneCharacter;
  public Character playerTwoCharacter;
  public Character playerThreeCharacter;
  public Character playerFourCharacter;

  private GameObject playerOne;
  private GameObject playerTwo;
  private GameObject playerThree;
  private GameObject playerFour;

  public Sprite playerOneWinsSprite;
  public Sprite playerTwoWinsSprite;
  public Sprite playerThreeWinsSprite;
  public Sprite playerFourWinsSprite;

  public static bool playerOneAlive = false;
  public static bool playerTwoAlive = false;
  public static bool playerThreeAlive = false;
  public static bool playerFourAlive = false;
  public static int playerWins = 0; //0 = gamein progress 1-4 = players 1-4 win respectively

	// Use this for initialization
	void Start () {
    UnityEngine.Cursor.visible = false;
    if (GameObject.Find("TitleScreenController") != null)
    {
      numPlayers = GameObject.Find("TitleScreenController").GetComponent<TitleScreenController>().numPlayersSelected;
      AIEnabled = GameObject.Find("TitleScreenController").GetComponent<TitleScreenController>().AISelected;
    }
    QualitySettings.vSyncCount = 0;
    Application.targetFrameRate = 60;
    playerOne = GameObject.Instantiate(playerOneCharacter.gameObject);
    playerOne.GetComponent<Transform>().position = Vector3.zero;
    playerOne.GetComponent<PlayerController>().player = Player.One;

    playerOne.GetComponent<PlayerController>().camera.rect = new Rect(0, 0, 1, 1);
    playerOne.GetComponent<Character>().skyCamera.rect = new Rect(0, 0, 1, 1);
    GameManager.playerOneAlive = true;

    if(numPlayers > 1) {
      playerTwo = GameObject.Instantiate(playerTwoCharacter.gameObject);
      playerTwo.GetComponent<Transform>().position = Vector3.zero;
      playerTwo.GetComponent<PlayerController>().player = Player.Two;
      playerTwo.GetComponent<Transform>().rotation = new Quaternion(0, 160, 0, 0);

      playerOne.GetComponent<PlayerController>().camera.rect = new Rect(0, 0.5f, 1, 0.5f);
      playerOne.GetComponent<Character>().skyCamera.rect = new Rect(0, 0.5f, 1, 0.5f);
      playerTwo.GetComponent<PlayerController>().camera.rect = new Rect(0, 0f, 1, 0.5f);
      playerTwo.GetComponent<Character>().skyCamera.rect = new Rect(0, 0f, 1, 0.5f);
      GameManager.playerTwoAlive = true;

      if(numPlayers > 2) {
        playerThree = GameObject.Instantiate(playerThreeCharacter.gameObject);
        playerThree.GetComponent<Transform>().position = Vector3.zero;
        playerThree.GetComponent<PlayerController>().player = Player.Three;

        playerOne.GetComponent<PlayerController>().camera.rect = new Rect(0, 0.5f, 1, 0.5f);
        playerOne.GetComponent<Character>().skyCamera.rect = new Rect(0, 0.5f, 1, 0.5f);
        playerTwo.GetComponent<PlayerController>().camera.rect = new Rect(0, 0f, 0.5f, 0.5f);
        playerTwo.GetComponent<Character>().skyCamera.rect = new Rect(0, 0f, 0.5f, 0.5f);
        playerThree.GetComponent<PlayerController>().camera.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
        playerThree.GetComponent<Character>().skyCamera.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
        GameManager.playerThreeAlive = true;
        if(numPlayers > 3) {
          playerFour = GameObject.Instantiate(playerFourCharacter.gameObject);
          playerFour.GetComponent<Transform>().position = Vector3.zero;
          playerFour.GetComponent<PlayerController>().player = Player.Four;

          playerOne.GetComponent<PlayerController>().camera.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
          playerOne.GetComponent<Character>().skyCamera.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
          playerTwo.GetComponent<PlayerController>().camera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
          playerTwo.GetComponent<Character>().skyCamera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
          playerThree.GetComponent<PlayerController>().camera.rect = new Rect(0, 0f, 0.5f, 0.5f);
          playerThree.GetComponent<Character>().skyCamera.rect = new Rect(0, 0f, 0.5f, 0.5f);
          playerFour.GetComponent<PlayerController>().camera.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
          playerFour.GetComponent<Character>().skyCamera.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
          GameManager.playerFourAlive = true;
        }
      }
    }
    int audio = Random.Range(1, 4);
    if(audio == 1)
    {
      GameObject.Find("track1").GetComponent<AudioSource>().Play();
    } else if (audio == 2)
    {
      GameObject.Find("track2").GetComponent<AudioSource>().Play();
    } else if (audio == 3)
    {
      GameObject.Find("track3").GetComponent<AudioSource>().Play();
    }
  }

	// Update is called once per frame
	void Update () {
    if (playerWins != 0)
    {
      GameObject.Find("track1").GetComponent<AudioSource>().Stop();
      GameObject.Find("track2").GetComponent<AudioSource>().Stop();
      GameObject.Find("track3").GetComponent<AudioSource>().Stop();
      GameObject.Find("gameover").GetComponent<AudioSource>().Play();
      GameOver();
      playerWins = 0;
    }
	}

  void GameOver()
  {
    SpriteRenderer winner3;
    SpriteRenderer winner4;
    SpriteRenderer winner1 = playerOne.GetComponent<Transform>().Find("Camera/Winner").GetComponent<SpriteRenderer>();
    SpriteRenderer winner2 = playerTwo.GetComponent<Transform>().Find("Camera/Winner").GetComponent<SpriteRenderer>();
    if (numPlayers > 2)
    {
      winner3 = playerThree.GetComponent<Transform>().Find("Camera/Winner").GetComponent<SpriteRenderer>();
      if (numPlayers > 3)
      {
        winner4 = playerFour.GetComponent<Transform>().Find("Camera/Winner").GetComponent<SpriteRenderer>();
      }
      else
      {
        winner4 = winner1;
      }
    }
    else
    {
      winner3 = winner4 = winner1;
    }
        winner1.enabled = winner2.enabled = winner3.enabled = winner4.enabled = true;
    if (playerWins == 1)
    {
      winner1.sprite = winner2.sprite = winner3.sprite = winner4.sprite = playerOneWinsSprite;
    }
    else if (playerWins == 2)
    {
      Debug.Log("success");
      winner1.sprite = winner2.sprite = winner3.sprite = winner4.sprite = playerTwoWinsSprite;
    } else if (playerWins == 3)
    {
      winner1.sprite = winner2.sprite = winner3.sprite = winner4.sprite = playerThreeWinsSprite;
    } else if (playerWins == 4)
    {
      winner1.sprite = winner2.sprite = winner3.sprite = winner4.sprite = playerFourWinsSprite;
    }
    StartCoroutine("ReturnToTitle");
  }

  IEnumerator ReturnToTitle()
  {
    Destroy(GameObject.Find("TitleScreenController"));
    for (int i = 0; i < 360; i++)
    {
      yield return null;
    }
    UnityEngine.SceneManagement.SceneManager.LoadScene("titleScreen");
  }
}
