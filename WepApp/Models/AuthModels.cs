using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WepApp.Models
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class UserProfile
    {
        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual int Id { get; set; }

        public virtual string UserName { get; set; }

        public UserProfile() : this(null) { }

        public UserProfile(User user)
        {
            if (user != null)
            {
                Email = user.Email;
                EmailConfirmed = user.EmailConfirmed;
                Id = user.Id;
                UserName = user.UserName;
            }
        }
    }
    public class VKModel
    {
        public int uid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string photo { get; set; }
        public string photo_rec { get; set; }
        public string hash { get; set; }

    }
}