using Microsoft.AspNetCore.Identity;

namespace ConferenceRoomAppAPI.Data.Models
{
    public class Users : IdentityUser
    {
        public ICollection<Orders> Orders { get; set; }
    }
}
