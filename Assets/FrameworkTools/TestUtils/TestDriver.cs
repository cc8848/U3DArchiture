using UnityEngine;
using System.Collections;

public class TestDriver : MonoBehaviour {

	void Update()
	{
#if UNITY_EDITOR
		TestKeyboardBuffer.Update();
#endif
	}
}
