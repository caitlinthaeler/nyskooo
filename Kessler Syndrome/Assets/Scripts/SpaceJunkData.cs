using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpaceJunkData", menuName ="ScriptableObjects/SpaceJunkData", order = 1)]
public class SpaceJunkData : ScriptableObject
{
    //these values should not be changed outside of the object
    public int playerDamage;
    public int mass;
}
