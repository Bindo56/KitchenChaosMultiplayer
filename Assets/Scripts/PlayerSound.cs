using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    Player player;
    float footStepTimer;
    float footStepTimerMax;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if (footStepTimer < 0f)
        {
            footStepTimer = footStepTimerMax;
            if (player.IsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootStepSound(transform.position, volume);
            }

        }



    }

}
