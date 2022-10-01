using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingExample.Core.Model
{
    public class BEReview
    {
        public int Reviewer { get; set; }
        public int Movie { get; set; }
        public int Grade { get; set; }
        public DateTime ReviewDate { get; set; }

        public BEReview(){}

        
        public BEReview(int reviewer, int movie, int grade, DateTime reviewDate)
        {
            Reviewer = reviewer;
            Movie = movie;
            Grade = grade;
            ReviewDate = reviewDate;
        }
    }
    
    
}
