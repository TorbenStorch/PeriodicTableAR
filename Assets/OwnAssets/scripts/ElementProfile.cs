using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType {O, H, Fe, Water}
[CreateAssetMenu]
public class ElementProfile : ScriptableObject
{
    public List<ElementType> canInteractWith;

    public ElementType type;

    public GameObject canSpawn;

}
