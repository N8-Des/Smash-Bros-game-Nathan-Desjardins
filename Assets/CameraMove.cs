using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public float zoomDistance;
    public GameObject stageCenter;
    public GameObject mainCamera;
    public float negYBounds;
    public float YBounds;
    public float ZBounds;
    void Update()
    {
        if (player1 != null && player2 != null)
        {
            zoomDistance = player1.transform.position.z - player2.transform.position.z;
            float distanceY = player1.transform.position.y - player2.transform.position.y;
            //Debug.Log(zoomDistance + " + " + distanceY + " = " + zoomDistance + distanceY);
            distanceY = Mathf.Abs(distanceY);
            distanceY *= 1.3f;
            zoomDistance = Mathf.Abs(zoomDistance - distanceY);
            if (zoomDistance > 7)
            {
                zoomDistance = 7;
            }
            if (zoomDistance < 1.5f)
            {
                zoomDistance = 1.5f;
            }
            Vector3 averagePos = (player1.transform.position - player2.transform.position) / 2 + player2.transform.position;
            if (averagePos.x < -ZBounds)
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, averagePos.y + 0.5f, -ZBounds);
            }
            else if (averagePos.x > ZBounds)
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, averagePos.y + 0.5f, ZBounds);
            }
            else if (averagePos.y < negYBounds)
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, -YBounds + 0.5f, averagePos.z);
            }
            else if (averagePos.y > YBounds)
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, YBounds + 0.5f, averagePos.z);
            }
            else
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, averagePos.y + 0.5f, averagePos.z);
            }
        }
        else if (player2 == null && player1 != null)
        {
            Vector3 averagePos = player1.transform.position;
            if (averagePos.x < -ZBounds)
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, averagePos.y + 0.5f, -ZBounds);
            }
            else if (averagePos.x > ZBounds)
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, averagePos.y + 0.5f, ZBounds);
            }
            else if (averagePos.y < -YBounds)
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, -YBounds + 0.5f, averagePos.z);
            }
            else if (averagePos.y > YBounds)
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, YBounds + 0.5f, averagePos.z);
            }
            else
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, averagePos.y + 0.5f, averagePos.z);
            }
        }
        else if (player1 == null && player2 != null)
        {
            Vector3 averagePos = player2.transform.position;
            if (averagePos.x < -ZBounds)
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, averagePos.y + 0.5f, -ZBounds);
            }
            else if (averagePos.x > ZBounds)
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, averagePos.y + 0.5f, ZBounds);
            }
            else if (averagePos.y < -YBounds)
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, -YBounds + 0.5f, averagePos.z);
            }
            else if (averagePos.y > YBounds)
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, YBounds + 0.5f, averagePos.z);
            }
            else
            {
                mainCamera.transform.position = new Vector3((zoomDistance * 1.3f) + 1f, averagePos.y + 0.5f, averagePos.z);
            }
        }
    }    
}