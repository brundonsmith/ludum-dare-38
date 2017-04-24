using UnityEngine;

public class TitleScreenController : MonoBehaviour {

  public int numPlayersSelected = 1;
  public bool AISelected = false;
	// Use this for initialization
	void Start () {
    UnityEngine.Cursor.visible = true;
    DontDestroyOnLoad(transform.gameObject);
	}
	
  public void StartGame ()
  {
    Debug.Log("start button clicked");
    UnityEngine.SceneManagement.SceneManager.LoadScene("scene1");
  }

  public void OnePlayer()
  {
    numPlayersSelected = 1;
  }
  public void TwoPlayer() { numPlayersSelected = 2; }
  public void ThreePlayer() { numPlayersSelected = 3; }
  public void FourPlayer() { numPlayersSelected = 4; }
  public void AIOn() { AISelected = true; }
  public void AIOff() { AISelected = false; }
	// Update is called once per frame
	void Update () {
		
	}
}
