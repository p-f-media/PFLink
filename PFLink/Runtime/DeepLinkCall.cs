// using System;
// using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;

namespace PFLink
{
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// プチフルへのDeepLinkアクセスクラス.
    /// </summary>
    // ------------------------------------------------------------------------------------------
    public class DeepLinkCall : MonoBehaviour
    {    
        static string pfURLScheme = "p-f";
        static string paramHeader = "?param=";

        void Start()
        {
            
        }


        // -----------------------------------------------------------------------------------------
        /// <summary>
        /// 単純なOPENURLでのURLスキームへのアクセス.
        /// </summary>
        /// <param name="paramString"> パラメータ文字列. </param>
        /// <param name="customHeader"> パラメータ前につける文字列（未入力でparamHeaderになる）. </param>
        // -----------------------------------------------------------------------------------------
        public static void SimpleLinkToPetitFour( string paramString = "", string customHeader = "" )
        {
            Uri uri = null;
            string url = "";
            try
            {
                if( string.IsNullOrEmpty( customHeader ) == true ) uri = new Uri( pfURLScheme + "://" + paramHeader + paramString );
                else  uri = new Uri( pfURLScheme + "://" + customHeader + paramString );

                url = uri.AbsoluteUri;
            }
            catch
            {
                if( string.IsNullOrEmpty( customHeader ) == true ) url = pfURLScheme + "://" + paramHeader + paramString;
                else  url = pfURLScheme + "://" + customHeader + paramString;
            }
            
            url = uri.AbsoluteUri;
            Application.OpenURL( url );
        }

        // -----------------------------------------------------------------------------------------
        /// <summary>
        /// URLスキームでのリンク.プラグインを使って、未インストールの場合はストアを開く.
        /// </summary>
        /// <param name="paramString"> パラメータ文字列. </param>
        /// <param name="packageName"> パッケージネーム. </param>
        /// <param name="androidStoreURL"> AndroidのストアURL（アプリIDも含める）. </param>
        /// <param name="iosAppId"> IOSのあぷりID. </param>
        /// <param name="customHeader"> パラメータ前につける文字列（未入力でparamHeaderになる）. </param>
        // -----------------------------------------------------------------------------------------
        public static void LinkToPetitFour( string paramString = "", 
                                            string packageName = "",
                                            string androidStoreURL = "https://play.google.com/store/apps/details?id=", 
                                            string iosAppId = "",
                                            string customHeader = "" )
        {
            Uri uri = null;
            string url = "";
            try
            {
                if( string.IsNullOrEmpty( customHeader ) == true ) uri = new Uri( pfURLScheme + "://" + paramHeader + paramString );
                else  uri = new Uri( pfURLScheme + "://" + customHeader + paramString );

                url = uri.AbsoluteUri;
            }
            catch
            {
                if( string.IsNullOrEmpty( customHeader ) == true ) url = pfURLScheme + "://" + paramHeader + paramString;
                else  url = pfURLScheme + "://" + customHeader + paramString;
            }
            
            if( Application.platform == RuntimePlatform.Android )
            {
            #if UNITY_ANDROID
                using ( AndroidJavaClass unityPlayer = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" ) )
                using ( AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>( "currentActivity" ) )
                using ( AndroidJavaClass intentStaticClass = new AndroidJavaClass( "android.content.Intent" ) )
                using ( AndroidJavaClass uriClass = new AndroidJavaClass( "android.net.Uri" ) )
                using ( AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>( "parse", url ) )
                {
                    var actionView = intentStaticClass.GetStatic<string>( "ACTION_VIEW" );

                    using ( AndroidJavaObject intent = new AndroidJavaObject( "android.content.Intent", actionView, uriObject ) )
                    {
                        try
                        {
                            currentActivity.Call( "startActivity", intent );
                        }
                        catch ( System.Exception e )
                        {
                            Debug.LogWarning( "Warning LinkURLScheme. : " + e );
                            // 未インストールならストアを開くなど
                            Application.OpenURL( androidStoreURL + packageName );
                            
                            // // ストアアプなら
                            // var urlStore = "market://details?id=" + packageName;
                            // using ( AndroidJavaClass uriClassStore = new AndroidJavaClass( "android.net.Uri" ))
                            // using ( AndroidJavaObject uriObjectStore = uriClassStore.CallStatic<AndroidJavaObject>( "parse", urlStore ) )
                            // using ( AndroidJavaObject intentStore = new AndroidJavaObject( "android.content.Intent", actionView, uriObjectStore ) )
                            // {
                            //     currentActivity.Call( "startActivity", intentStore );
                            // }
                        }
                    }
                }
            #endif
            }
            else if( Application.platform == RuntimePlatform.IPhonePlayer )
            {
            #if UNITY_IOS
                new DeepLinkPlugin().Launch( url, iosAppId );
            #endif
            }
            else
            {
                Debug.Log( "AndroidもしくはIOS端末でのみ動作します。" );
            }
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// IOSプラグインの読み込み.
        /// </summary>
        // -------------------------------------------------------------------------------
        public class DeepLinkPlugin
        {
    #if UNITY_IOS
            [DllImport("__Internal")]
            private static extern void launch ( string url, string itunesAppId );

            public void Launch( string url, string itunesAppId )
            {
                launch ( url, itunesAppId );
            }
    #endif
        }
    }
}