using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DBManager : MonoBehaviour
{
    const string TABLE_INGREDIENTS_NAME = "ingredients";
    const string TABLE_POTIONS_NAME = "potions";
    const string TABLE_POTION_TYPES_NAME = "potion_types";

    [Header("DB")]
    private IDbConnection dbConnection;

    private void Awake() {
        OpenDatabase();
    }

    public void Initialize()
    {
        LoadIngredientsFromDB();
    }

    private void OpenDatabase()
    {
        string dbUri = "URI=file:alchENTImist.db";
        dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();
    }

    public Dictionary<int, IngredientData> LoadIngredientsFromDB()
    {
        Dictionary<int, IngredientData> ingredients = new Dictionary<int, IngredientData>();

        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = SelectAllFrom(TABLE_INGREDIENTS_NAME);

        IDataReader dataReader = cmd.ExecuteReader();

        IngredientData ingredient;
        while (dataReader.Read()) {
            ingredient = new IngredientData(dataReader);
            ingredients.Add(ingredient.id, ingredient);
        }
        return ingredients;
    }
    public Dictionary<int, PotionType> LoadPotionTypesFromDB()
    {
        Dictionary<int, PotionType> potionTypes = new Dictionary<int ,PotionType>();
        string query = SelectAllFrom(TABLE_POTION_TYPES_NAME);

        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        PotionType potionType;
        while (dataReader.Read()) {
            potionType = new PotionType(dataReader);
            potionTypes.Add(potionType.id, potionType);
        }
        return potionTypes;
    }
    public List<PotionRaw> LoadPotionsFromDB()
    {
        List<PotionRaw> potions = new List<PotionRaw>();

        string query = SelectAllFrom(TABLE_POTIONS_NAME);

        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        PotionRaw potionRaw;
        while (dataReader.Read())
        {
            potionRaw = new PotionRaw(dataReader);
            potions.Add(potionRaw);
        }
        return potions;

    }
    public List<RecipeRaw> LoadRecipesFromDB(Potion[] p_potions) {

        List<RecipeRaw> recipes = new List<RecipeRaw>();

        IDbCommand cmd;

        RecipeRaw recipeRaw;

        foreach(Potion potion in p_potions)
        {
            cmd = dbConnection.CreateCommand();
            cmd.CommandText =
                "SELECT * FROM potions_ingredients " +
                "WHERE id_potion = " + potion.id + ";";
            IDataReader dataReader = cmd.ExecuteReader();
            recipeRaw = new RecipeRaw(dataReader, potion);
            recipes.Add(recipeRaw);
        }
        
        return recipes;

    }
    private string SelectAllFrom(string p_tableName) { return "SELECT * FROM " + p_tableName + ";"; }

}
