using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2AI : MonoBehaviour
{
    [SerializeField] GameObject Target;// The Target is the Spacestation
    float distance; //Distance is the space bewtween the ship and the Spacestation
    bool inRange = false;//InRange is trueif the spaceship is within a certain distance of the spaceship

    float time;//Basic timer to control how fast the ship shoots
    public Transform rocketSpawn;// Location where the rocket will shoot 
    public GameObject rocket;// Name of rocket projecctile
    int heightVal;
    float offsetVal;
    Vector3 destination;

    [SerializeField] private float speed = 100f;//Speed of the ship


    /// <summary>
    /// Shoot method is a basic method for ship to fire its weapon every 5 seconds
    /// </summary>
    void Shoot()
    {
        if (time > 5)
        {
            time = 0;
            transform.LookAt(Target.transform);
            Instantiate(rocket, rocketSpawn.transform.position+(transform.forward*100), rocketSpawn.transform.rotation);
        }
    }

    /// <summary>
    /// Awake is used  to locate the the player's "HomeBase" when the ship spawns
    /// doing this allows the ships to not sit as "unactivated" game objects in the game and only spawn/take up 
    /// space when needed
    /// </summary>
    void Awake()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
        Target = GameObject.FindGameObjectWithTag("HomeBase");
        heightVal = Random.Range(-1000, 1000);
        offsetVal = Random.Range(Target.transform.position.x - 1000, Target.transform.position.x - 700);
        destination = new Vector3(offsetVal, heightVal, Target.transform.position.z);
    }



    /// <summary>
    /// Just Verication and Adjusting things. 
    ///
    /// </summary>
    void Update()
    {
        float move = speed * Time.deltaTime; //move is made and assigned a value to control how fast the ship moves
        

        if (this.transform.position == destination) // If distance is less than 5, the scripts sets inRange to true which procs another statement and the ship begins to shoot
        {
            inRange = true;
            Shoot();
        }

        if (!inRange)// if inRange is not false the ship stops moving towardsthe homebase
        {
            //Debug.Log("this is being reached");
            this.transform.position = Vector3.MoveTowards(transform.position, destination, move);// the stoping distance is an offset of where the HomeBase is.
        }

        time += Time.deltaTime;//Updating time...
    }
}
