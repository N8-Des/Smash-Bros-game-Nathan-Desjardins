using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public bool isSelected = false;
    public GameObject CorrelatedCanvas;
    public GameObject selectedImage;

    void Update()
    {
        if (isSelected)
        {
            selectedImage.SetActive(true);
        }
        else
        {
            selectedImage.SetActive(false);
        }
    }
}
