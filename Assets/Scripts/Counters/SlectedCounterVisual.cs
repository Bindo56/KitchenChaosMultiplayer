using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlectedCounterVisual : MonoBehaviour
{

    [SerializeField] BaseCounter baseCounter;
    [SerializeField] GameObject[] visualGameObjectArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;

    }



    private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
       if(e.selectedCounter ==  baseCounter)
        {
            Show();
        }else 
            Hide();
    }

    private void Show()
    {
        foreach (GameObject visualGameObject   in visualGameObjectArray)
        {

          visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {

            visualGameObject.SetActive(false);
        }
    }
}
