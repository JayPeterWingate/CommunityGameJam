using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class CityScript : BlockScript
{
    [SerializeField] private GameObject[] m_healthyCityPrefabs;
    [SerializeField] private GameObject[] m_brokenCityPrefabs;
    [SerializeField] private ParticleSystem m_fire;
    [SerializeField] private ParticleSystem m_smoke;
    [SerializeField] private ParticleSystem m_explosion;


    private GameObject[] m_healthyBuildings;
    private GameObject[] m_brokenBuildings;

	[SerializeField] private SpriteRenderer m_happySprite;
	[SerializeField] private SpriteRenderer m_onFireSprite;

    [SerializeField] private Animator m_FakeAIAnimation;
    [SerializeField] private Animator m_FakeAITeleportAnimation;
    [SerializeField] private AudioSource m_FakeAIAudio;

    public bool isDead = false;
	float m_redPerc = 0;

	// Start is called before the first frame update
	void Start()
    {
        if (m_FakeAIAnimation != null)
        {
            FilterManager.OnChange.AddListener(AnimateFakeAI);
        }

        m_healthyBuildings = new GameObject[4];
        m_brokenBuildings = new GameObject[4];

        m_healthyBuildings[0] = Instantiate(m_healthyCityPrefabs[Random.Range(0, m_healthyCityPrefabs.Length)]
            , transform.position + new Vector3(0.25f, 0, 0.25f), Quaternion.Euler(new Vector3(0, 90 * Random.Range(0, 4), 0)));
        m_healthyBuildings[1] = Instantiate(m_healthyCityPrefabs[Random.Range(0, m_healthyCityPrefabs.Length)]
            , transform.position + new Vector3(0.75f, 0, 0.25f), Quaternion.Euler(new Vector3(0, 90 * Random.Range(0, 4), 0)));
        m_healthyBuildings[2] = Instantiate(m_healthyCityPrefabs[Random.Range(0, m_healthyCityPrefabs.Length)]
            , transform.position + new Vector3(0.25f, 0, 0.75f), Quaternion.Euler(new Vector3(0, 90 * Random.Range(0, 4), 0)));
        m_healthyBuildings[3] = Instantiate(m_healthyCityPrefabs[Random.Range(0, m_healthyCityPrefabs.Length)]
            , transform.position + new Vector3(0.75f, 0, 0.75f), Quaternion.Euler(new Vector3(0, 90 * Random.Range(0, 4), 0)));

        m_brokenBuildings[0] = Instantiate(m_brokenCityPrefabs[Random.Range(0, m_brokenCityPrefabs.Length)]
            , transform.position + new Vector3(0.25f, 0, 0.25f), Quaternion.Euler(new Vector3(0, 90 * Random.Range(0, 4), 0)));
        m_brokenBuildings[1] = Instantiate(m_brokenCityPrefabs[Random.Range(0, m_brokenCityPrefabs.Length)]
            , transform.position + new Vector3(0.75f, 0, 0.25f), Quaternion.Euler(new Vector3(0, 90 * Random.Range(0, 4), 0)));
        m_brokenBuildings[2] = Instantiate(m_brokenCityPrefabs[Random.Range(0, m_brokenCityPrefabs.Length)]
            , transform.position + new Vector3(0.25f, 0, 0.75f), Quaternion.Euler(new Vector3(0, 90 * Random.Range(0, 4), 0)));
        m_brokenBuildings[3] = Instantiate(m_brokenCityPrefabs[Random.Range(0, m_brokenCityPrefabs.Length)]
            , transform.position + new Vector3(0.75f, 0, 0.75f), Quaternion.Euler(new Vector3(0, 90 * Random.Range(0, 4), 0)));

        m_healthyBuildings[0].transform.parent = m_dark.transform;
        m_healthyBuildings[1].transform.parent = m_dark.transform;
        m_healthyBuildings[2].transform.parent = m_dark.transform;
        m_healthyBuildings[3].transform.parent = m_dark.transform;
        m_brokenBuildings[0].transform.parent = m_dark.transform;
        m_brokenBuildings[1].transform.parent = m_dark.transform;
        m_brokenBuildings[2].transform.parent = m_dark.transform;
        m_brokenBuildings[3].transform.parent = m_dark.transform;

        SetBreakCity(false);
		m_level.GetComponent<LevelProgression>().RegisterCity();
        //StartCoroutine(TestBreak());

        if (isDead)
        {
            SetBreakCity(true);
        }
    }

    public void AnimateFakeAI(bool isHappy)
    {
        m_FakeAIAnimation.SetBool("AnimateFakeAI", true);
    }
    public void AnimateFakeAITeleport()
    {
        m_FakeAITeleportAnimation.SetBool("TeleportOut", true);
        m_FakeAIAudio.clip = SoundController.Instance.chirpTeleport;
        m_FakeAIAudio.Play();
        //TODO Play sound
    }

    public void AnimateFakeAIShoot()
    {
        //TODO Play sound
    }

    override public void WasHit(int strength)
	{
		if (isDead == false)
		{
			m_level.GetComponent<LevelProgression>().DestroyCity();
			SetBreakCity(true);
			isDead = true;
		}
		
		if (gameObject.activeInHierarchy)
		{
			StartCoroutine(TakeHit());
		}
		
    }
	IEnumerator TakeHit()
	{
		float redPercent = 1;
		while (redPercent > 0)
		{
			yield return new WaitForEndOfFrame();
			redPercent = Mathf.Max(0, redPercent - 2 * Time.deltaTime);
			m_onFireSprite.color = new Vector4(0.5f, 0.5f - redPercent, 0.5f - redPercent, 0.5f - 0.5f * redPercent);
		}
	}
	private IEnumerator TestBreak()
    {
        yield return new WaitForSeconds(5);
        SetBreakCity(true);
    }

    private void SetBreakCity(bool broken)
    {
        m_healthyBuildings[0].SetActive(!broken);
        m_healthyBuildings[1].SetActive(!broken);
        m_healthyBuildings[2].SetActive(!broken);
        m_healthyBuildings[3].SetActive(!broken);

        m_brokenBuildings[0].SetActive(broken);
        m_brokenBuildings[1].SetActive(broken);
        m_brokenBuildings[2].SetActive(broken);
        m_brokenBuildings[3].SetActive(broken);

		m_happySprite.enabled = !broken;
		m_onFireSprite.enabled = broken;

        if (broken)
        {
            m_fire.Play();
            m_smoke.Play();
            m_explosion.Play();
        }
		// GetComponent<NavMeshObstacle>().carving = !broken;
    }
}
