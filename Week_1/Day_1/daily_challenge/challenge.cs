// challenge 1
public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine("give me a number ");
        int num = int.Parse(Console.ReadLine());
        Console.WriteLine("give me a length ");
        double leng = double.Parse(Console.ReadLine());
        for (int i = 1; i <= leng; i++)
        {
            Console.WriteLine(num * i);
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("give me a name : ");
        string name = Console.ReadLine();
        int leng = name.Length;
        string result = "";
        result += name[0].ToString();
        for (int i = 0; i < leng - 1; i++)
        {
            string a = name[i].ToString();
            string b = name[i + 1].ToString();
            if (a != b)
            {
                result += b;
            }
        }
        Console.WriteLine(result);
    }
}