﻿using System.ComponentModel.DataAnnotations;

namespace identity_with_Email.ViewModels
{
    public class ConfirmMobileVM
    {
        [Required, StringLength(6)]
        public string SmsCode { get; set; }

        public string Phone { get; set; }
        public string Code { get; set; }
    }
}
