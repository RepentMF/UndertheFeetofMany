using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Diary : Item
{
    public override Item Clone()
    {
        Diary diary = ScriptableObject.CreateInstance<Diary>();
        diary.Name = this.Name;
        diary.Description = this.Description;
        diary.Sprite = this.Sprite;
        diary.Category = this.Category;
        return diary;
    }
}