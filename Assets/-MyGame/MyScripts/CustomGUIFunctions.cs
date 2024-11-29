#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine;
using DA_Assets.FCU.Model;
using UnityEngine.UI;

public class CustomGUIFunctions : MonoBehaviour
{
    [MenuItem("Custom Functions/Clear Playerprefs")]
    static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All Playerprefs clear");
        Time.timeScale = 1;
    }

    [MenuItem("Custom Functions/Unlock All")]
    static void unlockAll()
    {
        PlayerPrefs.SetInt("levels", PlayerPrefs.GetInt("levels", 1));
        PlayerPrefs.SetInt("levelsInd", PlayerPrefs.GetInt("levelsInd", 1));
        PlayerPrefs.SetInt("Dollar", PlayerPrefs.GetInt("Dollar") + 50000);
        PlayerPrefs.SetInt("Gold", 400);
        Debug.Log("Unlocked");
    }

    [MenuItem("Custom Functions/Give Cash")]
    static void GiveCash()
    {
        PlayerPrefs.SetInt("Dollar", PlayerPrefs.GetInt("Dollar") + 5000);
        PlayerPrefs.SetInt("Gold", 400);
        Debug.Log("Unlocked");
        Time.timeScale = 1;
    }
    //[MenuItem("Custom Functions/Create Cube")]
    //static void CreateCube()
    //{
    //	print("creating cube");
    //}

    [MenuItem("Custom Functions/Create Cube")]
    static void LogSelectedTransformName()
    {
        Debug.Log("Selected Transform is on " + Selection.activeTransform.gameObject.name + ".");
        int CubeNumber = 0;
        RaycastHit hit;
        for (float x = -107; x <= 141; x += 11)
        {
            for (float z = 70.4f; z <= 294; z += 11)
            {
                GameObject cubeNew = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubeNew.transform.position = new Vector3(x, 120f, z);

                if (Physics.Raycast(cubeNew.transform.position, cubeNew.transform.TransformDirection(Vector3.down), out hit, 900))
                {
                    if (hit.transform.gameObject.CompareTag("Collision"))
                    {
                        cubeNew.transform.position = new Vector3(cubeNew.transform.position.x, hit.point.y + 0.8f, cubeNew.transform.position.z);
                        DestroyImmediate(cubeNew.GetComponent<BoxCollider>());
                        CubeNumber++;
                        cubeNew.transform.name = "Point " + CubeNumber;
                        cubeNew.transform.parent = Selection.activeTransform.gameObject.transform;
                    }
                    else
                    {
                        cubeNew.transform.position = Vector3.zero;
                        Destroy(cubeNew);
                    }
                }
                else
                {
                    cubeNew.transform.position = Vector3.zero;
                    Destroy(cubeNew);
                }
            }
        }
    }

    // Validate the menu item defined by the function above.
    // The menu item will be disabled if this function returns false.
    [MenuItem("Custom Functions/Create Cube", true)]
    static bool ValidateLogSelectedTransformName()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }

    [MenuItem("Custom Functions/Place All Childs To Ground")]
    static void PlaceToGround()
    {
        Debug.Log("Selected Transform is on " + Selection.activeTransform.gameObject.name + ".");
        GameObject parent = Selection.activeTransform.gameObject;
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            print(parent.transform.GetChild(i).name);
        }
    }

    // Validate the menu item defined by the function above.
    // The menu item will be disabled if this function returns false.
    [MenuItem("Custom Functions/Place All Childs To Ground", true)]
    static bool ValidatePlaceToGround()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }

    [MenuItem("Custom Functions/Change Layer")]
    static void ChangeLayerName()
    {
        Debug.Log("Selected Transform is on " + Selection.activeTransform.gameObject.name + ".");
        Selection.activeTransform.gameObject.layer = 13;

        Transform[] allChildren = Selection.activeTransform.gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            //child.gameObject.SetActive(false);
            child.gameObject.layer = 13;
        }
    }

    // Validate the menu item defined by the function above.
    // The menu item will be disabled if this function returns false.
    [MenuItem("Custom Functions/Change Layer", true)]
    static bool ValidateLogLayerNameObj()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }



    /// <Place all gameobjects at ground >
    /// //////////////////////////////////////////////////
    [MenuItem("Custom Functions/Object To Ground &x")]
    static void MultipleObjsToGround()
    {
        RaycastHit hit;
        Debug.Log("Selected Transform is on " + Selection.activeTransform.gameObject.name + ".");
        GameObject[] allSelectedObjects = Selection.gameObjects;//Selection.activeTransform.gameObject;
        foreach (GameObject cubeNew in allSelectedObjects)
        {
            if (Physics.Raycast(cubeNew.transform.position, cubeNew.transform.TransformDirection(Vector3.down), out hit, 900))
            {
                cubeNew.transform.position = new Vector3(cubeNew.transform.position.x, hit.point.y + 0.15f, cubeNew.transform.position.z);
            }
        }
    }

    [MenuItem("Custom Functions/Object To Ground &x", true)]
    static bool ValidateLogMultipleObjsToGround()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }


    /// <summary>
    /// ////////////////////////////////////////////
    /// 
    /// <Place all gameobjects at ground >
    /// //////////////////////////////////////////////////
    [MenuItem("Custom Functions/Remove Missing Script Component &z")]
    static void RemoveMissingScriptComponent()
    {

        var deepSelection = EditorUtility.CollectDeepHierarchy(Selection.gameObjects);
        int compCount = 0;
        int goCount = 0;
        foreach (var o in deepSelection)
        {
            if (o is GameObject go)
            {
                int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
                if (count > 0)
                {
                    // Edit: use undo record object, since undo destroy wont work with missing
                    Undo.RegisterCompleteObjectUndo(go, "Remove missing scripts");
                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                    compCount += count;
                    goCount++;
                }
            }
        }

        /////////
        ///
        Transform[] allChildren = Selection.activeTransform.gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.GetComponent<Rigidbody>())
                DestroyImmediate(child.GetComponent<Rigidbody>());
            if (child.GetComponent<Collider>())
                DestroyImmediate(child.GetComponent<Collider>());
        }
    }

    [MenuItem("Custom Functions/Remove Missing Script Component &z", true)]
    static bool ValidateLogRemoveMissingScriptComponent()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }

    /// </summary>

    //GameObject[] Trees = new GameObject[7];
    [MenuItem("Custom Functions/Replace GameObjects")]
    static void ReplaceObjects()
    {
        GameObject[] Trees = new GameObject[3];
        GameObject[] bush = new GameObject[3];
        Trees[0] = Resources.Load("Tree1") as GameObject;
        Trees[1] = Resources.Load("Tree2") as GameObject;
        Trees[2] = Resources.Load("Tree4") as GameObject;

        bush[0] = Resources.Load("Bush01") as GameObject;
        bush[1] = Resources.Load("Bush02") as GameObject;
        bush[2] = Resources.Load("Bush03") as GameObject;

        //RaycastHit hit;
        Debug.Log("Selected Transform is on " + Selection.activeTransform.gameObject.name + ".");
        int CubeNumber = 0;
        //Transform[] allChildren = Selection.activeTransform.gameObject.GetComponentsInChildren<Transform>();
        //foreach (Transform child in allChildren)
        GameObject obj = Selection.activeTransform.gameObject;
        int length = obj.transform.childCount;
        GameObject tree = null;


        for (int i = 0; i < length; i++)
        {
            CubeNumber++;
            Vector3 pos = new Vector3(obj.transform.GetChild(i).transform.position.x, obj.transform.GetChild(i).transform.position.y, obj.transform.GetChild(i).transform.position.z);
            if (obj.transform.GetChild(i).name.Contains("Tree"))
            {
                tree = Instantiate(Trees[Random.Range(0, Trees.Length)], pos, Quaternion.identity);
            }
            else
                tree = Instantiate(bush[Random.Range(0, bush.Length)], pos, Quaternion.identity);
            //if (Physics.Raycast(tree.transform.position, tree.transform.TransformDirection(Vector3.down), out hit, 900))
            //{
            //    tree.transform.position = hit.point;
            //}
            obj.transform.GetChild(i).gameObject.SetActive(false);
            tree.name = tree.name + " " + CubeNumber;
            tree.transform.parent = Selection.activeTransform.gameObject.transform;

        }


    }

    // Validate the menu item defined by the function above.
    // The menu item will be disabled if this function returns false.
    [MenuItem("Custom Functions/Replace GameObjects", true)]
    static bool ValidateLogSelectedReplaceObjects()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }

    [MenuItem("Custom Functions/Unlock Next Level")]
    static void UnlockNextLevel()
    {
        PlayerPrefs.SetInt("levels", PlayerPrefs.GetInt("levels") + 1);

        PlayerPrefs.SetInt("levelsInd", PlayerPrefs.GetInt("levelsInd") + 1);

        PlayerPrefs.SetInt("levelsRailway", PlayerPrefs.GetInt("levelsRailway") + 1);

    }



    ////////////////////////////////////////////////
    // make an array

    [MenuItem("Custom Functions/Make Level Array")]
    static void MakeLevelArray()
    {
        string txtlevelArray = "";
        //txtlevelArray += ("public string[] Level" + MainMenuManager.CurrentLevel + " = {");        

        //Debug.Log("Selected Transform is on " + Selection.activeTransform.gameObject.name + ".");
        GameObject parent = Selection.activeTransform.gameObject;
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            string tempTxt = "";
            if (i != 0)
                tempTxt = ",";
            GameObject child = parent.transform.GetChild(i).transform.gameObject;
            tempTxt += "\"" + child.name + "\",\"" + child.transform.position.x + "\",\"" + child.transform.position.y + "\",\"" + child.transform.position.z + "\",\"" + child.transform.eulerAngles.x + "\",\"" + child.transform.eulerAngles.y + "\",\"" + child.transform.eulerAngles.z + "\",\"" + child.transform.localScale.x + "\",\"" + child.transform.localScale.y + "\",\"" + child.transform.localScale.z + "\"";

            txtlevelArray += tempTxt;
        }
        txtlevelArray += "};";
        print(txtlevelArray);
        string path = "Assets/Resources/TempLevels.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(txtlevelArray);
        writer.Close();
    }

    // Validate the menu item defined by the function above.
    // The menu item will be disabled if this function returns false.
    [MenuItem("Custom Functions/Make Level Array", true)]
    static bool ValidateMakeLevelArray()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }




    [MenuItem("Custom Functions/Remove Script")]
    static void RemoveScript()
    {

        Debug.Log("Selected Transform is on " + Selection.activeTransform.gameObject.name + ".");
        int CubeNumber = 0;
        Transform[] allChildren = Selection.activeTransform.gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.GetComponent<SyncHelper>())
                DestroyImmediate(child.GetComponent<SyncHelper>());
            if (child.GetComponent<LayoutElement>())
                DestroyImmediate(child.GetComponent<LayoutElement>());
        }

    }

    [MenuItem("Custom Functions/Remove Script", true)]
    static bool ValidateRemoveScript()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }

}
#endif
