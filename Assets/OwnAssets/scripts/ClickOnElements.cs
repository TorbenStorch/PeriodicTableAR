using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ClickOnElements : MonoBehaviour
{
    private void Update()
    {
        ClickOnObjects();
    }

    private void ClickOnObjects()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.transform.tag == "Element")
                    {
                        if (!hitObject.transform.GetChild(1).gameObject.GetComponent<InfoBehavior>().infoOpen)
                        {
                            hitObject.transform.GetChild(1).gameObject.GetComponent<InfoBehavior>().OpenInfo();
                            hitObject.transform.GetChild(1).gameObject.GetComponent<InfoBehavior>().infoOpen = true;
                        }
                        else if (hitObject.transform.GetChild(1).gameObject.GetComponent<InfoBehavior>().infoOpen)
                        {
                            hitObject.transform.GetChild(1).gameObject.GetComponent<InfoBehavior>().CloseInfo();
                            hitObject.transform.GetChild(1).gameObject.GetComponent<InfoBehavior>().infoOpen = false;
                        }
                    }
                }
            }
        }
    }
}
