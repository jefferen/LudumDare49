using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTextOnlyOnFirstSetup : MonoBehaviour
{ 
    static int SceneLoadTimes = 0;
    static GameObject me;
    void Start()
    {
        me = this.gameObject;
        if (SceneLoadTimes != 0)
        {
            Destroy(me.GetComponent<TMPro.TextMeshPro>());
        }
        SceneLoadTimes++;
    }
}