//exercice 1

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine ("Welcome to C# Programming!");
    }
}

//exercice 2

public class HelloWorld
{
    public static void Main(string[] args)
    {
        string name="imad";
        int age=28;
        Console.WriteLine (" My name is " + name + " and my age is " + age);
    }
}

//exercice 3
public class HelloWorld
{
    public static void Main(string[] args)
    {
        int a=2;
        int b=3;
        int sum=(a+b);
        Console.WriteLine (sum);
    }
}

//exercice 4

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter your age: ");
        int userAge = int.Parse(Console.ReadLine());
        if (userAge >= 18)
        {
            Console.WriteLine("access granted");
        }
        else
        {
            Console.WriteLine("access denied");
        }
    }
}

//exercice 5

public class HelloWorld
{
    public static void Main(string[] args)
    {
        int b = 10;
        string x=("Liftoff");
        while (b>0)
        {
            Console.WriteLine(b--);
        }
        Console.WriteLine(x);
    }
}

//exercice 6

public class HelloWorld
{
    public static void Main(string[] args)
    {
        static void Sayhello (string name)
        {
            Console.WriteLine("hello" + name);
        }
        Sayhello (" imad ");
        Sayhello (" omar ");
    }
}

//exercice 7

public class HelloWorld
{
   public static void Main(string[] args)
 {
    int x = 1;

    while (x <= 10)
    {
        if (x % 2 == 0)
        {
            Console.WriteLine("The number " + x + " is even");
        }
        else
        {
            Console.WriteLine("The number " + x + " is odd");
        }

        x++; 
    }
 }
}
//exercice 8

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Give me a number : " );
        double celsius = double.Parse(Console.ReadLine());
        double Fahrenheit = celsius * 9/5 + 32;
        Console.WriteLine (" the temperature in Fahrenheit is " + Fahrenheit );
    }
}

//exercice 9

public class HelloWorld
{
    public static void Main(string[] args)
    {
        int a = 4;
        int b = 2;
        int temp = a;
        a=b;
        b=temp;
        Console.WriteLine("value of a is " + a);
        Console.WriteLine("value of b is " + b);
        
    }
}

//exercice 10

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Give me a number : " );
        int number = int.Parse(Console.ReadLine());
        int i = 1;
        
        while (i<=10)
        {
            i++;
            Console.WriteLine(number*i);
        }
        
    }
}