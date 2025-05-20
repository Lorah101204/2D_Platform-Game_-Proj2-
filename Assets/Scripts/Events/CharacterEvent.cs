using UnityEngine;
using UnityEngine.Events;

public class CharacterEvent
{
    public static UnityAction<GameObject, int> onCharacterTookenDamage;
    public static UnityAction<GameObject, int> onCharacterHealed;
}