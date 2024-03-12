using Cards.Domain.Shared.RequestFeatures;
using Cards.Presentation.Filters;
using Cards.Services.Abstraction;
using Cards.Services.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cards.Presentation.Controllers
{
    [Route("api/appUsers/{appUserId}/cards")]
    [ApiController]
    [HandleException]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCards(int appUserId, [FromQuery] CardParameters cardParameters )
        {
            (IEnumerable<CardDto> cards, MetaData metaData) = 
                await _cardService.GetCardsAsync(appUserId, cardParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(cards);
        }

        [HttpGet("{cardId}")]
        public async Task<IActionResult> GetCardById(int appUserId, int cardId)
        {
            CardDto cardsDto = await _cardService.GetCardByIdAsync(appUserId, cardId, trackChanges: false);

            return Ok(cardsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard(int appUserId, [FromBody] CardDto cardDto)
        {
            CardDto response = await _cardService.CreateCardAsync(appUserId, cardDto, trackChanges: false);

            return CreatedAtAction(nameof(GetCardById), new { appUserId = response.AppUserId, cardId = response.CardId }, response);
        }

        [HttpPut("{cardId}")]
        public async Task<IActionResult> UpdateCard(int appUserId, int cardId, [FromBody] CardDto cardDto)
        {
            await _cardService.UpdateCardAsync(appUserId, cardId, cardDto, 
                appUserTrackChanges: false, cardTrackChanges: true);

            return NoContent();
        }

        [HttpDelete("{cardId}")]
        public async Task<IActionResult> DeleteCard(int appUserId, int cardId)
        {
            await _cardService.DeleteCardAsync(appUserId, cardId, trackChanges: false);
            return NoContent();
        }
    }
}