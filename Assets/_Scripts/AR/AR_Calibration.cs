using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;


public class AR_Calibration : MonoBehaviour
{

    [Header("Game Objects & Prefabs")]
    [SerializeField] GameObject obj;
    [SerializeField] GameObject SpawnRegion0;
    [SerializeField] GameObject SpawnRegion1;
    [SerializeField] GameObject SpawnRegion2;
    [SerializeField] GameObject SpawnRegion3;
    [SerializeField] GameObject SpawnRegion4;
    [SerializeField] GameObject SpawnRegion5;
    [SerializeField] GameObject ship1;
    [SerializeField] GameObject ship2;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject gunSpawnsObj;
    [SerializeField] Camera ArCam; 

    [Header("UI & Button Prefabs")]
    [SerializeField] GameObject startbttn;
    [SerializeField] GameObject closebttn;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject settingsbttn;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject crossHair;
    [SerializeField] Slider objSizeSlider;
    public Toggle hard;
    public Toggle medium;
    public Toggle easy;

    [Header("Multipliers")]
    [SerializeField] private float spaceStationSizeMultplier = 7f;
    [SerializeField] private float shipSizeMultiplier = .045f;
    [SerializeField] private float explSizeMultiplier = 7f;

    [Header("Game State Variables")]
    public bool GameStart = false;
    public string gameDifficultyState = "";

    public static float shipDistanceMultiplier;

    
    private void Awake()
    {
        startbttn.SetActive(false);
        closebttn.SetActive(false);
        settingsbttn.SetActive(false);
        gameUI.SetActive(false);
        settingsPanel.SetActive(false);
        crossHair.SetActive(false);

    }

    private void OnEnable()
    {
        obj = GameObject.FindGameObjectWithTag("Instantiate");

        SpawnRegion0 = obj.GetComponentsInChildren<Transform>()[1].gameObject;
        SpawnRegion1 = obj.GetComponentsInChildren<Transform>()[3].gameObject;
        SpawnRegion2 = obj.GetComponentsInChildren<Transform>()[5].gameObject;
        SpawnRegion3 = obj.GetComponentsInChildren<Transform>()[7].gameObject;
        SpawnRegion4 = obj.GetComponentsInChildren<Transform>()[9].gameObject;
        SpawnRegion5 = obj.GetComponentsInChildren<Transform>()[11].gameObject;


        if (obj != null)
        {
            objSizeSlider.value = .5f;
            ChangeObjSize();
        }

        SpawnRegion2.SetActive(false);
        SpawnRegion3.SetActive(false);
        SpawnRegion4.SetActive(false);
        SpawnRegion5.SetActive(false);

        hard.isOn = false;
        medium.isOn = false;


        easy.isOn = true;
        easy_switch(easy.isOn);

        closebttn.SetActive(true);
        settingsPanel.SetActive(true);

    }

