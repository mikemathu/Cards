using Cards.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cards.Services.Dtos
{
    public class CardForUpdateDto : CardForManipulationDto
    {
        private string _status = null!;
        public int? StatusId { get; set; }

        [Required(ErrorMessage = "Status is a required field.")]
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;

                if (_status != null)
                {
                    if (_status == "ToDo")
                    {
                        StatusId = 1;
                    }
                    else if (_status == "In Progress")
                    {
                        StatusId = 2;
                    }
                    else if (_status == "Done")
                    {
                        StatusId = 3;
                    }
                }
            }
        }
    }

}
