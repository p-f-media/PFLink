******************************************************************************************
# PFLink
******************************************************************************************
## 概要

    - PFLink
        UnityPackageManager(UPM)で配布されるパッケージフォルダです。PFLinkフォルダ以下のもののみ配布されます。  
        「https://github.com/p-f-media/PFLink.git?path=/PFLink」をUPMの「Add package from Git URL...」に入力してインポートします。  
        インポートされたパッケージは、「Package」以下に配置されます。  
  
    - PFLink.unitypackage.
        配布パッケージを「.unitypackage」にしたものでインポートするとGitインポートとは違い「Assets」以下に配置されます。
        また、Gitインポート時には含まれません。

    - README.md( this )
        解説書。Gitインポートには含まれません。

******************************************************************************************
## 注意
    Gitインポートによりインポートされるのは「PFLink」フォルダの中「package.json」の存在するフォルダからです。  
    Gitインポートに含めないファイルに関してはこの階層から作成して下さい。

******************************************************************************************