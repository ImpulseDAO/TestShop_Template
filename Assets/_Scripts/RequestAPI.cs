using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RequestAPI : MonoBehaviour
{
    [SerializeField] private string url;
    [SerializeField] private string Posturl;
    [Space]
    [SerializeField] private Image preloader;

    private ChairsStructFromJson chairsAsStruct;

    public void Post(Checkout ch) => StartCoroutine(SendPostRequest(ch));

    private void OnEnable() => Cart.CheckoutMade += val => Post(val);

    private void OnDisable() => Cart.CheckoutMade -= Post;

    private void Awake() => StartCoroutine(SendGetRequest());

    private IEnumerator SendGetRequest()
    {
        ShowPreloader(true);

        UnityWebRequest request = UnityWebRequest.Get(this.url);

        yield return request.SendWebRequest();

        chairsAsStruct = JsonUtility.FromJson<ChairsStructFromJson>(request.downloadHandler.text);
        if (request.isDone)
        {
            ChairStorage.Instance.UpdateShopStorage(chairsAsStruct);
            ShowPreloader(false);
        }
    }

    private IEnumerator SendPostRequest(Checkout st)
    {
        ShowPreloader(true);

        var formData = new WWWForm();
        var chairStruct = st;
        var json = JsonUtility.ToJson(chairStruct);
        var jsonBytes = Encoding.UTF8.GetBytes(json);
        var request = UnityWebRequest.Post(Posturl, formData);
        var uploadHandler = new UploadHandlerRaw(jsonBytes);

        request.uploadHandler = uploadHandler;
        yield return request.SendWebRequest();

        if (request.isDone)
            ShowPreloader(false);
    }

    private void ShowPreloader(bool isActive) => preloader.gameObject.SetActive(isActive);
}
