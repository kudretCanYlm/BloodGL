using FluentValidation;

namespace BloodGL.Application.Dtos
{
	public class LoginDto
	{
		public string? Username { get; set; }
		public string? Password { get; set; }
	}

	public interface ILoginDtoValidator : IValidator<LoginDto>
	{

	}

	public class LoginDtoValidator : AbstractValidator<LoginDto>, ILoginDtoValidator
	{
		public LoginDtoValidator()
		{
			RuleFor(x => x.Username)
				.NotNull().WithMessage("boş olamaz")
				.NotEmpty().WithMessage("boş olamaz")
				.Length(5, 50).WithMessage("Uzunluk 5-50 karakter arası olmalı");

			RuleFor(x => x.Password)
				.NotNull().WithMessage("boş olamaz")
				.NotEmpty().WithMessage("boş olamaz")
				.Length(5, 50).WithMessage("Uzunluk 5-50 karakter arası olmalı");
		}
	}
}
