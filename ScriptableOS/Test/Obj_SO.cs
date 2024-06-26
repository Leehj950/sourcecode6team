using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/InteractItem", order = 0)]
public class Obj_SO : ScriptableObject
{
    public float TextFadeOutSpeed;
    public string promptText;
}

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/Script", order = 1)]
public class ScriptSO : ScriptableObject
{
    public Sprite[] images;
    public string[] speakers;
    [TextArea] public string[] bodyScripts;
    public AudioClip[] audioClips;
}
