using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alchentimist {

    public class UIHandler : Singleton<UIHandler>
    {
        [Header("Ingredients")]
        [SerializeField] Transform m_ingredientsPanelTr;
        [SerializeField] GameObject m_ingredientPrefab;

        [Header("Recipes")]
        [SerializeField] Transform m_recipesPanelTr;
        [SerializeField] GameObject m_recipePrefab;

        public void InstanceIngredientsToInventory(IngredientData[] p_ingredients)
        {
            GameObject newIngredient;
            IngredientModel ingredientModel;
            foreach(IngredientData ingredientData in p_ingredients)
            {
                newIngredient = Instantiate(m_ingredientPrefab, m_ingredientsPanelTr);
                ingredientModel = new IngredientModel(newIngredient, ingredientData);
                newIngredient.GetComponent<Ingredient>().Data = ingredientData;
            }
        }

        public void InstanceRecipes(Recipe[] p_recipes)
        {
            GameObject newRecipe;
            RecipeModel recipeModel;
            foreach (Recipe recipeData in p_recipes)
            {
                newRecipe = Instantiate(m_recipePrefab, m_recipesPanelTr);
                recipeModel = new RecipeModel(newRecipe, recipeData);
            }
        }
    }
}
