using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderController : MonoBehaviour
{
    public List<CollectibleData> ingredients;
    private LiquidHolderController liquidBase;

    private Recipe[] recipes;

    void Start() {
        liquidBase = GetComponent<LiquidHolderController>();

        recipes = GameObject.FindGameObjectWithTag("GameController").GetComponent<Dictionaries>().recipes;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.B)) {
            Blend();
        }
    }

    public void Blend() {
        string drinkToMake = "_empty";
        float volumeToMake = 0;

        for(int i = 0; i < recipes.Length; i++) {
            float newVolume = CheckRecipe(recipes[i]);

            if(newVolume > volumeToMake) {
                volumeToMake = newVolume;
                drinkToMake = recipes[i].result;
            }
        }
        
        if(volumeToMake > 0) {
            EmptyBlender();
            liquidBase.liquid = drinkToMake;
            liquidBase.volumeOfLiquid = volumeToMake;
        }
    }

    //returns the volume to make of this recipe
    float CheckRecipe(Recipe recipe) {
        int volumeMultiple = 0;

        List<CollectibleData> availableIngredients = new List<CollectibleData>(ingredients);
        string availableLiquid = liquidBase.liquid;
        float availableLiquidVolume = liquidBase.volumeOfLiquid;

        bool canMakeMore = availableLiquid == recipe.liquidBase;

        int loops = 100;
        while(loops > 0 && canMakeMore) {
            loops--;

            //Check base volume
            if(availableLiquidVolume >= recipe.liquidVolume) {
                availableLiquidVolume -= recipe.liquidVolume;
            } else {
                canMakeMore = false;
            }

            //Check ingredients
            for(int i = 0; i < recipe.ingredients.Length; i++) {
                bool foundIngredient = false;
        
                //find ingredient
                for(int j = 0; j < availableIngredients.Count; j++) {
                    //Debug.Log(availableIngredients[j].collectibleName + " " + recipe.ingredients[i]);
                    if(availableIngredients[j].collectibleName == recipe.ingredients[i] && !foundIngredient) {
                        availableIngredients.RemoveAt(j);
                        foundIngredient = true;
                    } 
                }
                if(!foundIngredient) { 
                    canMakeMore = false;
                } 
            }      
            
            if(canMakeMore) volumeMultiple += 1;     
        }
        return recipe.resultVolume * volumeMultiple;
    }

    void EmptyBlender() {
        for(int i = 0; i < ingredients.Count; i++) {
            Destroy(ingredients[i].gameObject);
        }

        ingredients.Clear();
        liquidBase.volumeOfLiquid = 0;
    }
}
