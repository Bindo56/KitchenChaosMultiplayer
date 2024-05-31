using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingVisual : MonoBehaviour
{
    private const string Cut = "Cut";
    Animator anim;
    [SerializeField] CuttingCounter cuttingCounter;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        anim.SetTrigger(Cut);
    }
}
