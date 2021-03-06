﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineGrow : MonoBehaviour {


  // inspector values
  public AnimationCurve growCurve;
  public float growSpeed;

  List<GameObject> growParts;
  
  int vineSize;

  GameObject[] leaves;

  public bool constantAxis = true;

  public Vector3 axisScale;

  private void OnEnable() {
    growParts = new List<GameObject>();
    
    initailizeGrowObject();
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.E)) {
      Debug.Log("Growing Vine");
      StartCoroutine(Curve(0));
    }
  }

  void initailizeGrowObject() {
    foreach (Transform child in transform) {
      child.gameObject.SetActive(false);
      child.transform.localScale = new Vector2(0f, 0f);
      growParts.Add(child.gameObject);
    }
    vineSize = growParts.Count;
  }

  IEnumerator Curve(int vineIndex) {
    if (vineIndex >= vineSize) {
      Debug.Log("End of Vines");
      yield break;
    }
    Debug.Log("Vine " + vineIndex);
    GameObject vine = growParts[vineIndex];
    StartCoroutine(Grow(vine));
    yield return new WaitForSeconds(1 - growSpeed);
    StartCoroutine(Curve(vineIndex + 1));
  }


  IEnumerator Grow(GameObject obj) {
    Vector2 initialScale = obj.transform.localScale;
    Vector3 finalScale = new Vector2(1f, -1f);

    //obj.transform.localScale = initialScale;
    obj.SetActive(true);

    float scale = 0.01f;
    while (scale <= 1.0f) {
      scale += growSpeed;
      yield return new WaitForSeconds(0.1f);
      obj.transform.localScale = Vector2.Lerp(initialScale, finalScale, growCurve.Evaluate(scale));

    }
  }

}
