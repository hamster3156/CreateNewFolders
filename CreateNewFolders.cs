// 参考リンク：https://qiita.com/harukin721/items/885e96e057aeb2d2557c

#if UNITY_EDITOR // 宣言が漏れるとPlayモードやビルド時にエラーが出てしまうので注意すること
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateNewFolders : EditorWindow
{
    private string rootFolderName = "NewFolder"; // デフォルトのフォルダ名

    // セットアップで作成するフォルダー名の配列
    private string[] _setupFolders = new string[]
    {
        // gitignoreでAssetを無視する時のファイル名と同じフォルダーの作成
        "AssetStoreTools",

        // ダウンロードしたエディタやプラグインを格納するフォルダ
        "Editor",
        "Plugins",

        // 自分達が作成した物を格納するフォルダ
        "Project"
    };

    // ルート名を元に作成するフォルダー名の配列
    private string[] _defaultAppFolders = new string[]
    {
        "Scripts",
        "ScriptableObjects",
        "Scenes",
        "Prefabs",
        "Editor",
        "Textures",
        "Animations",
        "Materials",
        "PhysicsMaterials",
        "Fonts",
        "Videos",
        "Audio/BGM",
        "Audio/SE",
        "Resources",
        "Editor",
        "Plugins"
    };

    /// <summary>
    /// 項目をクリックするとウィンドウを開く
    /// </summary>
    [MenuItem("Tools/CreateNewFolders")]
    private static void Init()
    {
        // CreateNewFoldersで定義したウィンドウを開く
        CreateNewFolders window = (CreateNewFolders)GetWindow(typeof(CreateNewFolders));
        window.Show();
    }

    /// <summary>
    /// 画面描画時に毎回実行されるイベント
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Label("Assets直下に特定のフォルダーを生成", EditorStyles.boldLabel);

        if (GUILayout.Button("Setup Assets Folders"))
        {
            CreateAppFolders(true, null, _setupFolders);
        }

        // タイトルテキスト設定
        GUILayout.Label("新規ディレクトリ構成の生成", EditorStyles.boldLabel);

        // フォルダ名入力欄を描画する。この値をディレクトリ生成時のルートディレクトリ名になる。
        rootFolderName = EditorGUILayout.TextField("Folder Name", rootFolderName);

        // Create Foldersと表示されたボタンを描画し、クリックされたらCreateAppFoldersを実行する
        if (GUILayout.Button("Create Folders"))
        {
            CreateAppFolders(false, rootFolderName, _defaultAppFolders);
        }
    }

    /// <summary>
    /// ルート名を元にフォルダーを作成する
    /// もし、ルート名がnullの場合、Setupのフォルダーが作成される
    /// </summary>
    /// <param name="rootFolderName"></param>
    /// <param name="folders"></param>
    private void CreateAppFolders(bool isSetup, string rootFolderName, string[] folders)
    {
        // ディレクトリを生成するパスを設定する
        string basePath = null;

        // セットアップフラグによってフォルダ生成先のディレクトリ名を変更する
        if (isSetup)
        {
            basePath = Path.Combine("Assets");
        }
        else
        {
            basePath = Path.Combine("Assets", rootFolderName);
        }

        if (!isSetup && string.IsNullOrWhiteSpace(rootFolderName))
        {
            Debug.LogError("フォルダ生成に失敗しました。ルート名が無効な文字列です");
            return;
        }

        // 上記リストのフォルダを一つずつ作成
        foreach (string folder in folders)
        {
            string path = Path.Combine(basePath, folder);
            if (!Directory.Exists(path))
            {
                // ディレクトリがまだ存在しない場合は作成する
                Directory.CreateDirectory(path);
                Debug.Log("Created Directory: " + path);
            }
        }

        // Unity側にディレクトリが追加されたことが検知されないかもしれないので、念のため手動で更新する。
        AssetDatabase.Refresh();
    }
}
#endif
