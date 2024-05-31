using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] Color failedColor;
    [SerializeField] Color successColor;
    [SerializeField] Sprite successSprite;
    [SerializeField] Sprite failedSprite;

    private const string POPUP = "Popup";
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        gameObject.SetActive(false);
        DeliveryManager.Instance.OnSuccess += DeliveryManager_OnSuccess;
        DeliveryManager.Instance.OnFailed += DeliveryManager_OnFailed;
    }

    private void DeliveryManager_OnFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        anim.SetTrigger(POPUP);
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        iconImage.color = Color.red;
        messageText.text = "DELIVERY\nFailed";
    }

    private void DeliveryManager_OnSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        anim.SetTrigger(POPUP);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        iconImage.color = Color.green;
        messageText.text = "DELIVERY\nSuccess";
    }
}
