using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;
using System.IO;
using System.Linq;
using B83.Win32;

public class imageLoader : MonoBehaviour
{
    [SerializeField]
    List<Texture2D> images;
    public CameraMovement cameraMove;
    public SceneManager sceneManager;

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
    }
    void OnDisable()
    {
        UnityDragAndDropHook.UninstallHook();
    }

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Renderer>().material.SetTexture("_MainTex", images[Random.Range(0, images.Count - 1)]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                openImage();
            }
        }
    }

    public void openImage()
    {
        StartCoroutine("importTexture");
    }

    IEnumerator importTexture()
    {
        yield return new WaitForEndOfFrame();

        var extensions = new[] {
            new ExtensionFilter("jpg", "jpeg", "png")
        };

        var paths = StandaloneFileBrowser.OpenFilePanel("", "", extensions, false);
        string file = Path.GetFileName(paths[0]);
        Texture2D texture;

        if(paths.Length > 0)
        {
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
                    GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
                }
            }
        }
        cameraMove.imageLoaded = true;
        sceneManager.onImageLoad();
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
                    GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
                }
            }
            cameraMove.imageLoaded = true;
            sceneManager.onImageLoad();
        }
    }
}
