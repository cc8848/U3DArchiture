using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using System.IO;

public class TestEditorWindow : EditorWindow
{
    static readonly string Path = "Assets/FrameworkTools/TestUtils/Editor";

    [MenuItem("Tools/ 测试窗口 %T", false, 1000)]
    static void Test()
    {
        var window = GetWindow<TestEditorWindow>();
        window.minSize = new Vector2(400, 200);
    }

    TestKeyboardBuffer keyBuffer = new TestKeyboardBuffer();

    GUIStyle style = new GUIStyle();
    int index = -1;
    AbstractUnitTest curTest;

    List<AbstractUnitTest> unitList;
    //TestUnitList unitList;

    //TestUnitList LoadList()
    //{
    //    return AssetDatabase.LoadAssetAtPath(Path + "/TestUnitList.asset", typeof(TestUnitList)) as TestUnitList;
    //}

    string[] btns;

    void Awake()
    {
        style.fontSize = 20;
        style.normal.textColor = Color.white;
    }

	void OnFocus()
	{
		try
		{
			ResetUnits();
		}
		catch (System.Exception e)
		{
			Debug.LogError(e.Message);
		}
	}

    void OnDestroy()
    {
        keyBuffer.Clear();
    }

    void ResetUnits()
    {
		unitList = SearchUnitTest();
        var list = unitList;
        btns = new string[list.Count];
        for (int i = 0; i < list.Count; i++)
            btns[i] = list[i].UnitName;

        if (index >= 0)
        {
            keyBuffer.Clear();
            curTest = list[index];
            curTest.SetActivity(keyBuffer);
        }
    }

    List<AbstractUnitTest> SearchUnitTest()
    {
        List<AbstractUnitTest> ret = new List<AbstractUnitTest>();

        var assets = AssetDatabase.FindAssets("t:Script", new string[] { Path });
        if (assets == null || assets.Length == 0)
            return ret;

        System.Type baseType = typeof(AbstractUnitTest);
        foreach (var asset in assets)
        {
            string path = AssetDatabase.GUIDToAssetPath(asset);
            var script = AssetDatabase.LoadAssetAtPath(path, typeof(MonoScript)) as MonoScript;
            var type = script.GetClass();
            if (!type.IsSubclassOf(baseType))
                continue;

            var obj = ScriptableObject.CreateInstance(type) as AbstractUnitTest;
            ret.Add(obj);
        }

        return ret;
    }

    //void ResetUnits()
    //{
    //    CreateTestAssets();

    //    var assets = AssetDatabase.FindAssets("t:ScriptableObject", new string[] { Path });
    //    if (assets == null || assets.Length == 0)
    //        return;

    //    var list = LoadList();
    //    list.UnitList = new List<AbstractUnitTest>(assets.Length - 1);
    //    foreach (var asset in assets)
    //    {
    //        string path = AssetDatabase.GUIDToAssetPath(asset);
    //        var fName = System.IO.Path.GetFileNameWithoutExtension(path);
    //        if (fName == "TestUnitList") continue;
    //        list.UnitList.Add(AssetDatabase.LoadAssetAtPath(path, typeof(AbstractUnitTest)) as AbstractUnitTest);
    //    }
    //    EditorUtility.SetDirty(list);
    //    AssetDatabase.SaveAssets();

    //    index = -1;
    //    Reset();
    //}

    //void CreateTestAssets()
    //{
    //    AssetDatabase.DeleteAsset(Path + "/scriptable");
    //    AssetDatabase.CreateFolder(Path, "scriptable");
    //    var assets = AssetDatabase.FindAssets("t:Script", new string[] { Path });
    //    if (assets == null || assets.Length == 0)
    //        return;
    //    foreach (var asset in assets)
    //    {
    //        string path = AssetDatabase.GUIDToAssetPath(asset);
    //        var script = AssetDatabase.LoadAssetAtPath(path, typeof(MonoScript)) as MonoScript;
    //        var type = script.GetClass();
    //        var obj = ScriptableObject.CreateInstance(type);

    //        AbstractUnitTest test = obj as AbstractUnitTest;
    //        if (test == null) continue;
    //        AssetDatabase.CreateAsset(obj, Path + "/scriptable/" + type.Name + ".asset");
    //    }
    //}

    void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("测试快捷键： T + 快捷键", style);
        //if (GUILayout.Button("重置测试单元"))
        //    ResetUnits();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        int select = -1;
        if (btns != null)
            select = DrewBtns();

        GUILayout.Space(10);
        DrawDesc();
        GUILayout.Space(10);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        if (index != select)
        {
            keyBuffer.Clear();
            index = select;
            curTest = unitList[index];
            curTest.SetActivity(keyBuffer);
        }
    }

    int DrewBtns()
    {
        return GUILayout.SelectionGrid(index, btns, 1, GUILayout.Width(100));
    }

    void DrawDesc()
    {
        GUILayout.BeginVertical();
        GUILayout.Space(3);
        var col = GUI.backgroundColor;
        GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
        EditorGUILayout.BeginVertical("AS TextArea", GUILayout.MinHeight(100f));
        var list = keyBuffer.GetList();
        for (int i = 0; i < list.Count; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(list[i].keyCode.ToString(), style, GUILayout.Width(30));
            GUILayout.Label(" : " + list[i].desc, style);
            GUILayout.EndHorizontal();
        }

        if (curTest != null)
            curTest.DrawUI();

        EditorGUILayout.EndVertical();
        GUI.backgroundColor = col;
        GUILayout.Space(3);
        GUILayout.EndVertical();
    }

    void DrawTableHeader()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("快捷键", style, GUILayout.Width(30));
        GUILayout.Label(" : 描述", style);
        GUILayout.EndHorizontal();
    }
}

public abstract class AbstractUnitTest : ScriptableObject
{
    public abstract string UnitName { get; }

    public abstract void SetActivity(TestKeyboardBuffer keyBuffer);

    public virtual void DrawUI() { }
}