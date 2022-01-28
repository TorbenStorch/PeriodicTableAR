//by Torben - image tracking (instantiate obj)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


[RequireComponent(typeof(ARTrackedImageManager))] //needus meedus this thing alla
public class OwnImageRecognition : MonoBehaviour
{
    #region Data
    //ARTrackedImageManager
    private ARTrackedImageManager arTrackedImageManager;

    //Objects we want to instantiate
    [Header("Name of prefab must be name of Image in the RIL")]
    [SerializeField] private GameObject[] prefabs;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>(); //set private later
    //IMPORTANT: the names of all objetcs in the prefabs array need to be the exact same as the image name in the imageLibary we want them to be tracked on
    //prefab name MUST BE referenceImage name

    //Scan Indicator Image
    [SerializeField] private GameObject scanIndicatorImage;
    [SerializeField] private bool showIndicatorIfImageNotVisible;

    [SerializeField] private bool removeObjectsIfImageNotFound;

    [SerializeField] float spawnOffset;
    #endregion 


    private void Awake()
    {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();

        foreach (var prefab in prefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity); //default pos&rot into new GameObject
            newPrefab.name = prefab.name; //get the name as a reference point aside the obj
            spawnedPrefabs.Add(prefab.name, newPrefab); //add the object & its name together into the dictionary
            newPrefab.SetActive(false);
        }
        scanIndicatorImage.SetActive(true); //show image from beginning
    }

    private void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }
    private void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    void OnImageChanged(ARTrackedImagesChangedEventArgs args) //what happens if we track an image
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            ActivateTrackedObject(trackedImage);
        }
        foreach (var trackedImage in args.updated)
        {
            UpdateTrackedObject(trackedImage);
        }
        foreach (var trackedImage in args.removed) //lul only works with arkit not arcore - rip android
        {
            //spawnedPrefabs[trackedImage.name].SetActive(false); //finds the current trackedimageName and disables the gameobj with that name in the dictionary
            Destroy(spawnedPrefabs[trackedImage.name]);
            //scanIndicatorImage.SetActive(false);
        }
    }

    void ActivateTrackedObject(ARTrackedImage trackedImage)
    {
        //if (scanIndicatorImage.activeSelf) scanIndicatorImage.SetActive(false);
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position + new Vector3(0,spawnOffset,0);
        Quaternion rotation = trackedImage.transform.rotation;

        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;
        prefab.transform.rotation = rotation;
        prefab.SetActive(true);
    }

    void UpdateTrackedObject(ARTrackedImage trackedImage) //what should happen on/with the tracked image
    {
        if (trackedImage.trackingState == TrackingState.Tracking) //if we have a clear view onto the tracked image
        {
            if (scanIndicatorImage.activeSelf) scanIndicatorImage.SetActive(false);
            string name = trackedImage.referenceImage.name;
            Vector3 position = trackedImage.transform.position + new Vector3(0, spawnOffset, 0);
            Quaternion rotation = trackedImage.transform.rotation;

            GameObject prefab = spawnedPrefabs[name];
            prefab.transform.position = position;       //same as: spawnedPrefabs[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
            prefab.transform.rotation = rotation;       //same as: spawnedPrefabs[trackedImage.referenceImage.name].transform.rotation = trackedImage.transform.rotation;
            prefab.SetActive(true);                     //same as: spawnedPrefabs[trackedImage.referenceImage.name].SetActive(true);
        }
        else//if we do not have a clear view onto the tracked image (image is not visible in camera)
        {
            string name = trackedImage.referenceImage.name;
            GameObject prefab = spawnedPrefabs[name];

            if (showIndicatorIfImageNotVisible) //works nicely if we only scan ONE image in this scene -> causes problem if we have more than one
            {
                prefab.SetActive(false);
                if (!scanIndicatorImage.activeSelf) scanIndicatorImage.SetActive(true); //makes problems if you use more then one image to track
            }
            else
            {
                if (removeObjectsIfImageNotFound)
                {
                    //prefab.SetActive(false);
                    prefab.transform.position = Vector3.up;
                }
            }
            
        }
    }
}





