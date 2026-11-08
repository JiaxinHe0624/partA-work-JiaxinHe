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

    public bool AddRecipe(Recipe recipe)
    {
        if (recipe is null)
        {
            throw new ArgumentNullException(nameof(recipe));
        }

        if (recipe.Id <= 0 || string.IsNullOrWhiteSpace(recipe.Title))
        {
            return false;
        }

        if (_byId.ContainsKey(recipe.Id))
        {
            return false;
        }

        _byId.Add(recipe.Id, recipe);
        return true;
    }
    public Recipe? FindRecipe(int recipeId)
    {
        if (_byId.TryGetValue(recipeId, out Recipe? recipe))
        {
            return recipe;
        }

        return null;
    }
    public bool RemoveRecipe(int recipeId)
    {
        if (!_byId.ContainsKey(recipeId))
        {
            return false;
        }

        if (_plan.Contains(recipeId))
        {
            return false;
        }

        return _byId.Remove(recipeId);
    }

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
    public bool AddRecipeToCookingPlan(int recipeId)
    {
        if (FindRecipe(recipeId) is null)
        {
            return false;
        }

        if (_plan.Contains(recipeId))
        {
            return false;
        }

        _plan.AddLast(recipeId);
        return true;
    }

    public bool RemoveRecipeFromCookingPlan(int recipeId)
    {
        if (!_plan.Remove(recipeId))
        {
            return false;
        }

        _lastRemoved.Push(recipeId);
        return true;
    }

    public bool RestoreLastRemovedRecipe()
    {
        if (_lastRemoved.Count == 0)
        {
            return false;
        }

        int recipeId = _lastRemoved.Pop();
        if (FindRecipe(recipeId) is null)
        {
            return false;
        }

        if (_plan.Contains(recipeId))
        {
            return false;
        }

        _plan.AddLast(recipeId);
        return true;
    }

    public int? PeekLastRemovedRecipe()
    {
        if (_lastRemoved.Count == 0)
        {
            return null;
        }

        return _lastRemoved.Peek();
    }

    public IReadOnlyList<int> GetCookingPlan()
    {
        return new List<int>(_plan);
    }

    public bool StartCooking(int recipeId)
    {
        Recipe? recipe = FindRecipe(recipeId);
        if (recipe is null)
        {
            return false;
        }

        if (recipe.Instructions.Count == 0)
        {
            return false;
        }

        _steps.Clear();
        foreach (string instruction in recipe.Instructions)
        {
            _steps.Enqueue(instruction);
        }

        return true;
    }

    public string? PeekNextInstruction()
    {
        if (_steps.Count == 0)
        {
            return null;
        }

        return _steps.Peek();
    }

    public string? CompleteNextInstruction()
    {
        if (_steps.Count == 0)
        {
            return null;
        }

        return _steps.Dequeue();
    }

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
