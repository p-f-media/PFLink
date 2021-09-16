#if UNITY_IOS
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
#endif

// --------------------------------------------------------------------------------------------
/// <summary>
/// IOSのみの処理です。IOSビルド後XcodeでURLスキームを使用しますという記載が必要なので、それの自動化.
/// </summary>
// --------------------------------------------------------------------------------------------
public class AutoDeepLinkInfo
{
#if UNITY_IOS
    //! URLスキーム.
    // static string urlScheme = "testUrlScheme";

    [PostProcessBuild]
    public static void OnPostprocessBuild( BuildTarget buildTarget, string path )
    {
        EditInfoPlist( path );
    }

    static void EditInfoPlist( string directory ) 
    {
        // 設定ファイルの検索.
        var assets = AssetDatabase.FindAssets( "URLSchemeInfoData" );
        if( assets != null && assets.Length > 0 )
        {
            foreach( var id in assets )
            {
                var assetPath = AssetDatabase.GUIDToAssetPath( id );
                if( assetPath.Contains( "PFLink/Data/URLSchemeInfoData" ) == true )
                {
                    UrlSchemeInfo asset = ( UrlSchemeInfo )AssetDatabase.LoadAssetAtPath( assetPath, typeof( UrlSchemeInfo ) );
                    var urlScheme = asset.UrlScheme;

                    // Info.plistへの書き込み.
                    WriteInfoplist( urlScheme, directory );

                    break;
                }
            }
        }


        // var path = Path.Combine( directory, "Info.plist" );
        // var document = new PlistDocument();
        // document.ReadFromFile( path );

        // // 呼び出す側のURLスキームを使いますよ宣言を追加
        // var urlTypes = document.root.CreateArray( "LSApplicationQueriesSchemes" );
        // // var dict = urlTypes.AddDict();
        // // dict.SetString("CFBundleURLName", "test_name");
        // // var urlSchemes = dict.CreateArray("CFBundleURLSchemes");
        // urlTypes.AddString( urlScheme );

        // document.WriteToFile( path );
    }

    static void WriteInfoplist( string urlScheme, string directory )
    {
        var path = Path.Combine( directory, "Info.plist" );
        var document = new PlistDocument();
        document.ReadFromFile( path );

        // 呼び出す側のURLスキームを使いますよ宣言を追加
        var urlTypes = document.root.CreateArray( "LSApplicationQueriesSchemes" );
        urlTypes.AddString( urlScheme );

        document.WriteToFile( path );
    }
#endif
}