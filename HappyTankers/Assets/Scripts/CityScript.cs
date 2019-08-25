using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityScript : BlockScript
{
    [SerializeField] private GameObject[] m_healthyCityPrefabs;
    [SerializeField] private GameObject[] m_brokenCityPrefabs;

    private GameObject[] m_healthyBuildings;
    private GameObject[] m_brokenBuildings;

    // Start is called before the first frame update
    void Start()
    {
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
    }
}
