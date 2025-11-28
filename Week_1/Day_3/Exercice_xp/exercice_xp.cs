//exercice 1 
using System;
using System.Collections.Generic;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Dictionary<string, object> num = new Dictionary<string, object>()
        {
        { "ten", 10 },
        { "twenty", 20 },
        { "thirty", 30 },
        };
        Console.WriteLine(num["twenty"]);
    }

}

//exercice 2

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Dictionary<string, int> family = new Dictionary<string, int>()
        {
            { "rick", 43 },
            { "beth", 13 },
            { "morty", 5 },
            { "summer", 8 },
        };
        int total = 0;
        foreach (var member in family)
        {
            if (member.Value < 3)
            {
                Console.WriteLine(0);
            }
            else if (member.Value <= 12)
            {
                Console.WriteLine(10);
                total += 10;
            }
            else
            {
                Console.WriteLine(15);
                total += 15;
            }
        }
        Console.WriteLine("Total cost: " + total);
    }
}

//exercice 3 

var brand = new Dictionary<string, object>
{
    {"name", "Zara"},
    {"creation_date", 1975},
    {"creator_name", "Amancio Ortega Gaona"},
    {"type_of_clothes", new List<string>{"men", "women", "children", "home"}},
    {"international_competitors", new List<string>{"Gap", "H&M", "Benetton"}},
    {"number_stores", 7000},
    {"major_color", new Dictionary<string, List<string>>
        {
            {"France", new List<string>{"blue"}},
            {"Spain", new List<string>{"red"}},
            {"US", new List<string>{"pink", "green"}}
        }
    }
};

brand["number_stores"] = 2;
Console.WriteLine($"Number of stores: {brand["number_stores"]}");

if (brand["type_of_clothes"] is List<string> types && types.Count > 0)
{
    Console.WriteLine($"Zara's clients are: {string.Join(", ", types)}.");
}

brand["country_creation"] = "Spain";
Console.WriteLine($"country_creation: {brand["country_creation"]}");

if (brand.TryGetValue("international_competitors", out var compObj))
{
    if (compObj is List<string> compList)
    {
        compList.Add("Desigual");
        Console.WriteLine("Desigual added to international_competitors");
    }
    else if (compObj is string s)
    {
        
        var list = new List<string> { s, "Desigual" };
        brand["international_competitors"] = list;
        Console.WriteLine("international_competitors was a string; normalized and added Desigual");
    }
    else
    {
        
        brand["international_competitors"] = new List<string> { "Desigual" };
        Console.WriteLine("international_competitors had unexpected type; replaced with list containing Desigual");
    }
}
else
{
    brand["international_competitors"] = new List<string> { "Desigual" };
    Console.WriteLine("international_competitors did not exist; added Desigual");
}
brand.Remove("creation_date");
Console.WriteLine("creation_date is removed");

if (brand["international_competitors"] is List<string> competitors && competitors.Count > 0)
{
    Console.WriteLine($"Last international competitor: {competitors[^1]}");
}

if (brand["major_color"] is Dictionary<string, List<string>> colors && colors.TryGetValue("US", out var usColors))
{
    Console.WriteLine($"Major colors in the US: {string.Join(", ", usColors)}");
}

Console.WriteLine($"Number of key-value pairs: {brand.Count}");

Console.WriteLine($"Keys: {string.Join(", ", brand.Keys)}");

var more_on_zara = new Dictionary<string, object>
{
    {"creation_date", 1975},
    {"number_stores", 10000}
};

foreach (var kvp in more_on_zara)
{
    brand[kvp.Key] = kvp.Value; 
}

Console.WriteLine($"After merging, number_stores will be : {brand["number_stores"]}");
Console.WriteLine("Explanation: merging the dictionaries overwrote the existing 'number_stores' value with the one from 'more_on_zara'.");


//exercice 4

static void DescribeCity(string city, string country = "Iceland")
{
    Console.WriteLine($"{city} is in {country}.");
}
DescribeCity("Reykjavik");
DescribeCity("Akureyri");
DescribeCity("Paris", "France");
DescribeCity("Berlin", "Germany");

