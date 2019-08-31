using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TankController : MonoBehaviour
{
	public Vector2 targetDirection;
	public Vector3 turretTarget;
	public bool boost;
	public UnityEvent fireEvent = new UnityEvent();
    public UnityEvent strongFireEvent = new UnityEvent();
	public UnityEvent shieldEvent = new UnityEvent();
	public Color color;
	public bool isAI;
	public virtual void SetCursor(bool happy, bool ready) { }

	public int lives;
}

public class TankScript : MonoBehaviour
{
    public static float TeleportWaitTime = 1.5f;
    public static List<TankScript> TankList = new List<TankScript>();
    [SerializeField] Transform m_leftTread;
    [SerializeField] Transform m_rightTread;
    [SerializeField] TankController m_controller;
    [SerializeField] float m_horsePower;
    [SerializeField] float m_rotatePower;
    [SerializeField] float m_turretRotateSpeed;
    [SerializeField] Transform m_turret;
    [SerializeField] Transform m_bulletSpawn;
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] GameObject m_strongBulletPrefab;
    [SerializeField] List<GameObject> m_bulletList;
    [SerializeField] Transform m_shield;
    [SerializeField] Renderer[] m_renderers;
    [SerializeField] SpriteRenderer[] m_sprites;
    [SerializeField] SpriteRenderer[] m_teleportSquares;
    [SerializeField] Animator m_animator;
    [SerializeField] AudioSource m_tankAudioHappy;
    [SerializeField] AudioSource m_turretAudioHappy;
    bool m_isFiring = false;
    bool m_isShielding = false;
    bool m_isTakingDamage = false;
    Vector3 m_currentTarget;
    Rigidbody m_body;
    public Color color;
    public bool paused = false;
	Vector3 m_lastDirection = new Vector3(0,0,1);
	[SerializeField] float m_boostMultiplier;

	// Start is called before the first frame update
	void Start()
    {
        TankList.Add(this);
        m_currentTarget = new Vector3();
        m_body = GetComponent<Rigidbody>();
        if (m_controller.fireEvent != null)
        {
            m_controller.fireEvent.AddListener(FireMainBattleCannon);
        }
        if (m_controller.strongFireEvent != null)
        {
            m_controller.strongFireEvent.AddListener(FireSecondaryStrongBattleCannon);
        }
        if (m_controller.shieldEvent != null)
        {
            m_controller.shieldEvent.AddListener(ActiveShield);
        }

        for (int i = 0; i < m_renderers.Length; i++)
        {
            m_renderers[i].material.color = m_controller.color;
        }
        for (int i = 0; i < m_sprites.Length; i++)
        {
            m_sprites[i].color = m_controller.color;
        }
        color = m_controller.color;
        m_bulletList = new List<GameObject>();

        m_shield.gameObject.SetActive(false);

        SetVisuals(true);
        FilterManager.OnChange.AddListener(SetVisuals);
    }
    void SetVisuals(bool isHappy)
    {
        /*for(int i = 0; i < m_renderers.Length; i++)
		{
			bool shouldBeActive = isHappy ?
				m_renderers[i].gameObject.layer == 9
				:
				m_renderers[i].gameObject.layer == 10||
				m_renderers[i].gameObject.layer == 12;
			m_renderers[i].enabled = shouldBeActive;
		}*/
        m_controller.SetCursor(isHappy, m_isFiring);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_controller && !paused)
        {
			Vector3 direction = new Vector3(m_controller.targetDirection.x, 0, m_controller.targetDirection.y).normalized;
			float power = m_horsePower;
			if (m_body.velocity.magnitude < 0.05)
			{
				power *= m_boostMultiplier;
			}
			if (!m_controller.isAI)
			{
				m_body.AddForce(direction * power);
			}
			if (direction == new Vector3())
			{
				direction = m_lastDirection;
			} else
			{
				m_lastDirection = direction;

			}
			
			
			transform.rotation = (Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), Time.time * m_rotatePower));

			// [TODO] get a smoother looking rotation
            Vector3 targetPosition = new Vector3(m_controller.turretTarget.x, m_turret.position.y, m_controller.turretTarget.z);
            var dir = targetPosition - m_turret.position;
            dir.y = 0.0f;
            m_turret.rotation = Quaternion.RotateTowards(m_turret.rotation, Quaternion.LookRotation(dir), Time.time * m_turretRotateSpeed);
			

		}


    }

    public void TeleportOut(bool death = false)
    {
        for (int i =0; i < m_teleportSquares.Length; i++)
        {
            m_teleportSquares[i].color = death ? Color.red : Color.white;
        }
        paused = true;
        DestroyBullets();
        m_animator.SetBool("TeleportOut", true);
        m_tankAudioHappy.clip = death ? SoundController.Instance.chirpDie : SoundController.Instance.chirpTeleport;
        m_tankAudioHappy.Play();
    }
    public void TeleportIn()
    {
        for (int i = 0; i < m_teleportSquares.Length; i++)
        {
            m_teleportSquares[i].color = Color.white;
        }
        m_animator.SetBool("TeleportOut", false);
        m_animator.SetBool("TeleportIn", true);
        m_tankAudioHappy.clip = SoundController.Instance.chirpTeleport;
        m_tankAudioHappy.Play();
        StartCoroutine(TPInDelayed());
    }
    private IEnumerator TPInDelayed()
    {
        yield return new WaitForSeconds(TeleportWaitTime);
        paused = false;
        m_animator.SetBool("TeleportIn", false);
    }

    private void FireMainBattleCannon()
    {
        if (!m_isFiring && !m_isShielding && !m_isTakingDamage && !paused)
        {
            try
            {
                PlayerScript playerController = (PlayerScript)m_controller;
                StartCoroutine(FireBullets(10, 0.05f, 1.0f, false));
            } catch {
                StartCoroutine(FireBullets(1, 0.05f, 0.5f, false));
            };

        }

    }
    private void FireSecondaryStrongBattleCannon()
    {
        if (!m_isFiring && !m_isShielding && !m_isTakingDamage && !paused)
        {
            StartCoroutine(FireBullets(1, 0.05f, 1.0f, true));
        }

    }
    private IEnumerator FireBullets(int bulletCount, float spawnGap, float cooldown, bool strong)
    {
        m_isFiring = true;
        m_controller.SetCursor(FilterManager.IsHappy, false);
        for (int i = 0; i < bulletCount; i++)
        {
            if (strong)
            {
                BouncyBulletStrong bullet = Instantiate(m_strongBulletPrefab, m_bulletSpawn.position, m_turret.rotation).GetComponent<BouncyBulletStrong>();
                bullet.InitialSetup(bullet.transform.forward);
                m_bulletList.Add(bullet.gameObject);
            }
            else
            {
                BouncyBullet bullet = Instantiate(m_bulletPrefab, m_bulletSpawn.position, m_turret.rotation).GetComponent<BouncyBullet>();
                bullet.InitialSetup(bullet.transform.forward);
                m_bulletList.Add(bullet.gameObject);
            }
            yield return new WaitForSeconds(spawnGap);
        }
        yield return new WaitForSeconds(cooldown);
        m_controller.SetCursor(FilterManager.IsHappy, true);
        m_isFiring = false;
    }
    private void ActiveShield()
    {
        if (!m_isFiring && !m_isShielding && !m_isTakingDamage)
        {
            StartCoroutine(StartShield(0.1f, 1f, 0.1f, new Vector3(5f, 5f, 5f)));
        }
    }
    private IEnumerator StartShield(float rise, float stay, float close, Vector3 size)
    {
        m_isShielding = true;
        float current = 0;
        m_shield.gameObject.SetActive(true);
        while (current < rise)
        {
            m_shield.transform.localScale = Vector3.Lerp(new Vector3(), size, current / rise);
            current += Time.deltaTime;
            DestroyBulletsInShield(m_shield.transform.localScale.x * (1.55f / 5));
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(stay);
        current = 0;
        while (current < close)
        {
            current += Time.deltaTime;
            m_shield.transform.localScale = Vector3.Lerp(size, new Vector3(), current / close);
            DestroyBulletsInShield(m_shield.transform.localScale.x * (1.55f / 5));
            yield return new WaitForEndOfFrame();
        }

        m_shield.gameObject.SetActive(false);
        m_isShielding = false;
    }
    public void DestroyBullets()
    {
        m_bulletList.ForEach((GameObject obj) => Destroy(obj));
    }

    public void DestroyBulletsInShield(float shieldRad)
    {
        m_bulletList.ForEach((GameObject obj) =>
        {
            if (obj != null && (new Vector3(obj.transform.position.x, 0, obj.transform.position.z) - new Vector3(m_shield.position.x, 0, m_shield.position.z)).magnitude < shieldRad)
            {
                Destroy(obj);
            }
        });
    }

	public void Hit(GameObject bullet)
	{
		if (!m_isTakingDamage)
		{
			if (m_controller.lives > 0)
			{
				Destroy(bullet);
				m_controller.lives -= 1;
                if (FilterManager.IsHappy)
                {
                    m_tankAudioHappy.clip = SoundController.Instance.chirpHitCity;
                    m_tankAudioHappy.Play();
                }
                
			}
			else
			{
				try
				{
					PlayerScript player = (PlayerScript)m_controller;
					if (FilterManager.IsHappy)
					{
                        StartCoroutine(HandleHappyReset());
                        player.lives = 3;
					} else
					{
						FilterManager.TriggerDeath();
					}
					
				}
				catch
				{
					// Controller is not a player
					TankList.Remove(this);
					Destroy(gameObject);
				}
			}
			print("IM HIT");
			m_isTakingDamage = true;
			Destroy(bullet);
			StartCoroutine(ResetDamage(1));
		}
	}

    private IEnumerator HandleHappyReset()
    {
        DestroyBullets();
        TeleportOut(true);
        m_tankAudioHappy.clip = SoundController.Instance.chirpDie;
        m_tankAudioHappy.Play();

        yield return new WaitForSeconds(TeleportWaitTime);

        PlayerScript player = (PlayerScript)m_controller;
        transform.position = player.startPos;
        TeleportIn();
    }

	IEnumerator ResetDamage(float resetTime)
	{
		bool onSwitch = true;
		for(int counter = 0; counter < 6; counter++)
		{
			yield return new WaitForSeconds(resetTime / 6);
			onSwitch = !onSwitch;
			for(int i = 0; i < m_sprites.Length; i++)
			{
				m_sprites[i].enabled = onSwitch;
			}
		}
		m_isTakingDamage = false;
	}

	private void OnDestroy()
	{
		TankList.Remove(this);
	}
}