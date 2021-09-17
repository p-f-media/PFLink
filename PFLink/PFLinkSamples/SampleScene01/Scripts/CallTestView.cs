using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using PFLink;

public class CallTestView : MonoBehaviour
{
    [SerializeField] string ver = "0.1.0";
    [SerializeField] Text verText = null;
    [SerializeField] string param = "?param=1234";
    [SerializeField] string packageName = "";
    [SerializeField] string androidStoreURL = "https://play.google.com/store/apps/details?id=";
    [SerializeField] string iosAppID = "";

    void Start()
    {
        if( verText != null ) verText.text = ver;
    }

    public void OnSimpleDeepLinkButtonClicked()
    {
        DeepLinkCall.SimpleLinkToPetitFour( param + " #SimpleLink." );
    }

    public void OnDeepLinkButtonClicked()
    {
        DeepLinkCall.LinkToPetitFour( param, packageName, androidStoreURL, iosAppID );
    }

}
