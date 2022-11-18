using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;

public enum INGREDIENT { ID, NAME, COST, ICON_PATH, DESCRIPTION, LAST_NO_USE}
public enum POTION_TYPE { ID, NAME, ICON_PATH, LAST_NO_USE}
public enum POTION { ID, NAME, COST ,ICON_PATH, DESCRIPTION, ID_POTION_TYPE, LAST_NO_USE }
public enum RECIPE { ID, NAME, COST, ICON_PATH, DESCRIPTION, LAST_NO_USE}
public enum POTION_INGREDIENT { ID, QUANTITY, ID_POTION, ID_INGREDIENT}
public struct IngredientData
{
    public int id;
    public string name;
    public float cost;
    public Path iconPath;
    public string description;


    public IngredientData(IDataReader p_reader)
    {
        id = p_reader.GetInt32((int)INGREDIENT.ID);
        name = p_reader.GetString((int)INGREDIENT.NAME);
        cost = p_reader.GetFloat((int)INGREDIENT.COST);
        iconPath.path = p_reader.GetString((int)INGREDIENT.ICON_PATH);
        iconPath.id = -1;
        description = p_reader.GetString((int)INGREDIENT.DESCRIPTION);
    }
    
    public override string ToString()
    {
        string toReturn = "ID: " + id + "\n";
        toReturn += "Name: " + name + "\n";
        toReturn += "Cost: " + cost + "\n";
        toReturn += "Icon Path: " + iconPath + "\n";
        toReturn += "Description: " + description + "\n";

        return toReturn;
    }

}

public struct PotionRaw
{

    public int id;
    public string name;
    public float cost;
    public Path iconPath;
    public string description;
    public int potionTypeID;


    public PotionRaw(IDataReader p_reader)
    {
        id = p_reader.GetInt32((int)POTION.ID);
        name = p_reader.GetString((int)POTION.NAME);
        cost = p_reader.GetFloat((int)POTION.COST);
        iconPath.path = p_reader.GetString((int)POTION.ICON_PATH);
        iconPath.id = -1;
        description = p_reader.GetString((int)POTION.DESCRIPTION);
        potionTypeID = p_reader.GetInt32((int)POTION.ID_POTION_TYPE);
    }

    public override string ToString()
    {
        string toReturn = "ID: " + id + "\n";
        toReturn += "Name: " + name + "\n";
        toReturn += "Cost: " + cost + "\n";
        toReturn += "Icon Path: " + iconPath.path + "\n";
        toReturn += "Description: " + description + "\n";
        toReturn += "PotionTypeID: " + potionTypeID+ "\n";

        return toReturn;
    }

}

public struct Potion
{
    public int id;
    public string name;
    public float cost;
    public Path iconPath;
    public string description;
    public PotionType potionType;

    public Potion(PotionRaw p_rawData, PotionType p_potionType)
    {
        id = p_rawData.id;
        name = p_rawData.name;
        cost = p_rawData.cost;
        iconPath = p_rawData.iconPath;
        description = p_rawData.description;
        potionType = p_potionType;
    }

}

public struct PotionType
{
    public int id;
    public string name;
    public Path iconPath;

    public PotionType(IDataReader p_reader)
    {
        id = p_reader.GetInt32((int)POTION_TYPE.ID);
        name = p_reader.GetString((int)POTION_TYPE.NAME);
        iconPath.path = p_reader.GetString((int)POTION_TYPE.ICON_PATH);
        iconPath.id = -1;
    }

    public override string ToString()
    {
        string toReturn = "ID: " + id + "\n";
        toReturn += "Name: " + name + "\n";
        toReturn += "Icon Path: " + iconPath.path + "\n";

        return toReturn;
    }

}

public struct RecipeRaw
{
    public List<PotionIngredient> ingredients;
    public Potion potion;

    public RecipeRaw(IDataReader p_reader, Potion p_potion)
    {
        potion = p_potion;
        ingredients = new List<PotionIngredient>();

        while (p_reader.Read())
        {
            PotionIngredient potionIngredient = new PotionIngredient(p_reader);
            ingredients.Add(potionIngredient);
        }
    }

}

public struct Recipe
{
    public List<IngredientData> ingredients;
    public Potion potion;
    public Recipe(RecipeRaw rawData, Dictionary<int, IngredientData> p_ingredients)
    {
        potion = rawData.potion;

        ingredients = new List<IngredientData>();
        IngredientData ingredient;

        foreach(PotionIngredient ingredientRaw in rawData.ingredients)
        {
            ingredient = p_ingredients[ingredientRaw.id];
            ingredients.Add(ingredient);
        }
    }

}

public struct PotionIngredient
{
    public int id;
    public int quantity;
    public int idPotion;
    public int idIngredient;

    public PotionIngredient(IDataReader p_reader)
    {
        id = p_reader.GetInt32((int)POTION_INGREDIENT.ID);
        quantity = p_reader.GetInt32((int)POTION_INGREDIENT.QUANTITY);
        idPotion = p_reader.GetInt32((int)POTION_INGREDIENT.ID_POTION);
        idIngredient = p_reader.GetInt32((int)POTION_INGREDIENT.ID);
    }

}

public struct Path
{
    public int id;
    public string path;
}