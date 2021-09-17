using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using PFLink;

public class CallTestView : MonoBehaviour
{
    [SerializeField] string param = "?param=1234";
    [SerializeField] string packageName = "";
    [SerializeField] string androidStoreURL = "https://play.google.com/store/apps/details?id=";
    [SerializeField] string iosAppID = "";

    void Start()
    {
        
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
