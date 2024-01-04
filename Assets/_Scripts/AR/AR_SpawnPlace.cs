using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using TMPro;
using UnityEngine.UI;

public class AR_SpawnPlace : MonoBehaviour
{
    //bool spawnPlacing = false;
    //int buttoncount = 0;

    //[SerializeField] GameObject Spawnprefab_0;
    //[SerializeField] GameObject Spawnprefab_1;
    [SerializeField] GameObject gunSpawnsObj;
    [SerializeField] GameObject SpaceStation;
    [SerializeField] GameObject objPrefab;
    [SerializeField] GameObject replaceButtonPrefab;
    [SerializeField] GameObject continueButtonPrefab;
    //[SerializeField] GameObject arCam;

    ARRaycastManager raycastManager;
    ARPlaneManager planeManager;

    List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    [SerializeField] TextMeshProUGUI toggleText;

    public GameObject spawnManager;
    public GameObject ARSessionOrigin;

    private void Awake()
    {
        raycastManager = ARSessionOrigin.GetComponent<ARRaycastManager>();
        planeManager = ARSessionOrigin.GetComponent<ARPlaneManager>();
        spawnManager.GetComponent<AR_Obj_Manager>().enabled = false;
        spawnManager.GetComponent<AR_Calibration>().enabled = false;

        gunSpawnsObj.SetActive(false);
        raycastManager.enabled = true;

        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;

        
    }

    private void FingerDown(EnhancedTouch.Finger finger)
    {
        
        if (finger.index != 0)
            return;

        if (raycastManager.Raycast(finger.currentTouch.screenPosition, raycastHits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = raycastHits[0].pose;

            //If using pose.rotation instead of quaternion.identity, the space ship rotates but the spawn points dont (weid bug)
            objPrefab = Instantiate(SpaceStation, pose.position, Quaternion.identity);

            EnhancedTouch.Touch.onFingerDown -= FingerDown;
            EnhancedTouch.TouchSimulation.Disable();
            EnhancedTouch.EnhancedTouchSupport.Disable();
            
        }
        
    }

    public void ReplaceObjects()
    {
        if(objPrefab != null)
        {
            Destroy(objPrefab);
        }

        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    public void ContinueButton()
    {
        //For testing on pc
        //objPrefab = Instantiate(SpaceStation, Vector3.zero, Quaternion.identity);

        if (objPrefab != null)
        {
            toggleText.gameObject.SetActive(false);

            TogglePlaneDetection();
            spawnManager.GetComponent<AR_Obj_Manager>().enabled = true;
            spawnManager.GetComponent<AR_Calibration>().enabled = true;

            replaceButtonPrefab.gameObject.SetActive(false);
            continueButtonPrefab.gameObject.SetActive(false);
            this.enabled = false;
        }
        
    }

    void TogglePlaneDetection()
    {
        planeManager.enabled = !planeManager.enabled;

        foreach (ARPlane plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(planeManager.enabled);
        }
    }

    
}
