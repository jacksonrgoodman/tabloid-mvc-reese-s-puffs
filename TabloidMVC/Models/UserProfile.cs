using Microsoft.Data.SqlClient.Server;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;

namespace TabloidMVC.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Date")]
        public DateTime CreateDateTime { get; set; }

        [DisplayName("Image")]
        public string ImageLocation { get; set; }

        [DisplayName("User Type")]
        public int UserTypeId { get; set; }

        public int Active { get; set; }

        [DisplayName("User Type")]
        public UserType UserType { get; set; }

        [DisplayName("Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public string DateString()
        {
            return CreateDateTime.ToShortDateString();
        }
    }
}