using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] GameObject hasProgressameObject;

     IHasProgress hasprogress;
    [SerializeField] Image barImage;


    private void Start()
    {
        hasprogress = hasProgressameObject.GetComponent<IHasProgress>();

        if(hasprogress == null )
        {
            Debug.LogError("GameObject" + hasProgressameObject + "Does Not Have Component that have IHasProgress");
        }

        hasprogress.OnProgressChanged += CuttingCounter_OnProgressChanged;
        barImage.fillAmount = 0;

        Hide();
    }

    private void CuttingCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArges e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0 || e.progressNormalized == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false) ;
    }
}
