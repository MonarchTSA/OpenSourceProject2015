using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSourceProject
{
    class Assignment
    {
        //The name of the the assignment
        public string Name { get; set; }

        //The multiplier of the asignment
        public double Multiplier { get; set; }

        //The points possible of the assignment
        public double PtsPoss
        {
            get
            {
                return ptsPoss;
            }
            set
            {
                ptsPoss = value * Multiplier;
            }
        }
        private double ptsPoss;
        
        //The score of the assignment
        public double Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value * Multiplier;
            }
        }
        private double score;

        //The percent calculated of the assignment (read only)
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

        public Assignment(string name = "", double multiplier = 1d, double ptsPoss = -1, double score = -1) {
            this.Name = name;
            this.Multiplier = multiplier;
            this.PtsPoss = ptsPoss;
            this.Score = score;
        }

        //This method retrieves the data given an index
        //-1 = unkown, 0 = name, 1 = multiplier, 2 = ptsPoss, 3 = score, 4 = percent
        public dynamic getFromIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return Name;
                case 1:
                    return Multiplier;
                case 2:
                    return PtsPoss;
                case 3:
                    return Score;
                case 4:
                    return Percent;
                default:
                    return -1;
            }
        }

        //This method set data given an index
        public void setFromIndex(dynamic val, int index)
        {
            switch (index)
            {
                case 0:
                    Name = val;
                    break;
                case 1:
                    Multiplier = val;
                    break;
                case 2:
                    PtsPoss = val;
                    break;
                case 3:
                    Score = val;
                    break;
                default:
                    break;
            }
        }
    }
}
