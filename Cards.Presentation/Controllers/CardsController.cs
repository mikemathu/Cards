using Cards.Domain.Shared.RequestFeatures;
using Cards.Presentation.Filters;
using Cards.Services.Abstraction;
using Cards.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cards.Presentation.Controllers
{
    [Route("api/appUsers/{appUserId}/cards")]
    [ApiController]
    [Authorize]
    [ValidateModel]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Gets the list of all cards
        /// </summary>
        /// <returns>The cards list</returns>
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCards(string appUserId, [FromQuery] CardParameters cardParameters )
        {
            (IEnumerable<CardDto> cards, MetaData metaData) = 
                await _cardService.GetAllCardsAsync(appUserId, cardParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(cards);
        }

        [HttpGet("forUser")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> GetCardsForUserAsync(string appUserId, [FromQuery] CardParameters cardParameters )
        {
            (IEnumerable<CardDto> cards, MetaData metaData) = 
                await _cardService.GetCardsForUserAsync(appUserId, cardParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(cards);
        }

        [HttpGet("{cardId}")]
        public async Task<IActionResult> GetCardById(string appUserId, string cardId)
        {
            CardDto cardsDto = await _cardService.GetCardByIdAsync(appUserId, cardId, trackChanges: false);

            return Ok(cardsDto);
        }


        /// <summary>
        /// Creates a newly created card.
        /// </summary>
        /// <param name="appUserId">The ID of the user creating the card.</param>
        /// <param name="cardForCreationDto">The data for creating the card.</param>
        /// <returns>A newly created card.</returns>
        /// <response code="201">Returns the newly created card.</response>
        /// <response code="400">If the color code is invalid.</response>
        /// <response code="401">If unauthorised.</response>
        /// <response code="404">If the specified member (user) is not found.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateCard(string appUserId, [FromBody] CardForCreationDto cardForCreationDto)
        {
            CardDto response = await _cardService.CreateCardAsync(appUserId, cardForCreationDto, trackChanges: false);

            return CreatedAtAction(nameof(GetCardById), new { appUserId, cardId = response.CardId }, response);
        }

        [HttpPut("{cardId}")]
        public async Task<IActionResult> UpdateCard(string appUserId, string cardId, [FromBody] CardForUpdateDto cardForUpdateDto)
        {
            await _cardService.UpdateCardAsync(appUserId, cardId, cardForUpdateDto, 
                appUserTrackChanges: false, cardTrackChanges: true);

            return NoContent();
        }

        [HttpDelete("{cardId}")]
        public async Task<IActionResult> DeleteCard(string appUserId, string cardId)
        {
            await _cardService.DeleteCardAsync(appUserId, cardId, trackChanges: false);
            return NoContent();
        }
    }
}