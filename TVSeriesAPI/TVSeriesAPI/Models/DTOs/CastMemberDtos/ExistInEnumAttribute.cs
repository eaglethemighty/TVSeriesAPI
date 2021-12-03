using System.ComponentModel.DataAnnotations;
using TVSeriesAPI.Models.Enums;

namespace TVSeriesAPI.Models.DTOs.CastMemberDtos
{
    public class ExistInEnum : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            return Enum.IsDefined(typeof(CastPosition), value.ToString());
        }
    }
}