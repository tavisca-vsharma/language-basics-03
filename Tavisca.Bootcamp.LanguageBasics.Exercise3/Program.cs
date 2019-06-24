using System;
using System.Linq;
using System.Collections.Generic;  

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int size = protein.Length; // number of items in the menu
            int[] calorie = new int[size]; // calorie array will be calculated for the respective meals and their contents of protein, carbs etc..
            for(int i=0;i<size;i++)
            {
                calorie[i] = (9*fat[i])+5*(protein[i]+carbs[i]); // saving the calorie to the resective meals
            }
            int[] result = new int[dietPlans.Length];
            for(int i=0;i<dietPlans.Length;i++)
            {
                string dietOfOne = dietPlans[i];
                result[i] = Finder(protein,carbs,fat,calorie,dietOfOne); // result of i'th person in the dietPlan
            }
            return result; // integer array for selecting the meal item.
        }
        
        public static int Finder(int[] protein, int[] carbs, int[] fat, int[] calorie, string diet)
        {
            int size = protein.Length; // size of array will give us the number of meals
            var mealRequired = new List<int>(); // List collection to hold the results after every calculation done for selecting meal by a Parameter.
            for(int i=0;i<size;i++)
            {
                mealRequired.Add(i); // Add the indices of all the meals to calculate the index required in the end of meal.
            }
            for(int i=0;i<diet.Length;i++)
            {
                var temp = new List<int>(); // Temporary List which will hold the result to pass it back again to mealRequired List
                if(mealRequired.Count==1) // If only one meal option is left then choose it.
                    return mealRequired[0];
                char ch = diet[i]; // this will hold the current restriction over meal type.
                int min=0; // this is a very imp. variable which will have the info of which index upto which you have gone to select meal.
                if(ch=='c')
                {
                    for(int j=0;j<mealRequired.Count;j++)
                    {
                        if(carbs[mealRequired[min]]> carbs[mealRequired[j]])
                            min=j;
                    }
                    for(int k=0;k<mealRequired.Count;k++)
                       {
                           if(carbs[mealRequired[k]]==carbs[mealRequired[min]])
                                temp.Add(mealRequired[k]);
                       }
                    mealRequired.Clear(); // clear the mealrequired
                    mealRequired=temp; // set it to the temporary which we have calculated in every condition.
                }
                
                else if(ch=='C')
                {
                    for(int j=0;j<mealRequired.Count;j++)
                    {
                        if(carbs[mealRequired[min]]< carbs[mealRequired[j]])
                            min=j;
                    }
                    for(int k=0;k<mealRequired.Count;k++)
                       {
                           if(carbs[mealRequired[k]]==carbs[mealRequired[min]])
                                temp.Add(mealRequired[k]);
                       }
                    mealRequired.Clear();
                    mealRequired=temp;
                }
                
                else if(ch=='p')
                {
                    for(int j=0;j<mealRequired.Count;j++)
                    {
                        if(protein[mealRequired[min]]> protein[mealRequired[j]])
                            min=j;
                    }
                    for(int k=0;k<mealRequired.Count;k++)
                       {
                           if(protein[mealRequired[k]]==protein[mealRequired[min]])
                                temp.Add(mealRequired[k]);
                       }
                    mealRequired.Clear();
                    mealRequired=temp;
                    
                }
                
                else if(ch=='P')
                {
                    for(int j=0;j<mealRequired.Count;j++)
                    {
                        if(protein[mealRequired[min]]< protein[mealRequired[j]])
                            min=j;
                    }
                    for(int k=0;k<mealRequired.Count;k++)
                       {
                           if(protein[mealRequired[k]]==protein[mealRequired[min]])
                                temp.Add(mealRequired[k]);
                       }
                    mealRequired.Clear();
                    mealRequired=temp;
                    
                }
                
                else if(ch=='f')
                {
                    for(int j=0;j<mealRequired.Count;j++)
                    {
                        if(fat[mealRequired[min]]> fat[mealRequired[j]])
                            min=j;
                    }
                    for(int k=0;k<mealRequired.Count;k++)
                       {
                           if(fat[mealRequired[k]]==fat[mealRequired[min]])
                                temp.Add(mealRequired[k]);
                       }
                    mealRequired.Clear();
                    mealRequired=temp;
                }
                
                else if(ch=='F')
                {
                    for(int j=0;j<mealRequired.Count;j++)
                    {
                        if(fat[mealRequired[min]]< fat[mealRequired[j]])
                            min=j;
                    }
                    for(int k=0;k<mealRequired.Count;k++)
                       {
                           if(fat[mealRequired[k]]==fat[mealRequired[min]])
                                temp.Add(mealRequired[k]);
                       }
                    mealRequired.Clear();
                    mealRequired=temp;
                }
                
                else if(ch=='t')
                {
                    for(int j=0;j<mealRequired.Count;j++)
                    {
                        if(calorie[mealRequired[min]]> calorie[mealRequired[j]])
                            min=j;
                    }
                    for(int k=0;k<mealRequired.Count;k++)
                       {
                           if(calorie[mealRequired[k]]==calorie[mealRequired[min]])
                                temp.Add(mealRequired[k]);
                       }
                    mealRequired.Clear();
                    mealRequired=temp;
                }
                
                else if(ch=='T')
                {
                    for(int j=0;j<mealRequired.Count;j++)
                    {
                        if(calorie[mealRequired[min]]< calorie[mealRequired[j]])
                            min=j;
                    }
                    
                    for(int k=0;k<mealRequired.Count;k++)
                       {
                           if(calorie[mealRequired[k]]==calorie[mealRequired[min]])
                                temp.Add(mealRequired[k]);
                       }
                    mealRequired.Clear();
                    mealRequired=temp;
                    
                }
               
                
            }
            return mealRequired[0]; // least indexed element to be returned to have least index answer.
        }
    }
}
