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
    GameObject.Find("purpler").GetComponent<Transform>().position = new Vector3(GameObject.Find("purpler").GetComponent<Transform>().position.x,
      263, GameObject.Find("purpler").GetComponent<Transform>().position.z);
  }
  public void TwoPlayer() { numPlayersSelected = 2;
    GameObject.Find("purpler").GetComponent<Transform>().position = new Vector3(GameObject.Find("purpler").GetComponent<Transform>().position.x,
      259, GameObject.Find("purpler").GetComponent<Transform>().position.z);
  }
  public void ThreePlayer() { numPlayersSelected = 3;
    GameObject.Find("purpler").GetComponent<Transform>().position = new Vector3(GameObject.Find("purpler").GetComponent<Transform>().position.x,
      229, GameObject.Find("purpler").GetComponent<Transform>().position.z);
  }
  public void FourPlayer() { numPlayersSelected = 4;
    GameObject.Find("purpler").GetComponent<Transform>().position = new Vector3(GameObject.Find("purpler").GetComponent<Transform>().position.x,
      200, GameObject.Find("purpler").GetComponent<Transform>().position.z);
  }
  public void AIOn() { AISelected = true; }
  public void AIOff() { AISelected = false; }
	// Update is called once per frame
	void Update () {
		
	}
}
