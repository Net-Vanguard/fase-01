using FCG.Application.DTOs;
using FCG.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly IGameAppService _gameAppService;

        public GamesController(IGameAppService gameAppService)
        {
            _gameAppService = gameAppService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var games = await _gameAppService.GetAllAsync();
            return Ok(games);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var game = await _gameAppService.GetByIdAsync(id);
            return game is null ? NotFound() : Ok(game);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGameRequest request)
        {
            var created = await _gameAppService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGameRequest request)
        {
            var updated = await _gameAppService.UpdateAsync(id, request);
            return updated ? NoContent() : NotFound();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _gameAppService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        [Authorize]
        [HttpPost("{id:guid}/acquire")]
        public async Task<IActionResult> Acquire(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst("id")!.Value);
            var result = await _gameAppService.AcquireAsync(id, userId);
            return result ? NoContent() : NotFound();
        }
    }
}
