using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose"; 
    Animator anim;
    [SerializeField] ContainerCounter containerCounter;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void Start()
    {
        containerCounter.OnPlayerGrabObject += ContainerCounter_OnPlayerGrabObject;
    }

    private void ContainerCounter_OnPlayerGrabObject(object sender, System.EventArgs e)
    {
        anim.SetTrigger(OPEN_CLOSE);
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
