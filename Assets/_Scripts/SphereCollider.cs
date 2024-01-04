using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollider : MonoBehaviour
{
    [SerializeField] private LineRenderer laser;
    [SerializeField] private Collider col;
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (laser.enabled || Weapons.isMissile)
            col.enabled = true;
        else
            col.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (laser.enabled && collision.gameObject.tag == "SmallShip")
        {
        
            Score.scoreCount++;
            Destroy(collision.gameObject);
            audioSources[0].Play();
            GameObject go = Instantiate(explosion, collision.transform.position, Quaternion.identity);
            Destroy(go, 1.5f);
        }

        if (Weapons.isMissile && collision.gameObject.tag == "BigShip")
        {
            Score.scoreCount = Score.scoreCount + 2;
            Destroy(collision.gameObject);
            audioSources[0].Play();
            GameObject go = Instantiate(explosion, collision.transform.position, Quaternion.identity);
            Destroy(go, 1.5f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (laser.enabled && collision.gameObject.tag == "SmallShip")
        {
            Score.scoreCount++;
            Destroy(collision.gameObject);
            audioSources[0].Play();
            GameObject go = Instantiate(explosion, collision.transform.position, Quaternion.identity);
            Destroy(go, 1.5f);
        }


        if (Weapons.isMissile && collision.gameObject.tag == "BigShip")
        {
            Score.scoreCount = Score.scoreCount + 2;
            
            Destroy(collision.gameObject);
            audioSources[0].Play();
            GameObject go = Instantiate(explosion, collision.transform.position, Quaternion.identity);
            Destroy(go, 1.5f);

        }
            
    }
}
