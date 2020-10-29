using FluentValidation;

namespace Application.Validators {
    public static class ValidatorExtensions 
    {
        public static IRuleBuilder<T, string> Password<T> (this IRuleBuilder<T, string> ruleBuilder) 
        {
            var options = ruleBuilder.NotEmpty ()
                .MinimumLength (6)
                .WithMessage ("Password should contain at leat six character")
                .Matches ("[A-Z]")
                .WithMessage ("Password should contain at leat one uppercase letter")
                .Matches ("[a-z]")
                .WithMessage ("Password should contain at leat one lowecase letter")
                .Matches ("[0-9]")
                .WithMessage ("Password should contain at leat one number")
                .Matches ("[^a-zA-Z0-9]")
                .WithMessage ("Password should contain non alphanumerical");

            return options;
        }
    }
}