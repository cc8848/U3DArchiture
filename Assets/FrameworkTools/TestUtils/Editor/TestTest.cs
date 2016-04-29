using UnityEngine;
using UnityEditor;
using System.Collections;

public class TestTest : AbstractUnitTest {

    public override string UnitName { get { return "Test"; } }


    public override void SetActivity(TestKeyboardBuffer keyBuffer)
    {
        keyBuffer.regist(KeyCode.W, "wahaah", () => { Debug.LogError("test hot key T"); });
    }

    public override void DrawUI()
    {
        EditorGUILayout.LabelField("this is a test");
    }
}
