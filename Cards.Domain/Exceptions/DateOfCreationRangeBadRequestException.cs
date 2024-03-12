namespace Cards.Domain.Exceptions
{
    public class DateOfCreationRangeBadRequestException : BadRequestException
    {
        public DateOfCreationRangeBadRequestException()
            :base("EndDate can't be less than StartDate.")
        {
                
        }
    }
}
