using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AR_Obj_Manager : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] GameObject SpawnRegion0; //We are going to call an function that checks if the game objects are active and if they are we commmunicate that to another script. 
    [SerializeField] GameObject SpawnRegion1;
    [SerializeField] GameObject SpawnRegion2;
    [SerializeField] GameObject SpawnRegion3;
    [SerializeField] GameObject SpawnRegion4;
    [SerializeField] GameObject SpawnRegion5;
    [SerializeField] GameObject ship1;
    [SerializeField] GameObject ship2;

    float timer = 0f;
    float setTime = 5f;

    AR_Calibration ar_Calibration;

    private void OnEnable()
    {
        
        ar_Calibration = GetComponent<AR_Calibration>();
        obj = GameObject.FindGameObjectWithTag("Instantiate");
        SpawnRegion0 = obj.GetComponentsInChildren<Transform>()[1].gameObject;
        SpawnRegion1 = obj.GetComponentsInChildren<Transform>()[3].gameObject;
        SpawnRegion2 = obj.GetComponentsInChildren<Transform>()[5].gameObject;
        SpawnRegion3 = obj.GetComponentsInChildren<Transform>()[7].gameObject;
        SpawnRegion4 = obj.GetComponentsInChildren<Transform>()[9].gameObject;
        SpawnRegion5 = obj.GetComponentsInChildren<Transform>()[11].gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        isSpawnActive();
       
    }
    void isSpawnActive()
    {
        if (SpawnRegion0.activeInHierarchy)
        {
            startSpawnCycle();
        }
    }

    void startSpawnCycle()
    {
        timer += Time.deltaTime;
        //text.text = "Time: " + timer;
        if (timer > setTime) // Basic set up to spawn ships every 10 seconds
        {
            SpawnShips();
        }
    }

    void SpawnShips()
    {
        if (ar_Calibration.GameStart == true)
        {
            if (ar_Calibration.gameDifficultyState == "easy")
            {
                Instantiate(ship1, SpawnRegion0.transform.position, SpawnRegion0.transform.rotation);
                Instantiate(ship2, SpawnRegion1.transform.position, SpawnRegion1.transform.rotation);
                timer = 0;
            }
            if (ar_Calibration.gameDifficultyState == "medium")
            {
                Instantiate(ship1, SpawnRegion0.transform.position, SpawnRegion0.transform.rotation);
                Instantiate(ship2, SpawnRegion1.transform.position, SpawnRegion1.transform.rotation);
                Instantiate(ship1, SpawnRegion2.transform.position, SpawnRegion2.transform.rotation);
                Instantiate(ship2, SpawnRegion3.transform.position, SpawnRegion3.transform.rotation);
                timer = 0;
            }
            if (ar_Calibration.gameDifficultyState == "hard")
            {
                Instantiate(ship1, SpawnRegion0.transform.position, SpawnRegion0.transform.rotation);
                Instantiate(ship2, SpawnRegion1.transform.position, SpawnRegion1.transform.rotation);
                Instantiate(ship1, SpawnRegion2.transform.position, SpawnRegion2.transform.rotation);
                Instantiate(ship2, SpawnRegion3.transform.position, SpawnRegion3.transform.rotation);
                Instantiate(ship1, SpawnRegion4.transform.position, SpawnRegion4.transform.rotation);
                Instantiate(ship2, SpawnRegion5.transform.position, SpawnRegion5.transform.rotation);
                timer = 0;
            }
        }
    }

    
}
