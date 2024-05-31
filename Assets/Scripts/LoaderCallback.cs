using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    bool isFirstUpdate = true;

    private void FixedUpdate()
    {
        if (isFirstUpdate)
        {
           
            isFirstUpdate = false;

            Loader.LoaderCallback();
        }
    }
}
