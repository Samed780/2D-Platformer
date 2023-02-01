using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;


public class CharacterEvent
{
    //damaged taken
    public static UnityAction<GameObject, float> characterDamaged;
    //amount healed
    public static UnityAction<GameObject, float> characterHealed;

}
