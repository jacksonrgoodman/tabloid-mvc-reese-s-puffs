using Microsoft.Data.SqlClient.Server;
using System;
using System.ComponentModel;
using System.Runtime.Intrinsics.X86;

namespace TabloidMVC.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Date")]
        public DateTime CreateDateTime { get; set; }


        public string ImageLocation { get; set; }
        public int UserTypeId { get; set; }

        [DisplayName("User Type")]
        public UserType UserType { get; set; }
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