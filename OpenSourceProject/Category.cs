using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSourceProject
{
    class Category
    {
        //The name of the category
        public string Name { get; set; }

        //Total points possible for the category (read only)
        public double PtsPoss
        {
            get
            {
                double sum = 0;
                foreach (Assignment a in AssignmentList)
                {
                    //If the assignment doesn't have the default value for ptsposs
                    if (!(a.PtsPoss < 0) && !(a.Score < 0))
                    {
                        sum += a.PtsPoss;
                    }
                }
                return sum;
            }
        }

        //Total score for the category (read only)
        public double Score
        {
            get
            {
                double sum = 0;
                foreach (Assignment a in AssignmentList)
                {
                    sum += a.Score;
                }
                return sum;
            }
        }

        //Percent of the category (not to be confused with weight, the score/ptsposs * 100) (read only)
        public double Percent
        {
            get
            {
                if (Score != -1 && PtsPoss != -1)
                {
                    return Score / PtsPoss * 100;
                }
                else
                {
                    return double.NaN;
                }
            }
        }

        //The weight of the category
        public double Weight { get; set; }

        //List of all the assignments
        public List<Assignment> AssignmentList { get; set; }

        public Category(string name, double weight) {
            this.Name = name;
            this.Weight = weight;
            AssignmentList = new List<Assignment>();
        }
    }
}
