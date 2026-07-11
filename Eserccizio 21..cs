class Program
{
    static void Main(string[] args)
    {
         var users = new List<User>
        {
            new User("Alice", 25, UserStatus.Active),
            new User("Bob",  17, UserStatus.Inactive),
            new User("Charlie",  30, UserStatus.Active),
            new User("David",  15, UserStatus.Inactive),
            new User("Eve",  22, UserStatus.Active)
        };
           

        var activeUser = FilterUser(users, IsActive);
        var inactveUser = FilterUser(users, IsInactive);
        var adultUser = FilterUser(users, IsAdult);

        console.WriteLine("Active Users:");
        PrintUsers(activeUser);

        console.WriteLine("\nInactive Users:");
        PrintUsers(inactveUser);

        console.WriteLine("\nAdult Users:");
        PrintUsers(adultUser);

        var activeAdults = FilterUser(users, user => user.IsActive && user.IsAdult);
        
        console.WriteLine("\nActive Adult Users:");
        PrintUsers(activeAdults);

        var activeAndAdultRule = And(IsActive, IsAdult);
        var activeAndAdultUser = FilterUser(users, activeAndAdultRule);
        console.WriteLine("\nActive and Adult Users:"); 

    
    }

    public static List<User> FilterUser(List<User> users, Func<User, bool> predicate)
    {
        var filteredUsers = new List<User>();
        foreach (var user in users)
        {
            if (rule(user))
            {
                filteredUsers.Add(user);
            }
        }
        return filteredUsers;
    }

    static void PrintUsers(List<User> users)
    {
        foreach (var user in users)
        {
            Console.WriteLine($"Name: {user.Name}, Age: {user.Age}, Active: {user.IsActive}");
        }
    }


    static func<User, bool> And (Func<User, bool> a, Func<User, bool> b) => u => a(u) && b(u);
    
}