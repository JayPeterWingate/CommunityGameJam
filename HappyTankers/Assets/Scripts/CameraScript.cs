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
		FilterManager.OnChange.AddListener(RF1_FreezeMovement);
		FilterManager.OnDeath.AddListener(ShowBlueScreen);
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

	public void RF1_FreezeMovement(bool isHappy)
    {
        SoundController.Instance.Silence();
        for (int i = 0; i < TankScript.TankList.Count; i++)
        {
            TankScript.TankList[i].paused = true;
        }
        StartCoroutine(RF2_ActuallyRemoveFilter());
    }

    private IEnumerator RF2_ActuallyRemoveFilter()
    {
        yield return new WaitForSeconds(4f);
        m_camera.cullingMask = 1 << 10 | 1 << 13 | 1 << 15 | 1 << 8;
        m_animator.SetBool("RemoveFilter", true);
        StartCoroutine(RF3_SombreFullStart());
    }

    private IEnumerator RF3_SombreFullStart()
	{
		yield return new WaitForSeconds(1f);
        SoundController.Instance.StartSombreState();
        m_camera.cullingMask = 1 << 0 | 1 << 10 | 1 << 12 | 1 << 13 | 1 << 15 | 1 << 8;
        for (int i = 0; i < TankScript.TankList.Count; i++)
        {
            TankScript.TankList[i].paused = false;
        }
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
