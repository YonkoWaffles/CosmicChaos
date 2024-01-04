using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Xml;
using System.IO;
using System;
using UnityEngine.Timeline;

public class Settings_setup : MonoBehaviour
{
    public GameObject inGameMenu;
    public GameObject instructionsMenu;
    public Slider soundFx_Slider;
    public Slider backFx_Slider;
    public Slider master_Slider;
    public Slider sens_Slider;

    public AudioMixer mixer;

    public AudioSource[] audioSources;
    public static bool isPaused;
  
    //Saving variables
    XmlDocument settings = new XmlDocument();
    string templateFile = "SettingsTemplate.xml";
    string directory;
    string savedFile = "SavedSettings.xml";
    TextAsset temp;

    private void Awake()
    {
        
        //These if statements create the needed files in the persistentDataPath if they don't already exist
        directory = Application.persistentDataPath + "/XML/";
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        if (!File.Exists(directory + templateFile))
        {
            temp = Resources.Load("XML/SettingsTemplate") as TextAsset;
            //string tempString = temp.text;
            //string[] data = tempString.Split(Environment.NewLine);
            StreamWriter sw = new StreamWriter(directory + templateFile, true);
            sw.Write(temp.text);
            sw.Close();
            temp = Resources.Load("XML/SavedSettings") as TextAsset;
            sw = new StreamWriter(directory + savedFile, true);
            sw.Write(temp.text);
            sw.Close();

        }

        isPaused = false;

        //Loads the template XML file and applies the default values in the file. May overwrite above PlayerPrefs code.
        settings.LoadXml(File.ReadAllText(directory + templateFile));

        LoadSettings();

    }

    // Start is called before the first frame update
    void Start()
    {
        
        //Assuming the values have been changed, this will load in the saved settings and apply them.
        settings.LoadXml(File.ReadAllText(directory + savedFile));
        LoadSettings();
        inGameMenu.SetActive(false);
        if(SceneManager.GetActiveScene().name == "OpeningScene")
            instructionsMenu.SetActive(false);
        
    }


    public void SetBackgroundVolume()
    {
        mixer.SetFloat("Background", Mathf.Log10(backFx_Slider.value) * 20);
    }

    public void SetSFXVolume()
    {
        mixer.SetFloat("SFX", Mathf.Log10(soundFx_Slider.value) * 20);
    }

    public void SetMasterVolume()
    {
        mixer.SetFloat("Master", Mathf.Log10(master_Slider.value) * 20);
    }

    public void SetSens()
    {
        CameraController.cameraSpeed = sens_Slider.value * 40f;
    }

    public void openMenu()
    {
        inGameMenu.SetActive(true);

        isPaused = true;

        if (SceneManager.GetActiveScene().name == "GameScene")
            Time.timeScale = 0;
    }

    public void closeMenu()
    {
        SaveSettings();
        isPaused = false;
        inGameMenu.SetActive(false);
        if (SceneManager.GetActiveScene().name == "GameScene")
            Time.timeScale = 1;
    }

    public void OpenInstructions()
    {
        instructionsMenu.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructionsMenu.SetActive(false);
    }

    public void quitToMain()
    {
        SaveSettings();
        SceneManager.LoadScene("OpeningScene");

    }

    public void StartGame()
    {
 
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }

    public void StartAR()
    {
        SceneManager.LoadScene("AR_Scene");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void SaveSettings()
    {
        XmlNode root = settings.FirstChild;

        foreach (XmlNode node in root.ChildNodes)
        {
            switch (node.Name)
            {
                case "master":
                    node.InnerText = master_Slider.value.ToString();
                    break;
                case "sfx":
                    node.InnerText = soundFx_Slider.value.ToString();
                    break;
                case "bg":
                    node.InnerText = backFx_Slider.value.ToString();
                    break;
                case "sensit":
                    node.InnerText = sens_Slider.value.ToString();
                    break;
            }
            settings.Save(directory + savedFile);
        }
    }

    public void LoadSettings()
    {
        XmlNode root = settings.FirstChild;
        foreach (XmlNode node in root.ChildNodes)
        {
            switch (node.Name)
            {
                case "master":
                    master_Slider.value = float.Parse(node.InnerText);
                    break;
                case "sfx":
                    soundFx_Slider.value = float.Parse(node.InnerText);
                    break;
                case "bg":
                    backFx_Slider.value = float.Parse(node.InnerText);
                    break;
                case "sensit":
                    sens_Slider.value = float.Parse(node.InnerText);
                    break;
            }
        }
    }
}
