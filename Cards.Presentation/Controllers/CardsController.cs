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
    [Produces("application/json")]
    public class CardsController(ICardService cardService) : ControllerBase
    {

        /// <summary>
        /// Retrieves all cards for Admin.
        /// </summary>
        /// <param name="appUserId">The Admin ID.</param>
        /// <param name="cardParameters">Parameters for pagination and filtering.</param>
        /// <returns>A list of cards and pagination metadata.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///
        /// </remarks>
        /// <response code="200">Returns the list of cards and pagination metadata.</response>
        /// <response code="401">If the user is not authorized to access this endpoint.</response>
        /// <response code="404">If the specified user is not found.</response>
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCards(string appUserId, [FromQuery] CardParameters cardParameters )
        {
            (IEnumerable<CardDto> cards, MetaData metaData) = 
                await cardService.GetAllCardsAsync(appUserId, cardParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(cards);
        }

        /// <summary>
        /// Retrieves cards for a specific user.
        /// </summary>
        /// <param name="appUserId">The ID of the user for whom the cards are retrieved.</param>
        /// <param name="cardParameters">Parameters for pagination and filtering.</param>
        /// <returns>A list of cards and pagination metadata.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///
        /// </remarks>
        /// <response code="200">Returns the list of cards and pagination metadata.</response>
        /// <response code="401">If the user is not authorized to access this endpoint.</response>
        /// <response code="404">If the specified user is not found.</response>
        [HttpGet("forUser")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCardsForUser(string appUserId, [FromQuery] CardParameters cardParameters )
        {
            (IEnumerable<CardDto> cards, MetaData metaData) = 
                await cardService.GetCardsForUserAsync(appUserId, cardParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(cards);
        }

        /// <summary>
        /// Retrieves a card by its ID.
        /// </summary>
        /// <param name="appUserId">The ID of the user for whom the card is retrieved.</param>
        /// <param name="cardId">The ID of the card to retrieve.</param>
        /// <returns>The card with the specified ID.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/appUsers/kev5f943-112f-4d49-888d-c671e210b8b8/cards/fixbugs2-cd1d-43cd-b997-71a7f2a20096
        ///
        /// </remarks>
        /// <response code="200">Returns the card with the specified ID.</response>
        /// <response code="404">If the specified user or card is not found.</response>
        [HttpGet("{cardId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCardById(string appUserId, string cardId)
        {
            CardDto cardsDto = await cardService.GetCardByIdAsync(appUserId, cardId, trackChanges: false);

            return Ok(cardsDto);
        }


        /// <summary>
        /// Creates a new card.
        /// </summary>
        /// <param name="appUserId">The ID of the user creating the card.</param>
        /// <param name="cardForCreationDto">The data for creating the card.</param>
        /// <returns>The newly created card.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/appUsers/userId/cards
        ///     {
        ///         "name": "New Card",
        ///         "color": "Red",
        ///         "status": "ToDo",
        ///         "dateOfCreation": "2022-01-01T00:00:00"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created card.</response>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="401">If the user is not authorized to access this endpoint.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateCard(string appUserId, [FromBody] CardForCreationDto cardForCreationDto)
        {
            CardDto response = await cardService.CreateCardAsync(appUserId, cardForCreationDto, trackChanges: false);

            return CreatedAtAction(nameof(GetCardById), new { appUserId, cardId = response.CardId }, response);
        }

        /// <summary>
        /// Updates an existing card.
        /// </summary>
        /// <param name="appUserId">The ID of the user updating the card.</param>
        /// <param name="cardId">The ID of the card to update.</param>
        /// <param name="cardForUpdateDto">The data for updating the card.</param>
        /// <returns>No content.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/appUsers/userId/cards/cardId
        ///     {
        ///         "name": "Updated Card",
        ///         "color": "Blue",
        ///         "status": "InProgress"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Indicates the card was successfully updated.</response>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="401">If the user is not authorized to access this endpoint.</response>
        /// <response code="404">If the specified user or card is not found.</response>
        [HttpPut("{cardId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCard(string appUserId, string cardId, [FromBody] CardForUpdateDto cardForUpdateDto)
        {
            await cardService.UpdateCardAsync(appUserId, cardId, cardForUpdateDto, 
                appUserTrackChanges: false, cardTrackChanges: true);

            return NoContent();
        }

        /// <summary>
        /// Deletes a card by its ID.
        /// </summary>
        /// <param name="appUserId">The ID of the user deleting the card.</param>
        /// <param name="cardId">The ID of the card to delete.</param>
        /// <returns>No content.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/appUsers/userId/cards/cardId
        ///
        /// </remarks>
        /// <response code="204">Indicates the card was successfully deleted.</response>
        /// <response code="401">If the user is not authorized to access this endpoint.</response>
        /// <response code="404">If the specified user or card is not found.</response>
        [HttpDelete("{cardId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCard(string appUserId, string cardId)
        {
            await cardService.DeleteCardAsync(appUserId, cardId, trackChanges: false);
            return NoContent();
        }
    }
}