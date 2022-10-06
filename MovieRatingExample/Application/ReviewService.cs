using MovieRatingExample.Core.Model;
using MovieRatingExample.Core.Repositories;
using MovieRatingExample.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingExample.Application
{
    public class ReviewService : IReviewService
    {
        public BEReview[] GetAll()
        {
            throw new NotImplementedException();
        }
        
        
        private IReviewRepository Repository;

        public ReviewService(IReviewRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentException("Missing repository");
            }
            Repository = repository;
        }

        public int GetAverageRateFromReviewer(int reviewer)
        {
            if (Repository.GetAll().Length == 0)
                throw new ArgumentException();
            int countReviews = 0;
            int totalReviews = 0;
            foreach (BEReview review in Repository.GetAll())
            {
                if (review.Reviewer == reviewer)
                {
                    totalReviews += review.Grade;
                    countReviews++;
                }
            }

            if (countReviews != 0)
                return totalReviews / countReviews;
            return 0;
        }

        
        public int GetNumberOfRatesByReviewer(int reviewer, int rate)
        {
            if (Repository.GetAll().Length == 0)
                throw new ArgumentException();
            int CountRate = 0;
           
            foreach (BEReview review in Repository.GetAll())
            {
                if (review.Reviewer == reviewer)
                {
                    if (review.Grade == rate)
                    {
                        CountRate++;
                    }
                }
            }
            return CountRate;
        }
        
        
        
        public double GetAverageRateOfMovie(int movie)
        {
            {
                if (Repository.GetAll().Length == 0)
                    throw new ArgumentException();
                int countMovie = 0;
                int totalReviews = 0;
                foreach (BEReview review in Repository.GetAll())
                {
                    if (review.Movie == movie)
                    {
                        totalReviews += review.Grade;
                        countMovie++;
                    }
                }

                if (countMovie != 0)
                    return totalReviews / countMovie;
                return 0;
            }
        }

        
        
        public List<int> GetMostProductiveReviewers()
        {
            if (Repository.GetAll().Length == 0)
                throw new ArgumentException();
            List<BEReview> allReviewers = new List<BEReview>();
            int reviewer = 0;
            int count = 0;
            int maxCount = 0;
            
            List<int> mostProductiveReviewers = new List<int>();
           
            foreach (BEReview review in Repository.GetAll())
            {
                allReviewers.Add(review);
                allReviewers.OrderByDescending(beReview => review.Reviewer);
            }

            foreach (BEReview review in allReviewers)
            {
                if (review.Reviewer == reviewer)
                {
                    count++;
                    if (count == maxCount)
                    {
                        mostProductiveReviewers.Add(reviewer);
                    }
                    else if (count > maxCount)
                    {
                        mostProductiveReviewers.Clear();
                        mostProductiveReviewers.Add(reviewer);
                        maxCount++;
                    }
                }
                else if (review.Reviewer != reviewer)
                {
                    count = 1;
                    reviewer = review.Reviewer;
                }
            }
            return mostProductiveReviewers;
        }

        public List<int> GetMoviesWithHighestNumberOfTopRates()
        {
            if (Repository.GetAll().Length == 0)
                throw new ArgumentException();
            List<int> MoviesWithHighestNumberOfTopRates = new List<int>();
            int Reviewer = 0;
           
            foreach (BEReview review in Repository.GetAll())
            {
                if (review.Reviewer == Reviewer)
                {
                    if (review.Grade == 5)
                    {
                        MoviesWithHighestNumberOfTopRates.Add(review.Movie);
                    }
                }
            }
            return MoviesWithHighestNumberOfTopRates;
        }

        public int GetNumberOfRates(int movie, int rate)
        {
            if (Repository.GetAll().Length == 0)
                throw new ArgumentException();
            int CountRate = 0;
           
            foreach (BEReview review in Repository.GetAll())
            {
                if (review.Movie == movie)
                {
                    if (review.Grade == rate)
                    {
                        CountRate++;
                    }
                }
            }
            return CountRate;
        }

       

        public int GetNumberOfReviews(int movie)
        {
            {
                int countOfReviewer = 0;
                foreach (BEReview review in Repository.GetAll())
                {
                    if (review.Reviewer == movie)
                        countOfReviewer++;
                }
                return countOfReviewer;
            }
        }

       

        public int GetNumberOfReviewsFromReviewer(int reviewer)
        {
            int count = 0;
            foreach (BEReview review in Repository.GetAll())
            {
                if (review.Reviewer == reviewer)
                    count++;
            }
            return count;
        }

        public List<int> GetReviewersByMovie(int movie)
        {
            if (Repository.GetAll().Length == 0)
                throw new ArgumentException();
            List<int> ReviewersByMovie = new List<int>();
            int Reviewer = 0;
           
            foreach (BEReview review in Repository.GetAll())
            {
                if (review.Reviewer == Reviewer)
                {
                    if (review.Movie == movie)
                    {
                        ReviewersByMovie.Add(review.Reviewer);
                    }
                }
            }
            //ReviewersByMovie.OrderByDescending()
            return ReviewersByMovie;
        }

        public List<int> GetTopMoviesByReviewer(int reviewer)
        {
            if (Repository.GetAll().Length == 0)
                throw new ArgumentException();
            List<BEReview> Reviews = new List<BEReview>();
            int sortedGrade = 5;
            List<BEReview> SortedReviews = new List<BEReview>();
            List<int> TopMoviesByReviewer = new List<int>();

            foreach (BEReview review in Repository.GetAll())
            {
                if (review.Reviewer == reviewer)
                {
                    Reviews.Add(review);
                    TopMoviesByReviewer.Add(review.Movie);
                }
            }

            Reviews.OrderByDescending(review => review.Grade).ThenBy(review => review.ReviewDate);
            foreach (BEReview review in Reviews)
            {
                if (review.Grade == sortedGrade)
                {
                    SortedReviews.Add(review);
                }
                else if (review.Grade != sortedGrade)
                {
                    SortedReviews.OrderByDescending(review => review.ReviewDate);
                    foreach (BEReview reviews in SortedReviews)
                        //preguntar si funciona
                    {
                        TopMoviesByReviewer.Add(reviews.Movie);
                    }
                }
                
            }
            return TopMoviesByReviewer;
        }

        public List<int> GetTopRatedMovies(int amount)
        {
            if (Repository.GetAll().Length == 0 || amount <= 0)
                throw new ArgumentException();
            List<BEReview> sortedReviews = new List<BEReview>();
            List<int> topRatedMovies = new List<int>();
            int count = 0;
           
            foreach (BEReview review in Repository.GetAll())
            {
                sortedReviews.Add(review);
            }

            sortedReviews.OrderByDescending(review => review.Grade);
            foreach (BEReview reviews in sortedReviews)
            {
                if (count < amount)
                {
                    topRatedMovies.Add(reviews.Movie);
                    count++;
                }
                
            }
            return topRatedMovies;
        }
    }
}
