using System; 
using System.ComponentModel.DataAnnotations;

namespace DojoDachi.Models

{
    public class Dachi 
    {
        public string Name { get; set; }
        public int Fullness { get; set; }
        public int Happiness { get; set; }
        public int Meals { get; set; }
        public int Energy { get; set; }
    
    public Dachi(string name)
    {
        Name = name;
        Fullness = 20;
        Happiness = 20;
        Meals = 3;
        Energy = 50;
    }

    public Dachi(string name, int fullness, int happiness, int meals, int energy)
    {
        Name = name;
        Fullness = fullness;
        Happiness = happiness;
        Meals = meals;
        Energy = energy;
    }

    // let Dachi beemo = new Dachi(Beemo);
    // System.Console.WriteLine(beemo)
    

    }




}
