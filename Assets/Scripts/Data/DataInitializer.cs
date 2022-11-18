using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Alchentimist
{
    public class DataInitializer : Singleton<DataInitializer>
    {
        DBManager m_DBManager;

        Dictionary<int, IngredientData> m_ingredients;
        Dictionary<int, Potion> m_potions;
        Dictionary<int, Recipe> m_recipes;
        Dictionary<int, PotionType> m_potionTypes;

        protected override void Awake()
        {
            base.Awake();
            m_ingredients = new Dictionary<int, IngredientData>();
            m_potionTypes = new Dictionary<int, PotionType>();
            m_potions = new Dictionary<int, Potion>();
            m_recipes = new Dictionary<int, Recipe>();

            m_DBManager = FindObjectOfType<DBManager>();
        }
        private void Start()
        {
            LoadDataFromDB();
            LoadResources();
            FindObjectOfType<RecipeInventory>().InitializeRecipes();

            UIHandler.Instance.InstanceIngredientsToInventory(m_ingredients.Values.ToArray());
        }

        private void LoadIngredientsResources()
        {
            ResourcesManager resourcesManager = ResourcesManager.Instance;
            IngredientData ingredient;

            int[] ingredientIDs = m_ingredients.Keys.ToArray();

            for (int i = 0; i < ingredientIDs.Length; i++)
            {
                ingredient = m_ingredients[ingredientIDs[i]];
                if(!resourcesManager.IsResourceAlreadyAdded(ingredient.iconPath.path, typeof(Texture2D)))
                {
                    Texture2D texture = Resources.Load(ingredient.iconPath.path) as Texture2D;

                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                    ingredient.iconPath.id = resourcesManager.AddResource<Sprite>(sprite, ingredient.iconPath);
                }
                m_ingredients[ingredientIDs[i]] = ingredient;
            }
        }

        private void LoadPotionsResources()
        {
            ResourcesManager resourcesManager = ResourcesManager.Instance;
            Potion potion;

            int[] potionIDs = m_potions.Keys.ToArray();

            for (int i = 0; i < potionIDs.Length; i++)
            {
                potion = m_potions[potionIDs[i]];
                if (!resourcesManager.IsResourceAlreadyAdded(potion.iconPath.path, typeof(Texture2D)))
                {
                    Texture2D texture = Resources.Load(potion.iconPath.path) as Texture2D;

                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                    potion.iconPath.id = resourcesManager.AddResource<Sprite>(sprite, potion.iconPath);
                }
                m_potions[potionIDs[i]] = potion;
            }

        }

        private void LoadPotionTypesResources()
        {
            ResourcesManager resourcesManager = ResourcesManager.Instance;
            PotionType potionType;

            int[] potionTypeIDs = m_potionTypes.Keys.ToArray();

            for (int i = 0; i < potionTypeIDs.Length; i++)
            {
                potionType = m_potionTypes[potionTypeIDs[i]];
                if (!resourcesManager.IsResourceAlreadyAdded(potionType.iconPath.path, typeof(Texture2D)))
                {
                    Texture2D texture = Resources.Load(potionType.iconPath.path) as Texture2D;

                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                    potionType.iconPath.id = resourcesManager.AddResource<Sprite>(sprite, potionType.iconPath);
                }
                m_potionTypes[potionTypeIDs[i]] = potionType;
            }
        }

        private void LoadResources()
        {
            LoadPotionsResources();
            LoadIngredientsResources();
            LoadPotionTypesResources();
        }

        public void LoadDataFromDB()
        {
            m_DBManager.Initialize();
            m_ingredients = m_DBManager.LoadIngredientsFromDB();
            m_potionTypes = m_DBManager.LoadPotionTypesFromDB();
            ProcessPotionRawData(m_DBManager.LoadPotionsFromDB());

            m_DBManager.LoadRecipesFromDB(m_potions.Values.ToArray());

            ProcessRecipeRawData(m_DBManager.LoadRecipesFromDB(m_potions.Values.ToArray()));

        }
        public void ProcessPotionRawData(List<PotionRaw> p_rawData)
        {
            Potion potion;
            foreach(PotionRaw rawData in p_rawData)
            {
                potion = new Potion(rawData, m_potionTypes[rawData.potionTypeID]);
                m_potions.Add(potion.id ,potion);
            }
        }

        public void ProcessRecipeRawData(List<RecipeRaw> p_rawData)
        {
            Recipe recipe;
            foreach (RecipeRaw rawData in p_rawData)
            {
                recipe = new Recipe(rawData, m_ingredients);
                m_recipes.Add(rawData.potion.id, recipe);
            }
        }

        public Recipe[] GetRecipes() { return m_recipes.Values.ToArray(); }

    }
}
