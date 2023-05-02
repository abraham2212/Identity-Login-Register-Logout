﻿using System.ComponentModel.DataAnnotations;

namespace Practice.ViewModels.Account
{
	public class RegisterVM
	{
		[Required]
		public string FullName { get; set; }
		public bool IsRememberMe { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		[DataType(DataType.Password),Compare("Password")]
		public string ConfirmPassword { get; set; }
	}
}
