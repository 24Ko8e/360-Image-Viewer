using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public List<GameObject> listToDisable;

    private void Update()
    {

    }

    public void onImageLoad()
    {
        for (int i = 0; i < listToDisable.Count; i++)
        {
            listToDisable[i].gameObject.SetActive(false);
        }
    }
}
