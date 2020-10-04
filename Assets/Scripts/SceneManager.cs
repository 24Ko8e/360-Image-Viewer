using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public List<GameObject> listToDisable;
    public List<GameObject> listToEnable;

    private void Update()
    {

    }

    public void onImageLoad()
    {
        for (int i = 0; i < listToDisable.Count; i++)
        {
            listToDisable[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < listToEnable.Count; i++)
        {
            listToEnable[i].gameObject.SetActive(true);
        }
    }
}
