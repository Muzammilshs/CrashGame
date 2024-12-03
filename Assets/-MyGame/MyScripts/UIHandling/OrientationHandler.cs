using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationHandler : MonoBehaviour
{
    public bool setLandscape = false;
    public OrientationObjets[] orientationObjets;
    void Start()
    {
        SetOrientation(setLandscape);
    }

    public void SetOrientation(bool isLandscape)
    {
        for (int i = 0; i < orientationObjets.Length; i++)
        {
            RectTransform posObj = isLandscape ? orientationObjets[i].landscapePos : orientationObjets[i].portraitPos;
            LocalSettings.SetPosAndRect(orientationObjets[i].originalObj, posObj, posObj.transform.parent);
        }
    }
}

[Serializable]
public class OrientationObjets
{
    public GameObject originalObj;
    public RectTransform portraitPos;
    public RectTransform landscapePos;
}
