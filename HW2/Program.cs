using HW2;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

List<string> menuItems =
    ["Get All",
    "Add",
    "Edit",
    "Exit"
];
ConsoleKeyInfo key;
int count = 0;

for (int i = 0; i < menuItems.Count; i++)
{
    if (i == count)
        Console.WriteLine($"> {menuItems[i]}");
    else Console.WriteLine(menuItems[i]);
}

while (true)
{

    key = Console.ReadKey();
    Console.Clear();
    switch (key.Key)
    {
        case ConsoleKey.UpArrow:
            count--;
            break;

        case ConsoleKey.DownArrow:
            count++;
            break;
        case ConsoleKey.Enter:
            if (count == 0)
            {
                Thread t1 = new Thread(GetAll);
                t1.Start();
            }
            else if (count == 1)
            {
                Thread t2 = new Thread(AddMenu);
                t2.Start();
            }
            else if (count == 2)
            {
                Thread t3 = new Thread(EditMenu);
                t3.Start();
            }
            else if (count == 3) return 0;
            break;

    }
    if (count < 0) count = menuItems.Count - 1;
    for (int i = 0; i < menuItems.Count; i++)
    {
        if (i == count % menuItems.Count)
            Console.WriteLine($"> {menuItems[i]}");
        else Console.WriteLine(menuItems[i]);
    }
}

void Loading()
{
    for (int i = 0; i <= 100; i++)
    {
        Console.Write($"\rLoading... {i}%");
        Thread.Sleep(50);
    }
    Console.WriteLine();
}

void GetAll()
{
    Thread t = new Thread(Loading);

    t.Start();
    t.Join();

    using var db = new LibraryContext();
    var authors = db.Authors.ToList();

    Console.Clear();
    Console.WriteLine("All authors:");
    foreach (var author in authors)
    {
        Console.WriteLine($"{author.Id}. {author.FirstName} {author.LastName}");
    }

    Console.WriteLine("\nPress any key to return...");
    Console.ReadKey();
}

void AddMenu()
{
    Console.Clear();

    Console.Write("Enter First Name: ");
    string fname = Console.ReadLine()!;

    Console.Write("Enter Last Name: ");
    string lname = Console.ReadLine()!;

    using var db = new LibraryContext();

    db.Authors.Add(new Author
    {
        FirstName = fname,
        LastName = lname
    });

    db.SaveChanges();

    Thread t = new Thread(Loading);
    t.Start();
    t.Join();

    Console.WriteLine("Author added!");
    Console.WriteLine("\nPress any key to return...");
    Console.ReadKey();
}

void EditMenu()
{
    Console.Clear();

    Thread t = new Thread(Loading);
    t.Start();
    t.Join();

    using var db = new LibraryContext();
    var authors = db.Authors.ToList();

    Console.WriteLine("Authors:");
    foreach (var author in authors)
    {
        Console.WriteLine($"{author.Id}. {author.FirstName} {author.LastName}");
    }


    Console.Write("Select Author Id:");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var author = db.Authors.FirstOrDefault(a => a.Id == id);
        if (author != null)
        {
            Console.Clear();
            Console.Write("Enter new FirstName: ");
            string fname = Console.ReadLine()!;

            Console.Write("Enter new LastName: ");
            string lname = Console.ReadLine()!;

            author.FirstName = fname;
            author.LastName = lname;

            db.SaveChanges();
            Console.WriteLine("Author updated!");
        }
        else
        {
            Console.WriteLine("Author not found!");
        }
    }
    else
    {
        Console.WriteLine("Invalid Id!");
    }

    Console.WriteLine("\nPress any key to return...");
    Console.ReadKey();
}
