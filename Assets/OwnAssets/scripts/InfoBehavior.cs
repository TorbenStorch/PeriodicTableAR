using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBehavior : MonoBehaviour
{
    Vector3 desiredScale = Vector3.zero; //default is "off"
    float speed = 6f;

    //look at cam
    Transform cam;
    Vector3 targetAngle = Vector3.zero;

    [HideInInspector] public bool infoOpen;

    private void Start()
    {
        gameObject.transform.localScale = Vector3.zero;

        //look at cam
        cam = Camera.main.transform;
    }


    private void Update()
    {
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, desiredScale, Time.deltaTime * speed);

        //look at camera
        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.position); //had to reverse it
    }

    public void OpenInfo()
    {
        desiredScale = Vector3.one;
    }
    public void CloseInfo()
    {
        desiredScale = Vector3.zero;
    }
}
