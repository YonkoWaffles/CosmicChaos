using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Christian 
public class CameraController : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Vector2 cameraTurn;
    [SerializeField] private float xBounds;
    [SerializeField] private float yBounds;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private RectTransform crossTrans;

    public static float cameraSpeed;

    private void Start()
    {
        crosshair.transform.localPosition = new Vector3(0, 0, 0);
        crossTrans = crosshair.GetComponent<RectTransform>();

        Debug.Log(Camera.main.aspect);
        if(Camera.main.aspect >= 2.1) //19:9
        {
            xBounds = .54f;
            yBounds = 1.15f;
        }
        else if(Camera.main.aspect >= 1.7) //16:9
        {
            xBounds = .485f;
            yBounds = .875f;
        }
        else if(Camera.main.aspect >= 1.5)  //16:10
        {
            xBounds = .455f;
            yBounds = .73f;
        }

    }


    private void Update()
    {
        crosshair.transform.localPosition += new Vector3(joystick.Horizontal * cameraSpeed, joystick.Vertical * cameraSpeed, 0);
    }


    /// <summary>
    /// Makes the joystick move the crosshair based on the cameraSpeed variable
    /// Anchors the crosshair within screen bounds
    /// </summary>
    private void LateUpdate()
    {
        

        Vector2 crossAnchor = crossTrans.anchoredPosition;
        float xpos = crossAnchor.x;
        float ypos = crossAnchor.y;
        xpos = Mathf.Clamp(xpos, -(Screen.width - crossTrans.sizeDelta.x) / xBounds, (Screen.width - crossTrans.sizeDelta.x) / xBounds);
        ypos = Mathf.Clamp(ypos, -(Screen.width - crossTrans.sizeDelta.y) / yBounds, (Screen.width - crossTrans.sizeDelta.y) / yBounds);
        crossAnchor.x = xpos;
        crossAnchor.y = ypos;
        crossTrans.anchoredPosition = crossAnchor;

    }
}
