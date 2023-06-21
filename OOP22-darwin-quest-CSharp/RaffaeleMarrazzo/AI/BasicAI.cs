using System.Collections.Immutable;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Decision;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.AI;
public class BasicAI : IAI
{
    private const int MOVE_DECISION_BOUND = 19;
    private const int SWAP_DECISION_BOUND = 2;
    private readonly Random _generator;
    private readonly IImmutableDictionary<IDecision, int> _decisions;

    public BasicAI(int seed)
    {
        _generator = new Random(seed);
        _decisions = ImmutableDictionary.Create<IDecision, int>()
            .Add(new SwapDecision(), SWAP_DECISION_BOUND)
            .Add(new MoveDecision(), MOVE_DECISION_BOUND);
    }

    public BasicAI()
    {
        _generator = new Random();
        _decisions = ImmutableDictionary.Create<IDecision, int>()
            .Add(new SwapDecision(), SWAP_DECISION_BOUND)
            .Add(new MoveDecision(), MOVE_DECISION_BOUND);
    }

    public IBanion DecideBanionDeployment(ICollection<IBanion> banions) => GetRandomElement(banions);

    public IBanion? DecideBanionSwap(ICollection<IBanion> banions)
    {
        ICollection<IBanion> remainingBanions = banions.Where(banion => banion.IsAlive).ToList();
        return remainingBanions.Count == 0 ? null : GetRandomElement(remainingBanions);
    }

    public IMove DecideMoveSelection(ICollection<IMove> moves) => GetRandomElement(moves);

    public IDecision GetDecision()
    {
        var value = _generator.Next(MOVE_DECISION_BOUND + 1);
        return GetRelatedKey(_decisions, GetNearestNumber(value, new List<int>(_decisions.Values)));
    }

    public override int GetHashCode() => HashCode.Combine(_generator, _decisions);

    private static K GetRelatedKey<K, V>(IImmutableDictionary<K, V> dict, V value)
    {
        ISet<KeyValuePair<K, V>> entrySet = new HashSet<KeyValuePair<K, V>>();
        foreach (var key in dict.Keys)
        {
            entrySet.Add(new KeyValuePair<K, V>(key, dict[key]));
        }
        return entrySet.Where(entry => entry.Value.Equals(value)).First().Key;
    }

    private static int GetNearestNumber(int num, ICollection<int> numbers)
    {
        int diff = int.MaxValue;
        int? nearestNumber = null;
        ICollection<int> remainingNumbers = numbers.Where(number => num <= number).ToList();
        foreach (var number in remainingNumbers)
        {
            if (number - num <= diff)
            {
                nearestNumber = number;
                diff = number - num;
            }
        }
        int retVal = nearestNumber.Value;
        return retVal;
    }

    private T GetRandomElement<T>(ICollection<T> coll) => coll.ToList()[_generator.Next(coll.Count)];
}
