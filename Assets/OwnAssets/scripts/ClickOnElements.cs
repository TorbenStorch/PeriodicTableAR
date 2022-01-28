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
                    GameObject hitGO = hitObject.transform.gameObject;
                    if (hitGO.tag == "Element")
                    {
                        if (hitGO.transform.childCount > 1)
                        {
                            if (hitGO.transform.GetChild(0).transform.gameObject.activeSelf)
                            {
                                hitGO.transform.GetChild(0).transform.gameObject.SetActive(false);
                                hitGO.transform.GetChild(1).transform.gameObject.SetActive(true);
                            }
                            else if (hitGO.transform.GetChild(1).transform.gameObject.activeSelf)
                            {
                                hitGO.transform.GetChild(0).transform.gameObject.SetActive(true);
                                hitGO.transform.GetChild(1).transform.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }
}
