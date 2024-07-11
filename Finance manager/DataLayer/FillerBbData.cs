using DataLayer.Models;

namespace DataLayer;

public class FillerBbData
{
    public static List<Account> Accounts { get { return _accounts; } }

    public static List<Wallet> Wallets { get { return _wallets; } }

    public static List<TransactionType> TransactionTypes { get { return _transactionTypes; } }

    public static List<Transaction> Transactions { get { return _transactions; } }

    private static PasswordCoder _passwordCoder = new();

    private static List<Account> _accounts = new()
    {
        new Account()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = _passwordCoder.ComputeSHA256Hash("password123"),
        },
        new Account()
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Password = _passwordCoder.ComputeSHA256Hash("password456"),
        },
        new Account()
        {
            Id = 3,
            FirstName = "Michael",
            LastName = "Johnson",
            Email = "michael.johnson@example.com",
            Password = _passwordCoder.ComputeSHA256Hash("password789"),
        },
        new Account()
        {
            Id = 4,
            FirstName = "Emily",
            LastName = "Davis",
            Email = "emily.davis@example.com",
            Password = _passwordCoder.ComputeSHA256Hash("password101"),
        },
        new Account()
        {
            Id = 5,
            FirstName = "Chris",
            LastName = "Brown",
            Email = "chris.brown@example.com",
            Password = _passwordCoder.ComputeSHA256Hash("password102"),
        }
    };

    private static List<Wallet> _wallets = new()
    {
       new Wallet()
        {
            Id = 1,
            Balance = 1000,
            AccountId = 1
        },
        new Wallet()
        {
            Id = 2,
            Balance = 1500,
            AccountId = 1
        },
        new Wallet()
        {
            Id = 3,
            Balance = 2000,
            AccountId = 2
        },
        new Wallet()
        {
            Id = 4,
            Balance = 2500,
            AccountId = 5
        },
        new Wallet()
        {
            Id = 5,
            Balance = 3000,
            AccountId = 3
        },
        new Wallet()
        {
            Id = 6,
            Balance = 3500,
            AccountId = 4
        },
        new Wallet()
        {
            Id = 7,
            Balance = 4000,
            AccountId = 5
        }
    };

    private static List<TransactionType> _transactionTypes = new()
    {
        new TransactionType()
        {
            Id = 1,
            Name = "Salary",
            Description = "Monthly salary",
            EntryType = EntryType.Income,
            WalletId = 1
        },
        new TransactionType()
        {
            Id = 2,
            Name = "Groceries",
            Description = "Weekly groceries",
            EntryType = EntryType.Exponse,
            WalletId = 1
        },
        new TransactionType()
        {
            Id = 3,
            Name = "Rent",
            Description = "Monthly rent payment",
            EntryType = EntryType.Exponse,
            WalletId = 2
        },
        new TransactionType()
        {
            Id = 4,
            Name = "Electricity Bill",
            Description = "Monthly electricity bill",
            EntryType = EntryType.Exponse,
            WalletId = 2
        },
        new TransactionType()
        {
            Id = 5,
            Name = "Freelance",
            Description = "Freelance project payment",
            EntryType = EntryType.Income,
            WalletId = 3
        },
        new TransactionType()
        {
            Id = 6,
            Name = "Dining Out",
            Description = "Dinner at restaurant",
            EntryType = EntryType.Exponse,
            WalletId = 3
        },
        new TransactionType()
        {
            Id = 7,
            Name = "Gym Membership",
            Description = "Monthly gym membership",
            EntryType = EntryType.Exponse,
            WalletId = 4
        },
        new TransactionType()
        {
            Id = 8,
            Name = "Gift",
            Description = "Birthday gift",
            EntryType = EntryType.Exponse,
            WalletId = 4
        },
        new TransactionType()
        {
            Id = 9,
            Name = "Consulting",
            Description = "Consulting services",
            EntryType = EntryType.Income,
            WalletId = 5
        },
        new TransactionType()
        {
            Id = 10,
            Name = "Travel",
            Description = "Travel expenses",
            EntryType = EntryType.Exponse,
            WalletId = 5
        },
        new TransactionType()
        {
            Id = 11,
            Name = "Investment",
            Description = "Stock market investment",
            EntryType = EntryType.Exponse,
            WalletId = 6
        },
        new TransactionType()
        {
            Id = 12,
            Name = "Dividends",
            Description = "Stock dividends",
            EntryType = EntryType.Income,
            WalletId = 6
        },
        new TransactionType()
        {
            Id = 13,
            Name = "Savings",
            Description = "Monthly savings",
            EntryType = EntryType.Income,
            WalletId = 7
        },
        new TransactionType()
        {
            Id = 14,
            Name = "Car Maintenance",
            Description = "Car repair and maintenance",
            EntryType = EntryType.Exponse,
            WalletId = 7
        },
        new TransactionType()
        {
            Id = 15,
            Name = "Internet Bill",
            Description = "Monthly internet bill",
            EntryType = EntryType.Exponse,
            WalletId = 1
        },
        new TransactionType()
        {
            Id = 16,
            Name = "Bonus",
            Description = "Annual bonus",
            EntryType = EntryType.Income,
            WalletId = 1
        },
        new TransactionType()
        {
            Id = 17,
            Name = "Insurance",
            Description = "Health insurance payment",
            EntryType = EntryType.Exponse,
            WalletId = 2
        },
        new TransactionType()
        {
            Id = 18,
            Name = "Book Sales",
            Description = "Income from book sales",
            EntryType = EntryType.Income,
            WalletId = 2
        },
        new TransactionType()
        {
            Id = 19,
            Name = "Clothing",
            Description = "Purchase of clothing",
            EntryType = EntryType.Exponse,
            WalletId = 3
        },
        new TransactionType()
        {
            Id = 20,
            Name = "Interest",
            Description = "Bank account interest",
            EntryType = EntryType.Income,
            WalletId = 3
        },
        new TransactionType()
        {
            Id = 21,
            Name = "Medical Bills",
            Description = "Payment for medical services",
            EntryType = EntryType.Exponse,
            WalletId = 4
        },
        new TransactionType()
        {
            Id = 22,
            Name = "Tuition",
            Description = "Payment for education",
            EntryType = EntryType.Exponse,
            WalletId = 4
        },
        new TransactionType()
        {
            Id = 23,
            Name = "Software Sales",
            Description = "Income from software sales",
            EntryType = EntryType.Income,
            WalletId = 5
        },
        new TransactionType()
        {
            Id = 24,
            Name = "Household Supplies",
            Description = "Purchase of household supplies",
            EntryType = EntryType.Exponse,
            WalletId = 5
        },
        new TransactionType()
        {
            Id = 25,
            Name = "Freelance Writing",
            Description = "Income from freelance writing",
            EntryType = EntryType.Income,
            WalletId = 6
        },
        new TransactionType()
        {
            Id = 26,
            Name = "Charity",
            Description = "Donation to charity",
            EntryType = EntryType.Exponse,
            WalletId = 6
        },
        new TransactionType()
        {
            Id = 27,
            Name = "Furniture",
            Description = "Purchase of furniture",
            EntryType = EntryType.Exponse,
            WalletId = 7
        },
        new TransactionType()
        {
            Id = 28,
            Name = "Music Sales",
            Description = "Income from music sales",
            EntryType = EntryType.Income,
            WalletId = 7
        },
        new TransactionType()
        {
            Id = 29,
            Name = "Tax Refund",
            Description = "Income from tax refund",
            EntryType = EntryType.Income,
            WalletId = 1
        },
        new TransactionType()
        {
            Id = 30,
            Name = "Subscriptions",
            Description = "Monthly subscriptions",
            EntryType = EntryType.Exponse,
            WalletId = 2
        }
    };

    private static List<Transaction> _transactions = new()
    {
        new Transaction() { Id = 1, Amount = 500, Date = new DateTime(2024, 1, 1), TypeId = 1 },
        new Transaction() { Id = 2, Amount = 100, Date = new DateTime(2024, 1, 2), TypeId = 2 },
        new Transaction() { Id = 3, Amount = 750, Date = new DateTime(2024, 1, 3), TypeId = 3 },
        new Transaction() { Id = 4, Amount = 120, Date = new DateTime(2024, 1, 4), TypeId = 4 },
        new Transaction() { Id = 5, Amount = 1500, Date = new DateTime(2024, 1, 5), TypeId = 5 },
        new Transaction() { Id = 6, Amount = 60, Date = new DateTime(2024, 1, 6), TypeId = 6 },
        new Transaction() { Id = 7, Amount = 40, Date = new DateTime(2024, 1, 7), TypeId = 7 },
        new Transaction() { Id = 8, Amount = 100, Date = new DateTime(2024, 1, 8), TypeId = 8 },
        new Transaction() { Id = 9, Amount = 2000, Date = new DateTime(2024, 1, 9), TypeId = 9 },
        new Transaction() { Id = 10, Amount = 300, Date = new DateTime(2024, 1, 10), TypeId = 10 },
        new Transaction() { Id = 11, Amount = 150, Date = new DateTime(2024, 1, 11), TypeId = 11 },
        new Transaction() { Id = 12, Amount = 200, Date = new DateTime(2024, 1, 12), TypeId = 12 },
        new Transaction() { Id = 13, Amount = 300, Date = new DateTime(2024, 1, 13), TypeId = 13 },
        new Transaction() { Id = 14, Amount = 70, Date = new DateTime(2024, 1, 14), TypeId = 14 },
        new Transaction() { Id = 15, Amount = 50, Date = new DateTime(2024, 1, 15), TypeId = 15 },
        new Transaction() { Id = 16, Amount = 1000, Date = new DateTime(2024, 1, 16), TypeId = 16 },
        new Transaction() { Id = 17, Amount = 200, Date = new DateTime(2024, 1, 17), TypeId = 17 },
        new Transaction() { Id = 18, Amount = 250, Date = new DateTime(2024, 1, 18), TypeId = 18 },
        new Transaction() { Id = 19, Amount = 80, Date = new DateTime(2024, 1, 19), TypeId = 19 },
        new Transaction() { Id = 20, Amount = 50, Date = new DateTime(2024, 1, 20), TypeId = 20 },
        new Transaction() { Id = 21, Amount = 300, Date = new DateTime(2024, 1, 21), TypeId = 21 },
        new Transaction() { Id = 22, Amount = 500, Date = new DateTime(2024, 1, 22), TypeId = 22 },
        new Transaction() { Id = 23, Amount = 1500, Date = new DateTime(2024, 1, 23), TypeId = 23 },
        new Transaction() { Id = 24, Amount = 40, Date = new DateTime(2024, 1, 24), TypeId = 24 },
        new Transaction() { Id = 25, Amount = 600, Date = new DateTime(2024, 1, 25), TypeId = 25 },
        new Transaction() { Id = 26, Amount = 100, Date = new DateTime(2024, 1, 26), TypeId = 26 },
        new Transaction() { Id = 27, Amount = 400, Date = new DateTime(2024, 1, 27), TypeId = 27 },
        new Transaction() { Id = 28, Amount = 300, Date = new DateTime(2024, 1, 28), TypeId = 28 },
        new Transaction() { Id = 29, Amount = 500, Date = new DateTime(2024, 1, 29), TypeId = 29 },
        new Transaction() { Id = 30, Amount = 20, Date = new DateTime(2024, 1, 30), TypeId = 30 },
        new Transaction() { Id = 31, Amount = 500, Date = new DateTime(2024, 2, 1), TypeId = 1 },
        new Transaction() { Id = 32, Amount = 100, Date = new DateTime(2024, 2, 2), TypeId = 2 },
        new Transaction() { Id = 33, Amount = 750, Date = new DateTime(2024, 2, 3), TypeId = 3 },
        new Transaction() { Id = 34, Amount = 120, Date = new DateTime(2024, 2, 4), TypeId = 4 },
        new Transaction() { Id = 35, Amount = 1500, Date = new DateTime(2024, 2, 5), TypeId = 5 },
        new Transaction() { Id = 36, Amount = 60, Date = new DateTime(2024, 2, 6), TypeId = 6 },
        new Transaction() { Id = 37, Amount = 40, Date = new DateTime(2024, 2, 7), TypeId = 7 },
        new Transaction() { Id = 38, Amount = 100, Date = new DateTime(2024, 2, 8), TypeId = 8 },
        new Transaction() { Id = 39, Amount = 2000, Date = new DateTime(2024, 2, 9), TypeId = 9 },
        new Transaction() { Id = 40, Amount = 300, Date = new DateTime(2024, 2, 10), TypeId = 10 },
        new Transaction() { Id = 41, Amount = 150, Date = new DateTime(2024, 2, 11), TypeId = 11 },
        new Transaction() { Id = 42, Amount = 200, Date = new DateTime(2024, 2, 12), TypeId = 12 },
        new Transaction() { Id = 43, Amount = 300, Date = new DateTime(2024, 2, 13), TypeId = 13 },
        new Transaction() { Id = 44, Amount = 70, Date = new DateTime(2024, 2, 14), TypeId = 14 },
        new Transaction() { Id = 45, Amount = 50, Date = new DateTime(2024, 2, 15), TypeId = 15 },
        new Transaction() { Id = 46, Amount = 1000, Date = new DateTime(2024, 2, 16), TypeId = 16 },
        new Transaction() { Id = 47, Amount = 200, Date = new DateTime(2024, 2, 17), TypeId = 17 },
        new Transaction() { Id = 48, Amount = 250, Date = new DateTime(2024, 2, 18), TypeId = 18 },
        new Transaction() { Id = 49, Amount = 80, Date = new DateTime(2024, 2, 19), TypeId = 19 },
        new Transaction() { Id = 50, Amount = 50, Date = new DateTime(2024, 2, 20), TypeId = 20 },
        new Transaction() { Id = 51, Amount = 300, Date = new DateTime(2024, 2, 21), TypeId = 21 },
        new Transaction() { Id = 52, Amount = 500, Date = new DateTime(2024, 2, 22), TypeId = 22 },
        new Transaction() { Id = 53, Amount = 1500, Date = new DateTime(2024, 2, 23), TypeId = 23 },
        new Transaction() { Id = 54, Amount = 40, Date = new DateTime(2024, 2, 24), TypeId = 24 },
        new Transaction() { Id = 55, Amount = 600, Date = new DateTime(2024, 2, 25), TypeId = 25 },
        new Transaction() { Id = 56, Amount = 100, Date = new DateTime(2024, 2, 26), TypeId = 26 },
        new Transaction() { Id = 57, Amount = 400, Date = new DateTime(2024, 2, 27), TypeId = 27 },
        new Transaction() { Id = 58, Amount = 300, Date = new DateTime(2024, 2, 28), TypeId = 28 },
        new Transaction() { Id = 59, Amount = 500, Date = new DateTime(2024, 2, 29), TypeId = 29 },
        new Transaction() { Id = 60, Amount = 20, Date = new DateTime(2024, 3, 1), TypeId = 30 },
        new Transaction() { Id = 61, Amount = 500, Date = new DateTime(2024, 3, 2), TypeId = 1 },
        new Transaction() { Id = 62, Amount = 100, Date = new DateTime(2024, 3, 3), TypeId = 2 },
        new Transaction() { Id = 63, Amount = 750, Date = new DateTime(2024, 3, 4), TypeId = 3 },
        new Transaction() { Id = 64, Amount = 120, Date = new DateTime(2024, 3, 5), TypeId = 4 },
        new Transaction() { Id = 65, Amount = 1500, Date = new DateTime(2024, 3, 6), TypeId = 5 },
        new Transaction() { Id = 66, Amount = 60, Date = new DateTime(2024, 3, 7), TypeId = 6 },
        new Transaction() { Id = 67, Amount = 40, Date = new DateTime(2024, 3, 8), TypeId = 7 },
        new Transaction() { Id = 68, Amount = 100, Date = new DateTime(2024, 3, 9), TypeId = 8 },
        new Transaction() { Id = 69, Amount = 2000, Date = new DateTime(2024, 3, 10), TypeId = 9 },
        new Transaction() { Id = 70, Amount = 300, Date = new DateTime(2024, 3, 11), TypeId = 10 },
        new Transaction() { Id = 71, Amount = 150, Date = new DateTime(2024, 3, 12), TypeId = 11 },
        new Transaction() { Id = 72, Amount = 200, Date = new DateTime(2024, 3, 13), TypeId = 12 },
        new Transaction() { Id = 73, Amount = 300, Date = new DateTime(2024, 3, 14), TypeId = 13 },
        new Transaction() { Id = 74, Amount = 70, Date = new DateTime(2024, 3, 15), TypeId = 14 },
        new Transaction() { Id = 75, Amount = 50, Date = new DateTime(2024, 3, 16), TypeId = 15 },
        new Transaction() { Id = 76, Amount = 1000, Date = new DateTime(2024, 3, 17), TypeId = 16 },
        new Transaction() { Id = 77, Amount = 200, Date = new DateTime(2024, 3, 18), TypeId = 17 },
        new Transaction() { Id = 78, Amount = 250, Date = new DateTime(2024, 3, 19), TypeId = 18 },
        new Transaction() { Id = 79, Amount = 80, Date = new DateTime(2024, 3, 20), TypeId = 19 },
        new Transaction() { Id = 80, Amount = 50, Date = new DateTime(2024, 3, 21), TypeId = 20 },
        new Transaction() { Id = 81, Amount = 300, Date = new DateTime(2024, 3, 22), TypeId = 21 },
        new Transaction() { Id = 82, Amount = 500, Date = new DateTime(2024, 3, 23), TypeId = 22 },
        new Transaction() { Id = 83, Amount = 1500, Date = new DateTime(2024, 3, 24), TypeId = 23 },
        new Transaction() { Id = 84, Amount = 40, Date = new DateTime(2024, 3, 25), TypeId = 24 },
        new Transaction() { Id = 85, Amount = 600, Date = new DateTime(2024, 3, 26), TypeId = 25 },
        new Transaction() { Id = 86, Amount = 100, Date = new DateTime(2024, 3, 27), TypeId = 26 },
        new Transaction() { Id = 87, Amount = 400, Date = new DateTime(2024, 3, 28), TypeId = 27 },
        new Transaction() { Id = 88, Amount = 300, Date = new DateTime(2024, 3, 29), TypeId = 28 },
        new Transaction() { Id = 89, Amount = 500, Date = new DateTime(2024, 3, 30), TypeId = 29 },
        new Transaction() { Id = 90, Amount = 20, Date = new DateTime(2024, 3, 31), TypeId = 30 },
        new Transaction() { Id = 91, Amount = 500, Date = new DateTime(2024, 4, 1), TypeId = 1 },
        new Transaction() { Id = 92, Amount = 100, Date = new DateTime(2024, 4, 2), TypeId = 2 },
        new Transaction() { Id = 93, Amount = 750, Date = new DateTime(2024, 4, 3), TypeId = 3 },
        new Transaction() { Id = 94, Amount = 120, Date = new DateTime(2024, 4, 4), TypeId = 4 },
        new Transaction() { Id = 95, Amount = 1500, Date = new DateTime(2024, 4, 5), TypeId = 5 },
        new Transaction() { Id = 96, Amount = 60, Date = new DateTime(2024, 4, 6), TypeId = 6 },
        new Transaction() { Id = 97, Amount = 40, Date = new DateTime(2024, 4, 7), TypeId = 7 },
        new Transaction() { Id = 98, Amount = 100, Date = new DateTime(2024, 4, 8), TypeId = 8 },
        new Transaction() { Id = 99, Amount = 2000, Date = new DateTime(2024, 4, 9), TypeId = 9 },
        new Transaction() { Id = 100, Amount = 300, Date = new DateTime(2024, 4, 10), TypeId = 10 }
    };
}
