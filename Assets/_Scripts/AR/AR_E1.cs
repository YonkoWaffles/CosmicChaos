using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class AR_E1 : MonoBehaviour
{
    GameObject TargetCenter;// The Target is the Spacestation
    public Transform[] Targets;
    float distance; //Distance is the space bewtween the ship and the Spacestation
    bool inRange = false;//InRange is trueif the spaceship is within a certain distance of the spaceship
    private float requiredDistance;

    float time;//Basic timer to control how fast the ship shoots
    public Transform rocketSpawn;// Location where the rocket will shoot 
    public Rigidbody rocket;// Name of rocket projecctile

    [SerializeField] Transform currentTargetTransform;

    [SerializeField] float speed = .5f;//Speed of the ship
    

    /// <summary>
    /// Shoot method is a basic method for ship to fire its weapon every 5 seconds
    /// </summary>
    void Shoot()
    {
        
        if (time > 10)
        {
            time = 0;
            transform.LookAt(TargetCenter.transform);
            Instantiate(rocket, rocketSpawn.transform.position , rocketSpawn.transform.rotation);
            
        }
    }

    /// <summary>
    /// Start is used  to locate the the player's "HomeBase" when the ship spawns
    /// doing this allows the ships to not sit as "unactivated" game objects in the game and only spawn/take up 
    /// space when needed
    /// </summary>
    void Start()
    {
        TargetCenter = GameObject.FindGameObjectWithTag("Instantiate");
        Targets = new Transform[6];
        rocket.transform.localScale = Vector3.one * AR_Calibration.shipDistanceMultiplier * .05f;
        int count = 0;
        for (int t = 0; t < TargetCenter.GetComponentsInChildren<Transform>().Length; t++)
        {
            
            if(TargetCenter.GetComponentsInChildren<Transform>()[t].CompareTag("Target"))
            {
                Targets[count] = TargetCenter.GetComponentsInChildren<Transform>()[t];
                count++;

                //Debug.Log("count " + count);
                //Debug.Log("Children " + TargetCenter.GetComponentsInChildren<Transform>()[t].name);
                //Debug.Log("Targets " + Targets[0] + " " + Targets[1] + " " + Targets[2]);
                if (count >= Targets.Length)
                    break;
            }
        }

        currentTargetTransform = Targets[Random.Range(0, Targets.Length)];
        requiredDistance = AR_Calibration.shipDistanceMultiplier;
        
    }

    /// <summary>
    /// Just Verication and Adjusting things. 
    ///
    /// </summary>
    void Update()
    {
        
        float move = speed * Time.deltaTime; //move is made and assigned a value to control how fast the ship moves

        if (TargetCenter.activeInHierarchy)
        {
            distance = Vector3.Distance(TargetCenter.transform.position, this.transform.position);//distance between the ship is constantly calculated

            if (distance <= requiredDistance) // If distance is less than 5, the scripts sets inRange to true which procs another statement and the ship begins to shoot
            {
                
                inRange = true;
                Shoot();
            }

            if (!inRange)// if inRange is true the ship stops moving towardsthe homebase
            {
                //Debug.Log("this is being reached");
                
                transform.LookAt(currentTargetTransform.position);
                transform.position = Vector3.Lerp(transform.position, currentTargetTransform.position, move);// the stoping distance is an offset of where the HomeBase is.
            }
        }

        time += Time.deltaTime;//Updating time...
    }
}
