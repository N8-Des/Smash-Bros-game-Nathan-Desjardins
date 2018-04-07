﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTimer : MonoBehaviour {
    public float time;
	void Start () {
        StartCoroutine(death());
	}
	public IEnumerator death()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
