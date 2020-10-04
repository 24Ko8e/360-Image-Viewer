using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class shortcutManager : MonoBehaviour
{
    public static shortcutManager current;

    // Awake is called before the first frame update
    void Awake()
    {
        current = this;
    }

    public event Action OpenFile;

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.O))
        {
            loadFile();
        }
#else
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                loadFile();
            }
        }
#endif
    }

    public void loadFile()
    {
        if(OpenFile != null)
        {
            OpenFile();
        }
    }
}
