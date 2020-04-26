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
        /// Constructor injects repository from DI container
        /// </summary>
        /// <param name="repository"></param>
        public ItemController(IRepository<Item> repository)
        {
            this._repository = repository;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<List<Item>>> Get()
        {
            try
            {
                return await _repository.GetAll().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            try
            {
                var item = await _repository.Get(id).ConfigureAwait(false);

                if (item == null)
                    return NotFound();

                return item;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Item item)
        {
            try
            {
                if (item == null || id != item.ItemId)
                    return BadRequest();

                await _repository.Update(item).ConfigureAwait(false);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<Item>> Post(Item item)
        {
            try
            {
                if (item == null)
                    return BadRequest();

                await _repository.Add(item).ConfigureAwait(false);
                return CreatedAtAction("Get", new { id = item.ItemId }, item);
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
    }
}