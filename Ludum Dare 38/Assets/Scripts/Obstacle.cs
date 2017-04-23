using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

  public GameObject collisionEffectPrefab;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  void OnTriggerEnter(Collider collider) {
    for(int i = 0; i < Random.value * 5 + 3; i++) {
      GameObject newEffect = GameObject.Instantiate(this.collisionEffectPrefab);
      newEffect.GetComponent<Transform>().position = this.GetComponent<Transform>().position + Random.insideUnitSphere * 0.3f;

      Camera colliderCamera = collider.GetComponent<Transform>().parent.GetComponentInChildren<Camera>();
      if(colliderCamera != null) {
        newEffect.GetComponent<Billboard>().camera = colliderCamera;
      }
    }
  }
}
