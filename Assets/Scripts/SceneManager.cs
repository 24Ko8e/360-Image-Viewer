﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;
using System.IO;
using System;
using System.Linq;
using B83.Win32;

public class SceneManager : MonoBehaviour
{
    public CameraMovement cameraMove;
    public GameObject Sphere;

    public List<GameObject> listToDisable;
    public List<GameObject> listToEnable;


    DropInfo dropInfo = null;
    class DropInfo
    {
        public string filepath;
        public Vector2 pos;
    }

    void OnEnable()
    {
        UnityDragAndDropHook.InstallHook();
        UnityDragAndDropHook.OnDroppedFiles += onDragDrop;
        shortcutManager.current.OpenFile += openImage;
    }

    void OnDisable()
    {
        UnityDragAndDropHook.UninstallHook();
        shortcutManager.current.OpenFile -= openImage;
    }

    private void Update()
    {

    }

    public void openImage()
    {
        var extensions = new[] {
        new ExtensionFilter("Images", "jpg", "jpeg", "png")
        };

        var paths = StandaloneFileBrowser.OpenFilePanel("", "", extensions, false);
        Texture2D texture;

        if (paths.Length > 0)
        {
            string file = Path.GetFileName(paths[0]);
            if (string.IsNullOrEmpty(paths[0]))
            {

            }
            else
            {
                byte[] byteArray = File.ReadAllBytes(paths[0]);
                texture = new Texture2D(2, 2);
                bool isLoaded = texture.LoadImage(byteArray);

                if (isLoaded)
                {
                    texture.name = file;
                    Sphere.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
                }
            }
            cameraMove.imageLoaded = true;

            for (int i = 0; i < listToDisable.Count; i++)
            {
                listToDisable[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < listToEnable.Count; i++)
            {
                listToEnable[i].gameObject.SetActive(true);
            }
        }
        else
        {

        }
    }

    void onDragDrop(List<string> aFiles, POINT aPos)
    {
        string file = "";
        // scan through dropped files and filter out supported image types
        foreach (var f in aFiles)
        {
            var fi = new System.IO.FileInfo(f);
            var ext = fi.Extension.ToLower();
            if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
            {
                file = f;
                break;
            }
        }
        // If the user dropped a supported file, create a DropInfo
        if (file != "")
        {
            var info = new DropInfo
            {
                filepath = file,
                pos = new Vector2(aPos.x, aPos.y)
            };
            dropInfo = info;
            string filename = Path.GetFileName(dropInfo.filepath);
            Texture2D texture;

            Debug.LogError(dropInfo.filepath);
            if (string.IsNullOrEmpty(dropInfo.filepath))
            {

            }
            else
            {
                byte[] byteArray = File.ReadAllBytes(dropInfo.filepath);
                texture = new Texture2D(2, 2);
                bool isLoaded = texture.LoadImage(byteArray);

                if (isLoaded)
                {
                    texture.name = file;
                    Sphere.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
                }
            }
            cameraMove.imageLoaded = true;
        }
    }

    public void fullscreenToggle()
    {
        if (Screen.fullScreen)
        {
            Debug.LogError(Screen.currentResolution);
            Screen.SetResolution(Screen.width, Screen.height, false);
        }
        else if (!Screen.fullScreen)
        {
            Debug.LogError(Screen.currentResolution);
            Screen.SetResolution(Screen.width, Screen.height, true);
        }
    }
}
