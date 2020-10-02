using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour {
    //fields set in the unity inspector pane
    public int numAsteroids = 40; //# of clouds to make
    public GameObject[] asteroidPrefabs; //the prefabs for the clouds
    public Vector3 asteroidPosMin; //min position of each cloud
    public Vector3 asteroidPosMax; //max position of each cloud
    public float asteroidScaleMin = 1; //min scale of each cloud
    public float asteroidScaleMax = 5; //max scale of each cloud
    public float asteroidSpeedMult = 0.5f; //adjusts speed of clouds

    public bool ______________________________;

    //fields set dynamically
    public GameObject[] asteroidInstances;

    void Awake()
    {
        //make an array large enough to hold all the Cloud instances
        asteroidInstances = new GameObject[numAsteroids];
        //find the CloudAnchor parent GameObject
        GameObject anchor = GameObject.Find("AsteroidAnchor");
        //iterate through and make Cloud_s
        GameObject asteroid;
        for (int i = 0; i < numAsteroids; i++)
        {
            //pick an int between 0 and cloudPrefabs.length-1
            //random.range will not ever pick as high as the top number
            int prefabNum = Random.Range(0, asteroidPrefabs.Length);
            //make an instance
            asteroid = Instantiate(asteroidPrefabs[prefabNum]) as GameObject;
            //position cloud
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(asteroidPosMin.x, asteroidPosMax.x);
            cPos.y = Random.Range(asteroidPosMin.y, asteroidPosMax.y);
            //scale cloud
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(asteroidScaleMin, asteroidScaleMax, scaleU);
            //smaller clouds (with smaller scaleU) should be nearer the ground
            cPos.y = Mathf.Lerp(asteroidPosMin.y, cPos.y, scaleU);
            //smaller clouds should be further away
            cPos.z = 100 - 90 * scaleU;
            //Apply these transforms to the cloud
            asteroid.transform.position = cPos;
            asteroid.transform.localScale = Vector3.one * scaleVal;
            //make cloud a child of the anchor
            asteroid.transform.parent = anchor.transform;
            //add the cloud to cloudInstances
            asteroidInstances[i] = asteroid;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //iterate over each cloud that was created
        foreach (GameObject asteroid in asteroidInstances)
        {
            //get the cloud scale and position
            float scaleVal = asteroid.transform.localScale.x;
            Vector3 cPos = asteroid.transform.position;
            //move larger clouds faster
            cPos.x -= scaleVal * Time.deltaTime * asteroidSpeedMult;
            //if a cloud has moved too far to the left
            if (cPos.x <= asteroidPosMin.x)
            {
                //move it to the far right
                cPos.x = asteroidPosMax.x;
            }

            //apply the new position to cloud
            asteroid.transform.position = cPos;

        }
    }
}