using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSourceProject
{
    class SchoolClass
    {
        //The name of the class
        public string Name { get; set; }

        //The grade/percent of the class
        public double Percent 
        {
            get
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
    }
}