// �Q�l�����N�Fhttps://qiita.com/harukin721/items/885e96e057aeb2d2557c

#if UNITY_EDITOR // �錾���R����Play���[�h��r���h���ɃG���[���o�Ă��܂��̂Œ��ӂ��邱��
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateNewFolders : EditorWindow
{
    private string rootFolderName = "NewFolder"; // �f�t�H���g�̃t�H���_��

    // �Z�b�g�A�b�v�ō쐬����t�H���_�[���̔z��
    private string[] _setupFolders = new string[]
    {
        // gitignore��Asset�𖳎����鎞�̃t�@�C�����Ɠ����t�H���_�[�̍쐬
        "AssetStoreTools",

        // �_�E�����[�h�����G�f�B�^��v���O�C�����i�[����t�H���_
        "Editor",
        "Plugins",

        // �����B���쐬���������i�[����t�H���_
        "Project"
    };

    // ���[�g�������ɍ쐬����t�H���_�[���̔z��
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
    /// ���ڂ��N���b�N����ƃE�B���h�E���J��
    /// </summary>
    [MenuItem("Tools/CreateNewFolders")]
    private static void Init()
    {
        // CreateNewFolders�Œ�`�����E�B���h�E���J��
        CreateNewFolders window = (CreateNewFolders)GetWindow(typeof(CreateNewFolders));
        window.Show();
    }

    /// <summary>
    /// ��ʕ`�掞�ɖ�����s�����C�x���g
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Label("Assets�����ɓ���̃t�H���_�[�𐶐�", EditorStyles.boldLabel);

        if (GUILayout.Button("Setup Assets Folders"))
        {
            CreateAppFolders(true, null, _setupFolders);
        }

        // �^�C�g���e�L�X�g�ݒ�
        GUILayout.Label("�V�K�f�B���N�g���\���̐���", EditorStyles.boldLabel);

        // �t�H���_�����͗���`�悷��B���̒l���f�B���N�g���������̃��[�g�f�B���N�g�����ɂȂ�B
        rootFolderName = EditorGUILayout.TextField("Folder Name", rootFolderName);

        // Create Folders�ƕ\�����ꂽ�{�^����`�悵�A�N���b�N���ꂽ��CreateAppFolders�����s����
        if (GUILayout.Button("Create Folders"))
        {
            CreateAppFolders(false, rootFolderName, _defaultAppFolders);
        }
    }

    /// <summary>
    /// ���[�g�������Ƀt�H���_�[���쐬����
    /// �����A���[�g����null�̏ꍇ�ASetup�̃t�H���_�[���쐬�����
    /// </summary>
    /// <param name="rootFolderName"></param>
    /// <param name="folders"></param>
    private void CreateAppFolders(bool isSetup, string rootFolderName, string[] folders)
    {
        // �f�B���N�g���𐶐�����p�X��ݒ肷��
        string basePath = null;

        // �Z�b�g�A�b�v�t���O�ɂ���ăt�H���_������̃f�B���N�g������ύX����
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
            Debug.LogError("�t�H���_�����Ɏ��s���܂����B���[�g���������ȕ�����ł�");
            return;
        }

        // ��L���X�g�̃t�H���_������쐬
        foreach (string folder in folders)
        {
            string path = Path.Combine(basePath, folder);
            if (!Directory.Exists(path))
            {
                // �f�B���N�g�����܂����݂��Ȃ��ꍇ�͍쐬����
                Directory.CreateDirectory(path);
                Debug.Log("Created Directory: " + path);
            }
        }

        // Unity���Ƀf�B���N�g�����ǉ����ꂽ���Ƃ����m����Ȃ���������Ȃ��̂ŁA�O�̂��ߎ蓮�ōX�V����B
        AssetDatabase.Refresh();
    }
}
#endif
