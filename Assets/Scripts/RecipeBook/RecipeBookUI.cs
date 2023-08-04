using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBookUI : MonoBehaviour
{
    [SerializeField] private Dictionaries dicts;

    [SerializeField] private Transform recipesParent;
    [SerializeField] private GameObject recipeUITemplatePrefab;

    [SerializeField] private GameObject playerDrag;

    private Animator recipeBookAC;

    void Start() {
        for(int i = 0; i < dicts.recipes.Length; i++) {
            Recipe recipeToRender = dicts.recipes[i];

            if(recipeToRender.result != "_empty") {
                RecipeUI newRecipeUI = Instantiate(recipeUITemplatePrefab, recipesParent).GetComponent<RecipeUI>();
                newRecipeUI.dicts = dicts;
                newRecipeUI.recipe = recipeToRender;
            }
        }

        recipeBookAC = GetComponent<Animator>();
    }

    public void OpenOrClose() {
        recipeBookAC.SetBool("show", !recipeBookAC.GetBool("show"));
        playerDrag.SetActive(!recipeBookAC.GetBool("show"));
    }
}
