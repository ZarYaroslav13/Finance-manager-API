namespace DataLayer.Models.Base;

public abstract class Entity
{
    public int Id { get; set; }

    protected bool AreEqualLists<T>(List<T>? list1, List<T>? list2) where T : class
    {
        return (list1 == null && list2 == null)
                    || (list1 != null && list2 != null && Enumerable.SequenceEqual(list1, list2));
    }

    public static bool operator ==(Entity? left, Entity? right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left == null || right == null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity? left, Entity? right)
    {
        return !(left == right);
    }
}
