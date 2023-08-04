using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeUI : MonoBehaviour
{
    public Dictionaries dicts;

    public Recipe recipe;
    
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text baseText;

    [SerializeField] private Transform ingredientsParent;
    [SerializeField] private GameObject ingredientUITemplatePrefab;

    void Start() {
        nameText.text = recipe.result;
        baseText.text = "Base: " + recipe.liquidBase;

        for(int i = 0; i < recipe.ingredients.Length; i++) {
            RecipeIngredientUI newingredientUI = Instantiate(ingredientUITemplatePrefab, ingredientsParent).GetComponent<RecipeIngredientUI>();
            newingredientUI.spriteOfIngredient = dicts.ingredientSprites[recipe.ingredients[i]];
        }
    }
}
