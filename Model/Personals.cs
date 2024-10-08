﻿using System.ComponentModel.DataAnnotations;

namespace HospitalApi.Model
{
    public class Personals
    {
        [Key]
        public int ID { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