    public void hard_switch(bool tog)
    {
        if (tog == true)
        {
            medium.isOn = false;
            easy.isOn = false;
            gameDifficultyState = "hard";

            float xpos = obj.transform.position.x - (.8f * spaceStationSizeMultplier * objSizeSlider.value);
            SpawnRegion0.transform.position = new Vector3(xpos, obj.transform.position.y, obj.transform.position.z);
            float xpos1 = obj.transform.position.x + (.8f * spaceStationSizeMultplier * objSizeSlider.value);
            SpawnRegion1.transform.position = new Vector3(xpos1, obj.transform.position.y, obj.transform.position.z);
            setting_difficulties(gameDifficultyState);
            float zpos2 = obj.transform.position.z - (.8f * spaceStationSizeMultplier * objSizeSlider.value);
            SpawnRegion2.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, zpos2);
            float zpos3 = obj.transform.position.z + (.8f * spaceStationSizeMultplier * objSizeSlider.value);
            SpawnRegion3.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, zpos3);
        }
    }
    public void medium_switch(bool tog)
    {
        if (tog == true)
        {
            hard.isOn = false;
            easy.isOn = false;
            gameDifficultyState = "medium";

            float xpos = obj.transform.position.x - (0.45f * spaceStationSizeMultplier * objSizeSlider.value);
            SpawnRegion0.transform.position = new Vector3(xpos, obj.transform.position.y, obj.transform.position.z);
            float xpos1 = obj.transform.position.x + (0.45f * spaceStationSizeMultplier * objSizeSlider.value);
            SpawnRegion1.transform.position = new Vector3(xpos1, obj.transform.position.y, obj.transform.position.z);
            setting_difficulties(gameDifficultyState);
            float zpos2 = obj.transform.position.z - (0.45f * spaceStationSizeMultplier * objSizeSlider.value);
            SpawnRegion2.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, zpos2);
            float zpos3 = obj.transform.position.z + (0.45f * spaceStationSizeMultplier * objSizeSlider.value);
            SpawnRegion3.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, zpos3);
        }
    }
    public void easy_switch(bool tog)
    {
        if (tog == true)
        {
            medium.isOn = false;
            hard.isOn = false;
            gameDifficultyState = "easy";

            float xpos = obj.transform.position.x - (0.3f * spaceStationSizeMultplier * objSizeSlider.value);
            SpawnRegion0.transform.position = new Vector3(xpos, obj.transform.position.y, obj.transform.position.z);
            float xpos1 = obj.transform.position.x + (0.3f * spaceStationSizeMultplier * objSizeSlider.value);
            SpawnRegion1.transform.position = new Vector3(xpos1, obj.transform.position.y, obj.transform.position.z);
            setting_difficulties(gameDifficultyState);
        }
    }

    void setting_difficulties(string state)
    {
        if (state == "easy")
        {
            SpawnRegion2.SetActive(false);
            SpawnRegion3.SetActive(false);
            SpawnRegion4.SetActive(false);
            SpawnRegion5.SetActive(false);

        }
        if (state == "medium")
        {
            SpawnRegion2.SetActive(true);
            SpawnRegion3.SetActive(true);
            SpawnRegion4.SetActive(false);
            SpawnRegion5.SetActive(false);
        }
        if (state == "hard")
        {
            SpawnRegion2.SetActive(true);
            SpawnRegion3.SetActive(true);
            SpawnRegion4.SetActive(true);
            SpawnRegion5.SetActive(true);
        }
    }

    public void StartButton()
    {
        GameStart = true;
        Time.timeScale = 1;
        startbttn.SetActive(false);
        gameUI.SetActive(true);
        crossHair.SetActive(true);

        Time.timeScale = 1;
        gunSpawnsObj.SetActive(true);

        
    }

    public void close()
    {
        startbttn.SetActive(true);
        closebttn.SetActive(false);
        settingsPanel.SetActive(false);
        settingsbttn.SetActive(true);
    }

    public void settings()
    {
        startbttn.SetActive(false);
        closebttn.SetActive(true);
        settingsPanel.SetActive(true);
        settingsbttn.SetActive(false);

        gunSpawnsObj.SetActive(false);
        GameStart = false;
        Time.timeScale = 0;
        
    }
   

    public void ChangeObjSize()
    {
        obj.transform.localScale = Vector3.one * objSizeSlider.value * spaceStationSizeMultplier;
        ship1.transform.localScale = Vector3.one * objSizeSlider.value * shipSizeMultiplier;
        ship2.transform.localScale = Vector3.one * objSizeSlider.value * shipSizeMultiplier;
        explosion.GetComponentsInChildren<Transform>()[1].localScale = Vector3.one * objSizeSlider.value * explSizeMultiplier;
        explosion.GetComponentsInChildren<Transform>()[2].localScale = Vector3.one * objSizeSlider.value * explSizeMultiplier;
        shipDistanceMultiplier = objSizeSlider.value;
        //Debug.Log(explosion.GetComponentsInChildren<Transform>()[1]);
        //Debug.Log(explosion.GetComponentsInChildren<Transform>()[2]);
    }

  
}
