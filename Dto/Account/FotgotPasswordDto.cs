using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utilities;

namespace Dto.Account
{
    public class ForgotPasswordDto : IDto
    {
        private string _Email;
		[Required]
		[EmailAddress]
		[Display(Name = "E-Posta")]
		[DataType(DataType.EmailAddress), MaxLength(60, ErrorMessage = "Maksimum 60 karakter")]
		[RegularExpression(pattern: @"^(([^<>()[\]\\.,;:\s@\']+(\.[^<>()[\]\\.,;:\s@\']+)*)|(\'.+\'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Geçersiz E-Posta Adresi")]
		public string Email
		{
            get { return _Email; }
            set { _Email = value.ToEmail(); }
        }
    }
}
