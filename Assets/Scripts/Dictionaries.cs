using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[System.Serializable]
public class LiquidMaterialsDict : SerializableDictionaryBase<string, Material> {
}

[System.Serializable] 
public class IngredientSpritesDict : SerializableDictionaryBase<string, Sprite> {

}

[System.Serializable]
public struct Recipe {
    public string result;
    public float resultVolume;
    public string[] ingredients;
    public string liquidBase;
    public float liquidVolume;
    public float timeToBlend;
}

[System.Serializable]
public struct DrinkOrderInfo {
    public string drinkName;
    public float cost;
    public float volumeOfDrink;
}


public class Dictionaries : MonoBehaviour
{                       
    [SerializeField]  
    public LiquidMaterialsDict liquidMaterials;

    [SerializeField]  
    public IngredientSpritesDict ingredientSprites;
 
    [SerializeField]
    public Recipe[] recipes;

    [SerializeField]
    public DrinkOrderInfo[] drinkOrderInfo; 
}

