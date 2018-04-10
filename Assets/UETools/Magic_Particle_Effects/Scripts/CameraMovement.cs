using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	private float m_CameraSpeed			= 0.0f;
	private Vector3 m_CameraPosition	= new Vector3(0.0f, 0.0f, -2.5f);

	private GUIStyle m_Style	= new GUIStyle();

	void OnGUI()
	{

		GUI.Label(new Rect(0 , 0, Screen.width, 100), new GUIContent("Use LEFT and RIGHT arrow to move with the camera."), m_Style);
	}

	void Start()
	{
		m_Style.fontSize = 32;
		m_Style.normal.textColor = Color.white;
		m_Style.alignment = TextAnchor.UpperCenter;
	}

	void Update()
	{
		m_CameraSpeed += (0 - m_CameraSpeed) * 1.5f * Time.deltaTime;

		if(Input.GetKey(KeyCode.LeftArrow))
		{
			m_CameraSpeed = Mathf.Max(m_CameraSpeed - 10.0f * Time.deltaTime, -10.0f);
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			m_CameraSpeed = Mathf.Min(m_CameraSpeed + 10.0f * Time.deltaTime, 10.0f);
		}

		m_CameraPosition.x = Mathf.Clamp(m_CameraPosition.x + m_CameraSpeed * Time.deltaTime, -34.0f, 33.0f);

		gameObject.transform.position = m_CameraPosition;
	}
}
