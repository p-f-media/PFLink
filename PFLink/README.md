# PFLink Package.
***********************************************************************
## 最終更新
2020.09.17

## バージョン
0.1.2

***********************************************************************
## 概要
プチフルアプリを別アプリから起動するための、Gitインストール用のパッケージです。  
サンプルはUnityPackageManagerのSamplesから別途インポートしてください。  

***********************************************************************

## 準備
インポートするだけで使用できます。  

***********************************************************************
## 使用方法

### 簡易接続
```
DeepLinkCall.SimpleLinkToPetitFour( string paramString = "" )
```

を実行すると、Application.OpenURL( string url )で簡易的にプチフルアプリを開きます。
端末にプチフルアプリがインストールされていないと何も起こりません。

```
paramString : パラメータとなる文字列（URLなど）
```  
  


### 通常接続
```
DeepLinkCall.LinkToPetitFour( string paramString = "", 
                              string packageName = "",
                              string androidStoreURL = "https://play.google.com/store/apps/details?id=", 
                              string iosAppId = "" )
```

を実行すると、ネイティブプラグインを使用してプチフルアプリを起動します。  
プチフルアプリが端末にない場合、各OSのストアを開きます。
```
paramString : パラメータとなる文字列（URLなど）
packageName : パッケージ名
androidStoreURL : Androidの際のGoogleStoreURL（IDを含める）
iosAppId : IOSの際のアプリID
```  
  
***********************************************************************