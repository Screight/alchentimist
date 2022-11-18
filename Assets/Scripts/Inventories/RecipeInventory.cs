using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alchentimist
{
    public class RecipeInventory : DynamicItemList
    {
        Recipe[] m_recipes;
        [SerializeField] TMPro.TextMeshProUGUI m_recipeIndexCount;
        [SerializeField] Image m_potionResultImage;
        [SerializeField] TMPro.TextMeshProUGUI m_potionNameText;

        public void ShowRecipeInUI(Recipe p_recipe)
        {
            for(int i = 0; i < m_itemSpaces.Count; i++)
            {
                if(i < p_recipe.ingredients.Count)
                {
                    Image image = m_itemSpaces[i].Transform.GetChild(0).GetComponent<Image>();
                    image.enabled = true;
                    image.sprite = ResourcesManager.Instance.GetResourceByPath(p_recipe.ingredients[i].iconPath.path, typeof(Sprite));
                }
                else
                {
                    m_itemSpaces[i].Transform.GetChild(0).GetComponent<Image>().enabled = false;
                }
            }
            m_potionResultImage.sprite = ResourcesManager.Instance.GetResourceByPath(p_recipe.potion.iconPath.path, typeof(Sprite));
            m_potionNameText.text = p_recipe.potion.name;
        }

        public void InitializeRecipes()
        {
            m_recipes = DataInitializer.Instance.GetRecipes();

            for (int i = 0; i < m_itemSpaces.Count; i++)
            {
                Image image = m_itemSpaces[i].Transform.GetChild(0).GetComponent<Image>();
                image.enabled = false;
            }
            m_currentRecipeIndex = 0;
            ShowRecipeInUI(m_recipes[m_currentRecipeIndex]);
            UpdateIndexCount();
            UIHandler.Instance.CloseRecipesUI();
        }

        int m_currentRecipeIndex;
        public void ShowNextRecipe() {
            if(m_currentRecipeIndex < m_recipes.Length - 1)
            {
                m_currentRecipeIndex++;
                ShowRecipeInUI(m_recipes[m_currentRecipeIndex]);
                UpdateIndexCount();
            }
        }

        public void ShowPreviousRecipe()
        {
            if (m_currentRecipeIndex > 0)
            {
                m_currentRecipeIndex--;
                ShowRecipeInUI(m_recipes[m_currentRecipeIndex]);
                UpdateIndexCount();
            }
        }

        void UpdateIndexCount()
        {
            m_recipeIndexCount.text = (m_currentRecipeIndex + 1).ToString() + " / " + m_recipes.Length.ToString();
        }

    }
}
