using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class SoundManager : MonoBehaviour
{


    const string Player_Pref_Volume = "PlayerPrefVolume";
    public static SoundManager Instance {  get; private set; }


    [SerializeField] AudioClipRefsSO audioClipRefsSO;

    float volume = 1.0f;
    private void Start()
    {
       volume =  PlayerPrefs.GetFloat(Player_Pref_Volume, 1f);
        DeliveryManager.Instance.OnFailed += Instance_OnFailed;
        DeliveryManager.Instance.OnSuccess += Instance_OnSuccess;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickup += Instance_OnPickup;
        BaseCounter.OnDrop += BaseCounter_OnDrop;
        TrashCounter.OnTrash += TrashCounter_OnTrash;
        
    }

    private void Awake()
    {
        Instance = this;
    }

    private void TrashCounter_OnTrash(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter =  sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnDrop(object sender, System.EventArgs e)
    {
       BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Instance_OnPickup(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void Instance_OnSuccess(object sender, System.EventArgs e)
    {
       DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryfail, deliveryCounter.transform.position);
    }

    private void Instance_OnFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    public void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultipler = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0,audioClipArray.Length)], position, volumeMultipler * volume);

    }

    public void PlayFootStepSound(Vector3 position , float volume)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }

    public void PlayCountdown(Vector3 position)
    {
        PlaySound(audioClipRefsSO.warning, position);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if(volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(Player_Pref_Volume, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
        

}
