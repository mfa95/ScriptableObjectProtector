
using UnityEngine;
using MFA.SO;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "New_Example_SO")]
public class ExampleSO : ScriptableObjectBase
{
    public float scale = 1;
    [System.NonSerialized] public int nonSerializedInt = 123;
    [SerializeField] private bool isBool;
    [SerializeField] private int intTest;
    [SerializeField] private int intTest2 = 28;
    [SerializeField] private Sprite spriteTest;
    [SerializeField] List<string> list = new List<string>();
    private int privateInt = 0;

    [ContextMenu("printPrivateInt")]
    void printPrivateInt() => Debug.Log(privateInt);

    [ContextMenu("setPrivateInt")]
    void setPrivateInt() => privateInt = Random.Range(1111111, 9999999);

    protected override void ResetSO()
    {
        base.ResetSO();
        Debug.Log("ExitingPlayMode Run!",this);
    }
}
