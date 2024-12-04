namespace ApplicationLayer.Models.Base;

public class ModelDTO
{
    public int Id { get; set; }

    protected bool AreEqualLists<T>(List<T>? list1, List<T>? list2) where T : class
    {
        return (list1 == null && list2 == null)
                    || (list1 != null && list2 != null && Enumerable.SequenceEqual(list1, list2));
    }

    public static bool operator ==(ModelDTO? left, ModelDTO? right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(ModelDTO? left, ModelDTO? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        return Id == ((ModelDTO)obj).Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    protected int GetHashCodeOfList<T>(List<T>? list) where T : class
    {
        if (list == null || list.Count == 0)
            return 0;

        int hashCode = 0;

        foreach (var item in list)
        {
            hashCode += item.GetHashCode();
        }

        return hashCode;
    }
}
