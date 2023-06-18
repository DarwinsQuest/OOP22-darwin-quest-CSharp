using System.Collections.Immutable;
using System.ComponentModel;
using System.Reflection;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Difficulty;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.World;

namespace OOP22_darwin_quest_CSharp.EnricoMarchionni;

public class Engine : IEngine
{
    private readonly IReadOnlySet<Type> _difficulties = ImmutableSortedSet.Create(typeof(NormalDifficulty));
    private IDifficulty? _difficulty;

    private string GetDifficultyName(Type type)
    {
        return type.GetCustomAttribute(typeof(DescriptionAttribute), false) is not DescriptionAttribute attribute
            ? throw new InvalidOperationException($"Difficulties should always have a {nameof(DescriptionAttribute)}")
            : attribute.Description;
    }

    public IReadOnlySet<string> Difficulties => _difficulties
        .Select(c => GetDifficultyName(c))
        .ToImmutableSortedSet();

    public IBoard Board
    {
        get
        {
            if (_difficulty is null)
            {
                throw new InvalidOperationException();
            }
            return _difficulty.Board;
        }
    }

    public bool StartGame(string difficulty)
    {
        if (_difficulty is not null)
        {
            return false;
        }

        if (Difficulties.Contains(difficulty))
        {
            _difficulty = Activator.CreateInstance(_difficulties.First(c => GetDifficultyName(c) == difficulty)) as IDifficulty;
        }

        return _difficulty is not null;
    }

    public bool IsGameOver()
    {
        throw new NotImplementedException();
    }
}