//exercice 5

Random rng = new();

static void RandomNumberGuess(int guess, Random rng)
{
    if (guess < 1 || guess > 100)
    {
        Console.WriteLine("Please provide a number between 1 and 100.");
        return;
    }

    int secret = rng.Next(1, 101); 
    if (guess == secret)
    {
        Console.WriteLine($"Success! You guessed {guess} and the number was {secret}.");
    }
    else
    {
        Console.WriteLine($"Fail. Your guess: {guess}, Actual number: {secret}.");
    }
}

RandomNumberGuess(50, rng);
RandomNumberGuess(1, rng);
RandomNumberGuess(100, rng);

//exercice 6

void MakeShirt(string size = "Large", string text = "I love C#")
{
    Console.WriteLine($"The size of the shirt is {size} and the text is '{text}'.");
}

MakeShirt();
MakeShirt("Medium");
MakeShirt("Small", "Gamedev");
MakeShirt(text: "Named arguments are neat", size: "XL");

//exercice 7

var rnd = new Random();

double RandomTemp(string season)
{
    int min = -10, max = 40;
    if (season is null) season = string.Empty;
    season = season.Trim().ToLowerInvariant();

    switch (season)
    {
        case "winter":
            min = -10; max = 16; break;
        case "spring":
            min = 0; max = 23; break;
        case "summer":
            min = 16; max = 40; break;
        case "autumn":
        case "fall":
            min = 0; max = 23; break;
        default:
            min = 0; max = 23; break;
    }
    double value = rnd.NextDouble() * (max - min) + min;
    return Math.Round(value, 1);
}

string? SeasonFromMonth(int month)
{
    if (month < 1 || month > 12) return null;
    return month switch
    {
        12 or 1 or 2 => "winter",
        3 or 4 or 5 => "spring",
        6 or 7 or 8 => "summer",
        9 or 10 or 11 => "autumn",
        _ => null
    };
}
Console.WriteLine("Enter a season name (winter/spring/summer/autumn) or a month number (1-12):");
var input = Console.ReadLine();
if (string.IsNullOrWhiteSpace(input))
{
    Console.WriteLine("No input provided. Exiting.");
    return;
}

string seasonInput;
if (int.TryParse(input.Trim(), out var month))
{
    var mapped = SeasonFromMonth(month);
    if (mapped is null)
    {
        Console.WriteLine("Invalid month number. Exiting.");
        return;
    }
    seasonInput = mapped;
    Console.WriteLine($"Interpreted month {month} as season '{seasonInput}'.");
}
else
{
    seasonInput = input.Trim();
}

var temp = RandomTemp(seasonInput);
Console.WriteLine($"Random temperature for {seasonInput}: {temp} °C");
if (temp <= 0)
{
    Console.WriteLine("Advice: It's freezing — wear a heavy coat, hat and gloves.");
}
else if (temp <= 16)
{
    Console.WriteLine("Advice: It's chilly — take a jacket.");
}
else if (temp <= 25)
{
    Console.WriteLine("Advice: Pleasant weather — light clothing is fine.");
}
else if (temp <= 35)
{
    Console.WriteLine("Advice: It's warm — stay hydrated and wear breathable fabrics.");
}
else
{
    Console.WriteLine("Advice: Very hot — avoid long exposure and cool down when possible.");
}

Console.WriteLine("Done.");

//exercice 8

List<Dictionary<string, string>> data = new()
{
    new Dictionary<string, string> { {"question", "What is Baby Yoda's real name?"}, {"answer", "Grogu"} },
    new Dictionary<string, string> { {"question", "Where did Obi-Wan take Luke after his birth?"}, {"answer", "Tatooine"} },
    new Dictionary<string, string> { {"question", "What year did the first Star Wars movie come out?"}, {"answer", "1977"} },
    new Dictionary<string, string> { {"question", "Who built C-3PO?"}, {"answer", "Anakin Skywalker"} },
    new Dictionary<string, string> { {"question", "Anakin Skywalker grew up to be who?"}, {"answer", "Darth Vader"} },
    new Dictionary<string, string> { {"question", "What species is Chewbacca?"}, {"answer", "Wookiee"} }
};

