using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject damageText;
    public GameObject healthText;

    public Canvas canvas;

    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        canvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvent.onCharacterTookenDamage += CharacterTookenDamage;
        CharacterEvent.onCharacterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvent.onCharacterTookenDamage -= CharacterTookenDamage;
        CharacterEvent.onCharacterHealed -= CharacterHealed;
    }

    public void CharacterTookenDamage(GameObject character, int damage)
    {
        Vector3 spawnPosition = cam.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(damageText, spawnPosition, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damage.ToString();
    }

    public void CharacterHealed(GameObject character, int heal)
    {
        Vector3 spawnPosition = cam.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthText, spawnPosition, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        tmpText.text = heal.ToString();
    }
}
