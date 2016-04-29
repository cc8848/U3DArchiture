using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IKeyDelegate
{
    KeyCode keyCode { get; }
    string desc { get; }
    System.Action del { get; }
}

class KeyboardeDelegate : IKeyDelegate
{
    public KeyCode keyCode;
    public string desc;
    public System.Action del;

    static public KeyboardeDelegate Create(KeyCode key, string des, System.Action del)
    {
        KeyboardeDelegate res = new KeyboardeDelegate();
        res.keyCode = key;
        res.desc = des;
        res.del = del;
        return res;
    }

    KeyCode IKeyDelegate.keyCode
    {
        get { return keyCode; }
    }

    string IKeyDelegate.desc
    {
        get { return desc; }
    }

    System.Action IKeyDelegate.del
    {
        get { return del; }
    }
}

public class TestKeyboardBuffer
{
    List<IKeyDelegate> listDel = new List<IKeyDelegate>();

    static TestKeyboardBuffer s_instance;

    public TestKeyboardBuffer()
    {
        s_instance = this;
    }

    public void regist(KeyCode key, string des, System.Action del)
    {
        listDel.Add(KeyboardeDelegate.Create(key, des, del));
    }

    public void Clear()
    {
        listDel.Clear();
    }

    public static void Update()
    {
        if (s_instance != null)
            s_instance.__Update();
    }

    void __Update()
    {
        int count = listDel.Count;
        for (int i = 0; i < count; i++)
        {
            if (Input.GetKey(KeyCode.T) && Input.GetKeyDown(listDel[i].keyCode))
                listDel[i].del();
        }
    }

    public List<IKeyDelegate> GetList()
    {
        return listDel;
    }
}
