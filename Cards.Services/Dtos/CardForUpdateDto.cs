using Cards.Domain.Constants;
using Cards.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cards.Services.Dtos
{
    public class CardForUpdateDto : CardForManipulationDtoBase
    {
        private string _status = null!;
        public string? StatusId { get; set; }

        [Required(ErrorMessage = "Status is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Status is 50 characters.")]
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value.ToLower();

                if (_status != null)
                {
                    if (_status == StatusDetails.ToDo.ToLower())
                    {
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.ToDo];
                    }
                    else if (_status == StatusDetails.InProgress.ToLower())
                    {
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.InProgress];
                    }
                    else if (_status == StatusDetails.Done.ToLower())
                    {
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.Done]; ;
                    }
                    else
                    {
                        throw new ArgumentException($"Status name '{value}' does not exist.");
                    }
                }
            }
        }
    }

}
