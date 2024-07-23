namespace DomainLayer.Models.Base;

public abstract class Model
{
    public int Id { get; set; }

    protected bool AreEqualLists<T>(List<T>? list1, List<T>? list2) where T : class
    {
        return (list1 == null && list2 == null)
                    || (list1 != null && list2 != null && Enumerable.SequenceEqual(list1, list2));
    }

    public static bool operator ==(Model? left, Model? right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Model? left, Model? right)
    {
        return !(left == right);
    }
}
