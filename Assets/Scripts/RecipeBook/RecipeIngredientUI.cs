using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeIngredientUI : MonoBehaviour
{
    public Sprite spriteOfIngredient;

    [SerializeField] private Image sprite;
    [SerializeField] private Image spriteShadow;

    void Start() {
        sprite.sprite = spriteOfIngredient;
        spriteShadow.sprite = spriteOfIngredient;
    } 
}
