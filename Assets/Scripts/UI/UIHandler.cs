using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alchentimist {

    public class UIHandler : Singleton<UIHandler>
    {
        [Header("Ingredients")]
        [SerializeField] IngredientsInventory m_inventory;
        [SerializeField] GameObject m_ingredientPrefab;

        [Header("Recipes")]
        [SerializeField] Transform m_recipesPanelTr;
        [SerializeField] GameObject m_recipePrefab;

        public void InstanceIngredientsToInventory(IngredientData[] p_ingredients)
        {
            GameObject newIngredient;
            IngredientModel ingredientModel;
            Ingredient ingredient;

            List<Draggable> m_ingredientList = new List<Draggable>();

            foreach(IngredientData ingredientData in p_ingredients)
            {
                newIngredient = Instantiate(m_ingredientPrefab);
                ingredientModel = new IngredientModel(newIngredient, ingredientData);
                ingredient = newIngredient.GetComponent<Ingredient>();
                ingredient.Data = ingredientData;
                m_ingredientList.Add(ingredient);
            }
            m_inventory.InitializeSpacesFromList(m_ingredientList);
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

        public void OpenRecipesUI()
        {
            m_recipePopUpGO.SetActive(true);
            m_mainScreenGO.SetActive(false);
        }

        [SerializeField] GameObject m_mainScreenGO;
        [SerializeField] GameObject m_recipePopUpGO;

        public void CloseRecipesUI()
        {
            m_recipePopUpGO.SetActive(false);
            m_mainScreenGO.SetActive(true);
        }

    }
}
