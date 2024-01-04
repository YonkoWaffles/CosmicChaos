using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


//Christian
public class Weapons : MonoBehaviour
{
    [SerializeField] private LineRenderer laser;
    [SerializeField] private Transform gunSpawn;
    [SerializeField] private RectTransform buttonRect;
    [SerializeField] private RectTransform Crosshair;
    [SerializeField] private Vector3 laserOffset;
    [SerializeField] private float maxDistance;
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private Vector3 rayOffset;
    [SerializeField] private GameObject missileFire;
    [SerializeField] private GameObject MissileCDBar;
    [SerializeField] private GameObject LaserCDBar;
    [SerializeField] private float missileCD = 1f;
    [SerializeField] private float nextMissile = .25f;
    [SerializeField] private float MaxLaserTime = 2f;
    private float laserTimer;
    private float laserDelay = .25f;
    private Vector3 localDirection;
    [SerializeField] bool pcFire;
    
    public static bool isMissile;
    public static bool isLaser;
    private bool laserSoundPlayed;
    private bool missileSoundPlayed;
    private bool rightSideTouch;
    

    private void Awake()
    {
        isMissile = false;
        isLaser = false;
        laser.enabled = false;
        laserSoundPlayed = false;
        missileSoundPlayed= false;

    }


    /// <summary>
    /// Enables the laser line renderer and plays the laser sound once
    /// </summary>
    private void LaserActivate()
    {
        laser.enabled = true;
        isLaser=true;
        
        if (!laserSoundPlayed)
        {
            audioSources[0].Play();
            laserSoundPlayed = true;
        }
    }

    /// <summary>
    /// Stops the laser and audio
    /// Resets the laser start and end positions
    /// </summary>
    private void LaserDeactivate()
    {
        laserDelay = .25f;
        laser.enabled = false;
        isLaser = false;
        laserSoundPlayed = false;
  
        audioSources[0].Stop();
        laser.SetPosition(0, gunSpawn.position);
        laser.SetPosition(1, gunSpawn.position);
        
    }

    private void MissileActivate()
    {
        isMissile = true;
        StartCoroutine(MissileCDBar.GetComponent<CDBar>().AnimateMissileBar(missileCD));
        if (!missileSoundPlayed)
        {
            audioSources[1].Play();
            missileSoundPlayed = true;
        }
    }

    private void MissileDeactivate()
    {
        isMissile = false;
        missileSoundPlayed = false;
        //audioSources[1].Stop();
    }

    /// <summary>
    /// Activates the laser if a stationary touch is found on the right side of the screen
    /// Not sure if the magnitude check was necessary, but it made it feel better imo
    /// </summary>
    private void Update()
    {
        if (Input.touchCount == 0 && laserTimer < MaxLaserTime && !Settings_setup.isPaused)
        {
            laserTimer += Time.deltaTime;
        }
        if (Input.touchCount > 0 && !Settings_setup.isPaused)
        {

            Touch touchInfo;

            touchInfo = Input.GetTouch(0);

            
            //if there is more than one finger detected, checks to see if the first finger detected is on the right side of the screen. If not, touchInfo gets reassigned to the second touch.
            if (Input.touchCount > 1 && RectTransformUtility.RectangleContainsScreenPoint(buttonRect, Input.GetTouch(1).position, Camera.main))
                touchInfo = Input.GetTouch(1);


            rightSideTouch = RectTransformUtility.RectangleContainsScreenPoint(buttonRect, touchInfo.position, Camera.main);

            Debug.Log("Touch count " + Input.touchCount);
            if (!isLaser && laserTimer < MaxLaserTime && !rightSideTouch)
            {
                laserTimer += Time.deltaTime;
                
            }


            if (!laser.enabled && (Time.time > nextMissile) && touchInfo.tapCount == 2 && rightSideTouch)
            {
                MissileActivate();
                Invoke("MissileDeactivate", .1f);
                nextMissile = Time.time + missileCD;

            }


            //Debug.Log("Touching " + touchInfo.fingerId.ToString());
            if ( (touchInfo.phase == TouchPhase.Stationary && rightSideTouch)
                || (touchInfo.phase == TouchPhase.Moved && rightSideTouch))
            {
                if (laserTimer >= 0f && laserDelay <= 0f)
                {
                    laserTimer -= Time.deltaTime;
                    LaserActivate();
                }
                else if (laserTimer <= 0f)
                {
                    LaserDeactivate();
                }
                if(!isLaser && laserDelay > 0f) 
                {
                    laserDelay -= Time.deltaTime;
                }

                //LaserActivate();

            }
            else if (touchInfo.phase == TouchPhase.Ended && rightSideTouch)
            {
                LaserDeactivate();
              
            }
            
        }

        //if (pcFire)
        //{
        //    //laserTimer -= Time.deltaTime;
        //    LaserActivate();
        //}
        //else
        //    LaserDeactivate();


        LaserCDBar.GetComponent<CDBar>().AnimateLaserBar(laserTimer / MaxLaserTime);

    }
    
    /// <summary>
    /// Sets the laser start position to the gun spawn transform with a small offset
    /// Sets the laser end position to the positon of the crosshair with a max distance to determin ray length
    /// </summary>
        private void FixedUpdate()
    {
        localDirection = transform.InverseTransformPoint(Crosshair.position) * maxDistance;
        
        if (!laser.enabled)
            return;

        else
        {

            laser.SetPosition(0, gunSpawn.localPosition + laserOffset);
            laser.SetPosition(1, localDirection);
  
        }

    }
}
