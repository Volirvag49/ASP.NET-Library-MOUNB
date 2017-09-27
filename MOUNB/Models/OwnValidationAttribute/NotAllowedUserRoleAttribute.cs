using System.ComponentModel.DataAnnotations;

namespace MOUNB.Models
{
    public class NotAllowedUserRoleAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            UserRole role = (UserRole)value;
            if (role == UserRole.Нет)
            {
                return false;
            }
            return true;
        }
    }
}