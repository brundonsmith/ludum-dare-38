using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStars : MonoBehaviour {

  public GameObject starPrefab;
  public int numStars;

	// Use this for initialization
	void Start () {
    for(int i = 0; i < this.numStars; i++) {
      GameObject newStar = GameObject.Instantiate(this.starPrefab);
      newStar.GetComponent<Transform>().parent = this.GetComponent<Transform>();
      newStar.GetComponent<Transform>().localPosition = Random.onUnitSphere * 500;
      newStar.GetComponent<Transform>().localScale = new Vector3(1, 1, 1) * Random.value * 3;
      newStar.GetComponent<Transform>().localRotation
        = Quaternion.FromToRotation(
            Vector3.forward,
            newStar.GetComponent<Transform>().position - this.GetComponent<Transform>().position
          );
    }
	}
}
