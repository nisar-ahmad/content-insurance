using AutoMapper;
using Insurance.Api.ViewModels;
using Insurance.Data.Interfaces;
using Insurance.Models.Content;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        /// <summary>
        /// Repository instance injected by constructor
        /// </summary>
        private readonly IRepository<Category> _repository;

        /// <summary>
        /// AutoMapper instance
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor injects repository and mapper from DI container
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public CategoryController(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<List<CategoryViewModel>>> Get()
        {
            try
            {
                var items = await _repository.GetAll().ConfigureAwait(false);
                return _mapper.Map<List<CategoryViewModel>>(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}