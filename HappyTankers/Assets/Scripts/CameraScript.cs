using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Animator m_animator;
    float m_timer = 0;
    Vector3 m_targetPos;
	[SerializeField]Camera m_camera;
	[SerializeField] GameObject m_transitionaryCamera;
	[SerializeField] GameObject m_plane;
	[SerializeField] GameObject m_border;
	[SerializeField] Texture2D m_darkCursor;
	[SerializeField] Texture2D m_happyCursor;
	public GameObject m_blueScreen;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_targetPos = transform.position;
		float verticleHeight = m_camera.orthographicSize * 2.0f;
		float horizontalHeight = verticleHeight * Screen.width / Screen.height;

		m_camera.cullingMask = 1 << 0 | 1 << 10;
		m_plane.transform.localScale = new Vector3(horizontalHeight / 10, 1, verticleHeight / 10);
		//m_camera.cullingMask = 1 << 9 | 1 << 8;
		m_blueScreen.SetActive(false);
		FilterManager.OnChange.AddListener(RemoveFilter);
		FilterManager.OnDeath.AddListener(ShowBlueScreen);
		Cursor.SetCursor(m_happyCursor, new Vector2(32,32),CursorMode.Auto);
    }

	private void ShowBlueScreen()
	{
		StartCoroutine(BlueScreenSequence());
	}

	private IEnumerator BlueScreenSequence()
	{
		m_blueScreen.SetActive(true);
		yield return new WaitForSeconds(2);
		Application.Quit();
	}

	public void RemoveFilter(bool isHappy)
    {
		StartCoroutine(UpdateCullMask());
		m_camera.cullingMask = 1 << 10 | 1 << 13 | 1 << 15 | 1 << 8;
		m_animator.SetBool("RemoveFilter",!isHappy);

		Cursor.SetCursor(m_darkCursor, new Vector2(32, 32), CursorMode.Auto);
	}
	private IEnumerator UpdateCullMask()
	{
		yield return new WaitForSeconds(1f);
		m_camera.cullingMask = 1 << 0 | 1 << 10 | 1 << 12 | 1 << 13 | 1 << 15 | 1 << 8;
	}
	public void OnFilterRemoved()
	{
		m_transitionaryCamera.SetActive(false);
		m_plane.SetActive(false);
		m_border.SetActive(false);
		m_camera.cullingMask = 1 << 0 | 1 << 10 | 1 << 12 | 1 << 13 | 1 << 15 | 1 << 8;
	}


	public void LerpMoveFocus(Vector3 newPos)
    {
        m_targetPos = newPos;
    }

    // Update is called once per frame
    void Update()
    {

        if ((m_targetPos - transform.position).magnitude > 0.1f)
        {
            transform.position += (m_targetPos - transform.position) * 1 * Time.deltaTime;
        }
        else
        {
            m_targetPos = transform.position;
        }
    }
}
