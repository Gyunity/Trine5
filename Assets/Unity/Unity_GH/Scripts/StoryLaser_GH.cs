using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLaser_GH : MonoBehaviour
{
    public GameObject firePoint;
    public float maxLength;
    public GameObject laserPrefab;

    private GameObject laser;


    // Start is called before the first frame update
    void Start()
    {
        laser = Instantiate(laserPrefab, firePoint.transform.position, firePoint.transform.rotation);
        laser.transform.parent = transform;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
