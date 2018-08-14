using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour {
    public bool isSelected = false;
    public Stage corrStage;
    public GameObject selectedImage;

	void Update () {
        if (isSelected)
        {
            selectedImage.SetActive(true);
        }else
        {
            selectedImage.SetActive(false);
        }
    }
}
