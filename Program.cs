using System;
using System.Text.RegularExpressions;

string[] pettingZoo =
{
    "alpacas", "capybaras", "chickens", "ducks", "emus", "geese",
    "goats", "iguanas", "kangaroos", "lemurs", "llamas", "macaws",
    "ostriches", "pigs", "ponies", "rabbits", "sheep", "tortoises",
};

School schoolA = new School("A", 6);
School schoolB = new School("B", 3);
School schoolC = new School("C", 2);

PlanSchoolVisit(schoolA);
PlanSchoolVisit(schoolB);
PlanSchoolVisit(schoolC);

Console.WriteLine("\n\nPress Enter to close the program");
Console.ReadLine();

//I recently learned about the Deconstruct method in C# and wanted to give it a go, so this method is a little different to the one in the course
void PlanSchoolVisit(School school)
{
    var (schoolName, numberOfGroups) = school;
    RandomizeAnimals();

    string[,] group = AssignGroups();

    Console.WriteLine($"{schoolName}");

    PrintGroup(group);

    Console.WriteLine("\n\n");
}

//the randomizedPettingZoo array isn't needed if using the Fisher - Yates shuffle approach
//string[] randomizedPettingZoo = new string[pettingZoo.Length];

//the RandomizeAnimals() method I thought of. Would need to call it by "RandomizeAnimals(pettingZoo, randomizedPettingZoo);". It's time-inefficient due to it iterating over the randomizedPettingZoo array for each new animal and occupies additional memory due to the creation of the new array randomizedPettingZoo
//string[] RandomizeAnimals(string[] pettingZoo, string[] randomizedPettingZoo)
//{
//    Random random = new Random();
//    int counter = 0;
//    while (counter < pettingZoo.Length)
//    {
//        int newAnimalIndex = random.Next(pettingZoo.Length);
//        if (!Array.Exists(randomizedPettingZoo, animal => String.Equals(animal, pettingZoo[newAnimalIndex], StringComparison.OrdinalIgnoreCase)))
//        {
//            string newAnimal = pettingZoo[newAnimalIndex];
//            randomizedPettingZoo[counter] = newAnimal;
//            counter++;
//        }
//    }
//    return randomizedPettingZoo;
//}

//the RandomizeAnimals() method the tutorial had. It uses what's called a Fisher - Yates Shuffle. It's more time-efficient as well as memory efficient, because the shuffling is done "in-place" without creating a new array and storing the results there
void RandomizeAnimals()
{
    Random random = new Random();

    for (int i = 0; i < pettingZoo.Length; i++)
    {
        int r = random.Next(i, pettingZoo.Length);

        string temp = pettingZoo[r];
        pettingZoo[r] = pettingZoo[i];
        pettingZoo[i] = temp;
    }
}

//method to assign animals to each group. By default each school will have 6 groups, so I'm using an optional parameter with a default value of 6
string[,] AssignGroups(int numberOfGroups = 6)
{
    int animalsPerGroup = pettingZoo.Length / numberOfGroups;

    string[,] result = new string[numberOfGroups, animalsPerGroup];

    int animalIndex = 0;

    for (int i = 0; i < numberOfGroups; i++)
    {
        for (int j = 0; j < result.GetLength(1); j++)
        {
            result[i, j] = pettingZoo[animalIndex++];
        }
    }

    return result;
}

void PrintGroup(string[,] group)
{
    for (int i = 0; i < group.GetLength(0); i++)
    {
        //Console.WriteLine($"\nGroup {i+1}");
        Console.Write($"\nGroup {i + 1}: ");
        for (int j = 0; j < group.GetLength(1); j++)
        {
            //Console.WriteLine(group[i, j]);
            if (j == (group.GetLength(1) - 1))
            {
                Console.Write($"{group[i, j]}");

            }
            else
            {
                Console.Write($"{group[i, j]}, ");
            }
        }
    }
}

//method to display randomized list just to ensure it's randomized (for troubleshooting purposes)
void DisplayAnimals()
{
    for (int i = 0; i < pettingZoo.Length; i++)
    {
        Console.WriteLine(pettingZoo[i]);
    }
}

//I only created the class to use the Deconstruct method that I learned about recently
public class School
{
    private readonly string _schoolName;

    private readonly int _numberOfGroups;

    public School(string schoolName, int numberOfGroups)
    {
        _schoolName = schoolName;
        _numberOfGroups = numberOfGroups;
    }

    //if I didn't have the Deconstruct method I'd need properties or methods for the external code to be able to access the fields. There are a few different approaches:
    //the Deconstruct approach requires that something like "var (schoolName, numberOfGroups) = school;" is required to utilize the object's fields
    //the property approach could use something like "public string SchoolName => _schoolName;" and then schoolObject.SchoolName to access the field
    //the method approach could use something like "public string GetSchoolName() => _schoolName;" and then GetSchoolName() to access the field. Methods offer the highest degree of control
    public void Deconstruct(out string schoolName, out int numberOfGroups)
    {
        schoolName = _schoolName;
        numberOfGroups = _numberOfGroups;
    }
}

