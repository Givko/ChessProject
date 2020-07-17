using Chess.Core.DataAccess.Entities;
using Chess.Core.Models;
using Chess.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Chess.Core.Api.Controllers
{
    [ApiController]
    public abstract class BaseCrudController<TService, TModel, TEntity> : Controller
        where TEntity : class, IBaseEntity
        where TModel : IBaseModel
        where TService : IBaseCrudService<TEntity, TModel>
    {
        protected readonly TService _service;
        protected readonly ILogger _logger;

        protected BaseCrudController(TService service,
            ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == default)
                return BadRequest();

            var model = await _service.GetByIdAsync(id);
            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(TModel model)
        {
            if (model == null)
                return BadRequest();

            var savedModel = await _service.InsertAsync(model);
            if (savedModel == null)
                return BadRequest();

            await _service.SaveChangesAsync();

            return Created(Request.Path.Value, savedModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(TModel model)
        {
            if (model == null)
                return BadRequest();

            var updatedModel = await _service.UpdateAsync(model);
            if (updatedModel == null)
                return StatusCode(500, $"Update failed.");

            await _service.SaveChangesAsync();

            return Ok(updatedModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                return BadRequest();

            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();

            return NoContent();
        }

        #region Dispose pattern
        private bool _disposed = false;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);


            if (_disposed || !disposing) return;

            _service.Dispose();
            _disposed = true;
        }
        #endregion
    }
}