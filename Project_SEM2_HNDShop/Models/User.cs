using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_SEM2_HNDShop.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserPassword { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        public int Rank { get; set; }
        public int Active { get; set; }

        public User() { }
        public User(string email, string userPassword, string firstName, string lastName, string phone, string address, int roleId, int rank, int active)
        {
            Email = email;
            UserPassword = userPassword;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Address = address;
            RoleId = roleId;
            Rank = rank;
            Active = active;
        }
    }
}
