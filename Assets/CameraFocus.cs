using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour {
    public LevelFocus FocusLevel;
    public List<GameObject> Players;
    public float DepthUpdateSpeed = 5f;
    public float AngleUpdateSpeed = 7f;
    public float PositionUpdateSpeed = 5f;
    public float DepthMax = 2f;
    public float DepthMin = 10f;
    public float AngleMax = 11f;
    public float AngleMin = 3f;
    private float CameraEulerX;
    private Vector3 CameraPosition;
    public Vector3 offset = new Vector3(0, 0, 0);

    private void Start()
    {
        Players.Add(FocusLevel.gameObject);
    }
    private void MoveCamera()
    {
        Vector3 position = gameObject.transform.position;
        if (position != CameraPosition)
        {
            Vector3 targetPosition = Vector3.zero;
            targetPosition.x = Mathf.MoveTowards(position.x, CameraPosition.x, DepthUpdateSpeed * Time.deltaTime);
            targetPosition.y = Mathf.MoveTowards(position.y, CameraPosition.y, PositionUpdateSpeed * Time.deltaTime);
            targetPosition.z = Mathf.MoveTowards(position.z, CameraPosition.z, PositionUpdateSpeed * Time.deltaTime);
            gameObject.transform.position = targetPosition;
        }
        /*Vector3 localEuelerAngles = gameObject.transform.localEulerAngles;
        if (localEuelerAngles.z != CameraEulerX)
        {
            Vector3 targetEulerAngles = new Vector3(localEuelerAngles.x, localEuelerAngles.y, CameraEulerX);
            gameObject.transform.localEulerAngles = Vector3.MoveTowards(localEuelerAngles, targetEulerAngles, AngleUpdateSpeed * Time.deltaTime);
        }*/
    }
    private void CalculateLocations()
    {
        Vector3 averageCenter = Vector3.zero;
        Vector3 totalPositions = Vector3.zero;
        Bounds playerBounds = new Bounds();
        for (int i = 0; i < Players.Count; i++)
        {
            Vector3 playerPosition = Players[i].transform.position;
            if (!FocusLevel.FocusBounds.Contains(playerPosition))
            {
                float playerX = Mathf.Clamp(playerPosition.z, FocusLevel.FocusBounds.min.z, FocusLevel.FocusBounds.max.z);
                float playerY = Mathf.Clamp(playerPosition.y, FocusLevel.FocusBounds.min.y, FocusLevel.FocusBounds.max.y);
                float playerZ = Mathf.Clamp(playerPosition.x, FocusLevel.FocusBounds.min.x, FocusLevel.FocusBounds.max.x);
                playerPosition = new Vector3(playerX, playerY, playerZ);
            }
            totalPositions += playerPosition;
            playerBounds.Encapsulate(playerPosition);
        }
        averageCenter = (totalPositions / Players.Count);
        float extents = (playerBounds.extents.z + playerBounds.extents.y);
        float lerpPercent = Mathf.InverseLerp(0, (FocusLevel.HalfZBounds + FocusLevel.HalfYBounds) / 5f, extents);
        float depth = Mathf.Lerp(DepthMax, DepthMin, lerpPercent);
        float angle = Mathf.Lerp(AngleMax, AngleMin, lerpPercent);
        CameraEulerX = angle;
        CameraPosition = new Vector3(depth, averageCenter.y, averageCenter.x) + offset;
    }
    private void LateUpdate()
    {
        CalculateLocations();
        MoveCamera();
    }
}
