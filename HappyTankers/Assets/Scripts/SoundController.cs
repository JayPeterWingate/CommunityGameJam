using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundController : MonoBehaviour
{
    [HideInInspector] static public SoundController Instance;
    [SerializeField] private AudioSource m_music;
    [SerializeField] private AudioSource m_ambient;
    [SerializeField] private AudioSource[] m_distantShooters;
    private int m_nextDistantShooterIdx = 0;

    [SerializeField] private AudioClip m_chip;
    [SerializeField] private AudioClip m_strings;
    [SerializeField] private AudioClip m_rock;

    [SerializeField] private AudioClip m_static;
    [SerializeField] private AudioClip m_rain;

    [SerializeField] private AudioClip[] m_distantFireClips;

    public AudioClip chirpSmallFire;
    public AudioClip chirpBigFire;
    public AudioClip chirpTeleport;
    public AudioClip chirpDie;
    public AudioClip chirpHitCity;

    private int m_distantFireQueue = 0;
    private AudioClip m_distantFireClip;
    private float m_distantFireVol;
    private float m_distantFireSpeed;
    private float m_distantFireTimer;
    private bool m_distantFireTurnedOn = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        m_music.loop = true;
        m_music.clip = m_chip;
        m_music.volume = 0.7f;
        m_music.Play();
        
        m_ambient.loop = true;
        m_ambient.clip = m_static;
        m_ambient.volume = 0f;
        m_ambient.Play();

        //Silence();
        //StartSombreState();
    }

    public void FadeStatic(float percStatic)
    {
        m_music.volume = 0.7f * (1 - percStatic);
        m_ambient.volume = 0.1f * percStatic;
    }

    public void Silence()
    {
        m_ambient.Stop();
        m_music.Stop();
    }

    public void StartSombreState()
    {
        m_ambient.loop = true;
        m_ambient.clip = m_rain;
        m_ambient.volume = 0.7f;
        m_ambient.Play();

        StartCoroutine(StartStringsDelayed());
        StartCoroutine(StartRockAndGunsDelayed());
    }

    private IEnumerator StartStringsDelayed()
    {
        yield return new WaitForSeconds(2);
        m_music.loop = false;
        m_music.clip = m_strings;
        m_music.volume = 0.7f;
        m_music.Play();
    }
    private IEnumerator StartRockAndGunsDelayed()
    {
        yield return new WaitForSeconds(25);
        m_music.loop = true;
        m_music.clip = m_rock;
        m_music.volume = 0.7f;
        m_music.Play();

        m_distantFireTurnedOn = true;
    }

    void Update()
    {
        if (m_distantFireTurnedOn)
        {
            if (m_distantFireQueue == 0)
            {
                if (Random.Range(0, 2000 * Time.deltaTime) < 1)
                {
                    m_distantFireQueue = (int)Random.Range(2, 6.5f);
                    m_distantFireSpeed = Random.Range(0.1f, 0.8f - (0.6f * (m_distantFireQueue / 6)));
                    m_distantFireClip = m_distantFireClips[(int)Random.Range(0, m_distantFireClips.Length - 0.001f)];
                    m_distantFireVol = Random.Range(0.1f, 1f);
                }
            }
            else
            {
                if (m_distantFireTimer < 0)
                {
                    m_distantFireQueue--;
                    if (m_distantFireQueue > 0)
                    {
                        DistantShooterFire(m_distantFireClip, m_distantFireVol);
                    }
                    m_distantFireTimer = m_distantFireSpeed;
                }
                m_distantFireTimer -= Time.deltaTime;
            }
        }
    }

    private void DistantShooterFire(AudioClip clip, float vol)
    {
        m_distantShooters[m_nextDistantShooterIdx].volume = vol;
        m_distantShooters[m_nextDistantShooterIdx].clip = clip;
        m_distantShooters[m_nextDistantShooterIdx].Play();
        m_nextDistantShooterIdx++;
        if (m_nextDistantShooterIdx + 1 == m_distantShooters.Length)
        {
            m_nextDistantShooterIdx = 0;
        }
    }
}
