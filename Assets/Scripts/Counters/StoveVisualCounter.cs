using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveVisualCounter : MonoBehaviour
{
    [SerializeField] GameObject stoveOn;
    [SerializeField] GameObject stoveOnParticalSystem;
    [SerializeField] StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool showVisual = (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried);

        Debug.Log(showVisual);
        stoveOn.SetActive(showVisual);
        stoveOnParticalSystem.SetActive(showVisual);
    }
}
