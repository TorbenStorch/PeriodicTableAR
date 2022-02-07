using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInteraction : MonoBehaviour
{
    public ElementProfile profile;

    GameObject spawnedObj;
    Transform connectedObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ElementInteraction>() != null && profile.canInteractWith.Contains(other.GetComponent<ElementInteraction>().profile.type))
        {

            //Vector3 test = new Vector3( (transform.position.x + other.transform.position.x / 2), (transform.position.y + other.transform.position.y / 2), (transform.position.z + other.transform.position.z / 2));
            connectedObj = other.transform;
            if (profile.canSpawn != null) spawnedObj = Instantiate(profile.canSpawn);
        }
    }
    private void Update()
    {
        if (spawnedObj != null && connectedObj != null)
        {
            spawnedObj.transform.position = ((transform.position + connectedObj.position) / 2) + new Vector3(0, 0.05f, 0);
            //spawnedObj.transform.position = new Vector3( (transform.position.x + connectedObj.position.x)/ 2,  0.07f, transform.position.z + connectedObj.position.z) / 2;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ElementInteraction>() != null && profile.canInteractWith.Contains(other.GetComponent<ElementInteraction>().profile.type))
        {
            connectedObj = null;
            Destroy(spawnedObj);
        }
    }
}
