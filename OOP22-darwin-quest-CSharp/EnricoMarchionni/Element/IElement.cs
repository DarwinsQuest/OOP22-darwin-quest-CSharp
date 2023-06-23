namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;

public interface IElement : IGameObject, INameable
{

    bool IsStronger(IElement other);

    bool IsWeaker(IElement other);
}
