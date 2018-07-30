using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFocus : MonoBehaviour
{
    public float HalfXBounds = 15f;
    public float HalfYBounds = 15f;
    public float HalfZBounds = 20f;
    public Bounds FocusBounds;

    void Update()
    {
        Vector3 position = gameObject.transform.position;
        Bounds bounds = new Bounds();
        bounds.Encapsulate(new Vector3(position.z - HalfZBounds, position.y - HalfYBounds, position.x - HalfXBounds));
        bounds.Encapsulate(new Vector3(position.z + HalfZBounds, position.y + HalfYBounds, position.x + HalfXBounds));
        FocusBounds = bounds;
    }
}
