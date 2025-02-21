namespace FinanceManager.Domain.Models;

public struct Period
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public static bool operator ==(Period? left, Period? right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Period? left, Period? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var period = (Period)obj;

        return StartDate == period.StartDate
            && EndDate == period.EndDate;

    }

    public override int GetHashCode()
    {
        return HashCode.Combine(StartDate, EndDate);
    }
}
