using Xunit;
using Moq;
using Insurance.Data.Interfaces;
using Insurance.Models.Content;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Insurance.Api.Profiles;
using Insurance.Api.Controllers;
using Insurance.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Tests.Controllers
{
    public class ItemControllerTests
    {
        private List<Item> GetMockItems()
        {
            var mockItems = new List<Item>();
            for (int i = 1; i <= 10; i++)
            {
                mockItems.Add(new Item
                {
                    ItemId = i,
                    Name = $"Item {i}",
                    Value = i * 100,
                    CategoryId = i % 3,
                });
            }

            return mockItems;
        }

        private Mock<IRepository<Item>> GetMockRepository(List<Item> mockItems)
        {
            if (mockItems == null)
                mockItems = GetMockItems();

            var mockRepository = new Mock<IRepository<Item>>();

            mockRepository.Setup(repo => repo.GetAll()).Returns(() => Task.FromResult(mockItems));
            mockRepository.Setup(repo => repo.Add(It.IsAny<Item>())).Returns((Item item) => Task.FromResult(item)).Callback((Item item) => mockItems.Add(item));
            mockRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Returns((int itemId) => Task.FromResult(new Item { ItemId = itemId, Name = $"Item {itemId}" })).Callback((int itemId) => mockItems.Remove(mockItems.Find(item => item.ItemId == itemId)));

            return mockRepository;
        }

        private IMapper GetMapper()
        {
            var profile = new ItemProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            return new Mapper(configuration);
        }

        private ItemController GetController(List<Item> mockItems = null)
        {
            var repo = GetMockRepository(mockItems);
            var mapper = GetMapper();

            return new ItemController(repo.Object, mapper);
        }

        [Fact]
        public async void GetAll_Returns_All_Items()
        {
            var controller = GetController();
            var result = await controller.Get();

            Assert.Equal(10, result.Value.Count);
        }

        [Fact]
        public async void Add_Inserts_Item()
        {
            var mockItems = GetMockItems();
            var controller = GetController(mockItems);
            var result = await controller.Post(new ItemViewModel { Name = "New Item", CategoryId = 2, Value = 1000 });

            var item = (result.Result as ObjectResult).Value as ItemViewModel;

            Assert.Equal("New Item", item.Name);
            Assert.Equal(2, item.CategoryId);
            Assert.Equal(1000, item.Value);
            Assert.Equal(11, mockItems.Count);
        }

        [Fact]
        public async void Delete_Removes_Item()
        {
            var mockItems = GetMockItems();
            var controller = GetController(mockItems);
            var result = await controller.Delete(5);

            Assert.Equal(5, result.Value.ItemId);
            Assert.Equal("Item 5", result.Value.Name);
            Assert.Equal(9, mockItems.Count);
        }
    }
}
