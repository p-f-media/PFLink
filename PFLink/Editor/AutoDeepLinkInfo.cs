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
    static string pfUrlScheme = "p-f";

    [PostProcessBuild]
    public static void OnPostprocessBuild( BuildTarget buildTarget, string path )
    {
        EditInfoPlist( path );
    }

    static void EditInfoPlist( string directory ) 
    {
        // Info.plistへの書き込み.
        WriteInfoplist( pfUrlScheme, directory );
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