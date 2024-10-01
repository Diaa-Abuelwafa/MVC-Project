﻿using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Address { get; set; }
    }
}
