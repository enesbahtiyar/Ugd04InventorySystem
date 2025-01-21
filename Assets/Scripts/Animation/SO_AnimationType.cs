using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "so_animationType", menuName = "Scriptable Objects/Animation/Animation Type")]
public class SO_AnimationType : ScriptableObject
{
    public AnimationClip animationClip;
    public AnimationName animationName;
    public CharacterPartAnimator characterPart;
    public PartVariantColor partVariantColor;
    public PartVariantType partVariantType;
}
