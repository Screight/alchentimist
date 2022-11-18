using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alchentimist
{
    public class RecipeModel
    {
        private List<IngredientModel> m_ingredientModel;
        private Image m_iconPotionResult;

        public RecipeModel(GameObject p_model)
        {
            InitializeToDefault(p_model);
        }

        public RecipeModel(GameObject p_model, Recipe p_data)
        {
            InitializeToDefault(p_model);

            for(int i = 0; i < m_ingredientModel.Count && i < p_data.ingredients.Count; i++)
            {
                m_ingredientModel[i].m_icon.enabled = true; 
                m_ingredientModel[i].m_icon.sprite = ResourcesManager.Instance.GetResourceByPath(p_data.ingredients[i].iconPath.path, typeof(Sprite));

                m_iconPotionResult.sprite = ResourcesManager.Instance.GetResourceByPath(p_data.potion.iconPath.path, typeof(Sprite));
                
            }
        }

        private void InitializeToDefault(GameObject p_model)
        {
            m_ingredientModel = new List<IngredientModel>();

            Transform ingredientParentTr = p_model.transform.GetChild(0);
            int numberOfIngredients = ingredientParentTr.childCount;

            {
                GameObject gO;
                for (int i = 0; i < numberOfIngredients; i++)
                {
                    gO = ingredientParentTr.GetChild(i).gameObject;
                    m_ingredientModel.Add(new IngredientModel(gO));
                    m_ingredientModel[i].m_icon.enabled = false;
                }
            }

            m_iconPotionResult = p_model.transform.GetChild(2).GetComponent<Image>();
        }

    }
}
