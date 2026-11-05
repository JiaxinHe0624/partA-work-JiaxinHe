using System;
using System.Collections.Generic;

namespace RecipeManagement.Core;

/// <summary>
/// Implement this class using the five Part A collections as private fields:
/// Dictionary&lt;int, Recipe&gt;, List&lt;string&gt;, LinkedList&lt;int&gt;,
/// Stack&lt;int&gt; and Queue&lt;string&gt;.
/// </summary>
public sealed class RecipeManager : IRecipeManager
{
    private readonly Dictionary<int, Recipe> _byId;
    private readonly List<string> _toBuy;
    private readonly LinkedList<int> _plan;
    private readonly Stack<int> _lastRemoved;
    private readonly Queue<string> _steps;
    public RecipeManager(IEnumerable<Recipe> recipes)
    {
        if (recipes is null)
        {
            throw new ArgumentNullException(nameof(recipes));
        }
        _byId = new Dictionary<int, Recipe>();
        _toBuy = new List<string>();
        _plan = new LinkedList<int>();
        _lastRemoved = new Stack<int>();
        _steps = new Queue<string>();
        foreach (Recipe recipe in recipes)
        {
            if (recipe is null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }
            if (recipe.Id <= 0)
            {
                throw new ArgumentException("Please enter positive integer:" + recipe.Id);
            }
            if (string.IsNullOrWhiteSpace(recipe.Title))
            {
                throw new ArgumentException("Please enter Title:ID" + recipe.Id);
            }
            if (_byId.ContainsKey(recipe.Id))
            {
                throw new ArgumentException("Don enter same ID:" + recipe.Id);
            }
            _byId.Add(recipe.Id, recipe);
        }
    }

    public int RecipeCount => _byId.Count;
    public int ShoppingItemCount => _toBuy.Count;
    public int CookingPlanCount => _plan.Count;
    public int PendingInstructionCount => _steps.Count;
    public int RemovedRecipeCount => _lastRemoved.Count;

    public bool AddRecipe(Recipe recipe) =>
        throw new NotImplementedException("Part A: implement AddRecipe.");
    public Recipe? FindRecipe(int recipeId)
    {
        if (_byId.TryGetValue(recipeId, out Recipe? recipe))
        {
            return recipe;
        }

        return null;
    }
    public bool RemoveRecipe(int recipeId) =>
        throw new NotImplementedException("Part A: implement RemoveRecipe.");

    public int AddIngredientsToShoppingList(int recipeId)
    {
        Recipe? recipe = FindRecipe(recipeId);
        if (recipe is null)
        {
            return 0;
        }

        int added = 0;
        foreach (string ingredient in recipe.Ingredients)
        {
            _toBuy.Add(ingredient);
            added++;
        }

        return added;
    }
    public IReadOnlyList<string> GetShoppingList()
    {
        return new List<string>(_toBuy);
    }
    public void ClearShoppingList()
    {
        _toBuy.Clear();
    }
    public bool AddRecipeToCookingPlan(int recipeId) =>
        throw new NotImplementedException("Part A: implement AddRecipeToCookingPlan.");

    public bool RemoveRecipeFromCookingPlan(int recipeId) =>
        throw new NotImplementedException("Part A: implement RemoveRecipeFromCookingPlan.");

    public bool RestoreLastRemovedRecipe() =>
        throw new NotImplementedException("Part A: implement RestoreLastRemovedRecipe.");

    public int? PeekLastRemovedRecipe() =>
        throw new NotImplementedException("Part A: implement PeekLastRemovedRecipe.");

    public IReadOnlyList<int> GetCookingPlan() =>
        throw new NotImplementedException("Part A: implement GetCookingPlan.");

    public bool StartCooking(int recipeId) =>
        throw new NotImplementedException("Part A: implement StartCooking.");

    public string? PeekNextInstruction() =>
        throw new NotImplementedException("Part A: implement PeekNextInstruction.");

    public string? CompleteNextInstruction() =>
        throw new NotImplementedException("Part A: implement CompleteNextInstruction.");

    public IReadOnlyList<Recipe> SearchByTitle(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByTitle.");

    public IReadOnlyList<Recipe> SearchByIngredient(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByIngredient.");

    public IReadOnlyList<Recipe> GetHighestProteinRecipes(int count) =>
        throw new NotImplementedException("Part B: implement GetHighestProteinRecipes.");

    public bool AddSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement AddSavedRecipe.");

    public bool RemoveSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement RemoveSavedRecipe.");

    public bool IsRecipeSaved(int recipeId) =>
        throw new NotImplementedException("Part B: implement IsRecipeSaved.");

    public IReadOnlyList<int> GetSavedRecipes() =>
        throw new NotImplementedException("Part B: implement GetSavedRecipes.");
}
