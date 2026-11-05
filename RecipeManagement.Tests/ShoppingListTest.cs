using System;
using System.Collections.Generic;
using RecipeManagement.Core;
using Xunit;

namespace RecipeManagement.Tests;

public sealed class ShoppingListTests
{
    private static RecipeManager CreateManager()
    {
        return new RecipeManager(new List<Recipe>
        {
            new Recipe
            {
                Id = 10,
                Title = "Recipe A",
                Ingredients = new List<string> { "1 apple", "2 eggs" }
            }
        });
    }

    [Fact]
    public void AddIngredients_AppendsInOrderAndReturnsCount()
    {
        RecipeManager manager = CreateManager();

        int added = manager.AddIngredientsToShoppingList(10);

        Assert.Equal(2, added);
        Assert.Equal("1 apple", manager.GetShoppingList()[0]);
        Assert.Equal("2 eggs", manager.GetShoppingList()[1]);
    }

    [Fact]
    public void AddIngredients_ReturnsZeroForMissingRecipe()
    {
        RecipeManager manager = CreateManager();

        Assert.Equal(0, manager.AddIngredientsToShoppingList(999));
        Assert.Equal(0, manager.ShoppingItemCount);
    }

    [Fact]
    public void GetShoppingList_ReturnsACopyNotTheInternalList()
    {
        RecipeManager manager = CreateManager();
        manager.AddIngredientsToShoppingList(10);

        var copy = manager.GetShoppingList();
        manager.ClearShoppingList();

        Assert.Equal(0, manager.ShoppingItemCount);
        Assert.Equal(2, copy.Count);
    }
}
