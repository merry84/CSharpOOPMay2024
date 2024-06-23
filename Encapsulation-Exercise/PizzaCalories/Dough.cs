using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Dough
    {
        private const double White = 1.50;
        private const double Wholegrain = 1.00;
        private const double Crispy = 0.90;
        private const double Chewy = 1.10;
        private const double Homemade = 1.00;
        private string flourType;//white or wholegrain
        private string bakingTechnique;//crispy, chewy, or homemade
        private double grams;

        public Dough(string flourType, string bakingTechnique, double grams)
        {
            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            Grams = grams;
        }

        private string FlourType
        {
            set
            {
                if (value.ToLower() != "white" && value.ToLower() != "wholegrain")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                flourType = value;
            }
        }
        private string BakingTechnique
        {
            set
            {
                if (value.ToLower() != "crispy" && value.ToLower() != "chewy" && value.ToLower() != "homemade")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                bakingTechnique = value;
            }
        }
        private double Grams
        {
            set//the range [1..200] grams
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }
                grams = value;
            }
        }
        /*The calories per gram of dough are calculated depending on the flour type and the baking technique. 
         * Every dough has 2 calories per gram as a base and a modifier that gives the exact calories. 
         * For example, a white dough has a modifier of 1.5, a chewy dough has a modifier of 1.1, 
         * which means that a white chewy dough, weighing 100 grams will have (2 * 100) * 1.5 * 1.1 = 330.00 total calories.
        */
        private double CaloriesPerGram
        {
            get
            {
                double calories = 2;
                if (flourType.ToLower() == "white") calories *= White;
                else if (flourType.ToLower() == "wholegrain") calories *= Wholegrain;
                if (bakingTechnique.ToLower() == "crispy") calories *= Crispy;
                else if (bakingTechnique.ToLower() == "chewy") calories *= Chewy;
                else if (bakingTechnique.ToLower() == "homemade") calories *= Homemade;
                return calories;

            }
        }
        public double GetCalories()
        {
            return grams * CaloriesPerGram;
        }

    }
}
