using MovieRatingExample.Core.Model;
using MovieRatingExample.Core.Repositories;
using MovieRatingExample.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }

        public List<int> GetMoviesWithHighestNumberOfTopRates()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public List<int> GetTopMoviesByReviewer(int reviewer)
        {
            throw new NotImplementedException();
        }

        public List<int> GetTopRatedMovies(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
