using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alchentimist
{
    public class IngredientModel
    {
        public Image m_icon;

        public IngredientModel(GameObject p_model)
        {
            m_icon = p_model.transform.GetChild(0).GetComponent<Image>();
        }

        public IngredientModel(GameObject p_model, IngredientData p_data)
        {
            m_icon = p_model.transform.GetChild(0).GetComponent<Image>();
            m_icon.sprite = ResourcesManager.Instance.GetResourcesByID(p_data.iconPath.id, typeof(Sprite));
        }
    }
}
