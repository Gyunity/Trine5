using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLaserFire_GH : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(laserPrefab, firePoint.transform.position, firePoint.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
