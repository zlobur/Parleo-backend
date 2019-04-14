﻿using FluentValidation;
using Parleo.BLL.Interfaces;
using ParleoBackend.ViewModels.Entities;

namespace ParleoBackend.Validators.User
{
    public class UserLoginViewModelValidator: AbstractValidator<UserLoginViewModel>
    {
        private readonly IAccountService _accountService;

        public UserLoginViewModelValidator(IAccountService accountService)
        {
            _accountService = accountService;

            RuleFor(user => user.Email)
                .NotEmpty().NotNull()
                .EmailAddress()
                .Must(IsExists).WithMessage(Constants.Errors.EMAIL_NOT_FOUND);
        }

        private bool IsExists(string email) => _accountService.IsUserExists(email).Result;
    }
}
