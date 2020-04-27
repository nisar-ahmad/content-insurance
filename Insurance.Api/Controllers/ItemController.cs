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
    public class ItemController : ControllerBase
    {
        /// <summary>
        /// Repository instance injected by constructor
        /// </summary>
        private readonly IRepository<Item> _repository;

        /// <summary>
        /// AutoMapper instance
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor injects repository and mapper from DI container
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ItemController(IRepository<Item> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<List<ItemViewModel>>> Get()
        {
            try
            {
                var items = await _repository.GetAll().ConfigureAwait(false);
                return _mapper.Map<List<ItemViewModel>>(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ItemViewModel>> Post(ItemViewModel itemViewModel)
        {
            try
            {
                if (itemViewModel == null)
                    return BadRequest();

                var item = _mapper.Map<Item>(itemViewModel);
                item.Category = null;

                await _repository.Add(item).ConfigureAwait(false);

                itemViewModel = _mapper.Map<ItemViewModel>(item);
                return CreatedAtAction("Get", new { id = item.ItemId }, itemViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> Delete(int id)
        {
            try
            {
                var item = await _repository.Delete(id).ConfigureAwait(false);
                if (item == null)
                    return NotFound();
                return item;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Commenting out GET and UPDATE controllers
        //
        //// GET: api/[controller]/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Item>> Get(int id)
        //{
        //    try
        //    {
        //        var item = await _repository.Get(id).ConfigureAwait(false);

        //        if (item == null)
        //            return NotFound();

        //        return item;
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //// PUT: api/[controller]/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, Item item)
        //{
        //    try
        //    {
        //        if (item == null || id != item.ItemId)
        //            return BadRequest();

        //        await _repository.Update(item).ConfigureAwait(false);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        // POST: api/[controller]
    }
}