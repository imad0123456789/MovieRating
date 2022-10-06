using Castle.Components.DictionaryAdapter;
using Moq;
using MovieRatingExample.Application;
using MovieRatingExample.Core.Model;
using MovieRatingExample.Core.Repositories;
using MovieRatingExample.Core.Service;

namespace XUnitTestProject
{
    public class ReviewServiceTest
    {
        [Fact]
        public void CreateReviewServiceWithRepository()
        {
            // Arrange
            Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();
            IReviewRepository repository = mockRepository.Object;

            // Act
            IReviewService service = new ReviewService(repository);

            // Assert
            Assert.NotNull(service);
            Assert.True(service is ReviewService);
        }

        [Fact]
        public void CreateReviewServiceWithNoRepositoryExceptArgumentException()
        {
            // Arrange
            IReviewService service = null;

            // Act + Assert
            var ex = Assert.Throws<ArgumentException>(() => service = new ReviewService(null));

            Assert.Equal("Missing repository", ex.Message);
            Assert.Null(service);
        }

        
        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(3, 0)]
        public void GetNumberOfReviewsFromReviewer(int reviewer, int expectedResult)
        {
            // Arrange
            BEReview[] fakeRepo = new BEReview[]
            {
                new BEReview() { Reviewer = 1, Movie = 1, Grade = 3, ReviewDate = new DateTime() },
                new BEReview() { Reviewer = 2, Movie = 1, Grade = 3, ReviewDate = new DateTime() },
                new BEReview() { Reviewer = 1, Movie = 2, Grade = 3, ReviewDate = new DateTime() },
            };

            Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();
            mockRepository.Setup(r => r.GetAll()).Returns(fakeRepo);

            IReviewService service = new ReviewService(mockRepository.Object);

            // Act
            int result = service.GetNumberOfReviewsFromReviewer(reviewer);

            // Assert
            Assert.Equal(expectedResult, result);
            mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [Theory]
        [InlineData(5, 2, 5, 4)]
        [InlineData(1, 2, 3, 2)]
        [InlineData(5, 5, 5, 5)]
        public void GetAverageRateFromReviewer(int num01, int num02, int num03, int expResult)
        {
            //
            List<BEReview> beReviews = new EditableList<BEReview>();

            // Arrange
            BEReview review01 = new BEReview();
            BEReview review02 = new BEReview();
            BEReview review03 = new BEReview();

            review01.Grade = num01;
            review02.Grade = num02;
            review03.Grade = num03;
            
            review01.Reviewer = 1;
            review02.Reviewer = 1;
            review03.Reviewer = 1;

            List<BEReview> reviews = new List<BEReview>();
            
            reviews.Add(review01);
            reviews.Add(review02);
            reviews.Add(review03);
            
            

            Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();
            IReviewRepository repository = mockRepository.Object;
            mockRepository.Setup(r => r.GetAll()).Returns(() =>reviews.ToArray());

            IReviewService service = new ReviewService(repository);

            // Act
            double result = service.GetAverageRateFromReviewer(1);

            // Assert
            Assert.Equal(expResult, result);
            mockRepository.Verify(r => r.GetAll(), Times.Exactly(2));
        }


        [Fact]
        
        public void GetNumberOfRatesByReviewer()
        {
            //
            List<BEReview> beReviews = new EditableList<BEReview>();

            // Arrange
            BEReview review01 = new BEReview();
            BEReview review02 = new BEReview();
            BEReview review03 = new BEReview();

            review01.Grade = 1;
            review02.Grade = 3;
            review03.Grade = 3;
            
            review01.Reviewer = 1;
            review02.Reviewer = 1;
            review03.Reviewer = 1;

            List<BEReview> reviews = new List<BEReview>();
            
            reviews.Add(review01);
            reviews.Add(review02);
            reviews.Add(review03);
            
            Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();
            IReviewRepository repository = mockRepository.Object;
            mockRepository.Setup(r => r.GetAll()).Returns(() =>reviews.ToArray());
            IReviewService service = new ReviewService(repository);

            //Act
            int result = service.GetNumberOfRatesByReviewer(1,3);
            
            //Assert
            Assert.Equal(2,result);
            
            
        }

        [Fact]
        public void GetNumberOfReviews()
        {
            // Arrange
            
            BEReview[] fakeRepo = new BEReview[]
            {
                new BEReview() { Reviewer = 1, Movie = 1, Grade = 3, ReviewDate = new DateTime() },
                new BEReview() { Reviewer = 2, Movie = 1, Grade = 3, ReviewDate = new DateTime() },
                new BEReview() { Reviewer = 3, Movie = 1, Grade = 3, ReviewDate = new DateTime() },
            };
            Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();
            mockRepository.Setup(r => r.GetAll()).Returns(fakeRepo);
            IReviewService service = new ReviewService(mockRepository.Object);
 
            
            //Act
            int result = service.GetNumberOfReviews(1);

            //Assert
            Assert.Equal(1, result);
            mockRepository.Verify(r => r.GetAll(), Times.Once);
        }


        
       
        [Fact]
        public void GetAverageRateOfMovie()
        {
            
            BEReview review01 = new BEReview();
            BEReview review02 = new BEReview();
            BEReview review03 = new BEReview();
            
            review01.Grade = 2;
            review02.Grade = 4;
            review03.Grade = 6;

            review01.Movie = 1;
            review02.Movie = 1 ;
            review03.Movie = 1;
         
            List<BEReview> reviews = new List<BEReview>();
            
            reviews.Add(review01);
            reviews.Add(review02);
            reviews.Add(review03);
            
            Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();
            IReviewRepository repository = mockRepository.Object;
            mockRepository.Setup(r => r.GetAll()).Returns(() => reviews.ToArray());
            IReviewService service = new ReviewService(repository);
            
            //Act
            double result = service.GetAverageRateOfMovie(1);
            
            //Assert
            Assert.Equal(4, result);
            mockRepository.Verify((r)=> r.GetAll(), Times.Exactly(2));
        }
        
        [Theory]
        [InlineData(5,2,5,4)]
        [InlineData(1,2,3,2)]
        [InlineData(5,5,5,5)]
        
        public void GetAverageRateOfMovieEx(int num01, int num02, int num03, int expResult)
        {
            
            BEReview review01 = new BEReview();
            BEReview review02 = new BEReview();
            BEReview review03 = new BEReview();
            
            review01.Grade = num01;
            review02.Grade = num02;
            review03.Grade = num03;

            review01.Movie = 1;
            review02.Movie = 1;
            review03.Movie = 1;
         
            List<BEReview> reviews = new List<BEReview>();
            
            reviews.Add(review01);
            reviews.Add(review02);
            reviews.Add(review03);
            
            Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();
            IReviewRepository repository = mockRepository.Object;
            mockRepository.Setup(r => r.GetAll()).Returns(() => reviews.ToArray());
            IReviewService service = new ReviewService(repository);
            
            //Act
            double result = service.GetAverageRateOfMovie(1);
            
            //Assert
            Assert.Equal(expResult, result);
            mockRepository.Verify((r)=> r.GetAll(), Times.Exactly(2));
        }


        [Fact]
        public void GetNumberOfRates()
        {
            int MovieId = 1; 
            
            BEReview review01 = new BEReview();
            BEReview review02 = new BEReview();
            BEReview review03 = new BEReview();
            
            review01.Grade = 1;
            review02.Grade = 2;
            review03.Grade = 3;
            
            review01.Movie = MovieId;
            review02.Movie = MovieId;
            review03.Movie = MovieId;
            
            List<BEReview> reviews = new List<BEReview>();
            
            reviews.Add(review01);
            reviews.Add(review02);
            reviews.Add(review03);
            
            Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();
            IReviewRepository repository = mockRepository.Object;
            mockRepository.Setup(r => r.GetAll()).Returns(() => reviews.ToArray());
            IReviewService service = new ReviewService(repository);
            
            //Act
            int result = service.GetNumberOfRates(MovieId, 3);
            
            //Assert
            Assert.Equal(1, result);
            mockRepository.Verify((r) => r.GetAll(), Times.AtLeast(1));
        }
        [Fact]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
            //Arrange

            BEReview review01 = new BEReview();
            BEReview review02 = new BEReview();
            BEReview review03 = new BEReview();
            BEReview review04 = new BEReview();
            
            review01.Grade = 1;
            review02.Grade = 3;
            review03.Grade = 5;
            review04.Grade = 5;

            review01.Movie = 1;
            review02.Movie = 1;
            review03.Movie = 1;
            review04.Movie = 2;
         
            List<BEReview> reviews = new List<BEReview>();
            
            reviews.Add(review01);
            reviews.Add(review02);
            reviews.Add(review03);
            reviews.Add(review04);
            
            Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();
            IReviewRepository repository = mockRepository.Object;
            mockRepository.Setup(r => r.GetAll()).Returns(() => reviews.ToArray());
            IReviewService service = new ReviewService(repository);
            
            //Act
            List<int> result = service.GetMoviesWithHighestNumberOfTopRates();
            
            //Assert
            Assert.Equal(2,result.Count);
        }
        [Fact]
        public void GetMostProductiveReviewers()
        {
            //Arrange

            BEReview review01 = new BEReview();
            BEReview review02 = new BEReview();
            BEReview review03 = new BEReview();
            
            review01.Grade = 1;
            review02.Grade = 3;
            review03.Grade = 5;

            review01.Movie = 1;
            review02.Movie = 1;
            review03.Movie = 1;

            List<BEReview> reviews = new List<BEReview>();
            
            reviews.Add(review01);
            reviews.Add(review02);
            reviews.Add(review03);
            
            Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();
            IReviewRepository repository = mockRepository.Object;
            mockRepository.Setup(r => r.GetAll()).Returns(() => reviews.ToArray());
            IReviewService service = new ReviewService(repository);
            
            //Act
            List<int> result = service.GetMostProductiveReviewers();
            
            //Assert
            Assert.Equal(2,result.Count);
        }
        [Fact]
        public void GetTopRatedMovies()
        {
            //Arrange

            BEReview review01 = new BEReview();
            BEReview review02 = new BEReview();
            BEReview review03 = new BEReview();
            
            review01.Grade = 1;
            review02.Grade = 3;
            review03.Grade = 5;

            review01.Movie = 1;
            review02.Movie = 1;
            review03.Movie = 1;

            List<BEReview> reviews = new List<BEReview>();
            
            reviews.Add(review01);
            reviews.Add(review02);
            reviews.Add(review03);
            
            Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();
            IReviewRepository repository = mockRepository.Object;
            mockRepository.Setup(r => r.GetAll()).Returns(() => reviews.ToArray());
            IReviewService service = new ReviewService(repository);
            
            //Act
            List<int> result = service.GetTopRatedMovies(3);
            
            //Assert
            Assert.Equal(2,result.Count);
        }
        [Fact]
        public void GetTopMoviesByReviewer()
        {
            //Arrange

            BEReview review01 = new BEReview();
            BEReview review02 = new BEReview();
            BEReview review03 = new BEReview();
            
            review01.Grade = 1;
            review02.Grade = 3;
            review03.Grade = 5;

            review01.Movie = 1;
            review02.Movie = 1;
            review03.Movie = 1;

            List<BEReview> reviews = new List<BEReview>();
            
            reviews.Add(review01);
            reviews.Add(review02);
            reviews.Add(review03);

            Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();
            IReviewRepository repository = mockRepository.Object;
            mockRepository.Setup(r => r.GetAll()).Returns(() => reviews.ToArray());
            IReviewService service = new ReviewService(repository);
            
            //Act
            List<int> result = service.GetTopMoviesByReviewer(1);
            
            //Assert
            Assert.Equal(2,result.Count);
        }
        [Fact]
        public void GetReviewersByMovie()
        {
            //Arrange

            BEReview review01 = new BEReview();
            BEReview review02 = new BEReview();
            BEReview review03 = new BEReview();

            review01.Grade = 1;
            review02.Grade = 3;
            review03.Grade = 5;

            review01.Movie = 1;
            review02.Movie = 1;
            review03.Movie = 1;

            List<BEReview> reviews = new List<BEReview>();
            
            reviews.Add(review01);
            reviews.Add(review02);
            reviews.Add(review03);
            
            Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();
            IReviewRepository repository = mockRepository.Object;
            mockRepository.Setup(r => r.GetAll()).Returns(() => reviews.ToArray());
            IReviewService service = new ReviewService(repository);
            
            //Act
            List<int> result = service.GetReviewersByMovie(1);
            
            //Assert
            Assert.Equal(2,result.Count);
        }
    }
}








