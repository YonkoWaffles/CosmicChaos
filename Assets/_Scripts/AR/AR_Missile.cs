using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class AR_Missile : MonoBehaviour
{
    float speed = .1f;
    Rigidbody rb;
    GameObject healthBar;
    ProgressBarCircle healthScript;
    GameObject settingsManager;
    private void Update()
    {
        //transform.position = transform.forward * speed;
    }

    private void Start()
    {
        settingsManager = GameObject.Find("SettingsManager");
        healthBar = GameObject.Find("HealthCircle");
        healthScript = healthBar.GetComponent<ProgressBarCircle>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HomeBase"))
        {
            Debug.Log("Missile hit");

            healthScript.BarValue -= 5;
            if (healthScript.BarValue <= 0)
            {
                settingsManager.GetComponent<AR_Base>().GameOver();
            }

            Destroy(this.gameObject);

        }
        
    }

}
