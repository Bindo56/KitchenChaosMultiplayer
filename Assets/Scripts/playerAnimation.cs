using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{

    const string Is_walking = "IsWalking";
    Animator anim;
    [SerializeField] Player player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        anim.SetBool(Is_walking, player.IsWalking());
    }
}
