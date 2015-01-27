using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSourceProject
{
    [Serializable()]
    public class SchoolClass
    {
        //The name of the class
        public string Name { get; set; }

        //The grade/percent of the class
        public double Percent 
        {
            get
            {
                //If the total weight does not add up to 100, then find the multiplier than makes the weight sum = 100.
                //Then multiply the weight of the category by the multiplier to get the new weight.
                double weightSum = 0;
                foreach (Category c in CategoryList)
                {
                    weightSum += c.Weight;
                }
                if (weightSum != 100)
                {
                    double multiplier = 100 / weightSum;
                    double percent = 0;
                    foreach (Category c in CategoryList)
                    {
                        percent += (c.Percent * (c.Weight / 100 * multiplier));
                    }
                    return percent;
                }
                else
                {
                    double percent = 0;
                    foreach (Category c in CategoryList)
                    {
                        //Multiply the current category's percent  time its weigh divided by 100 and add it to the percent;
                        percent += (c.Percent * (c.Weight / 100));
                    }
                    return percent;
                }
            }
        }

        public char LetterGrade
        {
            get
            {
                if (Percent >= 90)
                {
                    return 'A';
                }
                else if (Percent >= 80)
                {
                    return 'B';
                }
                else if (Percent >= 70)
                {
                    return 'C';
                }
                else if (Percent >= 60)
                {
                    return 'D';
                }
                else
                {
                    return 'F';
                }
            }
        }

        //The list of all the categories
        public List<Category> CategoryList { get; set; }

        //The index of the currently selected category
        public int CurrentCategoryIndex { get; set; }

        //Returns the category from categoryList[currentCategoryIndex] (read-only)
        public Category CurrentCategory 
        {
            get
            {
                return CategoryList[CurrentCategoryIndex];
            }
        }

        public SchoolClass(string name)
        {
            Name = name;
            CategoryList = new List<Category>();
        }

        private SchoolClass()
        {
            CategoryList = new List<Category>();
        }
    }
}