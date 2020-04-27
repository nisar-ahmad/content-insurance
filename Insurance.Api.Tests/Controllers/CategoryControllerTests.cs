using Xunit;
using Moq;
using Insurance.Data.Interfaces;
using Insurance.Models.Content;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Insurance.Api.Profiles;
using Insurance.Api.Controllers;

namespace Insurance.Api.Tests.Controllers
{
    public class CategoryControllerTests
    {
        private List<Category> GetMockCategories()
        {
            var mockCategories = new List<Category>();
            for (int i = 1; i <= 5; i++)
            {
                mockCategories.Add(new Category
                {
                    CategoryId = i,
                    Name = $"Category {i}",
                });
            }

            return mockCategories;
        }

        private Mock<IRepository<Category>> GetMockRepository(List<Category> mockCategories)
        {
            if (mockCategories == null)
                mockCategories = GetMockCategories();

            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(repo => repo.GetAll()).Returns(() => Task.FromResult(mockCategories));

            return mockRepository;
        }

        private IMapper GetMapper()
        {
            var profile = new CategoryProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            return new Mapper(configuration);
        }

        private CategoryController GetController(List<Category> mockItems = null)
        {
            var repo = GetMockRepository(mockItems);
            var mapper = GetMapper();

            return new CategoryController(repo.Object, mapper);
        }

        [Fact]
        public async void GetAll_Returns_All_Items()
        {
            var controller = GetController();
            var result = await controller.Get();

            Assert.Equal(5, result.Value.Count);
        }
    }
}
