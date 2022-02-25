using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager
{
    public static bool didShowTutorial
    {
        get
        {
            //return PlayerPrefs.GetInt("didShowTutorial", 0) == 1; //if not set, give 0 back else if its == 1 return one esle return 0
            return PlayerPrefs.GetInt(PlayerPrefsKey.didShowTutorial, 0) == 1; //if not set, give 0 back else if its == 1 return one esle return 0
        }
    }

    public static void SetTutorialAsShown()
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.didShowTutorial, 1); //-> PlayerPrefsKey stores the strings (its in a different script/class)
        //PlayerPrefs.SetInt("didShowTutorial", 1);
    }

    public static bool didTriggerCameraPermissionRequest
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsKey.didTriggerCameraPermissionRequest, 0) == 1;
        }
    }

    public static void SetCameraPermissionRequestAsTriggered()
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.didTriggerCameraPermissionRequest, 1);
    }
}