bool PlayQuiz(List<Dictionary<string, string>> questions)
{
    int correct = 0;
    int wrong = 0;
    var wrongAnswers = new List<Dictionary<string, string>>();

    Console.WriteLine("Star Wars Quiz\n");

    foreach (var q in questions)
    {
        if (!q.TryGetValue("question", out var question) || !q.TryGetValue("answer", out var answer))
            continue; // skip malformed entries

        Console.WriteLine(question);
        Console.Write("Your answer: ");
        var user = Console.ReadLine() ?? string.Empty;

        if (string.Equals(user.Trim(), answer.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Correct!\n");
            correct++;
        }
        else
        {
            Console.WriteLine($"Wrong. The correct answer is: {answer}\n");
            wrong++;
            wrongAnswers.Add(new Dictionary<string, string>
            {
                { "question", question },
                { "your_answer", user },
                { "correct_answer", answer }
            });
        }
    }

    Console.WriteLine($"Quiz finished. Correct: {correct}, Incorrect: {wrong}\n");

    if (wrongAnswers.Count > 0)
    {
        Console.WriteLine("Details of incorrect answers:");
        foreach (var w in wrongAnswers)
        {
            Console.WriteLine($"Question: {w["question"]}");
            Console.WriteLine($"Your answer: {w["your_answer"]}");
            Console.WriteLine($"Correct answer: {w["correct_answer"]}\n");
        }
    }

    if (wrong > 3)
    {
        Console.Write("You have more than 3 wrong answers. Do you want to play again? (y/n): ");
        var resp = Console.ReadLine() ?? string.Empty;
        return resp.Trim().Equals("y", StringComparison.OrdinalIgnoreCase);
    }

    return false;
}

while (true)
{
    var playAgain = PlayQuiz(data);
    if (!playAgain)
    {
        Console.WriteLine("Thanks for playing.");
        break;
    }

    Console.WriteLine("Restarting quiz...\n");
}

//exercice 9 

class Cat
{
    public string Name { get; }
    public int Age { get; }

    public Cat(string catName, int catAge)
    {
        Name = catName;
        Age = catAge;
    }
}

static class Program
{
    static void Main()
    {
        var cat1 = new Cat("baguera", 3);
        var cat2 = new Cat("mochimoch", 7);
        var cat3 = new Cat("dexter", 5);

        var cats = new List<Cat> { cat1, cat2, cat3 };
        var oldest = GetOldestCat(cats);

        if (oldest is not null)
        {
            Console.WriteLine($"The oldest cat is {oldest.Name}, and is {oldest.Age} years old.");
        }
        else
        {
            Console.WriteLine("No cats provided.");
        }
    }

    private static Cat GetOldestCat(List<Cat> cats)
    {
        if (cats == null || cats.Count == 0)
        {
            return null;
        }

        Cat oldest = cats[0];
        for (int i = 1; i < cats.Count; i++)
        {
            if (cats[i].Age > oldest.Age)
            {
                oldest = cats[i];
            }
        }
        return oldest;
    }
}

//exercice 10

class Dog
{
    public string Name { get; }
    public int Height { get; }

    public Dog(string name, int height)
    {
        Name = name;
        Height = height;
    }

    public void Bark()
    {
        Console.WriteLine($"{Name} goes woof!");
    }

    public void Jump()
    {
        int x = Height * 2;
        Console.WriteLine($"{Name} jumps {x} cm high!");
    }
}

class Program
{
    static void Main()
    {
        var davidsDog = new Dog("Rex", 50);
        davidsDog.Bark();
        davidsDog.Jump();

        var sarahsDog = new Dog("Teacup", 20);
        sarahsDog.Bark();
        sarahsDog.Jump();

        if (davidsDog.Height > sarahsDog.Height)
        {
            Console.WriteLine($"{davidsDog.Name} is taller.");
        }
        else if (sarahsDog.Height > davidsDog.Height)
        {
            Console.WriteLine($"{sarahsDog.Name} is taller.");
        }
        else
        {
            Console.WriteLine("Both dogs are the same height.");
        }
    }
}

//exercice 11

class Song
{
    private readonly List<string> _lyrics;

    public Song(List<string> lyrics)
    {
        _lyrics = lyrics ?? new List<string>();
    }

    public void SingMeASong()
    {
        foreach (var line in _lyrics)
        {
            Console.WriteLine(line);
        }
    }
}

class Program
{
    static void Main()
    {
        var stairway = new Song(new List<string>
        {
            "There's a lady who's sure",
            "all that glitters is gold",
            "and she's buying a stairway to heaven"
        });

        stairway.SingMeASong();
    }
}

//exercice 12

class Zoo
{
    public string Name { get; }
    private readonly List<string> _animals;

    public Zoo(string zooName)
    {
        Name = zooName;
        _animals = new List<string>();
    }

    public void AddAnimal(string newAnimal)
    {
        if (string.IsNullOrWhiteSpace(newAnimal))
        {
            Console.WriteLine("Cannot add an empty animal name.");
            return;
        }

        if (_animals.Any(a => string.Equals(a, newAnimal, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"{newAnimal} is already in the zoo.");
            return;
        }

        _animals.Add(newAnimal);
        Console.WriteLine($"Added {newAnimal} to {Name}.");
    }

    public void GetAnimals()
    {
        if (_animals.Count == 0)
        {
            Console.WriteLine("No animals in the zoo.");
            return;
        }

        Console.WriteLine($"Animals in {Name}:");
        foreach (var a in _animals)
        {
            Console.WriteLine("- " + a);
        }
    }

    public void SellAnimal(string animalSold)
    {
        var idx = _animals.FindIndex(a => string.Equals(a, animalSold, StringComparison.OrdinalIgnoreCase));
        if (idx >= 0)
        {
            var removed = _animals[idx];
            _animals.RemoveAt(idx);
            Console.WriteLine($"{removed} has been removed from {Name}.");
        }
        else
        {
            Console.WriteLine($"{animalSold} not found in {Name}.");
        }
    }
    public void SortAnimals()
    {
        _animals.Sort(StringComparer.OrdinalIgnoreCase);
        var groups = GetGroups();

        Console.WriteLine($"Grouped animals in {Name}:");
        foreach (var kv in groups)
        {
            Console.WriteLine($"'{kv.Key}': [" + string.Join(", ", kv.Value) + "]");
        }
    }
    public Dictionary<char, List<string>> GetGroups()
    {
        var result = new Dictionary<char, List<string>>();
        foreach (var animal in _animals.OrderBy(a => a, StringComparer.OrdinalIgnoreCase))
        {
            var first = char.ToUpperInvariant(animal[0]);
            if (!result.ContainsKey(first)) result[first] = new List<string>();
            result[first].Add(animal);
        }
        return result;
    }
}

class Program
{
    static void Main()
    {
        var newYorkZoo = new Zoo("New York Zoo");

        newYorkZoo.AddAnimal("Ape");
        newYorkZoo.AddAnimal("Baboon");
        newYorkZoo.AddAnimal("Bear");
        newYorkZoo.AddAnimal("Cat");
        newYorkZoo.AddAnimal("Cougar");
        newYorkZoo.AddAnimal("Eel");
        newYorkZoo.AddAnimal("Emu");

        Console.WriteLine();
        newYorkZoo.GetAnimals();

        Console.WriteLine();
        newYorkZoo.AddAnimal("Giraffe");
        newYorkZoo.AddAnimal("Ape");

        Console.WriteLine();
        newYorkZoo.SellAnimal("Bear");
        newYorkZoo.SellAnimal("Lion");

        Console.WriteLine();
        newYorkZoo.GetAnimals();

        Console.WriteLine();
        newYorkZoo.SortAnimals();

        Console.WriteLine();
        var groups = newYorkZoo.GetGroups();
        Console.WriteLine("Groups retrieved from GetGroups():");
        foreach (var kv in groups)
        {
            Console.WriteLine($"{kv.Key}: [{string.Join(", ", kv.Value)}]");
        }
    }
}






